using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

namespace ndp_invest_helper
{
    /// <summary>
    /// Статическая информация о ценной бумаге.
    /// </summary>
    class Security
    {
        public string Isin;
        public string Ticker;
        public Issuer Issuer;

        public string BestName
        {
            get
            {
                if (!String.IsNullOrEmpty(Ticker) && Ticker != "???")
                    return Ticker;
                else if (this is Share || this is Bond)
                    return Issuer.Name;
                else
                    return Isin;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("{0} {1}", Isin, Ticker);

            return sb.ToString();
        }
    }

    /// <summary>
    /// Динамическая информация о ценной бумаге.
    /// </summary>
    class SecurityInfo
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

    class Share : Security 
    {
        public string Currency;

        public override string ToString()
        {
            return base.ToString() + " " + Currency;
        }
    }

    class Bond : Security
    {
        public string Currency;

        public override string ToString()
        {
            return base.ToString() + " " + Currency;
        }
    }

    class ETF : Security
    {
        public Dictionary<Sector, decimal> Sectors = new Dictionary<Sector, decimal>();

        public Dictionary<string, decimal> Countries = new Dictionary<string, decimal>();

        public Dictionary<string, decimal> Currencies = new Dictionary<string, decimal>();

        public Dictionary<string, decimal> WhatInside = new Dictionary<string, decimal>();

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

            sb.Append("} What inside={");
            foreach (var item in WhatInside)
            {
                sb.AppendFormat("{0}:{1}, ", item.Key, item.Value);
            }
            sb.Append("}");

            return sb.ToString();
        }
    }
}
