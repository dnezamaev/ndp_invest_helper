using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

// !!! Заметка себе на будущее, когда захочется рефакторинга под красивое ООП.
// В теории все поля должны быть закрыты и доступны через ReadOnly интерфейсы
// либо методы, но на практике это лишний геморрой и усложнение кода.
// Тогда возвращаемые объекты Security, Issuer тоже должны быть Immutable.
// Можно заморочиться, но смысла нет.

namespace ndp_invest_helper
{
    /// <summary>
    /// Статическая информация о ценной бумаге.
    /// </summary>
    public class Security
    {
        /// <summary>
        /// Указывает уникальный номер бумаги в международном реестре ценных бумаг.
        /// </summary>
        public string Isin;

        /// <summary>
        /// Указывает уникальный тикер бумаги. Может быть пустым.
        /// </summary>
        public string Ticker;

        /// <summary>
        /// Название бумаги, например "Привелегированные акции Сбера".
        /// </summary>
        public string Name;

        /// <summary>
        /// Валюта, в которой торгуется бумага. Указывается код валюты.
        /// </summary>
        public string TradeCurrency;

        /// <summary>
        /// Эмитент бумаги.
        /// </summary>
        public Issuer Issuer;

        #region Виртуальные свойства с аттрибутами бумаг для экономии памяти.
        // Приоритет:
        // 1. аттрибуты фонда, у него свои реальные аттрибуты
        // 2. аттрибуты эмитента для обычных бумаг, т.к. они не имеют своих
        // Особый случай - валюта Currencies, см. ниже.

        /// <summary>
        /// Отрасли экономики фонда или эмитента бумаги.
        /// </summary>
        public virtual Dictionary<Sector, decimal> Sectors
        {
            get => Issuer.Sectors;
        }

        /// <summary>
        /// Страны фонда или эмитента бумаги.
        /// </summary>
        public virtual Dictionary<string, decimal> Countries
        {
            get => Issuer.Countries;
        }

        /// <summary>
        /// Валюты фонда или бумаги. Приоритет:
        /// 1. если фонд, то его валютный состав;
        /// 2. если не фонд, то валюта, в которой торгуется бумага;
        /// 3. если она не указана, то валюта эмитента
        /// </summary>
        public virtual Dictionary<string, decimal> Currencies
        {
            get => !string.IsNullOrEmpty(TradeCurrency) ? 
                new Dictionary<string, decimal> { { TradeCurrency, 1.0M } }
                : Issuer.Currencies;
        }
        #endregion

        #region Текстовые имена.
        /// <summary>
        /// Лучшее уникальное имя бумаги для отображения.
        /// Порядок: Name, Ticker, Isin.
        /// </summary>
        public string BestUniqueFriendlyName
        {
            get
            {
                if (!string.IsNullOrEmpty(Name))
                    return Name;

                if (!string.IsNullOrEmpty(Ticker) && Ticker != "???")
                    return Ticker;

                return Isin;
            }
        }

        /// <summary>
        /// Лучшее имя бумаги для отображения.
        /// Порядок: Name, Ticker, Issuer.Name, Isin.
        /// </summary>
        public string BestFriendlyName
        {
            get
            {
                if (!string.IsNullOrEmpty(Name))
                    return Name;

                if (!string.IsNullOrEmpty(Ticker) && Ticker != "???")
                    return Ticker;

                if (this is Share || this is Bond)
                    return Issuer.Name;

                return Isin;
            }
        }

        /// <summary>
        /// Полное наименование бумаги включая ISIN, Ticker, Name, Issuer.Name.
        /// </summary>
        public string FullName
        {
            get
            {
                var sb = new StringBuilder(Isin);

                if (!string.IsNullOrEmpty(Ticker) && Ticker != "???")
                    sb.AppendFormat(" {0}", Ticker);

                if (!string.IsNullOrEmpty(Name))
                    sb.AppendFormat(" {0}", Name);

                sb.AppendFormat(" {0}", Issuer.Name);

                return sb.ToString();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder(base.ToString());

            sb.Append(" Sectors={");
            foreach (var item in Sectors)
            {
                sb.AppendFormat("{0}:{1}, ", item.Key.Name, item.Value);
            }

            sb.Append("} Countries={");
            foreach (var item in Countries)
            {
                sb.AppendFormat("{0}:{1}, ", item.Key, item.Value);
            }

            sb.Append("} Currencies={");
            foreach (var item in Currencies)
            {
                sb.AppendFormat("{0}:{1}, ", item.Key, item.Value);
            }
            sb.Append("}");

            return sb.ToString();
        }
        #endregion
    }

    /// <summary>
    /// Акция или депозитарная расписка.
    /// </summary>
    public class Share : Security 
    {
    }

    /// <summary>
    /// Облигация или еврооблигация.
    /// </summary>
    public class Bond : Security
    {
    }

    /// <summary>
    /// Фонд БПИФ, ETF и т.п.
    /// </summary>
    public class ETF : Security
    {
        private Dictionary<Sector, decimal> sectors; 

        private Dictionary<string, decimal> countries;

        private Dictionary<string, decimal> currencies;

        /// <summary>
        /// Состав фонда по типам активов: акции, облигации, золото, деньги и т.п.
        /// Список см. в SecuritiesManager.SecTypeFriendlyNames
        /// </summary>
        public Dictionary<string, decimal> WhatInside;

        public ETF()
        {
            sectors = new Dictionary<Sector, decimal>();
            countries = new Dictionary<string, decimal>();
            currencies = new Dictionary<string, decimal>();
            WhatInside = new Dictionary<string, decimal>();
        }

        public ETF(
            Dictionary<Sector, decimal> sectors,
            Dictionary<string, decimal> countries,
            Dictionary<string, decimal> currencies,
            Dictionary<string, decimal> whatInside
            )
        {
            this.sectors = sectors;
            this.countries = countries;
            this.currencies = currencies;
            this.WhatInside = whatInside;
        }

        public override Dictionary<Sector, decimal> Sectors
        {
            get => sectors;
        }

        public override Dictionary<string, decimal> Countries
        {
            get => countries;
        }

        public override Dictionary<string, decimal> Currencies
        {
            get => currencies;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(base.ToString());

            sb.Append(" What inside={");
            foreach (var item in WhatInside)
            {
                sb.AppendFormat("{0}:{1}, ", item.Key, item.Value);
            }
            sb.Append("}");

            return sb.ToString();
        }
    }

    /// <summary>
    /// Динамическая информация о ценной бумаге.
    /// </summary>
    public class SecurityInfo
    {
        /// <summary>
        /// Количество в портфеле, отчете и т.п.
        /// </summary>
        public UInt64 Count;

        /// <summary>
        /// Цена в рублях.
        /// </summary>
        public decimal Price;

        /// <summary>
        /// Коэффициент коррекции для итоговой суммы. 
        /// Нужен при группировке фондов с разбором содержимого.
        /// </summary>
        public decimal Correction = 1;

        public decimal Total { get { return Count * Price * Correction; } }

        public SecurityInfo() { }

        public SecurityInfo(SecurityInfo securityInfo) 
        {
            this.Count = securityInfo.Count;
            this.Price = securityInfo.Price;
            this.Correction = securityInfo.Correction;
        }

        public SecurityInfo(SecurityInfo securityInfo, decimal correction) 
        {
            this.Count = securityInfo.Count;
            this.Price = securityInfo.Price;
            this.Correction = correction;
        }
    }

    /// <summary>
    /// Эмитент ценной бумаги.
    /// </summary>
    public class Issuer
    {
        /// <summary>
        /// Уникальное наименование эмитента.
        /// </summary>
        public string Name;

        public Dictionary<Sector, decimal> Sectors = new Dictionary<Sector, decimal>();

        public Dictionary<string, decimal> Countries = new Dictionary<string, decimal>();

        public Dictionary<string, decimal> Currencies = new Dictionary<string, decimal>();

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
            foreach (var item in Currencies)
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
        public static readonly Dictionary<string, string> SecTypeFriendlyNames = 
            new Dictionary<string, string>
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
        public static readonly Dictionary<Type, string> TypesDictionary = new Dictionary<Type, string> 
        {
            { typeof(Share), "share" },
            { typeof(Bond),  "bond" },
            { typeof(ETF),    "etf" },
        };

        public static void ParseXmlText(string xmlText)
        {
            Issuers = new List<Issuer>();
            Securities = new List<Security>();
            SecuritiesByIsin = new Dictionary<string, Security>();
            SecuritiesByTicker = new Dictionary<string, Security>();

            var xRoot = XElement.Parse(xmlText);

            foreach (var xIssuer in xRoot.Elements("issuer"))
            {
                var issuer = new Issuer
                {
                    Name = xIssuer.Attribute("name").Value
                };

                // Разбираем страны, валюты, отрасли эмитента.
                issuer.Countries = Utils.HandleComplexStringXmlAttribute(
                    xIssuer, "country");
                issuer.Currencies = Utils.HandleComplexStringXmlAttribute(
                    xIssuer, "currency");
                Utils.HandleSectorAttribute(xIssuer, issuer.Sectors, SectorsManager.DefaultSectorId);

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

        public static void ParseXmlFile(string filePath)
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
                    // У фондов в составе может быть несколько стран, валют, секторов экономики.
                    // У акций, облигаций тоже, но информация ним хранится в эмитентах.
                    security = new ETF(
                        Utils.HandleSectorAttribute(xSecurity, SectorsManager.DefaultSectorId),
                        Utils.HandleComplexStringXmlAttribute(xSecurity, "country"),
                        Utils.HandleComplexStringXmlAttribute(xSecurity, "currency"),
                        Utils.HandleComplexStringXmlAttribute(xSecurity, "what_inside")
                    );
                    break;
                default:
                    break;
            }

            security.Isin = isin;
            security.Ticker = ticker;
            security.TradeCurrency = currency;

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
