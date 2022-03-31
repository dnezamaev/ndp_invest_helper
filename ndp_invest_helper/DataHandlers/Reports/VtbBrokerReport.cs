using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

using ndp_invest_helper.Models;

namespace ndp_invest_helper
{
    class VtbBrokerReport : BrokerReport
    {
        private XNamespace xNamespace;
        private XElement xRoot;

        public override void ParseFile(string filePath)
        {
            FilePath = filePath;

            var xmlReportText = File.ReadAllText(filePath);
            xRoot = XElement.Parse(xmlReportText);
            xNamespace = xRoot.GetDefaultNamespace();

            ParseCurrentSecurities();
        }

        /// <summary>
        /// Ищем все имеющиеся в портфеле бумаги.
        /// </summary>
        private void ParseCurrentSecurities()
        {
            //var xCurSecRoot = xRoot.Element(xNamespace + "Tablix_h2_acc");
            var xSecuritiesList = xRoot.Descendants(xNamespace + "FinInstr");
            foreach (var xSecurity in xSecuritiesList)
            {
                var secNameAttr = xSecurity.Attribute("FinInstr").Value;
                var secNameStrings = secNameAttr.Split(',');
                var secName = secNameStrings[0].Trim();
                var secIsin = secNameStrings[2].Trim();

                var xSecDetails = xSecurity.Descendants(xNamespace + "Подробности21").First();
                var secCount = (UInt64)decimal.Parse(xSecDetails.Attribute("remains_plan").Value, 
                    NumberStyles.Any, CultureInfo.InvariantCulture);
                var secCurrency = xSecDetails.Attribute("currency_ISO").Value;

                // В отчете используется старый код рубля.
                if (secCurrency == "RUR")
                    secCurrency = "RUB";

                decimal secPrice;

                // костыль для определения облигации
                // можно заменить на анализ тэга предка bond_type1="ОБЛИГАЦИЯ"
                // Но пока нет смысла, этот чудо формат может измениться в любой момент.
                // По хорошему надо смотреть по ISIN на Мосбирже.
                if (xSecDetails.Attribute("coupon_date") != null)
                {
                    // это облигация
                    var priceInPercents = decimal.Parse(xSecDetails.Attribute("bond_price1").Value,
                        NumberStyles.Any, CultureInfo.InvariantCulture);
                    var nominal = decimal.Parse(xSecDetails.Attribute("Textbox31").Value,
                        NumberStyles.Any, CultureInfo.InvariantCulture);
                    var nkd = decimal.Parse(xSecDetails.Attribute("nkd_nominal1").Value,
                        NumberStyles.Any, CultureInfo.InvariantCulture);
                    secPrice = priceInPercents / 100 * nominal + nkd / secCount;
                }
                else
                {
                    // это акция или фонд
                    secPrice = decimal.Parse(xSecDetails.Attribute("bond_price1").Value,
                        NumberStyles.Any, CultureInfo.InvariantCulture);
                }

                secPrice *= CurrenciesManager.RatesToRub[secCurrency];

                var securityInfo = new SecurityInfo
                {
                    Price = secPrice,
                    Count = secCount
                };

                Security security;
                if (SecuritiesManager.SecuritiesByIsin.TryGetValue(secIsin, out security))
                {
                    Securities[security] = securityInfo;
                }
                else // такой бумаги нет в базе, добавляем в неизвестные
                {
                    UnknownSecurities.Add(new Security { Isin = secIsin });
                }
            }
        }
    }
}
