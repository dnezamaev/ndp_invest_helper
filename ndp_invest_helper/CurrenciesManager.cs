using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Currency = ndp_invest_helper.Models.Currency;

namespace ndp_invest_helper
{
    public class CurrenciesManager
    {
        public const string RubCode = "RUB";

        public static List<Currency> Currencies;

        public static Dictionary<string, decimal> RatesToRub;

        private static void Init()
        {
            Currencies = new List<Currency>();
            RatesToRub = new Dictionary<string, decimal>();
        }

        public static void LoadFromXmlFile(string filePath)
        {
            Init();

            var xRoot = XElement.Parse(System.IO.File.ReadAllText(filePath));

            foreach (var xCurrency in xRoot.Elements("currency"))
            {
                string rateStr = xCurrency.Attribute("rate").Value;
                var currency = new Currency()
                {
                    Code = xCurrency.Attribute("code").Value,
                    NameEng = xCurrency.Attribute("name_en").Value,
                };

                // Has rate? It can be null or incorrect format.
                if (!string.IsNullOrEmpty(rateStr)
                    && decimal.TryParse(rateStr, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out decimal rateDec))
                {
                    currency.RateToRub = rateDec;
                    RatesToRub[currency.Code] = rateDec;
                }

                Currencies.Add(currency);
            }
        }

        public static void LoadFromDatabase(DatabaseManager database)
        {
            Init();

            Currencies = database.GetFullTable<Currency>("Currencies");
            MakeRates();
        }

        private static void MakeRates()
        {
            RatesToRub.Clear();

            foreach (var item in Currencies)
            {
                if (item.RateToRub.HasValue)
                    RatesToRub[item.Code] = item.RateToRub.Value;
            }
        }
    }
}
