using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace ndp_invest_helper
{
    public abstract class Report
    {
        /// <summary>
        /// Обнаруженные в отчете бумаги.
        /// </summary>
        public Dictionary<Security, SecurityInfo> Securities = 
            new Dictionary<Security, SecurityInfo>();

        /// <summary>
        /// Обнаруженная в отчете наличность.
        /// </summary>
        public Dictionary<string, decimal> Cash = new Dictionary<string, decimal>();

        /// <summary>
        /// Обнаруженные в отчете бумаги, о которых нет информации в базе.
        /// </summary>
        public HashSet<Security> UnknownSecurities = new HashSet<Security>();

        /// <summary>
        /// Разобрать файл отчета.
        /// </summary>
        /// <param name="xmlReportFilePath"></param>
        abstract public void ParseXmlFile(string xmlReportFilePath);
    }

    public class CashReport : Report
    {
        public override void ParseXmlFile(string xmlReportFilePath)
        {
            var xRoot = XElement.Parse(System.IO.File.ReadAllText(xmlReportFilePath));

            foreach (var xCash in xRoot.Elements("currency"))
            {
                var xKey = xCash.Attribute("key");
                var xValue = xCash.Attribute("value");

                if (xKey == null || xValue == null)
                    throw new ArgumentException(string.Format(
                        "В файле {0} обнаружена запись без обязательного аттрибута " +
                        "{1}. {2}.",
                        xmlReportFilePath, xKey == null ? "key" : "value"));

                var cashValue = decimal.Parse(
                    xValue.Value.ToString(), 
                    NumberStyles.Any, CultureInfo.InvariantCulture );

                Cash[xKey.Value] = cashValue;
            }
        }
    }
}
