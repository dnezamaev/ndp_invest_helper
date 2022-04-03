using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using ndp_invest_helper.Models;

namespace ndp_invest_helper
{
    /// <summary>
    /// Исполняет задачи из Task.xml
    /// </summary>
    class TaskManager
    {
        private Portfolio portfolio;

        private PortfolioAnalyticsResult currentResult;

        private bool printEachAction;

        private StringBuilder output;

        public TaskManager(Portfolio portfolio)
        {
            this.portfolio = portfolio;
        }

        public PortfolioAnalyticsResult ParseXmlFile(string filePath, StringBuilder output)
        {
            this.output = output;

            var xRoot = XElement.Parse(File.ReadAllText(filePath));

            foreach (var xTask in xRoot.Elements("task"))
            {
                HandleTask(xTask);
            }

            return currentResult;
        }

        private void HandleTask(XElement xTask)
        {
            currentResult = null;
            printEachAction = false;

            var xPrintEachAction = xTask.Attribute("print_each_action");

            if (xPrintEachAction != null)
                printEachAction = xPrintEachAction.Value.ToString().ToLower() == "yes";

            var xTaskName = xTask.Attribute("name");
            var taskName = (xTaskName != null) ? xTaskName.Value : ("\n" + xTask.ToString());
            output.AppendFormat("*** Задание: {0} \n\n", taskName);

            // Обработка действий в задании.
            foreach (var xAction in xTask.Elements())
            {
                HandleAction(xAction);
            }

            if (currentResult == null)
                return; // Пустое задание.

            #region hide_parts_less attribute
            // Пропускаем категории с значением фильтра указанного в этой переменной.
            // Значение в xml файле приведено в %
            decimal hidePartsLess = 0; 
            var xHidePartsLess = xTask.Attribute("hide_parts_less");
            if (xHidePartsLess != null)
                hidePartsLess = decimal.Parse(
                    xHidePartsLess.Value.ToString(), 
                    NumberStyles.Any, CultureInfo.InvariantCulture
                    ) / 100;

            var keysToHide =
                from item in currentResult.Analytics
                where item.Value.Part < hidePartsLess
                select item.Key;

            currentResult.RemoveKeys(false, keysToHide.ToArray());
            #endregion

            #region order_by attribute
            var xOrderBy = xTask.Attribute("order_by");
            var orderBy = (xOrderBy == null) ? "part" : xOrderBy.Value.ToString().ToLower();
            var orderBySplitted = orderBy.Split(';');
            currentResult.OrderBy = orderBySplitted[0];

            if (orderBySplitted.Length == 2)
                currentResult.OrderAscending = orderBySplitted[1] != "desc";
            #endregion

            #region remove_isins, remove_tickers, keep_only_isins, keep_only_tickers

            var xRemoveIsins = xTask.Attribute("remove_isins");
            var xRemoveTickers = xTask.Attribute("remove_tickers");
            var xKeepIsins = xTask.Attribute("keep_only_isins");
            var xKeepTickers = xTask.Attribute("keep_only_tickers");

            if ( 
                (xRemoveIsins != null || xRemoveTickers != null) 
                && (xKeepIsins != null || xKeepTickers != null)
                )
                throw new ArgumentException(
                    "Нельзя совмещать аттрибуты remove_isin и remove_tickers" +
                    " с аттрибутами keep_only_isins и keep_only_tickers");

            var securitiesToRemove = new List<Security>();

            if (xRemoveIsins != null)
            {
                securitiesToRemove.AddRange(
                    from isinToRemove in xRemoveIsins.Value.ToString().Split(';')
                    select SecuritiesManager.SecuritiesByIsin[isinToRemove]
                    );
            }

            if (xRemoveTickers != null)
            {
                securitiesToRemove.AddRange(
                    from tickerToRemove in xRemoveTickers.Value.ToString().Split(';')
                    select SecuritiesManager.SecuritiesByTicker[tickerToRemove]
                    );
            }

            var securitiesToKeep = new List<Security>();

            if (xKeepIsins != null)
            {
                securitiesToKeep.AddRange(
                    from isinToKeep in xKeepIsins.Value.ToString().Split(';')
                    select SecuritiesManager.SecuritiesByIsin[isinToKeep]
                    );
            }

            if (xKeepTickers != null)
            {
                securitiesToKeep.AddRange(
                    from tickerToKeep in xKeepTickers.Value.ToString().Split(';')
                    select SecuritiesManager.SecuritiesByTicker[tickerToKeep]
                    );
            }

            var securitiesInResult = currentResult.GetSecurities();

            if (securitiesToKeep.Count != 0)
            {
                securitiesToRemove = securitiesInResult.Except(securitiesToKeep).ToList();
            }

            currentResult.RemoveSecurities(
                correctParts: true,
                addCash: false, 
                (Currency)CommonData.Currencies.ByCode["RUB"], 
                securitiesToRemove.ToArray());

            #endregion

            if (!printEachAction)
            {
                output.AppendLine(currentResult.ToString());
                output.AppendLine();
            }
        }

        private void HandleAction(XElement xAction)
        {
            var action = xAction.Name.ToString().ToLower();

            switch (action)
            {
                case "group":
                    HandleGroupByAction(xAction);
                    
                    if (printEachAction)
                        output.AppendFormat("--- {0}\n{1}\n\n", xAction, currentResult);
                    break;
                case "buy":
                case "sell":
                    HandleBuySellAction(xAction);
                    // TODO: выводить подробности сделки - что, почем и на какую сумму
                    // для этого надо, чтобы методы редактирования портфеля возвращали
                    // необходимые данные, которые нужно протащить до самого верха
                    // по стеку вызовов
                    break;
                default:
                    throw new ArgumentException(string.Format(
                        "Неизвестное действие {0} в задании {1}", action, xAction));
            }
        }

        private void HandleGroupByAction(XElement xAction)
        {
            var xGroupBy = xAction.Attribute("by");

            if (xGroupBy == null)
                throw new ArgumentException(string.Format(
                    "В задании по группировке отсутствует критерий {0}.", xAction));
            
            var groupBy = xGroupBy.Value.ToString().ToLower();

            // Проверяем первое ли это действие и был ли результат до него.
            var currentPortfolio = currentResult == null ?
                portfolio : new Portfolio(currentResult);

            switch (groupBy)
            {
                case "country":
                    currentResult = currentPortfolio.GroupByCountry();
                    break;

                case "currency":
                    currentResult = currentPortfolio.GroupByCurrency();
                    break;

                case "type":
                    var xUnpackEtf = xAction.Attribute("unpack_etf");
                    var sUnpackEtf = (xUnpackEtf == null) ? "yes" : xUnpackEtf.Value;
                    var bUnpackEtf = sUnpackEtf == "yes";

                    currentResult = currentPortfolio.GroupByType(bUnpackEtf);
                    break;

                case "sector":
                    var xLevel = xAction.Attribute("level");
                    var level = 1;
                    if (xLevel != null)
                    {
                        var isParseOk = int.TryParse(xLevel.Value, out level);
                        if (!isParseOk)
                            throw new ArgumentException(string.Format(
                                "Уровень группировки по секторам level " +
                                "должен быть целочисленным значением, а указан {0} " +
                                "в задании {1}", 
                                xLevel.Value, xAction));
                    }
                    currentResult = currentPortfolio.GroupBySector(level);
                    break;

                default:
                    throw new ArgumentException(string.Format(
                        "Неизвестный критерий группировки {0} в задании {1}", groupBy, xAction));
            }

            currentResult.GrouppedBy = groupBy;

            #region remove_keys, keep_only_keys

            var xRemoveKeys = xAction.Attribute("remove_keys");
            var xKeepOnlyKeys = xAction.Attribute("keep_only_keys");

            if (xRemoveKeys != null && xKeepOnlyKeys != null)
                throw new ArgumentException("Нельзя совмещать аттрибуты remove_keys и keep_only_keys");

            string[] keysToRemove = null;

            if (xRemoveKeys != null)
                keysToRemove = xRemoveKeys.Value.Split(';');
            
            if (xKeepOnlyKeys  != null)
            {
                var keysToKeep = xKeepOnlyKeys.Value.Split(';');
                keysToRemove = currentResult.Analytics.Keys
                    .Where(x => ! keysToKeep.Contains(x.Code))
                    .Select(x => x.Code)
                    .ToArray();
            }

            if (keysToRemove != null)
            {
                currentResult.RemoveKeys(
                    true,
                    currentResult.Analytics.Keys
                    .Where(x => keysToRemove.Contains(x.Code))
                    .ToArray()
                    );
            }

            #endregion
        }

        /// <summary>
        /// Покупка/продажа бумаг. 
        /// Действие может быть только до группировки, 
        /// иначе придется повторять последнюю группировку, пока такое ограничение. 
        /// В будущем можно сохранять последнее задание группировки и повторять его. 
        /// </summary>
        /// <param name="xAction"></param>
        private void HandleBuySellAction(XElement xAction)
        {
            if (currentResult == null)
                currentResult = new PortfolioAnalyticsResult(portfolio);

            var action = xAction.Name.ToString().ToLower();

            var xIsin = xAction.Attribute("isin");
            var xTicker = xAction.Attribute("ticker");
            var xCount = xAction.Attribute("count");
            var xPrice = xAction.Attribute("price");
            var xCurrency = xAction.Attribute("currency");

            if (xIsin == null && xTicker == null)
                throw new ArgumentException(string.Format(
                    "В действии {0} не хватает аттрибута isin или ticker.", xAction.ToString()));

            // Количество нужно указывать только при покупке.
            // При продаже можно опустить.
            if (xCount == null && xAction.Name == "buy")
                throw new ArgumentException(string.Format(
                    "В действии {0} не хватает аттрибута count.", xAction.ToString()));

            Security security = null;

            if (xIsin != null) // Ищем по ISIN.
            {
                var isin = xIsin.Value.ToString();

                if (!SecuritiesManager.SecuritiesByIsin.TryGetValue(isin, out security))
                    throw new ArgumentException(string.Format(
                        "В действии {0} указана неизвестная бумага {1}." +
                        "Возможно, её стоит добавить в базу {2}, " +
                        "либо проверить правильность указания isin.",
                        xAction.ToString(), isin, Settings.SecuritiesXmlFilePath));
            }
            else // Ищем по ticker.
            {
                var ticker = xTicker.Value.ToString();

                if (!SecuritiesManager.SecuritiesByTicker.TryGetValue(ticker, out security))
                    throw new ArgumentException(string.Format(
                        "В действии {0} указана неизвестная бумага {1}." +
                        "Возможно, её стоит добавить в базу {2}, " +
                        "либо проверить правильность указания ticker.",
                        xAction.ToString(), ticker, Settings.SecuritiesXmlFilePath));
            }

            UInt64? count = null;

            if (xCount != null)
            {
                UInt64 sell_count;
                if (!UInt64.TryParse(xCount.Value.ToString(), out sell_count))
                    throw new ArgumentException(string.Format(
                        "В действии {0} указан аттрибут count={1}, " +
                        "который не удалось преобразовать в целое число.",
                        xAction.ToString(), xCount.Value.ToString()));
                count = sell_count;
            }

            decimal price = 0;

            if (xPrice != null)
                if (!decimal.TryParse(
                    xPrice.Value.ToString(), 
                    NumberStyles.Any, CultureInfo.InvariantCulture, 
                    out price)
                    )
                    throw new ArgumentException(string.Format(
                        "В действии {0} указан аттрибут price={1}, " +
                        "который не удалось преобразовать в число.",
                        xAction.ToString(), xPrice.Value.ToString()));

            var xUseCash = xAction.Attribute("correct_cash");
            var useCash = true;

            if (xUseCash != null)
                useCash = xUseCash.Value.ToString() == "no";

            var currency = (Currency)CommonData.Currencies.ByCode["RUB"];

            if (xCurrency != null)
            {
                currency = (Currency)CommonData.Currencies.ByCode[xCurrency.Value];

                if (!CommonData.Currencies.RatesToRub.ContainsKey(currency))
                    throw new ArgumentException(string.Format(
                        "В действии {0} указан аттрибут currency={1}, " +
                        "такая валюта не найдена, её стоит добавить в Settings.xml.",
                        xAction.ToString(), currency));
            }

            if (action == "buy")
                currentResult.BuySecurity(security, count.Value, price, useCash, currency);
            else
                currentResult.SellSecurity(security, count, useCash, currency);
        }
    }
}
