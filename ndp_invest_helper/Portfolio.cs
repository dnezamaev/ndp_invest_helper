using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    /// <summary>
    /// Результат группировки портфеля.
    /// </summary>
    public class PortfolioAnalyticsResult
    {
        public Dictionary<string, PortfolioAnalyticsItem> Analytics =
            new Dictionary<string, PortfolioAnalyticsItem>();


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
            Analytics[""] = new PortfolioAnalyticsItem()
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
        public void RemoveSecurities(bool correctParts, bool addCash, string currency, params Security[] securities)
        {
            foreach (var item in Analytics)
            {
                foreach (var securityToRemove in securities)
                {
                    item.Value.Portfolio.RemoveSecurity(securityToRemove, UInt64.MaxValue, addCash, currency);
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
        public void BuySecurity(Security security, UInt64 count, decimal price, 
            bool removeCash, string currency)
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
        public void SellSecurity(Security security, UInt64? count, bool addCash, string currency)
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
        public void RemoveKeys(bool correctParts, params string[] keys)
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
            IEnumerable<KeyValuePair<string, PortfolioAnalyticsItem>> sorted = null;
            
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
                switch (GrouppedBy)
                {
                    case "country":
                        sb.AppendLine(string.Format("{0} - {1}", 
                            item.Key, CountriesManager.Countries[item.Key]));
                        break;

                    case "currency":
                        sb.AppendLine(item.Key);
                        break;

                    case "type":
                        sb.AppendLine(item.Key);
                        break;

                    case "sector":
                        sb.AppendLine(string.Format("{0} - {1}", 
                            item.Key, SectorsManager.ById[item.Key].Name));
                        break;

                    default:
                        throw new ArgumentException(string.Format(
                            "Неизвестный критерий группировки {0}", GrouppedBy));
                }

                // Основное содержимое.
                sb.AppendLine(item.Value.ToString());
            }

            sb.AppendLine(string.Format("Итого по учтенным бумагам: {0:0.00}", Total));
            sb.AppendLine();

            return sb.ToString();
        }
    }

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

    public class Portfolio
    {
        private Dictionary<Security, SecurityInfo> securities =
            new Dictionary<Security, SecurityInfo>();

        /// <summary>
        /// Все имеющиеся в портфеле бумаги.
        /// </summary>
        public Dictionary<Security, SecurityInfo> Securities { get => securities; }

        private Dictionary<string, decimal> cash = 
            new Dictionary<string, decimal>();

        /// <summary>
        /// Деньги в чистом виде: наличность, депозиты, 
        /// остатки на биржевых счетах и т.п.
        /// </summary>
        public Dictionary<string, decimal> Cash { get => cash; }

        /// <summary>
        /// Сумма по всем деньгам в рублях.
        /// </summary>
        public decimal CashTotal
        {
            get { return cash.Sum(kvp => CurrenciesManager.CurrencyRates[kvp.Key] * kvp.Value); }
        }

        /// <summary>
        /// Общая оценка всех бумаг в рублях.
        /// </summary>
        private decimal securitiesTotal;

        /// <summary>
        /// Общая оценка портфеля в рублях.
        /// </summary>
        public decimal Total { get { return securitiesTotal + CashTotal; } }

        public Portfolio(
            Dictionary<Security, SecurityInfo> securities = null,
            Dictionary<string, decimal> cash = null
            )
        {
            // Копируем значения, чтобы избежать порчи
            // исходного портфеля по ссылкам SecurityInfo.
            if (securities != null)
                foreach (var secKvp in securities)
                    AddSecurity(secKvp.Key, secKvp.Value);

            if (cash != null)
                foreach (var cashKvp in cash)
                    AddCash(cashKvp.Key, cashKvp.Value);
        }

        public Portfolio(Portfolio porfolio)
            : this(porfolio.Securities, porfolio.cash)
        { }
        
        public Portfolio(PortfolioAnalyticsResult analytics) : this(null, null)
        {
            foreach (var kvp in analytics.Analytics)
            {
                foreach (var secKvp in kvp.Value.Portfolio.Securities)
                    AddSecurity(secKvp.Key, secKvp.Value);

                foreach (var cashKvp in kvp.Value.Portfolio.Cash)
                    AddCash(cashKvp.Key, cashKvp.Value);
            }
        }

        public Portfolio()
        {
            cash = new Dictionary<string, decimal>(CurrenciesManager.Cash);
        }

        public void AddSecurity(Security security, SecurityInfo securityInfo)
        {
            // TODO: здесь непонятно как быть, если у текущей и добавляемой бумаг
            // отличаются цены и коэффициенты коррекции.
            // Возможно, стоит вычислять среднее взвешенное.
            var thisSecInfo = securities.GetValueOrSetDefault(security, new SecurityInfo());

            var oldTotal = thisSecInfo.Total;
            thisSecInfo.Count += securityInfo.Count;
            thisSecInfo.Price = securityInfo.Price;
            thisSecInfo.Correction = securityInfo.Correction;
            var newTotal = thisSecInfo.Total;

            securitiesTotal += newTotal - oldTotal;
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
        public void AddSecurity(Security security, UInt64 count, 
            decimal price, bool removeCash, string currency)
        {
            SecurityInfo secInfo = null;
            decimal rubPrice = CurrenciesManager.CurrencyRates[currency] * price; // Цена 1 бумаги в рублях.

            // Дополняем инфу о бумаге: корректируем цену, количество.
            if (securities.TryGetValue(security, out secInfo))
            {
                // Бумага уже есть в портфеле.
                secInfo.Count += count;

                // Указана цена, значит обновляем её в сведениях о бумаге.
                // Это повляет и на старые бумаги, произойдет переоценка.
                if (rubPrice != 0)
                    secInfo.Price = rubPrice;
                else
                    rubPrice = secInfo.Price; // Цена не указана, просто берем старую.
            }
            else // Бумаги нет в портфеле.
            {
                if (price == 0)
                    throw new ArgumentException(
                        "Не указана цена покупки {0}, при этом бумаги нет в портфеле.",
                        security.BestFriendlyName);

                secInfo = new SecurityInfo()
                {
                    Price = rubPrice,
                    Count = count
                };
            }

            securities[security] = secInfo;

            var rubTotal = rubPrice * count; // Сумма сделки в рублях.
            var currencyTotal = rubTotal / CurrenciesManager.CurrencyRates[currency]; // Сумма сделки в валюте.

            if (removeCash)
                AddCash(currency, -currencyTotal);

            securitiesTotal += rubTotal;
        }

        /// <summary>
        /// Удалить частично или полностью бумагу. Откорректировать оценку портфеля.
        /// </summary>
        /// <param name="security">Бумага для удаления.</param>
        /// <param name="count">Количество бумаг для удаления.
        /// Если указать больше, чем есть в портфеле, или null,
        /// то бумага будет удалена полностью.</param>
        /// <param name="addCash">Добавлять наличность от продажи, имитируя продажу.</param>
        /// <param name="currency">Валюта покупки.</param>
        public void RemoveSecurity(Security security, UInt64? count, bool addCash, string currency)
        {
            if (!securities.ContainsKey(security))
                return;

            var secInfo = securities[security];

            // Если передали null или больше, чем есть, то продаем все.
            if (!count.HasValue || count > secInfo.Count)
                count = secInfo.Count;

            var rubPrice = secInfo.Price; // Цена 1 бумаги в рублях.
            var rubTotal = rubPrice * count.Value; // Сумма сделки в рублях.
            var currencyTotal = rubTotal / CurrenciesManager.CurrencyRates[currency]; // Сумма сделки в валюте.

            securitiesTotal -= rubTotal;

            if (addCash)
                AddCash(currency, currencyTotal);

            secInfo.Count -= count.Value;

            if (secInfo.Count == 0)
                securities.Remove(security);
        }

        /// <summary>
        /// Добавить или убавить деньги в чистом виде.
        /// </summary>
        /// <param name="currency">Валюта.</param>
        /// <param name="amount">Количество, может быть отрицательным.</param>
        public void AddCash(string currency, decimal amount)
        {
            var value = cash.GetValueOrDefault(currency, 0);
            value += amount;
            cash[currency] = value;
        }

        /// <summary>
        /// Убрать все деньги в указанной валюте.
        /// </summary>
        /// <param name="currency"></param>
        public void RemoveCash(string currency)
        {
            cash.Remove(currency);
        }

        /// <summary>
        /// Добавить бумаги из отчета банка в портфель.
        /// </summary>
        public void AddReport(Report report)
        {
            foreach (var reportRecord in report.Securities)
            {
                var portfolioValue = this.Securities.GetValueOrDefault(
                    reportRecord.Key, new SecurityInfo());

                this.securities[reportRecord.Key] = portfolioValue;

                portfolioValue.Count += reportRecord.Value.Count;
                // TODO later:
                // Эта цена может перетираться, если в двух отчетах есть одна бумага.
                // Может стоит брать с более позднего отчета.
                // Она не имеет смысла, если брать данные с Мосбиржи.
                portfolioValue.Price = reportRecord.Value.Price;

                this.securitiesTotal += portfolioValue.Total;
            }
        }

        private void CheckNotUsedSecurities(PortfolioAnalyticsResult result)
        {
            var notUsed = result.FindNotUsedSecurities(Securities.Keys);

            if (notUsed.Count == 0)
                return;

            var sb = new StringBuilder("Обнаружены бумаги с недозаполеннной информацией: ");
            var isins = notUsed.Select(x => x.Isin);
            sb.AppendJoin(", ", isins);

            throw new ArgumentException(sb.ToString());
        }

        public PortfolioAnalyticsResult GroupByCurrency()
        {
            var result = new PortfolioAnalyticsResult();
            var analytics = result.Analytics;

            // Сначала считаем общую сумму по каждой валюте.
            foreach (var portfolioSecurity in Securities)
            {
                var security = portfolioSecurity.Key;
                var securityInfo = portfolioSecurity.Value;

                foreach (var currencyRecord in security.Currencies)
                {
                    // Ищем существующую запись по этой валюте.
                    // Если ее нет, создаем пустую.
                    var resultValue = analytics.GetValueOrSetDefault(
                        currencyRecord.Key, new PortfolioAnalyticsItem());

                    // Добавляем бумагу с коэффициентом коррекции.
                    // Создаем копию securitryInfo, чтобы не портить запись в исходном портфеле.
                    // TODO: проверить учитываются ли доли?
                    resultValue.Portfolio.AddSecurity(security, 
                        new SecurityInfo(securityInfo, currencyRecord.Value));
                }
            }

            // Учитываем наличку.
            foreach (var cashRecord in cash)
            {
                var resultValue = analytics.GetValueOrSetDefault(
                    cashRecord.Key, new PortfolioAnalyticsItem());

                resultValue.Portfolio.AddCash(cashRecord.Key, cashRecord.Value);
            }

            // Расчитываем доли.
            result.CalculateParts();

            // Проверяем, что не упустили бумаги с недозаполненной инофрмацией.
            CheckNotUsedSecurities(result);

            return result;
        }

        public PortfolioAnalyticsResult GroupByCountry()
        {
            // Логика та же, что у группировки по валютам, см. комменты там.
            var result = new PortfolioAnalyticsResult();
            var analytics = result.Analytics;

            foreach (var portfolioSecurity in Securities)
            {
                var security = portfolioSecurity.Key;
                var securityInfo = portfolioSecurity.Value;

                foreach (var countryRecord in security.Countries)
                {
                    var resultValue = analytics.GetValueOrSetDefault(
                        countryRecord.Key, new PortfolioAnalyticsItem());

                    resultValue.Portfolio.AddSecurity(security, 
                        new SecurityInfo(securityInfo, countryRecord.Value));
                }
            }

            result.CalculateParts();
            CheckNotUsedSecurities(result);

            return result;
        }

        /// <summary>
        /// Группировка по типу актива:
        /// акция, облигация, фонд, деньги.
        /// </summary>
        /// <param name="unpackEtf">True - раскидывать ETF по составляющим.</param>
        /// <returns>Группированные по типу активы. Ключ - тип.
        /// Может быть любая строка, но есть зарезеврированные: 
        /// share, bond, etf, cash. Остальные будут браться из аттрибута
        /// what_inside у ETF в файле Securities.xml, если unpackEtf == true.</returns>
        public PortfolioAnalyticsResult GroupByType(bool unpackEtf)
        {
            var result = new PortfolioAnalyticsResult();
            var analytics = result.Analytics;
            analytics["cash"] = new PortfolioAnalyticsItem
            {
                Portfolio = new Portfolio(null, this.cash)
            };

            PortfolioAnalyticsItem analyticsItem;

            // Разбираем бумаги по типам.
            foreach (var type in SecuritiesManager.TypesDictionary.Keys)
            {
                // Выбираем бумаги с типом совпадающим текущему type.
                var foundSecurities = Securities.
                    Where(x => x.Key.GetType() == type).
                    ToDictionary(x => x.Key, x => x.Value);

                // Заполняем результат, считаем долю, сумму.
                analyticsItem = new PortfolioAnalyticsItem()
                {
                    Portfolio = new Portfolio(foundSecurities)
                };

                // Преобразуем тип в строку из словаря и используем её как ключ.
                var key = SecuritiesManager.TypesDictionary[type];
                analytics[key] = analyticsItem;
            }

            // Раскидываем ETF по другим активам, если надо.
            if (unpackEtf)
            {
                var etfAnalytics = analytics[SecuritiesManager.TypesDictionary[typeof(ETF)]];

                // Проходимся по всем фондам.
                foreach (var securityItem in etfAnalytics.Portfolio.Securities)
                {
                    var etf = securityItem.Key as ETF;

                    // Проходимся по всем внутренностям фонда.
                    foreach (var whatInside in etf.WhatInside)
                    {
                        // Элемент результата (словаря по типам), в который идет фонд.
                        // Если такого нет, то создаем.
                        var analyticsItemDestination =
                            analytics.GetValueOrSetDefault(
                                whatInside.Key,
                                new PortfolioAnalyticsItem());

                        // Учитываем поправку в итоговой цене.
                        var correctedSecInfo = new SecurityInfo(
                            securityItem.Value, whatInside.Value);

                        // Добавляем в список бумаг.
                        analyticsItemDestination.Portfolio.AddSecurity(
                            securityItem.Key, correctedSecInfo);
                    }
                }

                // Теперь фонды в чистом виде нам не нужны.
                analytics.Remove(SecuritiesManager.TypesDictionary[typeof(ETF)]);
            }

            result.CalculateParts();
            CheckNotUsedSecurities(result);

            return result;
        }

        public PortfolioAnalyticsResult GroupBySector(int level = 1)
        {
            var result = new PortfolioAnalyticsResult();
            var analytics = result.Analytics;

            foreach (var portfolioSecurity in Securities)
            {
                var security = portfolioSecurity.Key;
                var securityInfo = portfolioSecurity.Value;

                foreach (var sectorRecord in security.Sectors)
                {
                    var sector = sectorRecord.Key;

                    if (level < sector.Level)
                        sector = SectorsManager.GetParent(sector);

                    var resultValue = analytics.GetValueOrSetDefault(
                        sector.Id, new PortfolioAnalyticsItem());

                    resultValue.Portfolio.AddSecurity(security, 
                        new SecurityInfo(securityInfo, sectorRecord.Value));
                }
            }

            result.CalculateParts();
            CheckNotUsedSecurities(result);

            return result;
        }

        private decimal CalculateSecuritiesTotal()
        {
            return securitiesTotal = securities.Sum(x => x.Value.Total);
        }
    }
}
