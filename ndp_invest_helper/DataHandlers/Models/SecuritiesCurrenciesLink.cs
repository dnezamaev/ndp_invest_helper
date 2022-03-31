using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class SecuritiesCurrenciesLink
    {
        public long SecurityId { get; set; }
        public string CurrencyCode { get; set; }
        public double Part { get; set; }

        public virtual Currency CurrencyCodeNavigation { get; set; }
        public virtual Security Security { get; set; }
    }
}
