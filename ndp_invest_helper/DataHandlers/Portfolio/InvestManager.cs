using System;
using System.IO;

namespace ndp_invest_helper
{
    public class InvestManager
    {
        public BrokerReportsManager BrokerReports { get; private set; }

        public AnalyticsManager Analytics { get; private set; }

        public InvestManager()
        {
            BrokerReports = new BrokerReportsManager();
            Analytics = new AnalyticsManager();

            BrokerReports.LoadReports(Settings.ReportsDirectory);
            Analytics.DoAnalytics(BrokerReports);
        }

        public void RedoAnalytics(bool repeatDeals = false)
        {
            Analytics.RedoAnalytics(BrokerReports, repeatDeals);
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
