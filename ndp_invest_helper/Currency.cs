using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ndp_invest_helper
{
    class CurrenciesManager
    {
        public static Dictionary<string, decimal> Rates;

        public static void SetRates(Settings settings)
        {
            Rates = settings.Rates.ToDictionary(
                x => x.Key, x => x.Value);
        }
    }
}
