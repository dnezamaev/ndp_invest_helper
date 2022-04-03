using ndp_invest_helper.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_invest_helper
{
    /// <summary>
    /// Global common data.
    /// </summary>
    public static class CommonData
    {
        public static CountriesManager countries;

        public static CurrenciesManager currencies;

        public static SectorsManager sectors;

        public static CountriesManager Countries
        {
            get
            {
                if (countries == null)
                {
                    countries = new CountriesManager();
                }

                return countries;
            }
        }
        
        public static CurrenciesManager Currencies
        {
            get
            {
                if (currencies == null)
                {
                    currencies = new CurrenciesManager();
                }

                return currencies;
            }
        }

        public static SectorsManager Sectors
        {
            get
            {
                if (sectors == null)
                {
                    sectors = new SectorsManager();
                }

                return sectors;
            }
        }
        
        private static StringBuilder log = new StringBuilder();

        /// <summary>
        /// Load all common data: securities, currencies, counties, sectors.
        /// Source is selected on Settings.
        /// </summary>
        public static void Load()
        {
            switch (Settings.CommonDataSource)
            {
                case CommonDataSources.SqliteDb:
                    LoadCommonDataFromSqlite();
                    break;
                case CommonDataSources.XmlFiles:
                    LoadCommonDataFromXml();
                    break;
                default:
                    throw new NotImplementedException
                        (
                        $"Неизвестное значение настройки {nameof(Settings.CommonDataSource)} " +
                        $"= {Settings.CommonDataSource}"
                        );
            }
        }

        private static void LoadCommonDataFromXml()
        {
            Currencies.LoadFromXmlFile(Settings.CurrenciesXmlFilePath);
            Countries.LoadFromXmlFile(Settings.CountriesXmlFilePath);
            Sectors.LoadFromXmlFile(Settings.SectorsXmlFilePath);
            SecuritiesManager.LoadFromXmlFile(Settings.SecuritiesXmlFilePath);
        }

        private static void InitDatabase()
        {
            DatabaseManager.Init(
                connectionStringName: "DapperSQLite",
                dbCreateScript: File.ReadAllText(Settings.SqliteDatabaseCreateScriptFilePath)
                );
        }

        private static void LoadCommonDataFromSqlite()
        {
            InitDatabase();

            Currencies.LoadFromDatabase();
            Countries.LoadFromDatabase();
            Sectors.LoadFromDatabase();

            SecuritiesManager.LoadFromDatabase();
        }

        public static void XmlToSqlite()
        {
            InitDatabase();
            DatabaseManager.CreateDatabase();
            DatabaseManager.Import();
        }

        public static void LogAddText(string text)
        {
            if (Settings.WriteLog)
            {
                log.AppendLine(text);
                log.AppendLine();
            }
        }

        public static void LogSave()
        {
            if (Settings.WriteLog)
            {
                File.WriteAllText(Settings.LogFilePath, log.ToString());
            }
        }
    }
}
