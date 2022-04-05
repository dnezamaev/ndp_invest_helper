using System.Collections.Generic;

namespace ndp_invest_helper.DataHandlers
{
    public interface IDiversified
    {
        long Id { get; }
        Dictionary<DiversityItem, decimal> Countries { get; }
        Dictionary<DiversityItem, decimal> Currencies { get; }
        Dictionary<DiversityItem, decimal> Sectors { get; }
    }
}