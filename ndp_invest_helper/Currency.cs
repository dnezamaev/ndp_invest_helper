using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ndp_invest_helper
{
    public class CurrenciesManager
    {
        public static Dictionary<string, decimal> CurrencyRates;

        public static Dictionary<string, decimal> Cash = new Dictionary<string, decimal>();

        public static void SetRates(Settings settings)
        {
            CurrencyRates = settings.CurrencyRates.ToDictionary(
                x => x.Key, x => x.Value);
        }
    }
}
