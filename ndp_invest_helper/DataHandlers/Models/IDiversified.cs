using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public interface IDiversified
    {
        Dictionary<Country, decimal> Countries { get; }
        Dictionary<Currency, decimal> Currencies { get; }
        Dictionary<Sector, decimal> Sectors { get; }
    }
}