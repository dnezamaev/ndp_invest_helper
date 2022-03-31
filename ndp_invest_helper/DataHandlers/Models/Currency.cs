using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class Currency
    {
        public string Code { get; set; }
        public string NameEng { get; set; }
        public decimal? RateToRub { get; set; }
    }
}
