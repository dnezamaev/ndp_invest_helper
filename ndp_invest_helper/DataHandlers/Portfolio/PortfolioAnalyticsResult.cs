using ndp_invest_helper.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_invest_helper
{
    /// <summary>
    /// Результат группировки портфеля.
    /// </summary>
    public class PortfolioAnalyticsResult
    {
        public Dictionary<DiversityElement, PortfolioAnalyticsItem> Analytics =
            new Dictionary<DiversityElement, PortfolioAnalyticsItem>();

        /// <summary>
        /// Критерий сортировки в ToString(): key/part - по ключу/доле
        /// </summary>
        public string OrderBy = "part";

        public string GrouppedBy = null;

        /// <summary>
        /// Порядок сортировки в ToString(): key/part - по ключу/доле
        /// </summary>
        public bool OrderAscending = false;

        public PortfolioAnalyticsResult() { }

        public PortfolioAnalyticsResult(Portfolio portfolio)
        {
            // TODO: check if it works with null
            Analytics[null] = new PortfolioAnalyticsItem()
            {
                Portfolio = new Portfolio(portfolio),
                Part = 1
            };
        }

        public List<Security> GetSecurities()
        {
            var result = new List<Security>();

            foreach (var item in Analytics)
            {
                result.AddRange(item.Value.Portfolio.Securities.Keys);
            }

            return result;
        }

        /// <summary>
        /// Удалить из аналитики несколько бумаг.
        /// </summary>
        /// <param name="correctParts">Нужно ли пересчитывать доли.</param>
        /// <param name="securities">Ключи элементов для удаления.</param>
        public void RemoveSecurities(bool correctParts, bool addCash, Currency currency, params Security[] securities)
        {
            foreach (var item in Analytics)
            {
                foreach (var securityToRemove in securities)
                {
                    item.Value.Portfolio.RemoveSecurity(
                        securityToRemove, UInt64.MaxValue, addCash, currency);
                }
            }

            // Сумма по всем оставшимся элементам.
            var total = Analytics.Sum(x => x.Value.Portfolio.Total);

            // Корректируем доли.
            if (correctParts)
            {
                foreach (var item in Analytics)
                {
                    item.Value.Part = total == 0 ? 0 :
                        item.Value.Portfolio.Total / total;
                }
            }
        }

        /// <summary>
        /// Добавить/докупить бумагу в портфель.
        /// </summary>
        /// <param name="security">Бумага для добавления.</param>
        /// <param name="count">Сколько бумаг добавить.</param>
        /// <param name="price">По какой цене. Если бумага уже есть в портфеле,
        /// то можно указать нулевую цену, тогда будет взята старая цена.
        /// Если цена указана явно, то она будем применена и к старым бумагам.</param>
        /// <param name="removeCash">Вычитать деньги, тем самым имитируя покупку.</param>
        /// <param name="currency">Валюта покупки.</param>
        public void BuySecurity(
            Security security, UInt64 count, decimal price, 
            bool removeCash, Currency currency)
        {
            foreach (var item in Analytics)
            {
                item.Value.Portfolio.AddSecurity(security, count, price, removeCash, currency);
            }

            CalculateParts();
        }

        /// <summary>
        /// Продать бумагу.
        /// </summary>
        /// <param name="security">Бумага для продажи.</param>
        /// <param name="count">Количество бумаг для удаления.
        /// Если указать больше, чем есть в портфеле, или null,
        /// то бумага будет удалена полностью.</param>
        /// <param name="addCash">Добавлять ли наличность от продажи в портфель.</param>
        /// <param name="currency">Валюта покупки.</param>
        public void SellSecurity(Security security, UInt64? count, bool addCash, Currency currency)
        {
            foreach (var item in Analytics)
            {
                // Возможно, здесь стоит удалять пропорционально, а не весь сount???
                // Хотя коэффициент коррекции должен это учитывать, по идее.
                item.Value.Portfolio.RemoveSecurity(security, count, addCash, currency);
            }

            CalculateParts();
        }

        /// <summary>
        /// Удалить из аналитики несколько элементов.
        /// </summary>
        /// <param name="correctParts">Нужно ли пересчитывать доли.</param>
        /// <param name="keys">Ключи элементов для удаления.</param>
        public void RemoveKeys(bool correctParts, params DiversityElement[] keys)
        {
            foreach (var item in keys)
            {
                Analytics.Remove(item);
            }

            // Сумма по всем оставшимся элементам.
            var total = Analytics.Sum(x => x.Value.Portfolio.Total);

            // Корректируем доли.
            if (correctParts)
            {
                foreach (var item in Analytics)
                {
                    item.Value.Part = total == 0 ? 0 :
                        item.Value.Portfolio.Total / total;
                }
            }
        }

        /// <summary>
        /// Ищем бумаги с недозаполненной информацией. 
        /// Они не попали в результат, хотя присутствуют в портфеле.
        /// </summary>
        /// <param name="allSecurities">Список всех проанализированных бумаг.</param>
        /// <returns>Бумаги с недозаполненной информацией по принципу:
        /// если не попала ни в одну группу.  </returns>
        public List<Security> FindNotUsedSecurities(IEnumerable<Security> allSecurities)
        {
            // Полный список бумаг в портфеле.
            // Из него будем вычитать хорошие бумаги (найденные в группах),
            // чтобы найти плохие методом исключения (ненайденные).
            var badSecuritiesInGroup = new List<Security>(allSecurities);
            foreach (var analyticsRecord in Analytics)
            {
                // Убираем найденные в группах - хорошие.
                badSecuritiesInGroup.RemoveAll(
                    x => analyticsRecord.Value.Portfolio.Securities.ContainsKey(x));
            }

            return badSecuritiesInGroup;
        }

        /// <summary>
        /// Общая оценка полученного результата.
        /// </summary>
        public decimal Total { get => Analytics.Sum(x => x.Value.Portfolio.Total); }

        public void CalculateParts()
        {
            var total = Total;

            foreach (var item in Analytics)
            {
                item.Value.Part = item.Value.Portfolio.Total / total;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            IEnumerable<KeyValuePair<DiversityElement, PortfolioAnalyticsItem>> sorted = null;
            
            switch (OrderBy)
            {
                case "key":
                    sorted = OrderAscending ? 
                        Analytics.OrderBy(x => x.Key) :
                        Analytics.OrderByDescending(x => x.Key);
                    break;
                case "part":
                    sorted = OrderAscending ? 
                        Analytics.OrderBy(x => x.Value.Part) :
                        Analytics.OrderByDescending(x => x.Value.Part);
                    break;
                default:
                    throw new ArgumentException("Неизвестный ключ сортировки  " + OrderBy);
            }

            foreach (var item in sorted)
            {
                // Заголовок группы.
                sb.AppendLine(item.Key.FriendlyName);

                // Основное содержимое.
                sb.AppendLine(item.Value.ToString());
            }

            sb.AppendLine(string.Format("Итого по учтенным бумагам: {0:0.00}", Total));
            sb.AppendLine();

            return sb.ToString();
        }
    }

}
