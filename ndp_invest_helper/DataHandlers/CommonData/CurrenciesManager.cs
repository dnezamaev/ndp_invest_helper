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

        public static Dictionary<int, Currency> ById;

        public static Dictionary<string, Currency> ByCode;

        public static Dictionary<string, decimal> RatesToRub;

        private static void Init()
        {
            Currencies = new List<Currency>();
            ById = new Dictionary<int, Currency>();
            ByCode = new Dictionary<string, Currency>();
            RatesToRub = new Dictionary<string, decimal>();
        }

        public static void LoadFromXmlText(string text)
        {
            Init();

            var xRoot = XElement.Parse(text);

            foreach (var xCurrency in xRoot.Elements("currency"))
            {
                string rateStr = xCurrency.Attribute("rate").Value;
                var currency = new Currency()
                {
                    Id = int.Parse(xCurrency.Attribute("number").Value),
                    Code = xCurrency.Attribute("code").Value,
                    NameEng = xCurrency.Attribute("name_en").Value,
                    NameRus = xCurrency.Attribute("name_ru").Value,
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
                ById[currency.Id] = currency;
                ByCode[currency.Code] = currency;
            }
        }

        public static void LoadFromXmlFile(string filePath)
        {
            LoadFromXmlText(System.IO.File.ReadAllText(filePath));
        }

        public static void LoadFromDatabase()
        {
            Init();

            Currencies = DatabaseManager.GetFullTable<Currency>("Currencies");
            MakeRates();

            ById = Currencies.ToDictionary(
                k => k.Id,
                v => v
                );

            ByCode = Currencies.ToDictionary(
                k => k.Code,
                v => v
                );
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
