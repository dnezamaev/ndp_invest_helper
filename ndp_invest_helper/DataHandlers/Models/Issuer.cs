﻿using System.Text;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    /// <summary>
    /// Организационный тип эмитента: ПАО, ЗАО и т.п.
    /// </summary>
    public enum IssuerType
    {
        /// <summary>
        /// Не удалось определить.
        /// </summary>
        Unknown,
        /// <summary>
        /// ПАО.
        /// </summary>
        Public
    }

    /// <summary>
    /// Эмитент ценной бумаги.
    /// </summary>
    public class Issuer
    {
        /// <summary>
        /// Уникальный идентификатор эмитента из БД.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Уникальное наименование эмитента.
        /// </summary>
        public string NameRus { get; set; }

        public Dictionary<Sector, decimal> Sectors = new Dictionary<Sector, decimal>();

        public Dictionary<string, decimal> Countries = new Dictionary<string, decimal>();

        public Dictionary<string, decimal> Currencies = new Dictionary<string, decimal>();

        public List<Security> Securities = new List<Security>();

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(NameRus);

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
}