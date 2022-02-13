using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class FundSecurity
    {
        public long InnerSecurityId { get; set; }
        public long FundSecurityId { get; set; }
        public double Part { get; set; }

        public virtual Security FundSecurityNavigation { get; set; }
        public virtual Security InnerSecurity { get; set; }
    }
}
