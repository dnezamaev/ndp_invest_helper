﻿using System;
using System.Collections.Generic;

namespace ndp_invest_helper.DataHandlers
{
    public partial class FundsAssetsLink
    {
        public int FundSecurityId { get; set; }
        public int AssetTypeId { get; set; }
        public decimal Part { get; set; }
    }
}
