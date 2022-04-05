using ndp_invest_helper.DataHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_invest_helper
{
    public class Deal
    {
        public Security Security;
        public decimal Price;
        public Currency Currency;
        public UInt64 Count;
        public bool UseCash;
        public bool Buy;

        public decimal Total { get => Price * Count; }

        public string FriendlyName 
        { 
            get => string.Format(
            "{0} {1}{2}",
            Security.BestUniqueFriendlyName, Buy ? '+' : '-', Count);
        }
    }
}
