using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ndp_invest_helper
{
    /// <summary>
    /// Результат группировки портфеля.
    /// </summary>
    class PortfolioAnalyticsResult
    {
        public Dictionary<string, PortfolioAnalyticsItem> Analytics =
            new Dictionary<string, PortfolioAnalyticsItem>();

        public string OrderBy = "key";

        public bool OrderAscending = true;

        /// <summary>
        /// Флажочек, сигнализирующий о проблеме в файле с бумагами.
        /// </summary>
        public static bool FoundNotUsedSecurities = false;

        /// <summary>
        /// Бумаги с недозаполненной информацией по принципу:
        /// если не попала ни в одну группу.
        /// </summary>
        public List<Security> NotUsedSecurities = new List<Security>();

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
        public void RemoveSecurities(bool correctParts, params Security[] securities)
        {
            foreach (var item in Analytics)
            {
                foreach (var securityToRemove in securities)
                {
                    item.Value.Portfolio.RemoveSecurity(securityToRemove);
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
        /// </summary>
        /// <param name="allSecurities">Список всех проанализированных бумаг.</param>
        public void FindNotUsedSecurities(IEnumerable<Security> allSecurities)
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
            // Добавляем плохие в итоговое множество.
            NotUsedSecurities = badSecuritiesInGroup;

            if (NotUsedSecurities.Count > 0)
                FoundNotUsedSecurities = true;
        }

        public void CalculateParts()
        {
            var total = Analytics.Sum(x => x.Value.Portfolio.Total);

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
                sb.AppendLine(item.Key.ToString());
                sb.AppendLine(item.Value.ToString());
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// Результат группировки портфеля по выбранному критерию.
    /// </summary>
    class PortfolioAnalyticsItem
    {
        public Portfolio Portfolio = new Portfolio();

        public decimal Part;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Part = {0:0.00}%\n", Part * 100);
            sb.AppendFormat("Total = {0:0.00}\n", Portfolio.Total);
            sb.AppendJoin("; ", Portfolio.Securities.Select(x => x.Key.BestName));

            if (Portfolio.CashTotal != 0)
                sb.AppendFormat("; наличные {0}", Portfolio.CashTotal);

            sb.AppendLine();

            return sb.ToString();
        }
    }

    class Portfolio
    {
        /// <summary>
        /// Все имеющиеся в портфеле бумага. Собираются из отчетов.
        /// </summary>
        private Dictionary<Security, SecurityInfo> securities =
            new Dictionary<Security, SecurityInfo>();

        public Dictionary<Security, SecurityInfo> Securities { get => securities; }

        /// <summary>
        /// Деньги в чистом виде: наличность, депозиты, 
        /// остатки на биржевых счетах и т.п.
        /// </summary>
        private Dictionary<string, decimal> cash = 
            new Dictionary<string, decimal>();

        public Dictionary<string, decimal> Cash { get => cash; }

        /// <summary>
        /// Сумма по всем деньгам в рублях.
        /// </summary>
        private decimal cashTotal;

        /// <summary>
        /// Сумма по всем деньгам в рублях.
        /// </summary>
        public decimal CashTotal { get => cashTotal; }

        /// <summary>
        /// Общая оценка портфеля в рублях.
        /// </summary>
        private decimal total;

        /// <summary>
        /// Общая оценка портфеля в рублях.
        /// </summary>
        public decimal Total { get => total; }

        public Portfolio(
            Dictionary<Security, SecurityInfo> securities = null,
            Dictionary<string, decimal> cash = null
            )
        {
            if (securities != null)
                this.securities = new Dictionary<Security, SecurityInfo>(securities);
            if (cash != null)
                this.cash = new Dictionary<string, decimal>(cash);

            CalculateTotals();
        }

        public Portfolio(Portfolio porfolio)
            : this(porfolio.Securities, porfolio.cash)
        { }
        
        public Portfolio(PortfolioAnalyticsResult analytics) : this(null, null)
        {
            foreach (var kvp in analytics.Analytics)
            {
                foreach (var secKvp in kvp.Value.Portfolio.Securities)
                {
                    AddSecurity(secKvp.Key, secKvp.Value);
                }
            }
        }

        public Portfolio(Settings settings)
        {
            cash = settings.Cash.ToDictionary(x => x.Key, x => x.Value);
            CalculateTotals();
        }

        public void AddSecurity(Security security, SecurityInfo securityInfo)
        {
            var thisSecInfo = securities.GetValueOrDefault(security, new SecurityInfo());
            thisSecInfo.Count += securityInfo.Count;
            thisSecInfo.Price += securityInfo.Price;
            securities[security] = thisSecInfo;
            this.total += securityInfo.Total;
        }

        public void RemoveSecurity(Security security)
        {
            if (!securities.ContainsKey(security))
                return;

            total -= securities[security].Total;
            securities.Remove(security);
        }

        public void AddCash(string currency, decimal amount)
        {
            var value = cash.GetValueOrDefault(currency, 0);
            value += amount;
            var valueInRub = amount * CurrenciesManager.Rates[currency];
            cashTotal += valueInRub;
            this.total += valueInRub;
        }

        public void RemoveCash(string currency)
        {
            if (cash.ContainsKey(currency))
            {
                var valueInRub = cash[currency] * CurrenciesManager.Rates[currency];
                cashTotal -= valueInRub;
                total -= valueInRub;
                cash.Remove(currency);
            }
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

                this.total += portfolioValue.Total;
            }
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

                // Данные из файла issuers.xml.
                Dictionary<string, decimal> currenciesDict = null;
                if (security is ETF)
                    currenciesDict = (security as ETF).Currencies;
                else
                {
                    var currency = security is Share ?
                        (security as Share).Currency :
                        (security as Bond).Currency;

                    currenciesDict = new Dictionary<string, decimal>
                    {
                        { currency, 1 }
                    };
                }

                foreach (var currencyRecord in currenciesDict)
                {
                    // Ищем существующую запись по этой валюте.
                    // Если ее нет, создаем пустую.
                    var resultValue = analytics.GetValueOrDefault(
                        currencyRecord.Key, new PortfolioAnalyticsItem());

                    analytics[currencyRecord.Key] = resultValue;

                    // Добавляем бумагу.
                    resultValue.Portfolio.AddSecurity(security, securityInfo);
                }
            }

            // Учитваем наличку.
            foreach (var cashRecord in cash)
            {
                analytics[cashRecord.Key].Portfolio.AddCash(
                    cashRecord.Key, cashRecord.Value);
            }

            result.CalculateParts();
            result.FindNotUsedSecurities(Securities.Keys);

            return result;
        }

        public PortfolioAnalyticsResult GroupByCountry()
        {
            var result = new PortfolioAnalyticsResult();
            var analytics = result.Analytics;

            // Сначала считаем общую сумму по каждой стране.
            foreach (var portfolioSecurity in Securities)
            {
                var security = portfolioSecurity.Key;
                var securityInfo = portfolioSecurity.Value;

                // Данные из файла issuers.xml.
                var countriesDict = security is ETF
                    ? (security as ETF).Countries
                    : security.Issuer.Countries;

                foreach (var countryRecord in countriesDict)
                {
                    // Ищем существующую запись по этой стране.
                    // Если ее нет, создаем пустую.
                    var resultValue = analytics.GetValueOrDefault(
                        countryRecord.Key, new PortfolioAnalyticsItem());

                    analytics[countryRecord.Key] = resultValue;

                    // Добавляем бумагу и её стоимость в эту запись
                    // с коррекцией на долю.
                    resultValue.Portfolio.AddSecurity(security, 
                        new SecurityInfo(securityInfo, countryRecord.Value));
                }
            }

            result.CalculateParts();
            result.FindNotUsedSecurities(Securities.Keys);

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
            // Словарь соответствия типов C# и типов в аттрибутах securities.xml.
            // На случай рефакторинга, чтобы не побилась зависимость при переименовании типа.
            var typesDict = new Dictionary<Type, string> {
                { typeof(Share), "share" },
                { typeof(Bond),  "bond" },
                { typeof(ETF),    "etf" },
            };

            var result = new PortfolioAnalyticsResult();
            var analytics = result.Analytics;
            analytics["cash"] = new PortfolioAnalyticsItem
            {
                Portfolio = new Portfolio(null, this.cash)
            };

            PortfolioAnalyticsItem analyticsItem;

            // Разбираем бумаги по типам.
            foreach (var type in typesDict.Keys)
            {
                // Выбираем бумаги с типом совпадающим текущему type.
                var foundSecurities =
                    Securities.Where(x => x.Key.GetType() == type).ToDictionary(
                    x => x.Key, x => x.Value);

                // Заполняем результат, считаем долю, сумму.
                analyticsItem = new PortfolioAnalyticsItem()
                {
                    Portfolio = new Portfolio(foundSecurities)
                };

                // Преобразуем тип в строку из словаря и используем её как ключ.
                var key = typesDict[type];
                analytics[key] = analyticsItem;
            }

            // Раскидываем ETF по другим активам, если надо.
            if (unpackEtf)
            {
                var etfAnalytics = analytics[typesDict[typeof(ETF)]];

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
                            analytics.GetValueOrDefault(
                                whatInside.Key,
                                new PortfolioAnalyticsItem());

                        // Учитываем поправку в итоговой цене.
                        var correctedSecInfo = new SecurityInfo(securityItem.Value);
                        correctedSecInfo.Correction = whatInside.Value;

                        // Добавляем в список бумаг.
                        analyticsItemDestination.Portfolio.AddSecurity(
                            securityItem.Key, correctedSecInfo);

                        analytics[whatInside.Key] = analyticsItemDestination;
                    }
                }

                // Теперь фонды в чистом виде нам не нужны.
                analytics.Remove(typesDict[typeof(ETF)]);
            }

            result.CalculateParts();
            result.FindNotUsedSecurities(Securities.Keys);

            return result;
        }

        public PortfolioAnalyticsResult GroupBySector()
        {
            var result = new PortfolioAnalyticsResult();
            var analytics = result.Analytics;

            // Сначала считаем общую сумму по каждому сектору.
            foreach (var portfolioSecurity in Securities)
            {
                var security = portfolioSecurity.Key;
                var securityInfo = portfolioSecurity.Value;

                // Данные из файла issuers.xml.
                var sectorsDict = security is ETF
                    ? (security as ETF).Sectors
                    : security.Issuer.Sectors;

                foreach (var sectorRecord in sectorsDict)
                {
                    // Ищем существующую запись по этому сектору.
                    // Если ее нет, создаем пустую.
                    var resultValue = analytics.GetValueOrDefault(
                        sectorRecord.Key.Id, new PortfolioAnalyticsItem());

                    analytics[sectorRecord.Key.Id] = resultValue;

                    // Добавляем бумагу и её стоимость в эту запись.
                    resultValue.Portfolio.AddSecurity(security, securityInfo);
                }
            }

            result.CalculateParts();
            result.FindNotUsedSecurities(Securities.Keys);

            return result;
        }

        private void CalculateTotals()
        {
            total = securities.Sum(x => x.Value.Total);

            // Подсчитываем весь кэш, переводя валюту в рубли.
            cashTotal = cash.Sum(x => x.Value * CurrenciesManager.Rates[x.Key]);
            total += cashTotal;
        }
    }
}
