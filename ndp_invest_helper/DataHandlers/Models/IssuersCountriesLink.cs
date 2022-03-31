using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class IssuersCountriesLink
    {
        public long IssuerId { get; set; }
        public string CountryCode { get; set; }
        public decimal Part { get; set; }
    }
}
