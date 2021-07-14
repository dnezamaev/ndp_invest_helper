using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ndp_invest_helper
{
    class TaskManager
    {
        private Portfolio portfolio;
        private PortfolioAnalyticsResult currentResult;

        public TaskManager(Portfolio portfolio)
        {
            this.portfolio = portfolio;
        }

        public void ParseXmlFile(string filePath)
        {
            var xRoot = XElement.Parse(File.ReadAllText(filePath));

            foreach (var xTask in xRoot.Elements("task"))
            {
                HandleTask(xTask);
            }
        }

        private void HandleTask(XElement xTask)
        {
            currentResult = null;

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
                hidePartsLess = decimal.Parse(xHidePartsLess.Value.ToString()) / 100;

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

            currentResult.RemoveSecurities(true, securitiesToRemove.ToArray());

            #endregion

            var xTaskName = xTask.Attribute("name");
            var taskName = (xTaskName != null) ? xTaskName.Value : ("\n" + xTask.ToString());

            Console.Write("Результаты по заданию: {0} \n\n", taskName);
            Console.WriteLine(currentResult);
            Console.WriteLine();
        }

        private void HandleAction(XElement xAction)
        {
            var action = xAction.Name.ToString().ToLower();

            switch (action)
            {
                case "group":
                    HandleGroupByAction(xAction);
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
            var currentPortfolio = (currentResult == null) ? 
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
                keysToRemove = currentResult.Analytics.Keys.Where(
                    x => ! keysToKeep.Contains(x)
                    ).ToArray();
            }

            if (keysToRemove != null)
                currentResult.RemoveKeys(true, keysToRemove);

            #endregion
        }
    }
}
