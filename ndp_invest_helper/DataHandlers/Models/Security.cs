using System;
using System.Collections.Generic;
using System.Text;

namespace ndp_invest_helper.Models
{
    /// <summary>
    /// Статическая информация о ценной бумаге.
    /// Для списков Sectors, Countries, Currencies следующие правила:
    /// 1) приоритет у бумаги перед эмитентом, 
    /// т.е. при получении списка стран через get свойство сначала проверяется 
    /// список стран бумаги, а если он пуст, то список эмитента.
    /// 2) при модификации нужно использовать свойство SecuritySectors
    /// и ему подобные, т.к. неизвестно какой список вернет Sectors.
    /// </summary>
    public class Security : IEquatable<Security>, IDiversified
    {
        /// <summary>
        /// Уникальный идентификатор бумаги из БД.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Указывает уникальный номер бумаги в международном реестре ценных бумаг.
        /// </summary>
        public string Isin { get; set; }

        /// <summary>
        /// Указывает уникальный тикер бумаги. Может быть пустым.
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        /// Название бумаги, например "Привелегированные акции Сбера".
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Эмитент бумаги.
        /// </summary>
        public Issuer Issuer { get; set; }

        /// <summary>
        /// Тип бумаги.
        /// </summary>
        public virtual AssetTypeEnum SecurityType { get => AssetTypeEnum.Unknown; }

        /// <summary>
        /// Является ли бумага полностью заполненной в базе Securities.xml по всем параметрам.
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                return !(
                    Countries.Count == 0 ||
                    Currencies.Count == 0 ||
                    Sectors.Count == 0 ||
                    (this is ETF && (this as ETF).WhatInside.Count == 0)
                    );
            }
        }

        protected Dictionary<DiversityElement, decimal> sectors =
            new Dictionary<DiversityElement, decimal>(); 

        protected Dictionary<DiversityElement, decimal> countries = 
            new Dictionary<DiversityElement, decimal>();

        protected Dictionary<DiversityElement, decimal> currencies = 
            new Dictionary<DiversityElement, decimal>();

        /// <summary>
        /// Отрасли экономики бумаги или эмитента.
        /// </summary>
        public Dictionary<DiversityElement, decimal> Sectors
        {
            get => sectors.Count != 0 ? sectors : Issuer.Sectors;
        }

        /// <summary>
        /// Страны бумаги или эмитента.
        /// </summary>
        public Dictionary<DiversityElement, decimal> Countries
        {
            get => countries.Count != 0 ? countries : Issuer.Countries;
        }

        /// <summary>
        /// Валюты бумаги или эмитента.
        /// </summary>
        public Dictionary<DiversityElement, decimal> Currencies
        {
            get => currencies.Count != 0 ? currencies : Issuer.Currencies;
        }

        /// <summary>
        /// Отрасли экономики бумаги.
        /// </summary>
        public Dictionary<DiversityElement, decimal> SecuritySectors
        {
            get => sectors;
        }

        /// <summary>
        /// Страны бумаги.
        /// </summary>
        public Dictionary<DiversityElement, decimal> SecurityCountries
        {
            get => countries;
        }

        /// <summary>
        /// Валюты бумаги.
        /// </summary>
        public Dictionary<DiversityElement, decimal> SecurityCurrencies
        {
            get => currencies;
        }

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
                    return Issuer.NameRus;

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

                sb.AppendFormat(" {0}", Issuer.NameRus);

                return sb.ToString();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder(base.ToString());

            sb.Append(" Sectors={");
            foreach (var item in Sectors)
            {
                sb.AppendFormat("{0}:{1}, ", item.Key.NameRus, item.Value);
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

        public override bool Equals(object obj)
        {
            return Id == ((Security)obj).Id;
        }

        public override int GetHashCode()
        {
            return (int)Id;
        }

        public bool Equals(Security other)
        {
            return Id == other.Id;
        }
    }

    /// <summary>
    /// Типы акций - обыкновенная, привилегированная и т.п.
    /// </summary>
    public enum ShareType
    {
        /// <summary>
        /// Не удалось определить.
        /// </summary>
        Unknown,
        /// <summary>
        /// Обыкновенная.
        /// </summary>
        Common,
        /// <summary>
        /// Привилегированная.
        /// </summary>
        Preferred
    }

    /// <summary>
    /// Акция или депозитарная расписка.
    /// </summary>
    public class Share : Security 
    {
        public override AssetTypeEnum SecurityType { get => AssetTypeEnum.Share; }
    }

    /// <summary>
    /// Облигация или еврооблигация.
    /// </summary>
    public class Bond : Security
    {
        public override AssetTypeEnum SecurityType { get => AssetTypeEnum.Bond; }
    }

    /// <summary>
    /// Фонд БПИФ, ETF и т.п.
    /// </summary>
    public class ETF : Security
    {
        /// <summary>
        /// Состав фонда по типам активов: акции, облигации, золото, деньги и т.п.
        /// Список см. в SecuritiesManager.SecTypeFriendlyNames
        /// </summary>
        public Dictionary<AssetType, decimal> WhatInside { get; }
            = new Dictionary<AssetType, decimal>();

        /// <summary>
        /// Состав фонда по ценным бумагам с долями.
        /// </summary>
        public Dictionary<Security, decimal> Securities { get; }
            = new Dictionary<Security, decimal>();

        public override AssetTypeEnum SecurityType { get => AssetTypeEnum.Etf; }

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
    /// Динамическая информация о ценной бумаге в портфеле.
    /// </summary>
    public class SecurityInfo
    {
        /// <summary>
        /// Количество в портфеле, отчете и т.п.
        /// </summary>
        public UInt64 Count { get; set; }

        /// <summary>
        /// Цена в рублях.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Цена в валюте. Курс валюты должен быть заполнен в currencies.xml,
        /// иначе будет исключение, что ключ не найден.
        /// </summary>
        /// <param name="currency">Код валюты по ISO 4217.</param>
        /// <returns>Пересчитанная в валюте цена.</returns>
        public decimal PriceInCurrency(Currency currency)
        {
            return  Price / CurrenciesManager.RatesToRub[currency];
        }

        /// <summary>
        /// Коэффициент коррекции для итоговой суммы. 
        /// Нужен при группировке фондов с разбором содержимого.
        /// </summary>
        public decimal Correction { get => correction; set => correction = value; }

        private decimal correction = 1;

        public decimal Total { get { return Count * Price * Correction; } }

        public SecurityInfo() { }

        public SecurityInfo(SecurityInfo securityInfo) 
        {
            Count = securityInfo.Count;
            Price = securityInfo.Price;
            Correction = securityInfo.Correction;
        }

        public SecurityInfo(SecurityInfo securityInfo, decimal correction) 
        {
            Count = securityInfo.Count;
            Price = securityInfo.Price;
            Correction = correction;
        }
    }

    public class SecurityModel
    {
        public long ID { get; set; }

        public string ISIN { get; set; }

        public string Ticker { get; set; }

        public long SecurityType { get; set; }

        public long? IssuerID { get; set; }

        public string NameRus { get; set; }
    }
}
