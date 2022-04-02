using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class SecuritiesEconomySectorsLink
    {
        public long SecurityId { get; set; }
        public int SectorId { get; set; }
        public decimal Part { get; set; }
    }
}
