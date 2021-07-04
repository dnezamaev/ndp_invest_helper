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

            var countriesManager = CountriesManager.FromXmlFile(@"data\info\countries.xml");
            var sectorsManager = SectorsManager.FromXmlFile(@"data\info\sectors.xml");
            var securitiesManager = SecuritiesManager.FromXmlFile(@"data\info\securities.xml", sectorsManager);

            var portfolio = new Portfolio(settings);
            var reportsDirPath = (args.Length == 0) ? @"data\reports\vtb" : args[0];
            var reports = HandleReportsDirectory(reportsDirPath, securitiesManager);
            foreach (var report in reports)
                portfolio.AddReport(report);

            var taskFilePath = "Task.xml"; 
            var taskManager = new TaskManager(portfolio);
            taskManager.ParseXmlFile(taskFilePath);
        }

        static List<Report> HandleReportsDirectory(
            string directoryPath, SecuritiesManager securitiesManager)
        {
            var result = new List<Report>();
            var vtbDirectoryFiles = Directory.GetFiles(directoryPath);

            foreach (var vtbReportFile in vtbDirectoryFiles)
            {
                var vtbReport = new VtbReport();
                vtbReport.ParseXmlFile(vtbReportFile, securitiesManager);
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

        static void CorrectData(
            SecuritiesManager securitiesManager,
            SectorsManager sectorsManager)
        {
            securitiesManager.CorrectData(@"data\info\securities.xml");
            securitiesManager = new SecuritiesManager(sectorsManager);
            securitiesManager.ParseXmlFile(@"data\info\securities.xml");
        }
    }
}
