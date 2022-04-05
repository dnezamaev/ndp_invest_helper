using ndp_invest_helper.DataHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_invest_helper
{
    public class AnalyticsManager
    {
        public List<GrouppingResults> GrouppingResults { get; private set; }

        public bool DisableEvents { get; set; } = false;

        public List<Deal> Deals { get; private set; }

        public event Action AllDealsRemoved;

        public event Action LastDealRemoved;

        public event Action<Deal> DealCompleted;

        public Portfolio FirstPortfolio {  get => GrouppingResults[0].Portfolio; }

        public Portfolio CurrentPortfolio { get => GrouppingResults.Last().Portfolio; }

        public GrouppingResults FirstResult { get => GrouppingResults[0]; }

        public GrouppingResults CurrentResult { get => GrouppingResults.Last(); }

        public event Action AnalyticsResultChanged;

        public AnalyticsManager()
        {
            GrouppingResults = new List<GrouppingResults>();
            Deals = new List<Deal>();
        }

        /// <summary>
        /// Make new portfolio and analyze it.
        /// </summary>
        public void DoAnalytics(BrokerReportsManager brokerReports)
        {
            var portfolio = new Portfolio();

            foreach (var report in brokerReports.Reports)
            {
                portfolio.AddReport(report);
            }

            foreach (var item in Settings.Cash)
            {
                portfolio.AddCash((Currency)CommonData.Currencies.ByCode[item.Key], item.Value);
            }

            GrouppingResults.Clear();
            GrouppingResults.Add(new GrouppingResults(portfolio));

            RaiseAnalyticsResultChanged();
        }

        /// <summary>
        /// Clean current analytics and deals results.
        /// Make new portfolio and repeat deals if necessary.
        /// </summary>
        /// <param name="repeatDeals">True - repeat deals. False - drop deals.</param>
        public void RedoAnalytics(
            BrokerReportsManager brokerReports, 
            bool repeatDeals = false)
        {
            if (repeatDeals)
            {
                DoAnalytics(brokerReports);
                RemakeDeals();
            }
            else
            {
                RemoveAllDeals();
                DoAnalytics(brokerReports);
            }
        }

        /// <summary>
        /// Clean all deals results and recalculate them.
        /// </summary>
        public void RemakeDeals()
        {
            var savedDeals = new List<Deal>(Deals);
            RemoveAllDeals();

            foreach (var item in savedDeals)
            {
                MakeDeal(item);
            }
        }

        public void MakeDeal(Deal deal)
        {
            var newPortfolio = new Portfolio(CurrentPortfolio);
            newPortfolio.MakeDeal(deal);

            GrouppingResults.Add(new GrouppingResults(newPortfolio));

            Deals.Add(deal);

            var logText = string.Format
                (
                "{0} {1} {2} шт. по цене {3:n2} {4} на сумму {5:n2} {4}.",
                deal.Buy ? "Покупка" : "Продажа",
                deal.Security.BestUniqueFriendlyName, deal.Count, deal.Price, 
                deal.Currency, deal.Total
                );

            CommonData.LogAddText(logText);

            RaiseDealCompleted(deal);
            RaiseAnalyticsResultChanged();
        }

        public void RemoveLastDeal()
        {
            if (Deals.Count == 0)
                return;

            GrouppingResults.Remove(CurrentResult);
            Deals.RemoveAt(Deals.Count - 1);

            RaiseAnalyticsResultChanged();
            RaiseLastDealRemoved();
        }

        public void RemoveAllDeals()
        {
            if (Deals.Count == 0)
                return;

            GrouppingResults.RemoveRange(1, GrouppingResults.Count - 1);
            Deals.Clear();

            RaiseAnalyticsResultChanged();
            RaiseAllDealsRemoved();
        }

        private void RaiseAnalyticsResultChanged()
        {
            if (AnalyticsResultChanged != null && !DisableEvents)
            {
                AnalyticsResultChanged();
            }
        }

        private void RaiseAllDealsRemoved()
        {
            if (AllDealsRemoved != null && !DisableEvents)
            {
                AllDealsRemoved();
            }
        }
        
        private void RaiseLastDealRemoved()
        {
            if (LastDealRemoved != null && !DisableEvents)
            {
                LastDealRemoved();
            }
        }

        private void RaiseDealCompleted(Deal deal)
        {
            if (DealCompleted != null && !DisableEvents)
            {
                DealCompleted(deal);
            }
        }
    }
}
