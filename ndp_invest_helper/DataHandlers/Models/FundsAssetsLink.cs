using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class FundsAssetsLink
    {
        public long FundSecurityId { get; set; }
        public long AssetTypeId { get; set; }
        public decimal Part { get; set; }
    }
}
