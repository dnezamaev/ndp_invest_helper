using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ndp_invest_helper.Models;

namespace ndp_invest_helper
{
    public class InvestManager
    {
        public List<BrokerReport> BrokerReports { get; private set; }

        public DatabaseManager Database { get; private set; }

        public HashSet<Security> UnknownSecurities { get; private set; }

        public HashSet<Security> IncompleteSecurities { get; private set; }

        public List<GrouppingResults> GrouppingResults { get; private set; }

        private System.Text.StringBuilder log;

        public InvestManager()
        {
            Init();

            LoadCommonData();
        }

        private void Init()
        {
            UnknownSecurities = new HashSet<Security>();
            IncompleteSecurities = new HashSet<Security>();
            BrokerReports = new List<BrokerReport>();
            GrouppingResults = new List<GrouppingResults>();
            log = new System.Text.StringBuilder();
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

        public void LoadPortfolio()
        {
            var portfolio = new Portfolio();
            var reports = LoadReports(Settings.ReportsDirectory);

            foreach (var report in reports)
            {
                foreach (var security in report.Securities.Keys)
                    if (!security.IsCompleted)
                        IncompleteSecurities.Add(security);

                foreach (var security in report.UnknownSecurities)
                    UnknownSecurities.Add(security);

                portfolio.AddReport(report);
            }

            GrouppingResults.Add(new GrouppingResults(portfolio));
        }

        private List<BrokerReport> LoadReports(string directoryPath)
        {
            var result = new List<BrokerReport>();

            if (!Directory.Exists(directoryPath))
            {
                throw new ArgumentException
                    ("Не удалось найти рабочий каталог с отчетами " + directoryPath);
            }

            var cashFile = directoryPath + "\\cash.xml";
            if (File.Exists(cashFile))
            {
                var cashReport = new CashReport();
                cashReport.ParseFile(cashFile);
                result.Add(cashReport);
            }

            foreach (var reportFile in Directory.GetFiles(directoryPath + "\\vtb"))
            {
                var vtbReport = new VtbBrokerReport();
                vtbReport.ParseFile(reportFile);
                result.Add(vtbReport);
            }

            foreach (var reportFile in Directory.GetFiles(directoryPath + "\\tinkoff"))
            {
                var tinkoffReport = new TinkoffBrokerReport();
                tinkoffReport.ParseFile(reportFile);
                result.Add(tinkoffReport);
            }

            return result;
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
                File.WriteAllText(Settings.LogFile, log.ToString());
        }

        public void ExecuteTasks()
        {
            var taskManager = new TaskManager(FirstPortfolio);
            var taskOutput = new System.Text.StringBuilder();
            taskManager.ParseXmlFile(Settings.TaskInputFile, taskOutput);
            File.WriteAllText(Settings.TaskOutputFile, taskOutput.ToString());
        }

        public Portfolio FirstPortfolio {  get => GrouppingResults[0].Portfolio; }

        public Portfolio CurrentPortfolio { get => GrouppingResults.Last().Portfolio; }

        public GrouppingResults FirstResult { get => GrouppingResults[0]; }

        public GrouppingResults CurrentResult { get => GrouppingResults.Last(); }

    }
}
