using System;
using System.Collections.Generic;

namespace ndp_invest_helper.DataHandlers
{
    public partial class IssuersCountriesLink
    {
        public long IssuerId { get; set; }
        public int CountryId { get; set; }
        public decimal Part { get; set; }
    }
}
