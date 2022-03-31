using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class IssuersEconomySectorsLink
    {
        public long IssuerId { get; set; }
        public string SectorId { get; set; }
        public decimal Part { get; set; }
    }
}
