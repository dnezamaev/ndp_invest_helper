using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class Country
    {
        public Country()
        {
            SecuritiesCountriesLinks = new HashSet<SecuritiesCountriesLink>();
        }

        public string Code { get; set; }
        public string NameRus { get; set; }
        public long? RegionId { get; set; }
        public long? DevelopmentLevel { get; set; }

        public virtual CountryDevelopmentLevel DevelopmentLevelNavigation { get; set; }
        public virtual CountryRegion Region { get; set; }
        public virtual ICollection<SecuritiesCountriesLink> SecuritiesCountriesLinks { get; set; }
    }
}
