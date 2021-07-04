﻿using System;
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
        [Serializable]
        public class Currency
        {
            [XmlAttribute]
            public string Key { get; set; }
            [XmlAttribute]
            public decimal Value { get; set; }
        }

        /// <summary>
        /// Внебиржевые деньги в чистом виде: наличность, депозиты и т.п.
        /// </summary>
        [XmlArray(ElementName = "cash")]
        [XmlArrayItem(ElementName = "currency")]
        public List<Currency> Cash = new List<Currency>();

        /// <summary>
        /// Курсы валют к рублю.
        /// </summary>
        [XmlArray(ElementName = "rates")]
        [XmlArrayItem(ElementName = "currency")]
        public List<Currency> Rates = new List<Currency>();

        public static Settings ReadFromFile(string xmlFilePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(Settings));
            var streamReader = new StreamReader(xmlFilePath);
            var settings = (Settings)xmlSerializer.Deserialize(streamReader);
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