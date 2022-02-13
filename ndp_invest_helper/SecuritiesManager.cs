﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using ndp_invest_helper.Models;

// !!! Заметка себе на будущее, когда захочется рефакторинга под красивое ООП.
// В теории все поля должны быть закрыты и доступны через ReadOnly интерфейсы
// либо методы, но на практике это лишний геморрой и усложнение кода.
// Тогда возвращаемые объекты Security, Issuer тоже должны быть Immutable.
// Можно заморочиться, но смысла нет.

namespace ndp_invest_helper
{
    /// <summary>
    /// База всех эмитентов и их ценных бумаг.
    /// </summary>
    public static class SecuritiesManager
    {
        private static string parsedFilePath;

        public static List<Issuer> Issuers;
               
        public static List<Security> Securities;
               
        public static Dictionary<string, Security> SecuritiesByIsin;
               
        public static Dictionary<string, Security> SecuritiesByTicker;

        /// <summary>
        /// Словарь: тип актива в sec_type файла securities.xml -> название для отображения.
        /// </summary>
        public static readonly Dictionary<string, string> SecTypeFriendlyNames
            = new Dictionary<string, string>
            {
                { "cash", "Деньги" },
                { "share", "Акции" },
                { "bond", "Облигации" },
                { "etf", "Фонды" },
                { "gold", "Золото" },
            };

        /// <summary>
        /// Словарь соответствия типов C# и типов в аттрибутах securities.xml.
        /// На случай рефакторинга, чтобы не побилась зависимость при переименовании типа.
        /// </summary>
        public static readonly Dictionary<Type, string> TypesDictionary
            = new Dictionary<Type, string> 
            {
                { typeof(Share), "share" },
                { typeof(Bond),  "bond" },
                { typeof(ETF),    "etf" },
            };

        private static void Init()
        {
            Issuers = new List<Issuer>();
            Securities = new List<Security>();
            SecuritiesByIsin = new Dictionary<string, Security>();
            SecuritiesByTicker = new Dictionary<string, Security>();
        }

        public static void LoadFromDatabase(DatabaseManager database)
        {
            Init();

            LoadIssuersFromDatabase(database);
            LoadSecuritiesFromDatabase(database);
        }

        private static void LoadIssuersFromDatabase(DatabaseManager database)
        {
            Init();

            var dbIssuers = database
                .GetFullTable<Issuer>(
                "Issuers");

            var dbCountries = database
                .GetFullTable<IssuersCountriesLink>(
                "Issuers_Countries_Link");

            var dbCurrencies = database
                .GetFullTable<IssuersCurrenciesLink>(
                "Issuers_Currencies_Link");

            var dbSectors = database
                .GetFullTable<IssuersEconomySectorsLink>(
                "Issuers_EconomySectors_Link");

            foreach (var dbIssuer in dbIssuers)
            {
                var issuer = new Issuer 
                {
                    Id = dbIssuer.Id,

                    NameRus = dbIssuer.NameRus,

                    Currencies = dbCurrencies
                    .Where(x => x.IssuerId == dbIssuer.Id)
                    .ToDictionary(
                        k => k.CurrencyCode,
                        v => v.Part),

                    Countries = dbCountries
                    .Where(x => x.IssuerId == dbIssuer.Id)
                    .ToDictionary(
                        k => k.CountryCode,
                        v => v.Part),

                    Sectors = dbSectors
                    .Where(x => x.IssuerId == dbIssuer.Id)
                    .ToDictionary(
                        k => SectorsManager.ById[k.SectorId],
                        v => v.Part)
                };

                Issuers.Add(issuer);
            }
        }

        private static void LoadSecuritiesFromDatabase(DatabaseManager database)
        {
            var dbCountries = database
                .GetFullTable<SecuritiesCountriesLink>(
                "Securities_Countries_Link");

            var dbCurrencies = database
                .GetFullTable<SecuritiesCurrenciesLink>(
                "Securities_Currencies_Link");

            var dbSectors = database
                .GetFullTable<SecuritiesEconomySectorsLink>(
                "Securities_EconomySectors_Link");

            var dbFundAssets = database
                .GetFullTable<FundsAssetsLink>(
                "Funds_Assets_Link");

            var dbAssetTypes = database
                .GetFullTable<AssetType>(
                "AssetTypes");

            var dbSecurities = database
                .GetFullTable<SecurityModel>(
                "Securities");

            foreach (var dbSecurity in dbSecurities)
            {
                Security security;

                switch ((AssetTypes)dbSecurity.SecurityType)
                {
                    case AssetTypes.Share:
                        security = new Share();
                        break;
                    case AssetTypes.Bond:
                        security = new Bond();
                        break;
                    case AssetTypes.Etf:
                        security = new ETF();
                        break;
                    default:
                        throw new NotImplementedException("Unknown security type");
                }

                security.Id = dbSecurity.ID;
                security.Isin = dbSecurity.ISIN;
                security.Ticker = dbSecurity.Ticker;
                security.Name = dbSecurity.NameRus;
                security.Issuer = Issuers.Find(
                    x => x.Id == dbSecurity.IssuerID);
                security.Issuer.Securities.Add(security);

                if (!string.IsNullOrEmpty(security.Isin))
                    SecuritiesByIsin[security.Isin] = security;

                if (!string.IsNullOrEmpty(security.Ticker))
                    SecuritiesByTicker[security.Ticker] = security;

                var dbSecurityCountries = dbCountries
                    .Where(x => x.SecurityId == dbSecurity.ID)
                    .ToArray();

                foreach (var country in dbSecurityCountries)
                {
                    security.SecurityCountries.Add(
                        country.CountryCode, (decimal)country.Part);
                }

                var dbSecurityCurrencies = dbCurrencies
                    .Where(x => x.SecurityId == dbSecurity.ID)
                    .ToArray();

                foreach (var currency in dbSecurityCurrencies)
                {
                    security.SecurityCurrencies.Add(
                        currency.CurrencyCode, (decimal)currency.Part);
                }

                var dbSecuritySectors = dbSectors
                    .Where(x => x.SecurityId == dbSecurity.ID)
                    .ToArray();

                foreach (var sector in dbSecuritySectors)
                {
                    security.SecuritySectors.Add(
                        SectorsManager.ById[sector.SectorId.ToString()],
                        (decimal)sector.Part);
                }

                if (security is ETF)
                {
                    var etf = security as ETF;

                    // Assets for this ETF.
                    var currentEtfAssets = dbFundAssets
                        .Where(x => x.FundSecurityId == dbSecurity.ID)
                        .ToArray();

                    foreach (var asset in currentEtfAssets)
                    {
                        etf.WhatInside.Add(
                            dbAssetTypes
                                .Where(x => x.Id == asset.AssetTypeId)
                                .First().NameRus,
                            asset.Part);
                    }
                }

                Securities.Add(security);
            }
        }

        public static void ParseXmlText(string xmlText)
        {
            Init();

            var xRoot = XElement.Parse(xmlText);

            foreach (var xIssuer in xRoot.Elements("issuer"))
            {
                var issuer = new Issuer
                {
                    NameRus = xIssuer.Attribute("name").Value
                };

                // Разбираем страны, валюты, отрасли эмитента.
                issuer.Countries = Utils.HandleComplexStringXmlAttribute(
                    xIssuer, "country");
                issuer.Currencies = Utils.HandleComplexStringXmlAttribute(
                    xIssuer, "currency");
                Utils.HandleSectorAttribute(issuer.Sectors, xIssuer, SectorsManager.DefaultSectorIdLevel2);

                // Разбираем бумаги эмитента и заполняем словари быстрого доступа.
                foreach (var xSecurity in xIssuer.Elements("security"))
                {
                    var security = ParseXmlSecurity(xSecurity);
                    security.Issuer = issuer;
                    issuer.Securities.Add(security);
                    Securities.Add(security);
                    SecuritiesByIsin.Add(security.Isin, security);

                    // Тикер может быть пустой.
                    if (!string.IsNullOrEmpty(security.Ticker) && security.Ticker != "???")
                        SecuritiesByTicker.Add(security.Ticker, security);
                }

                Issuers.Add(issuer);
            }
        }

        public static void LoadFromXmlFile(string filePath)
        {
            parsedFilePath = filePath;
            ParseXmlText(File.ReadAllText(filePath));
        }

        private static Security ParseXmlSecurity(XElement xSecurity)
        {
            // Разбираем общие параметры.
            var xSecType = xSecurity.Attribute("sec_type");

            if (xSecType == null)
                throw new ArgumentException(string.Format(
                    "В файле {0} найдена ценная бумага без обязательного аттрибута sec_type {2}.",
                    parsedFilePath, xSecurity.ToString()));

            var secType = xSecType.Value;

            var xIsin = xSecurity.Attribute("isin");

            if (xIsin == null)
                throw new ArgumentException(string.Format(
                    "В файле {0} найдена ценная бумага без обязательного аттрибута isin {2}.",
                    parsedFilePath, xSecurity.ToString()));

            var isin = xIsin.Value;

            var ticker = "";
            var xTicker = xSecurity.Attribute("ticker");

            if (xTicker != null)
                ticker = xTicker.Value;

            var currency = "";
            var xCurrency = xSecurity.Attribute("currency");
            if (xCurrency != null)
                currency = xCurrency.Value;

            Security security = null;

            // Разбираем бумаги по типам - акции, облигации, фонды.
            switch (secType)
            {
                case "share":
                    security = new Share();
                    break;

                case "bond":
                    security = new Bond();
                    break;

                case "etf":
                    security = new ETF();
                    Utils.HandleComplexStringXmlAttribute(
                        (security as ETF).WhatInside, xSecurity, "what_inside");
                    break;
                default:
                    break;
            }

            security.Isin = isin;
            security.Ticker = ticker;

            Utils.HandleComplexStringXmlAttribute(
               security.SecurityCountries, xSecurity, "country");
            Utils.HandleComplexStringXmlAttribute(
               security.SecurityCurrencies, xSecurity, "currency");
            Utils.HandleSectorAttribute(
                security.SecuritySectors, xSecurity, SectorsManager.DefaultSectorIdLevel2);

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
                var issuer = Issuers.Find(x => x.NameRus == xIssuer.Attribute("name").Value);

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