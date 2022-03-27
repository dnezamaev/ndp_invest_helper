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

        public event Action AllDealsRemoved;

        public event Action LastDealRemoved;

        public event Action<Deal> DealCompleted;

        public List<Deal> Deals { get; private set; }

        public Portfolio FirstPortfolio {  get => GrouppingResults[0].Portfolio; }

        public Portfolio CurrentPortfolio { get => GrouppingResults.Last().Portfolio; }

        public GrouppingResults FirstResult { get => GrouppingResults[0]; }

        public GrouppingResults CurrentResult { get => GrouppingResults.Last(); }

        private System.Text.StringBuilder log;

        public InvestManager()
        {
            UnknownSecurities = new HashSet<Security>();
            IncompleteSecurities = new HashSet<Security>();
            BrokerReports = new List<BrokerReport>();
            GrouppingResults = new List<GrouppingResults>();
            Deals = new List<Deal>();
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
                dbCreateScript: File.ReadAllText
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
            IncompleteSecurities.Clear();
            UnknownSecurities.Clear();
            GrouppingResults.Clear();

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

            HandleBadReportSecurities();

            foreach (var item in Settings.Cash)
            {
                portfolio.AddCash(item.Key, item.Value);
            }

            GrouppingResults.Add(new GrouppingResults(portfolio));
        }

        /// <summary>
        /// Clean current analytics results.
        /// Again load common data, reports, make portfolio, analyze it, 
        /// repeat all deals on new data.
        /// </summary>
        public void Reload()
        {
            LoadCommonData();
            LoadPortfolio();

            var savedDeals = new List<Deal>(Deals);
            RemoveAllDeals();

            foreach (var item in savedDeals)
            {
                MakeDeal(item);
            }
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

        private void HandleBadReportSecurities()
        {
            var sb = new System.Text.StringBuilder();

            if (UnknownSecurities.Count != 0)
            {
                sb.AppendLine("Найдены неизвестные бумаги, они будут проигнорированы.");
                foreach (var security in UnknownSecurities)
                {
                    sb.AppendLine(security.BestUniqueFriendlyName);
                }
            }

            if (IncompleteSecurities.Count != 0)
            {
                sb.AppendLine("Найдены недозаполненные бумаги, они будут проигнорированы.");
                foreach (var security in IncompleteSecurities)
                {
                    sb.AppendLine(security.BestUniqueFriendlyName);
                }
            }

            if (UnknownSecurities.Count != 0 || IncompleteSecurities.Count != 0)
            {
                sb.AppendLine("Рекомендуется дополнить базу Securities.xml.");
            }

            LogAddText(sb.ToString());
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

        public void MakeDeal(Deal deal)
        {
            var newPortfolio = new Portfolio(CurrentPortfolio);
            newPortfolio.MakeDeal(deal);

            GrouppingResults.Add(new GrouppingResults(newPortfolio));

            Deals.Add(deal);

            var logText = string.Format
                (
                "{0} {1} {2} шт. по цене {3:n2} {4} на сумму {5:n2} {4}.",
                deal.Buy ? "Покупка" : "Продажа",
                deal.Security.BestUniqueFriendlyName, deal.Count, deal.Price, 
                deal.Currency, deal.Total
                );

            LogAddText(logText);

            if (DealCompleted != null)
                DealCompleted(deal);
        }

        public void RemoveLastDeal()
        {
            if (Deals.Count == 0)
                return;

            GrouppingResults.Remove(CurrentResult);

            if (LastDealRemoved != null)
                LastDealRemoved();

            Deals.RemoveAt(Deals.Count - 1);
        }

        public void RemoveAllDeals()
        {
            if (Deals.Count == 0)
                return;

            GrouppingResults.RemoveRange(1, GrouppingResults.Count - 1);

            if (AllDealsRemoved != null)
                AllDealsRemoved();

            Deals.Clear();
        }
    }
}
