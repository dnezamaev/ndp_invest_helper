using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace ndp_invest_helper
{
    class Issuer
    {
        public string Name;

        public Dictionary<Sector, decimal> Sectors = new Dictionary<Sector, decimal>();

        public Dictionary<string, decimal> Countries = new Dictionary<string, decimal>();

        public List<Security> Securities = new List<Security>();

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Name);

            sb.Append("  ");
            foreach (var item in Sectors)
            {
                if (item.Key == null)
                    continue;

                sb.AppendFormat("{0} = {1}; ", item.Key.Name, item.Value);
            }
            sb.AppendLine();


            sb.Append("  ");
            foreach (var item in Countries)
            {
                sb.AppendFormat("{0} = {1}; ", item.Key, item.Value);
            }
            sb.AppendLine();

            sb.Append("  ");
            foreach (var item in Securities)
            {
                sb.AppendFormat("{0}; ", 
                    item.Ticker == "???" || string.IsNullOrEmpty(item.Ticker)
                    ? item.Isin : item.Ticker);
            }
            sb.AppendLine();

            return sb.ToString();
        }
    }

    /// <summary>
    /// База всех эмитентов и их ценных бумаг.
    /// !!! Заметка себе на будущее, когда захочется рефакторинга под красивое ООП.
    /// В теории все поля должны быть закрыты и доступны через ReadOnly интерфейсы
    /// либо методы, но на практике это лишний геморрой и усложнение кода.
    /// Тогда возвращаемые объекты Security, Issuer тоже должны быть Immutable.
    /// Можно заморочиться, но смысла нет.
    /// </summary>
    static class SecuritiesManager
    {
        public static List<Issuer> Issuers;
               
        public static List<Security> Securities;
               
        public static Dictionary<string, Security> SecuritiesByIsin;
               
        public static Dictionary<string, Security> SecuritiesByTicker;

        public static void ParseXmlFile(string filePath)
        {
            Issuers = new List<Issuer>();
            Securities = new List<Security>();
            SecuritiesByIsin = new Dictionary<string, Security>();
            SecuritiesByTicker = new Dictionary<string, Security>();

            var xRoot = XElement.Parse(File.ReadAllText(filePath));

            foreach (var xIssuer in xRoot.Elements("issuer"))
            {
                var issuer = new Issuer
                {
                    Name = xIssuer.Attribute("name").Value
                };

                issuer.Countries = Utils.HandleComplexStringXmlAttribute(
                    xIssuer, "country");
                Utils.HandleSectorAttribute(xIssuer, issuer.Sectors);

                foreach (var xSecurity in xIssuer.Elements("security"))
                {
                    var security = ParseXmlSecurity(xSecurity);
                    security.Issuer = issuer;
                    Securities.Add(security);
                    SecuritiesByIsin.Add(security.Isin, security);

                    if (!string.IsNullOrEmpty(security.Ticker) && security.Ticker != "???")
                        SecuritiesByTicker.Add(security.Ticker, security);
                }

                Issuers.Add(issuer);
            }
        }

        private static Security ParseXmlSecurity(XElement xSecurity)
        {
            // Разбираем общие параметры.
            Security security = null;
            var sec_type = xSecurity.Attribute("sec_type").Value;
            var isin = xSecurity.Attribute("isin").Value;
            var ticker = xSecurity.Attribute("ticker").Value;

            var currency = "???";
            var xCurrency = xSecurity.Attribute("currency");
            if (xCurrency != null)
                currency = xCurrency.Value;

            // Разбираем бумаги по типам - акции, облигации, ПИФы.
            switch (sec_type)
            {
                case "share":
                    security = new Share()
                    {
                        Isin = isin,
                        Ticker = ticker,
                        Currency = currency
                    };
                    break;
                case "bond":
                    security = new Bond()
                    {
                        Isin = isin,
                        Ticker = ticker,
                        Currency = currency
                    };
                    break;
                case "etf":
                    security = new ETF()
                    {
                        Isin = isin,
                        Ticker = ticker,
                    };

                    var etf = security as ETF;

                    // У ПИФ-ов в составе может быть несколько стран, валют, секторов экономики.
                    // У акций, облигаций тоже, но информация по странам и секторам хранится в эмитентах.
                    etf.Countries = 
                        Utils.HandleComplexStringXmlAttribute(xSecurity, "country");
                    etf.Currencies = 
                        Utils.HandleComplexStringXmlAttribute(xSecurity, "currency");
                    etf.WhatInside = 
                        Utils.HandleComplexStringXmlAttribute(xSecurity, "what_inside");
                    Utils.HandleSectorAttribute(xSecurity, etf.Sectors);
                    break;
                default:
                    break;
            }

            return security;
        }

        /// <summary>
        /// Правим данные в файле. Только для внутреннего использования.
        /// Заполняем страны.
        /// </summary>
        internal static void CorrectData(string filePath)
        {
            var xRoot = XElement.Parse(File.ReadAllText(filePath));

            foreach (var xIssuer in xRoot.Elements("issuer"))
            {
                var issuer = Issuers.Find(x => x.Name == xIssuer.Attribute("name").Value);

                // Уже заполнено, но не вопросами.
                if (issuer.Countries.Count > 0
                    && !(issuer.Countries.Count == 1 && issuer.Countries.ContainsKey("???"))
                    )
                    continue;

                // Если все бумаги с ISIN="RU...", то эмитент российский.
                if (issuer.Securities.TrueForAll(x => x.Isin.StartsWith("RU")))
                    xIssuer.SetAttributeValue("country", "RU");

                var xSecuritiesList = new List<XElement>(xIssuer.Elements("security"));

                foreach (var xSecurity in xSecuritiesList)
                {
                    var security = SecuritiesByIsin[xSecurity.Attribute("isin").Value];

                    // ETF заполняем вручную.
                    if (security is ETF)
                        continue;

                    // Если бумага с ISIN="RU...", то она российская.
                    if (security.Isin.StartsWith("RU"))
                        xSecurity.SetAttributeValue("country", "RU");
                }
            }

            xRoot.Save(filePath);
        }
    }
}
