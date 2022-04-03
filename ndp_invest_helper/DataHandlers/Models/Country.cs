using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    /// <summary>
    /// ISO 3166. Id is numeric code. Code is alpha-2 country code.
    /// </summary>
    public partial class Country : DiversityItem
    {
        /// <summary>
        /// Alpha-3 country code.
        /// </summary>
        public string Code3 { get; set; }

        /// <summary>
        /// Full name in russian.
        /// </summary>
        public string NameRusFull { get; set; }

        public int? RegionId { get; set; }

        public int? DevelopmentLevel { get; set; }

    }
}
