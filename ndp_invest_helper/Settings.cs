using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;

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
                Properties.Settings.Default.ShowDifferenceFrom =
                    PortfolioDifferenceSourceToString.First(x => x.enumValue == value)
                    .settingString;
            }
        }

        public static bool WriteLog
        {
            get => UserSettings.WriteLog;
            set => UserSettings.WriteLog = value;
        }

        public static List<KeyValuePair<string, decimal>> Cash
        {
            get
            {
                // Format: EUR=12.50 USD=23.45
                var settingValue = UserSettings.Cash;

                var pairs = settingValue.Split(
                    new[] { ' ' }, 
                    StringSplitOptions.RemoveEmptyEntries);

                var result = new List<KeyValuePair<string, decimal>>();

                foreach (var item in pairs)
                {
                    var kvpArray = item.Split('=');

                    if (kvpArray.Length != 2)
                    {
                        throw new Exception(
                            "Неверное значение в файле настроек " +
                            $"{SettingFilePath} в параметре Cash " +
                            $" в значении {item}. " +
                            "Ожидаемый формат: валюта=значение");
                    }

                    var currency = kvpArray[0];
                    decimal value = 0;
                    var parsedOk = decimal.TryParse
                        (
                        kvpArray[1],
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out value
                        );

                    if (!parsedOk)
                    {
                        throw new Exception(
                            "Неверное значение в файле настроек " +
                            $"{SettingFilePath} в параметре Cash " +
                            $" в значении {item}. " +
                            "Ожидаемый формат: валюта=значение");
                    }

                    result.Add(
                        new KeyValuePair<string, decimal>(
                            currency, value));
                }

                return result;
            }
            set
            {
                var sb = new StringBuilder();

                foreach (var item in value)
                {
                    sb.Append(
                        $"{item.Key}=" +
                        $"{item.Value.ToString(CultureInfo.InvariantCulture)} ");
                }

                UserSettings.Cash = sb.ToString().TrimEnd();
            }
        }

        // TODO: find way to save config to same place as exe.
        public static void Save()
        {
            Properties.Settings.Default.Save();
        }

        private static string SettingFilePath
        {
            get => ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
        }

        private static Properties.Settings UserSettings
        {
            get => Properties.Settings.Default; 
        }
    }
}
