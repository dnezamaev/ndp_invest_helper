using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    /// <summary>
    /// ISO 3166
    /// </summary>
    public partial class Country
    {
        public Country()
        {
            SecuritiesCountriesLinks = new HashSet<SecuritiesCountriesLink>();
        }

        /// <summary>
        /// Numeric country code.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Alpha-2 country code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Alpha-3 country code.
        /// </summary>
        public string Code3 { get; set; }

        /// <summary>
        /// Short name in russian.
        /// </summary>
        public string NameRus { get; set; }

        /// <summary>
        /// Full name in russian.
        /// </summary>
        public string NameRusFull { get; set; }

        /// <summary>
        /// Short name in english.
        /// </summary>
        public string NameEng { get; set; }

        public long? RegionId { get; set; }
        public long? DevelopmentLevel { get; set; }

        public virtual CountryDevelopmentLevel DevelopmentLevelNavigation { get; set; }
        public virtual CountryRegion Region { get; set; }
        public virtual ICollection<SecuritiesCountriesLink> SecuritiesCountriesLinks { get; set; }
    }
}
