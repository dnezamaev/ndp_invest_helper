using System;
using System.Linq;

namespace ndp_invest_helper
{
    public enum PortfolioDifferenceSource
    {
        Origin, LastDeal
    }

    public enum CommonDataSources
    {
        XmlFiles, SqliteDb
    }

    [Serializable]
    public class Settings
    {
        public const string
            SecuritiesXmlFile = "securities.xml",
            CountriesXmlFile = "countries.xml",
            CurrenciesXmlFile = "currencies.xml",
            SectorsXmlFile = "sectors.xml",
            SqliteDatabaseFile = "sqlite.db",
            SqliteDatabaseCreateScriptFile = "db_create.sql",
            LogFile = "log.txt",
            TaskInputFile = "task.xml",
            TaskOutputFile = "task.txt";

        public static string ReportsDirectory
        {
            get => UserSettings.BrokerReportsDirectory;
            set => UserSettings.BrokerReportsDirectory = value;
        }

        public static string CommonDataDirectory
        {
            get => UserSettings.CommonDataDirectory;
            set => UserSettings.CommonDataDirectory = value;
        }

        private static (CommonDataSources enumValue, string settingString)[]
            CommonDataSourcesToString = new[]
            {
                (CommonDataSources.SqliteDb, "SqliteDb"),
                (CommonDataSources.XmlFiles, "XmlFiles")
            };

        public static CommonDataSources CommonDataSource
        {
            get
            {
                return CommonDataSourcesToString.First
                    (x => x.settingString == Properties.Settings.Default.CommonDataSource)
                    .enumValue;
            }

            set
            {
                Properties.Settings.Default.CommonDataSource =
                    CommonDataSourcesToString.First(x => x.enumValue == value)
                    .settingString;
            }
        }

        private static (PortfolioDifferenceSource enumValue, string settingString)[]
            PortfolioDifferenceSourceToString = new[]
            {
                (PortfolioDifferenceSource.LastDeal, "Origin"),
                (PortfolioDifferenceSource.Origin, "LastDeal")
            };

        public static PortfolioDifferenceSource ShowDifferenceFrom
        {
            get
            {
                return PortfolioDifferenceSourceToString.First
                    (x => x.settingString == Properties.Settings.Default.ShowDifferenceFrom)
                    .enumValue;
            }

            set
            {
                Properties.Settings.Default.CommonDataSource =
                    PortfolioDifferenceSourceToString.First(x => x.enumValue == value)
                    .settingString;
            }
        }

        public static bool WriteLog
        {
            get => UserSettings.WriteLog;
            set => UserSettings.WriteLog = value;
        }

        // TODO: find way to save config to same place as exe.
        public static void Save()
        {
            // Shows actual config location.
            //var path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;

            Properties.Settings.Default.Save();
        }

        private static Properties.Settings UserSettings
        {
            get => Properties.Settings.Default; 
        }
    }
}
