using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ndp_invest_helper
{
    [Serializable]
    public class Settings
    {
        public static bool WriteLog { 
            get => Instance.Options.WriteLog == "yes"; 
            set => Instance.Options.WriteLog = (value ? "yes" : "no"); 
        }

        public static string LogFile { get => Instance.Files.Log; }

        /// <summary>
        /// Последний прочитанный ReadFromFile() объект.
        /// </summary>
        public static Settings Instance { get => instance; }

        private static Settings instance;

        [Serializable]
        public class Currency
        {
            [XmlAttribute(AttributeName = "key")]
            public string Key { get; set; }

            [XmlAttribute(AttributeName = "value")]
            public decimal Value { get; set; }
        }

        [Serializable]
        public class FilePaths
        {
            [XmlAttribute(AttributeName = "securities_info")]
            public string SecuritiesInfo;

            [XmlAttribute(AttributeName = "countries_info")]
            public string CountriesInfo;

            [XmlAttribute(AttributeName = "sectors_info")]
            public string SectorsInfo;

            [XmlAttribute(AttributeName = "reports_dir")]
            public string ReportsDir;

            [XmlAttribute(AttributeName = "task_output")]
            public string TaskOutput;

            [XmlAttribute(AttributeName = "log")]
            public string Log;
        }

        [XmlElement(ElementName = "files")]
        public FilePaths Files;

        [Serializable]
        public class OptionsType
        {
            [XmlAttribute(AttributeName = "write_log")]
            public string WriteLog;

            [XmlAttribute(AttributeName = "show_difference_from")]
            public string ShowDifferenceFrom;
        }

        [XmlElement(ElementName = "options")]
        public OptionsType Options;

        /// <summary>
        /// Курсы валют к рублю.
        /// </summary>
        [XmlArray(ElementName = "currency_rates")]
        [XmlArrayItem(ElementName = "currency")]
        public List<Currency> CurrencyRates = new List<Currency>();

        public static Settings ReadFromFile(string xmlFilePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(Settings));
            var streamReader = new StreamReader(xmlFilePath);
            var settings = (Settings)xmlSerializer.Deserialize(streamReader);
            instance = settings;
            streamReader.Close();

            return settings;
        }

        public void WriteToFile(string xmlFilePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(Settings));
            var streamWriter = new StreamWriter(xmlFilePath);
            xmlSerializer.Serialize(streamWriter, this);
            streamWriter.Close();
        }
    }
}
