﻿using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class SecuritiesCurrenciesLink
    {
        public long SecurityId { get; set; }
        public int CurrencyId { get; set; }
        public double Part { get; set; }
    }
}
