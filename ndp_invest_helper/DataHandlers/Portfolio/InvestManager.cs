using System;
using System.IO;

namespace ndp_invest_helper
{
    public class InvestManager
    {
        public BrokerReportsManager BrokerReports { get; private set; }

        public AnalyticsManager Analytics { get; private set; }

        public DatabaseManager Database { get; private set; }

        private System.Text.StringBuilder log;

        public InvestManager()
        {
            BrokerReports = new BrokerReportsManager();
            Analytics = new AnalyticsManager();

            log = new System.Text.StringBuilder();

            LoadCommonData();
            BrokerReports.LoadReports(Settings.ReportsDirectory);
            LoadPortfolio();
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
            CurrenciesManager.LoadFromXmlFile(Settings.CurrenciesXmlFilePath);
            CountriesManager.LoadFromXmlFile(Settings.CountriesXmlFilePath);
            SectorsManager.LoadFromXmlFile(Settings.SectorsXmlFilePath);
            SecuritiesManager.LoadFromXmlFile(Settings.SecuritiesXmlFilePath);
        }

        private void InitDatabase()
        {
            Database = new DatabaseManager(
                connectionStringName: "DapperSQLite",
                dbCreateScript: File.ReadAllText(Settings.SqliteDatabaseCreateScriptFilePath)
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

        /// <summary>
        /// Clean current analytics results.
        /// Again load common data, reports, make portfolio, analyze it. 
        /// </summary>
        public void ReloadAll()
        {
            Analytics.DisableEvents = true;
            Analytics.RemoveAllDeals();
            Analytics.DisableEvents = false;

            LoadCommonData();
            LoadPortfolio();
        }

        /// <summary>
        /// Make portfolio and analyze it.
        /// </summary>
        public void LoadPortfolio()
        {
            Analytics.DoAnalytics(BrokerReports);
        }

        public void RedoAnalytics()
        {
            LoadPortfolio();
            Analytics.RemakeDeals();
        }

        public void LogAddText(string text)
        {
            if (Settings.WriteLog)
            {
                log.AppendLine(text);
                log.AppendLine();
            }
        }

        public void LogSave()
        {
            if (Settings.WriteLog)
            {
                File.WriteAllText(Settings.LogFilePath, log.ToString());
            }
        }

        public void ExecuteTasks()
        {
            var taskManager = new TaskManager(Analytics.FirstPortfolio);
            var taskOutput = new System.Text.StringBuilder();
            taskManager.ParseXmlFile(Settings.TaskInputFilePath, taskOutput);
            File.WriteAllText(Settings.TaskOutputFilePath, taskOutput.ToString());
        }
    }
}
