using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_invest_helper
{
    /// <summary>
    /// Результат группировки портфеля по выбранному критерию.
    /// </summary>
    public class PortfolioAnalyticsItem
    {
        public Portfolio Portfolio = new Portfolio();

        public decimal Part;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(string.Format("Доля = {0:0.00}%", Part * 100));
            sb.AppendLine(string.Format("Сумма = {0:0.00}", Portfolio.Total));
            sb.AppendJoin("; ", Portfolio.Securities.Select(
                x => string.Format("{0} ({1:0.00})", x.Key.BestFriendlyName, x.Value.Total)));

            if (Portfolio.CashTotal != 0)
                sb.AppendFormat("; наличные {0:0.00}", Portfolio.CashTotal);

            sb.AppendLine();

            return sb.ToString();
        }
    }
}
