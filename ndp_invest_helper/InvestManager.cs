using System;
using System.Collections.Generic;

namespace ndp_invest_helper
{
    public class InvestManager
    {
        public List<Report> BrokerReports { get; private set; }

        public DatabaseManager Database { get; private set; }

        public InvestManager()
        {
            LoadCommonData();
        }

        /// <summary>
        /// Load common data about securities. Source is selected on Settings.
        /// </summary>
        public void LoadCommonData()
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

        private void LoadCommonDataFromXml()
        {
            var infoDir = Settings.CommonDataDirectory;

            CurrenciesManager.LoadFromXmlFile(
                infoDir + "\\" + Settings.CurrenciesXmlFile);
            CountriesManager.LoadFromXmlFile(
                infoDir + "\\" + Settings.CountriesXmlFile);
            SectorsManager.LoadFromXmlFile(
                infoDir + "\\" + Settings.SectorsXmlFile);
            SecuritiesManager.LoadFromXmlFile(
                infoDir + "\\" + Settings.SecuritiesXmlFile);
        }

        private void InitDatabase()
        {
            Database = new DatabaseManager(
                connectionStringName: "DapperSQLite",
                dbCreateScript: System.IO.File.ReadAllText
                (
                    Settings.CommonDataDirectory + "\\" 
                    + Settings.SqliteDatabaseCreateScriptFile)
                );
        }

        private void LoadCommonDataFromSqlite()
        {
            InitDatabase();

            CurrenciesManager.LoadFromDatabase(Database);
            CountriesManager.LoadFromDatabase(Database);
            SectorsManager.LoadFromDatabase(Database);
            SecuritiesManager.LoadFromDatabase(Database);
        }

        public void XmlToSqlite()
        {
            InitDatabase();
            Database.CreateDatabase();
            Database.Import();
        }
    }
}
