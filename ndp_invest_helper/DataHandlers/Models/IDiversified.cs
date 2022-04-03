using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public interface IDiversified
    {
        Dictionary<DiversityItem, decimal> Countries { get; }
        Dictionary<DiversityItem, decimal> Currencies { get; }
        Dictionary<DiversityItem, decimal> Sectors { get; }
    }
}