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

    class SecuritiesManager
    {
        private SectorsManager sectorsManager;

        public List<Issuer> Issuers;

        public List<Security> Securities = new List<Security>();

        public Dictionary<string, Security> SecuritiesByIsin = new Dictionary<string, Security>();

        public Dictionary<string, Security> SecuritiesByTicker = new Dictionary<string, Security>();

        public SecuritiesManager(SectorsManager sectorsManager)
        {
            this.sectorsManager = sectorsManager;
        }

        public static SecuritiesManager FromXmlFile(
            string filePath, SectorsManager sectorsManager
            )
        {
            var securitiesManager = new SecuritiesManager(sectorsManager);
            securitiesManager.ParseXmlFile(filePath);
            return securitiesManager;
        }

        public void ParseXmlFile(string filePath)
        {
            Issuers = new List<Issuer>();

            var xRoot = XElement.Parse(File.ReadAllText(filePath));

            foreach (var xIssuer in xRoot.Elements("issuer"))
            {
                var issuer = new Issuer
                {
                    Name = xIssuer.Attribute("name").Value
                };

                issuer.Countries = Utils.HandleComplexStringXmlAttribute(
                    xIssuer, "country");
                Utils.HandleSectorAttribute(xIssuer, issuer.Sectors, sectorsManager);

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

        private Security ParseXmlSecurity(XElement xSecurity)
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
                    Utils.HandleSectorAttribute(xSecurity, etf.Sectors, sectorsManager);
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
        internal void CorrectData(string filePath)
        {
            var xRoot = XElement.Parse(File.ReadAllText(filePath));

            foreach (var xIssuer in xRoot.Elements("issuer"))
            {
                var issuer = this.Issuers.Find(x => x.Name == xIssuer.Attribute("name").Value);

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
