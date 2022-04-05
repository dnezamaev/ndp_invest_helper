using ndp_invest_helper.DataHandlers;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Currency = ndp_invest_helper.DataHandlers.Currency;

namespace ndp_invest_helper
{
    public class CurrenciesManager : DiversityManager
    {
        public const string RubCode = "RUB";

        /// <summary>
        /// All currencies that have not null rate.
        /// </summary>
        public Dictionary<Currency, decimal> RatesToRub { get; private set; }
            = new Dictionary<Currency, decimal>();

        public decimal GetRate(string code)
        {
            return RatesToRub[(Currency)ByCode[code]];
        }

        public decimal GetRate(int id)
        {
            return RatesToRub[(Currency)ById[id]];
        }

        public override void LoadFromXmlText(string text)
        {
            Items.Clear();

            var xRoot = XElement.Parse(text);

            foreach (var xCurrency in xRoot.Elements("currency"))
            {
                string rateStr = xCurrency.Attribute("rate").Value;

                var currency = new Currency()
                {
                    Id = int.Parse(xCurrency.Attribute("number").Value),
                    Code = xCurrency.Attribute("code").Value
                };

                var xNameEng = xCurrency.Attribute("name_en");
                currency.NameEng = xNameEng == null ? "" : xNameEng.Value;

                var xNameRus = xCurrency.Attribute("name_ru");
                currency.NameRus = xNameRus == null ? "" : xNameRus.Value;

                // Has rate? It can be null or incorrect format.
                if (!string.IsNullOrEmpty(rateStr)
                    && decimal.TryParse(rateStr, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out decimal rateDec))
                {
                    currency.RateToRub = rateDec;
                    RatesToRub[currency] = rateDec;
                }

                Items.Add(currency);
            }

            FillDictionaries();
        }

        public override void LoadFromXmlFile(string filePath)
        {
            LoadFromXmlText(System.IO.File.ReadAllText(filePath));
        }

        public override void LoadFromDatabase()
        {
            Items = DatabaseManager.GetFullTable<Currency>("Currencies").Cast<DiversityItem>().ToList();

            FillDictionaries();

            MakeRates();
        }

        private void MakeRates()
        {
            RatesToRub.Clear();

            foreach (Currency item in Items)
            {
                if (item.RateToRub.HasValue)
                    RatesToRub[item] = item.RateToRub.Value;
            }
        }

        public override void HandleXml(XElement xTag, Dictionary<DiversityItem, decimal> destination)
        {
            InnerHandleXml(xTag, "currency", destination);
        }
    }
}
