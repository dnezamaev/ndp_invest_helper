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
            var xTaskName = xTask.Attribute("name");
            var taskName = (xTaskName != null) ? xTaskName.Value : ("\n" + xTask.ToString());
            Console.Write("Результаты по заданию: {0} \n\n", taskName);

            currentResult = null;

            // Пропускаем категории с значением фильтра указанного в этой переменной.
            // Значение в xml файле приведено в %
            decimal hidePartsLess = 0; 
            var xHidePartsLess = xTask.Attribute("hide_parts_less");
            if (xHidePartsLess != null)
                hidePartsLess = decimal.Parse(xHidePartsLess.Value.ToString()) / 100;

            foreach (var xAction in xTask.Elements())
            {
                HandleAction(xAction);
            }

            var keysToHide =
                from item in currentResult.Analytics
                where item.Value.Part < hidePartsLess
                select item.Key;

            currentResult.RemoveKeys(false, keysToHide.ToArray());

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
                    currentResult = currentPortfolio.GroupBySector();
                    break;

                default:
                    throw new ArgumentException(string.Format(
                        "Неизвестный критерий группировки {0} в задании {1}", groupBy, xAction));
            }

            // Опциональный аттрибут remove_keys.
            // Удаляет указанные значения ключей из результата группировки.
            // Значения указаны через точку с запятой.
            // Пересчитывает доли.
            var xRemoveKeys = xAction.Attribute("remove_keys");
            if (xRemoveKeys != null)
            {
                var keysToRemove = xRemoveKeys.Value.Split(';');
                currentResult.RemoveKeys(true, keysToRemove);
            }

            // Опциональный аттрибут keep_only_keys .
            // Оставляет только указанные значения ключей в результате группировки.
            // Значения указаны через точку с запятой.
            // Пересчитывает доли.
            var xKeep_only_keys = xAction.Attribute("keep_only_keys");
            if (xKeep_only_keys  != null)
            {
                var keysToKeep = xKeep_only_keys.Value.Split(';');
                var keysToRemove = currentResult.Analytics.Keys.Where(
                    x => ! keysToKeep.Contains(x)
                    ).ToArray();

                currentResult.RemoveKeys(true, keysToRemove);
            }
        }
    }
}
