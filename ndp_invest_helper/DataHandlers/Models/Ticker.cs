using System;
using System.Collections.Generic;

namespace ndp_invest_helper.DataHandlers
{
    public partial class Ticker
    {
        public string Ticker1 { get; set; }
        public string Market { get; set; }
        public long SecurityId { get; set; }

        public virtual Security Security { get; set; }
    }
}
