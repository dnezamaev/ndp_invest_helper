using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class CountryDevelopmentLevel
    {
        public CountryDevelopmentLevel()
        {
            Countries = new HashSet<Country>();
        }

        public long Id { get; set; }
        public string NameRus { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
