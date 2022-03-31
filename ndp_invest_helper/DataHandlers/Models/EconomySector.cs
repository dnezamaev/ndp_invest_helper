using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class EconomySector
    {
        public EconomySector()
        {
            InverseParent = new HashSet<EconomySector>();
            SecuritiesEconomySectorsLinks = new HashSet<SecuritiesEconomySectorsLink>();
        }

        public long Id { get; set; }
        public long Level { get; set; }
        public string NameRus { get; set; }
        public long? ParentId { get; set; }

        public virtual EconomySector Parent { get; set; }
        public virtual ICollection<EconomySector> InverseParent { get; set; }
        public virtual ICollection<SecuritiesEconomySectorsLink> SecuritiesEconomySectorsLinks { get; set; }
    }
}
