using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

using Dapper;
using ndp_invest_helper.Models;

namespace ndp_invest_helper
{
    public static class DatabaseManager
    {
        private static string connectionString;

        public static string ConnectionString
        {
            get => connectionString;
            set => connectionString = 
                ConfigurationManager.ConnectionStrings[value].ConnectionString;
        }

        private static string DbCreateScript { get; set; }

        public static void Init(
            string connectionStringName,
            string dbCreateScript)
        {
            ConnectionString = connectionStringName;
            DbCreateScript = dbCreateScript;
        }

        public static List<T> GetFullTable<T>(string tableName)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                return db.Query<T>(
                    $"SELECT * FROM {tableName}").ToList();
            }
        }

        public static void CreateDatabase()
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Execute(DbCreateScript);
            }
        }
        
        /// <summary>
        /// Запись информации в базу данных.
        /// Данные должны быть предварительно загружены в менеджеры.
        /// </summary>
        public static void Import()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    ImportXmlSectors(connection);
                    ImportXmlCountries(connection);
                    ImportXmlCurrencies(connection);
                    ImportXmlSecurites(connection);

                    transaction.Commit();
                }
            }
        }

        private static Int64 GetLastInsertRowId(SQLiteConnection connection)
        {
            var command = new SQLiteCommand("SELECT last_insert_rowid()", connection);
            return (Int64)command.ExecuteScalar();
        }

        private static void CheckExecuteResult(
            string command, object parameter, int result, int expectedResult = 1)
        {
            if (result != expectedResult)
                throw new Exception(
                    $"Error executing command \"{command}\". " +
                    $"Returned result = {result}, expected = {expectedResult}. " +
                    $"Parameter = {parameter ?? "Null"}.");
        }

        private static void ImportXmlSecurites(SQLiteConnection connection)
        {
            var XmlToDbAssetNames
                = new Dictionary<string, AssetTypeEnum>()
                {
                    {"unknown", AssetTypeEnum.Unknown },
                    {"share", AssetTypeEnum.Share },
                    {"bond", AssetTypeEnum.Bond },
                    {"etf", AssetTypeEnum.Etf },
                    {"gold", AssetTypeEnum.Gold },
                    {"cash", AssetTypeEnum.Cash },
                };

            int commandResult;

            var insertIssuerCommand =
                "INSERT INTO Issuers(ID, NameRus) VALUES(@Id, @NameRus)";

            var insertIssuerCountriesCommand = 
                "INSERT INTO Issuers_Countries_Link(IssuerID, CountryID, Part)" +
                "VALUES(@IssuerID, @CountryID, @Part)";

            var insertIssuerCurrenciesCommand = 
                "INSERT INTO Issuers_Currencies_Link(IssuerID, CurrencyId, Part)" +
                "VALUES(@IssuerID, @CurrencyId, @Part)";

            var insertIssuerSectorsCommand = 
                "INSERT INTO Issuers_EconomySectors_Link(IssuerID, SectorID, Part)" +
                "VALUES(@IssuerID, @SectorID, @Part)";

            var insertSecurityCommand = 
                "INSERT INTO Securities(ID, ISIN, Ticker, SecurityType, IssuerID, NameRus)" +
                "VALUES(@Id, @Isin, @Ticker, @SecurityType, @IssuerID, @NameRus)";

            var insertSecurityCountriesCommand = 
                "INSERT INTO Securities_Countries_Link(SecurityID, CountryID, Part)" +
                "VALUES(@SecurityID, @CountryID, @Part)";

            var insertSecurityCurrenciesCommand = 
                "INSERT INTO Securities_Currencies_Link(SecurityID, CurrencyId, Part)" +
                "VALUES(@SecurityID, @CurrencyId, @Part)";

            var insertSecuritySectorsCommand = 
                "INSERT INTO Securities_EconomySectors_Link(SecurityID, SectorID, Part)" +
                "VALUES(@SecurityID, @SectorID, @Part)";

            var insertFundAssetsCommand = 
                "INSERT INTO Funds_Assets_Link(FundSecurityID, AssetTypeID, Part)" +
                "VALUES(@FundSecurityID, @AssetTypeID, @Part)";

            int issuerId = 1;
            int securityId = 1;

            foreach (var issuer in SecuritiesManager.Issuers)
            {
                issuer.Id = issuerId;

                commandResult = connection.Execute(insertIssuerCommand, issuer);
                CheckExecuteResult(insertIssuerCommand, issuer, commandResult);

                // Insert countries, currencies, sectors info.
                foreach (var item in issuer.Countries)
                {
                    var param = new
                    {
                        IssuerID = issuerId,
                        CountryId = item.Key.Id,
                        Part = item.Value
                    };

                    commandResult = connection.Execute(
                        insertIssuerCountriesCommand, param);

                    CheckExecuteResult(
                        insertIssuerCountriesCommand, param, commandResult);
                }

                foreach (var item in issuer.Currencies)
                {
                    var param = new
                    {
                        IssuerID = issuerId,
                        CurrencyId = item.Key.Id,
                        Part = item.Value
                    };

                    commandResult = connection.Execute(
                        insertIssuerCurrenciesCommand, param);

                    CheckExecuteResult(
                        insertIssuerCurrenciesCommand, param, commandResult);
                }

                foreach (var item in issuer.Sectors)
                {
                    var param = new
                    {
                        IssuerID = issuerId,
                        SectorID = item.Key.Id,
                        Part = item.Value
                    };

                    commandResult = connection.Execute(
                        insertIssuerSectorsCommand, param);

                    CheckExecuteResult(
                        insertIssuerSectorsCommand, param, commandResult);
                }

                // Handle Securities.
                foreach (var security in issuer.Securities)
                {
                    security.Id = securityId;

                    var ticker =
                        (security.Ticker == "???" 
                        || string.IsNullOrEmpty(security.Ticker)) 
                        ? null : security.Ticker;

                    int secType;
                    if (security is Share)
                        secType = (int)AssetTypeEnum.Share;
                    else if (security is Bond)
                        secType = (int)AssetTypeEnum.Bond;
                    else if (security is ETF)
                        secType = (int)AssetTypeEnum.Etf;
                    else
                        throw new ArgumentException("Unknown security type.");

                    // Insert security.
                    commandResult = connection.Execute(insertSecurityCommand,
                        new {
                            Id = security.Id,
                            Isin = security.Isin,
                            Ticker = ticker,
                            SecurityType = secType,
                            IssuerID = issuerId,
                            NameRus = security.Name
                        });
                    CheckExecuteResult(insertSecurityCommand, issuer, commandResult);

                    // Insert countries, currencies, sectors info.
                    foreach (var item in security.SecurityCountries)
                    {
                        var param = new
                        {
                            SecurityID = securityId,
                            CountryID = item.Key.Id,
                            Part = item.Value
                        };

                        commandResult = connection.Execute(
                            insertSecurityCountriesCommand, param);

                        CheckExecuteResult(
                            insertSecurityCountriesCommand, param, commandResult);
                    }

                    foreach (var item in security.SecurityCurrencies)
                    {
                        var param = new
                        {
                            SecurityID = securityId,
                            CurrencyId = item.Key.Id,
                            Part = item.Value
                        };

                        commandResult = connection.Execute(
                            insertSecurityCurrenciesCommand, param);

                        CheckExecuteResult(
                            insertSecurityCurrenciesCommand, param, commandResult);
                    }

                    foreach (var item in security.SecuritySectors)
                    {
                        var param = new
                        {
                            SecurityID = securityId,
                            SectorID = item.Key.Id,
                            Part = item.Value
                        };

                        commandResult = connection.Execute(
                            insertSecuritySectorsCommand, param);

                        CheckExecuteResult(
                            insertSecuritySectorsCommand, param, commandResult);
                    }

                    if (security is ETF)
                    {
                        var etf = security as ETF;

                        foreach (var item in etf.WhatInside)
                        {
                            var param = new
                            {
                                FundSecurityId = securityId,
                                AssetTypeId = item.Key.Id,
                                Part = item.Value
                            };

                            commandResult = connection.Execute(
                                insertFundAssetsCommand, param);

                            CheckExecuteResult(
                                insertFundAssetsCommand, param, commandResult);
                        }
                    }

                    securityId++;
                }

                issuerId++;
            }
        }

        private static void ImportXmlCurrencies(SQLiteConnection connection)
        {
            var command = new SQLiteCommand(connection);
            command.CommandText =
                "INSERT INTO Currencies(ID, Code, NameEng, NameRus, RateToRub)" +
                "VALUES(@ID, @Code, @NameEng, @NameRus, @RateToRub)";

            foreach (Currency currency in CommonData.Currencies.Items)
            {
                command.Parameters.AddWithValue("@ID", currency.Id);
                command.Parameters.AddWithValue("@Code", currency.Code);
                command.Parameters.AddWithValue("@NameEng", currency.NameEng);
                command.Parameters.AddWithValue("@NameRus", currency.NameRus);
                command.Parameters.AddWithValue("@RateToRub", currency.RateToRub);
                command.Prepare();
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
        }

        private static void ImportXmlCountries(SQLiteConnection connection)
        {
            var insertCommand = new SQLiteCommand(connection);
            var updateCommand = new SQLiteCommand(connection);

            insertCommand.CommandText =
                "INSERT INTO Countries(ID, Code, Code3, NameRus, NameRusFull, NameEng)" +
                "VALUES(@ID, @Code, @Code3, @NameRus, @NameRusFull, @NameEng)";

            var levels = CommonData.Countries.ByDevelopment
                .OrderBy(x => x.Key).ToArray();
            var regions = CommonData.Countries.ByRegion
                .OrderBy(x => x.Key).ToArray();

            foreach (Country country in CommonData.Countries.Items)
            {
                insertCommand.Parameters.AddWithValue("@ID", country.Id);
                insertCommand.Parameters.AddWithValue("@Code", country.Code);
                insertCommand.Parameters.AddWithValue("@Code3", country.Code3);
                insertCommand.Parameters.AddWithValue("@NameRus", country.NameRus);
                insertCommand.Parameters.AddWithValue("@NameRusFull", country.NameRusFull);
                insertCommand.Parameters.AddWithValue("@NameEng", country.NameEng);
                insertCommand.Prepare();
                insertCommand.ExecuteNonQuery();

                insertCommand.Parameters.Clear();
            }

            insertCommand.CommandText =
                "INSERT INTO CountryDevelopmentLevels(NameRus)" +
                "VALUES(@NameRus)";

            updateCommand.CommandText =
                "UPDATE Countries " +
                "SET DevelopmentLevel = @DevelopmentLevel " +
                "WHERE Code = @Code";

            for (int i = 0; i < levels.Length; i++)
            {
                var level = levels[i];
                insertCommand.Parameters.AddWithValue("@NameRus", level.Key);
                insertCommand.Prepare();
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();

                foreach (var country in level.Value)
                {
                    updateCommand.Parameters.AddWithValue("@DevelopmentLevel", i + 1);
                    updateCommand.Parameters.AddWithValue("@Code", country);
                    updateCommand.Prepare();
                    updateCommand.ExecuteNonQuery();
                    updateCommand.Parameters.Clear();
                }
            }

            insertCommand.CommandText =
                "INSERT INTO CountryRegions(NameRus)" +
                "VALUES(@NameRus)";

            updateCommand.CommandText =
                "UPDATE Countries " +
                "SET RegionID = @RegionID " +
                "WHERE Code = @Code";

            for (int i = 0; i < regions.Length; i++)
            {
                KeyValuePair<string, HashSet<string>> region = regions[i];
                insertCommand.Parameters.AddWithValue("@NameRus", region.Key);
                insertCommand.Prepare();
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();

                foreach (var country in region.Value)
                {
                    updateCommand.Parameters.AddWithValue("@RegionID", i + 1);
                    updateCommand.Parameters.AddWithValue("@Code", country);
                    updateCommand.Prepare();
                    updateCommand.ExecuteNonQuery();
                    updateCommand.Parameters.Clear();
                }
            }
        }

        private static void ImportXmlSectors(SQLiteConnection connection)
        {
            var command = new SQLiteCommand(connection);
            command.CommandText =
                "INSERT INTO EconomySectors(ID, Level, NameRus, NameEng, ParentID)" +
                "VALUES(@ID, @Level, @NameRus, @NameEng, @ParentID)";

            foreach (EconomySector sector in CommonData.Sectors.Items)
            {
                command.Parameters.AddWithValue("@ID", sector.Id);
                command.Parameters.AddWithValue("@Level", sector.Level);
                command.Parameters.AddWithValue("@NameRus", sector.NameRus);
                command.Parameters.AddWithValue("@NameEng", sector.NameEng);
                command.Parameters.AddWithValue("@ParentID", sector.ParentId);
                command.Prepare();

                command.ExecuteNonQuery();

                command.Parameters.Clear();
            }
        }
    }
}
