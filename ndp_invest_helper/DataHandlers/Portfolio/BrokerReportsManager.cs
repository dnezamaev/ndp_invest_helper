using ndp_invest_helper.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_invest_helper
{
    public class BrokerReportsManager
    {
        public List<BrokerReport> Reports { get; private set; }

        public HashSet<Security> UnknownSecurities { get; private set; }

        public HashSet<Security> IncompleteSecurities { get; private set; }

        public BrokerReportsManager()
        {
            UnknownSecurities = new HashSet<Security>();
            IncompleteSecurities = new HashSet<Security>();
            Reports = new List<BrokerReport>();
        }

        public void LoadReports(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new ArgumentException
                    ("Не удалось найти рабочий каталог с отчетами " + directoryPath);
            }

            //var cashFile = directoryPath + "\\cash.xml";
            //if (File.Exists(cashFile))
            //{
            //    var cashReport = new CashReport();
            //    cashReport.ParseFile(cashFile);
            //    result.Add(cashReport);
            //}

            foreach (var reportFile in Directory.GetFiles(directoryPath + "\\vtb"))
            {
                var vtbReport = new VtbBrokerReport();
                vtbReport.ParseFile(reportFile);
                Reports.Add(vtbReport);
            }

            foreach (var reportFile in Directory.GetFiles(directoryPath + "\\tinkoff"))
            {
                var tinkoffReport = new TinkoffBrokerReport();
                tinkoffReport.ParseFile(reportFile);
                Reports.Add(tinkoffReport);
            }

            HandleBadReportSecurities();
        }

        private void HandleBadReportSecurities()
        {
            foreach (var report in Reports)
            {
                foreach (var security in report.Securities.Keys)
                    if (!security.IsCompleted)
                        IncompleteSecurities.Add(security);

                foreach (var security in report.UnknownSecurities)
                    UnknownSecurities.Add(security);
            }
        }

        public string GetBadSecuritiesMessage()
        {
            var sb = new StringBuilder();

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

            return sb.ToString();
        }
    }
}
