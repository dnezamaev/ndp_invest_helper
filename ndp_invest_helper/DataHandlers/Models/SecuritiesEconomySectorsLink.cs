using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class SecuritiesEconomySectorsLink
    {
        public long SecurityId { get; set; }
        public long SectorId { get; set; }
        public decimal Part { get; set; }

        public virtual EconomySector Sector { get; set; }
        public virtual Security Security { get; set; }
    }
}
