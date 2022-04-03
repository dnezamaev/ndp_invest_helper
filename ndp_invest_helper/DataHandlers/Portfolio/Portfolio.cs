using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ndp_invest_helper.Models;

namespace ndp_invest_helper
{
    public class Portfolio
    {
        /// <summary>
        /// Все имеющиеся в портфеле бумаги.
        /// </summary>
        public Dictionary<Security, SecurityInfo> Securities { get; }
            = new Dictionary<Security, SecurityInfo>();

        /// <summary>
        /// Деньги в чистом виде: наличность, депозиты, 
        /// остатки на биржевых счетах и т.п.
        /// </summary>
        public Dictionary<Currency, decimal> Cash { get; }
            = new Dictionary<Currency, decimal>();

        /// <summary>
        /// Сумма по всем деньгам в рублях.
        /// </summary>
        public decimal CashTotal
        {
            get { return Cash.Sum(kvp => CommonData.Currencies.RatesToRub[kvp.Key] * kvp.Value); }
        }

        /// <summary>
        /// Общая оценка всех бумаг в рублях.
        /// </summary>
        public decimal SecuritiesTotal { get; private set; }

        /// <summary>
        /// Общая оценка портфеля в рублях.
        /// </summary>
        public decimal Total { get { return SecuritiesTotal + CashTotal; } }

        public Portfolio(
            Dictionary<Security, SecurityInfo> securities = null,
            Dictionary<Currency, decimal> cash = null
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
            : this(porfolio.Securities, porfolio.Cash)
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
        }

        public void MakeDeal(Deal deal)
        {
            if (deal.Buy)
                AddSecurity(deal.Security, deal.Count, deal.Price, 
                    deal.UseCash, deal.Currency);
            else 
                RemoveSecurity(deal.Security, deal.Count, 
                    deal.UseCash, deal.Currency);
        }

        public void AddSecurity(Security security, SecurityInfo securityInfo)
        {
            // TODO: здесь непонятно как быть, если у текущей и добавляемой бумаг
            // отличаются цены и коэффициенты коррекции.
            // Возможно, стоит вычислять среднее взвешенное.
            var thisSecInfo = Securities.GetValueOrSetDefault(security, new SecurityInfo());

            var oldTotal = thisSecInfo.Total;
            thisSecInfo.Count += securityInfo.Count;
            thisSecInfo.Price = securityInfo.Price;
            thisSecInfo.Correction = securityInfo.Correction;
            var newTotal = thisSecInfo.Total;

            SecuritiesTotal += newTotal - oldTotal;
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
            decimal price, bool removeCash, Currency currency)
        {
            SecurityInfo secInfo = null;
            decimal rubPrice = CommonData.Currencies.RatesToRub[currency] * price; // Цена 1 бумаги в рублях.

            // Дополняем инфу о бумаге: корректируем цену, количество.
            if (Securities.TryGetValue(security, out secInfo))
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

            Securities[security] = secInfo;

            var rubTotal = rubPrice * count; // Сумма сделки в рублях.
            var currencyTotal = rubTotal / CommonData.Currencies.RatesToRub[currency]; // Сумма сделки в валюте.

            if (removeCash)
                AddCash(currency, -currencyTotal);

            SecuritiesTotal += rubTotal;
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
        public void RemoveSecurity(Security security, UInt64? count, bool addCash, Currency currency)
        {
            if (!Securities.ContainsKey(security))
                return;

            var secInfo = Securities[security];

            // Если передали null или больше, чем есть, то продаем все.
            if (!count.HasValue || count > secInfo.Count)
                count = secInfo.Count;

            var rubPrice = secInfo.Price; // Цена 1 бумаги в рублях.
            var rubTotal = rubPrice * count.Value; // Сумма сделки в рублях.
            var currencyTotal = rubTotal / CommonData.Currencies.RatesToRub[currency]; // Сумма сделки в валюте.

            SecuritiesTotal -= rubTotal;

            if (addCash)
                AddCash(currency, currencyTotal);

            secInfo.Count -= count.Value;

            if (secInfo.Count == 0)
                Securities.Remove(security);
        }

        /// <summary>
        /// Добавить или убавить деньги в чистом виде.
        /// </summary>
        /// <param name="currency">Валюта.</param>
        /// <param name="amount">Количество, может быть отрицательным.</param>
        public void AddCash(Currency currency, decimal amount)
        {
            var value = Cash.GetValueOrDefault(currency, 0);
            value += amount;
            Cash[currency] = value;
        }

        /// <summary>
        /// Убрать все деньги в указанной валюте.
        /// </summary>
        /// <param name="currency"></param>
        public void RemoveCash(Currency currency)
        {
            Cash.Remove(currency);
        }

        /// <summary>
        /// Добавить бумаги из отчета банка в портфель.
        /// </summary>
        public void AddReport(BrokerReport report)
        {
            foreach (var reportRecord in report.Securities)
            {
                // Иногда в отчетах попадаются пустые позиции. Игнорируем их.
                if (reportRecord.Value.Count == 0)
                    continue;

                // Игнорируем недозаполеннные бумаги.
                if (!reportRecord.Key.IsCompleted)
                    continue;

                var portfolioValue = this.Securities.GetValueOrDefault(
                    reportRecord.Key, new SecurityInfo());

                this.Securities[reportRecord.Key] = portfolioValue;

                portfolioValue.Count += reportRecord.Value.Count;
                // TODO later:
                // Эта цена может перетираться, если в двух отчетах есть одна бумага.
                // Может стоит брать с более позднего отчета.
                // Она не имеет смысла, если брать данные с Мосбиржи.
                portfolioValue.Price = reportRecord.Value.Price;

                this.SecuritiesTotal += portfolioValue.Total;
            }

            foreach (var cashRecord in report.Cash)
            {
                AddCash(cashRecord.Key, cashRecord.Value);
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
            foreach (var cashRecord in Cash)
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
        /// <returns>Группированные по типу активы.</returns>
        public PortfolioAnalyticsResult GroupByType(bool unpackEtf)
        {
            var result = new PortfolioAnalyticsResult();
            var analytics = result.Analytics;
            analytics[AssetType.Cash] = new PortfolioAnalyticsItem
            {
                Portfolio = new Portfolio(null, this.Cash)
            };

            PortfolioAnalyticsItem analyticsItem;

            // Разбираем бумаги по типам.
            foreach (var type in AssetType.Securities)
            {
                // Выбираем бумаги с типом совпадающим текущему type.
                var foundSecurities = Securities.
                    Where(x => x.Key.SecurityType == type.Key).
                    ToDictionary(x => x.Key, x => x.Value);

                // Заполняем результат, считаем долю, сумму.
                analyticsItem = new PortfolioAnalyticsItem()
                {
                    Portfolio = new Portfolio(foundSecurities)
                };

                analytics[type.Value] = analyticsItem;
            }

            // Раскидываем ETF по другим активам, если надо.
            if (unpackEtf)
            {
                var etfAnalytics = analytics[AssetType.Etf];

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
                analytics.Remove(AssetType.Etf);
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
                    var sector = (EconomySector)sectorRecord.Key;

                    if (level < sector.Level)
                        sector = CommonData.Sectors.GetParent(sector);

                    var resultValue = analytics.GetValueOrSetDefault(
                        sector, new PortfolioAnalyticsItem());

                    resultValue.Portfolio.AddSecurity(security, 
                        new SecurityInfo(securityInfo, sectorRecord.Value));
                }
            }

            result.CalculateParts();
            CheckNotUsedSecurities(result);

            return result;
        }
    }
}
