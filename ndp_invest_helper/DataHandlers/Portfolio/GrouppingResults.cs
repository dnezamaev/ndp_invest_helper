using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_invest_helper
{
    public class GrouppingResults
    {
        private PortfolioAnalyticsResult
            byCountry, byCurrency, bySector, byType;

        public Portfolio Portfolio { get; set; }

        public GrouppingResults(Portfolio portfolio, bool unpackEtf = true)
        {
            Portfolio = portfolio;
            ByCountry = portfolio.GroupByCountry();
            ByCurrency = portfolio.GroupByCurrency();
            BySector = portfolio.GroupBySector();
            ByType = portfolio.GroupByType(unpackEtf);
        }

        public PortfolioAnalyticsResult[] All
        {
            get
            {
                return new PortfolioAnalyticsResult[] { byCountry, byCurrency, bySector, byType };
            }
        }

        public PortfolioAnalyticsResult ByCountry
        {
            get => byCountry;
            set { byCountry = value; value.GrouppedBy = "country"; }
        }


        public PortfolioAnalyticsResult ByCurrency
        {
            get => byCurrency;
            set { byCurrency = value; value.GrouppedBy = "currency"; }
        }

        public PortfolioAnalyticsResult BySector
        {
            get => bySector;
            set { bySector = value; value.GrouppedBy = "sector"; }
        }

        public PortfolioAnalyticsResult ByType
        {
            get => byType;
            set { byType = value; value.GrouppedBy = "type"; }
        }
    }
}
