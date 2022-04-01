using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string NameEng { get; set; }
        public string NameRus { get; set; }
        public decimal? RateToRub { get; set; }
    }
}
