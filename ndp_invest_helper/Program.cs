using System;
using System.IO;
using System.Collections.Generic;

namespace ndp_invest_helper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var settings = Settings.ReadFromFile("settings.xml");
            CurrenciesManager.SetRates(settings);

            CountriesManager.ParseXmlFile(settings.Files.CountriesInfo);
            SectorsManager.ParseXmlFile(settings.Files.SectorsInfo);
            SecuritiesManager.ParseXmlFile(settings.Files.SecuritiesInfo);

            var portfolio = new Portfolio(settings);
            var reportsDirPath = (args.Length == 0) ? settings.Files.ReportsDir : args[0];
            var reports = HandleReportsDirectory(reportsDirPath);
            foreach (var report in reports)
                portfolio.AddReport(report);

            var taskFilePath = "Task.xml"; 
            var taskManager = new TaskManager(portfolio);
            taskManager.ParseXmlFile(taskFilePath);
        }

        static List<Report> HandleReportsDirectory(string directoryPath)
        {
            var result = new List<Report>();
            var vtbDirectoryFiles = Directory.GetFiles(directoryPath);

            foreach (var vtbReportFile in vtbDirectoryFiles)
            {
                var vtbReport = new VtbReport();
                vtbReport.ParseXmlFile(vtbReportFile);
                result.Add(vtbReport);
            }

            return result;
        }

        static void ManualTesting(Portfolio portfolio)
        {
            var countryAnalytics = portfolio.GroupByCountry();
            Console.WriteLine(countryAnalytics);

            var typeAnalytics = portfolio.GroupByType(true);
            //Console.WriteLine(typeAnalytics);

            var shareCountryAnalytics = 
                typeAnalytics.Analytics["share"].Portfolio.GroupByCountry();
            //Console.WriteLine(shareCountryAnalytics);

            var currencyAnalytics = portfolio.GroupByCurrency();
            Console.WriteLine(currencyAnalytics);

            //var sectorAnalytics = portfolio.GroupBySector();
            //Console.WriteLine(sectorAnalytics);

            //Console.WriteLine("Finished.");
            //Console.ReadKey();
        }
    }
}
