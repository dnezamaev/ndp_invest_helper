using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class SecuritiesCountriesLink
    {
        public long SecurityId { get; set; }
        public int CountryId { get; set; }
        public double Part { get; set; }
    }
}
