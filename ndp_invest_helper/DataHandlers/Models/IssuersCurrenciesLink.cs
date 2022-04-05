using System;
using System.Collections.Generic;

namespace ndp_invest_helper.DataHandlers
{
    public partial class IssuersCurrenciesLink
    {
        public long IssuerId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Part { get; set; }
    }
}
