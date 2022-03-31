using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class SecuritiesCountriesLink
    {
        public long SecurityId { get; set; }
        public string CountryCode { get; set; }
        public double Part { get; set; }

        public virtual Country CountryCodeNavigation { get; set; }
        public virtual Security Security { get; set; }
    }
}
