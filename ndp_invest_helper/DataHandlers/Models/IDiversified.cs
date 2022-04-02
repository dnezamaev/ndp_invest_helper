using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public interface IDiversified
    {
        Dictionary<DiversityElement, decimal> Countries { get; }
        Dictionary<DiversityElement, decimal> Currencies { get; }
        Dictionary<DiversityElement, decimal> Sectors { get; }
    }
}