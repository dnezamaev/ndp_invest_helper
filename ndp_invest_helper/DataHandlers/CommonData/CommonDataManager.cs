using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_invest_helper
{
    public static class CommonDataManager
    {
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
            CurrenciesManager.LoadFromXmlFile(Settings.CurrenciesXmlFilePath);
            CountriesManager.LoadFromXmlFile(Settings.CountriesXmlFilePath);
            SectorsManager.LoadFromXmlFile(Settings.SectorsXmlFilePath);
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

            CurrenciesManager.LoadFromDatabase();
            CountriesManager.LoadFromDatabase();
            SectorsManager.LoadFromDatabase();
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
