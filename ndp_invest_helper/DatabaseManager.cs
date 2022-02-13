﻿using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

using Dapper;
using ndp_invest_helper.Models;

namespace ndp_invest_helper
{
    public class DatabaseManager
    {
        private string connectionString;

        public string ConnectionString
        {
            get => connectionString;
            set => connectionString = 
                ConfigurationManager.ConnectionStrings[value].ConnectionString;
        }

        private string dbCreateScript;

        public DatabaseManager(
            string connectionStringName,
            string dbCreateScript)
        {
            this.ConnectionString = connectionStringName;
            this.dbCreateScript = dbCreateScript;
        }

        public List<T> GetFullTable<T>(string tableName)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                return db.Query<T>(
                    $"SELECT * FROM {tableName}").ToList();
            }
        }

        public void CreateDatabase()
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Execute(dbCreateScript);
            }
        }
        
        /// <summary>
        /// Запись информации в базу данных.
        /// Данные должны быть предварительно загружены в менеджеры.
        /// </summary>
        public void Import()
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

        private Int64 GetLastInsertRowId(SQLiteConnection connection)
        {
            var command = new SQLiteCommand("SELECT last_insert_rowid()", connection);
            return (Int64)command.ExecuteScalar();
        }

        private void CheckExecuteResult(
            string command, object parameter, int result, int expectedResult = 1)
        {
            if (result != expectedResult)
                throw new Exception(
                    $"Error executing command \"{command}\". " +
                    $"Returned result = {result}, expected = {expectedResult}. " +
                    $"Parameter = {parameter ?? "Null"}.");
        }

        private void ImportXmlSecurites(SQLiteConnection connection)
        {
            var XmlToDbAssetNames
                = new Dictionary<string, AssetTypes>()
                {
                    {"share", AssetTypes.Share },
                    {"bond", AssetTypes.Bond },
                    {"etf", AssetTypes.Etf },
                    {"gold", AssetTypes.Gold },
                    {"cash", AssetTypes.Cash },
                };

            int commandResult;

            var insertIssuerCommand =
                "INSERT INTO Issuers(ID, NameRus) VALUES(@Id, @NameRus)";

            var insertIssuerCountriesCommand = 
                "INSERT INTO Issuers_Countries_Link(IssuerID, CountryCode, Part)" +
                "VALUES(@IssuerID, @CountryCode, @Part)";

            var insertIssuerCurrenciesCommand = 
                "INSERT INTO Issuers_Currencies_Link(IssuerID, CurrencyCode, Part)" +
                "VALUES(@IssuerID, @CurrencyCode, @Part)";

            var insertIssuerSectorsCommand = 
                "INSERT INTO Issuers_EconomySectors_Link(IssuerID, SectorID, Part)" +
                "VALUES(@IssuerID, @SectorID, @Part)";

            var insertSecurityCommand = 
                "INSERT INTO Securities(ID, ISIN, Ticker, SecurityType, IssuerID, NameRus)" +
                "VALUES(@Id, @Isin, @Ticker, @SecurityType, @IssuerID, @NameRus)";

            var insertSecurityCountriesCommand = 
                "INSERT INTO Securities_Countries_Link(SecurityID, CountryCode, Part)" +
                "VALUES(@SecurityID, @CountryCode, @Part)";

            var insertSecurityCurrenciesCommand = 
                "INSERT INTO Securities_Currencies_Link(SecurityID, CurrencyCode, Part)" +
                "VALUES(@SecurityID, @CurrencyCode, @Part)";

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
                        CountryCode = item.Key,
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
                        CurrencyCode = item.Key,
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
                        SectorID = int.Parse(item.Key.Id),
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
                        secType = (int)AssetTypes.Share;
                    else if (security is Bond)
                        secType = (int)AssetTypes.Bond;
                    else if (security is ETF)
                        secType = (int)AssetTypes.Etf;
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
                            CountryCode = item.Key,
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
                            CurrencyCode = item.Key,
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
                            SectorID = int.Parse(item.Key.Id),
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
                                AssetTypeId = (long)XmlToDbAssetNames[item.Key],
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

        private void ImportXmlCurrencies(SQLiteConnection connection)
        {
            var command = new SQLiteCommand(connection);
            command.CommandText =
                "INSERT INTO Currencies(Code, NameEng, RateToRub)" +
                "VALUES(@Code, @NameEng, @RateToRub)";

            foreach (var currency in CurrenciesManager.Currencies)
            {
                command.Parameters.AddWithValue("@Code", currency.Code);
                command.Parameters.AddWithValue("@NameEng", currency.NameEng);
                command.Parameters.AddWithValue("@RateToRub", currency.RateToRub);
                command.Prepare();
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
        }

        private void ImportXmlCountries(SQLiteConnection connection)
        {
            var insertCommand = new SQLiteCommand(connection);
            var updateCommand = new SQLiteCommand(connection);

            insertCommand.CommandText =
                "INSERT INTO Countries(Code, NameRus)" +
                "VALUES(@Code, @NameRus)";

            var levels = CountriesManager.ByDevelopment
                .OrderBy(x => x.Key).ToArray();
            var regions = CountriesManager.ByRegion
                .OrderBy(x => x.Key).ToArray();

            foreach (var country in CountriesManager.Countries)
            {
                insertCommand.Parameters.AddWithValue("@Code", country.Key);
                insertCommand.Parameters.AddWithValue("@NameRus", country.Value);
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

        private void ImportXmlSectors(SQLiteConnection connection)
        {
            var command = new SQLiteCommand(connection);
            command.CommandText =
                "INSERT INTO EconomySectors(ID, Level, NameRus, ParentID)" +
                "VALUES(@ID, @Level, @NameRus, @ParentID)";

            foreach (var sector in SectorsManager.Sectors)
            {
                command.Parameters.AddWithValue("@ID", sector.Id);
                command.Parameters.AddWithValue("@Level", sector.Level);
                command.Parameters.AddWithValue("@NameRus", sector.Name);
                command.Parameters.AddWithValue("@ParentID", sector.ParentId);
                command.Prepare();

                command.ExecuteNonQuery();

                command.Parameters.Clear();
            }
        }
    }
}
