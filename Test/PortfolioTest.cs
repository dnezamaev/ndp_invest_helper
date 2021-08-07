using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ndp_invest_helper;
using System.Xml.Linq;

namespace Test
{
    [TestClass]
    public class PortfolioTest
    {
        [TestInitialize]
        public void Initialize()
        {
            CountriesManager.ParseXmlFile(@"data\info\countries.xml");
            SectorsManager.ParseXmlFile(@"data\info\sectors.xml");
        }

        [TestMethod]
        public void TestHandleComplexStringXmlAttribute()
        {
            #region Xml text.
            var xmlText_Empty = @"
              <issuer>
                  <security/>
              </issuer>
            ";

            // Вложенные теги both имеют приоритет над аттрибутом родительского тега.
            var xmlText_Priority = @"
                <security only_here='key' both='key1'>
                    <both key='key2' value='20' />
                    <both key='key3' value='30' />
                </security>
            ";
            #endregion

            // Если нет аттрибута и дочерних тегов, получаем пустой результат.
            var resultEmpry = Utils.HandleComplexStringXmlAttribute(XElement.Parse(xmlText_Empty), "country");
            Assert.AreEqual(resultEmpry.Count, 0);

            var xmlTag_Priority = XElement.Parse(xmlText_Priority);

            var resultOnly = Utils.HandleComplexStringXmlAttribute(xmlTag_Priority, "only_here");
            Assert.AreEqual(1, resultOnly.Keys.Count);
            Assert.IsTrue(resultOnly.ContainsKey("key"));
            Assert.AreEqual(1M, resultOnly["key"]);

            var resultBoth = Utils.HandleComplexStringXmlAttribute(xmlTag_Priority, "both");
            Assert.AreEqual(3, resultBoth.Keys.Count);
            Assert.IsTrue(resultBoth.ContainsKey("key2"));
            Assert.IsTrue(resultBoth.ContainsKey("key3"));
            Assert.IsTrue(resultBoth.ContainsKey("???"));
            Assert.AreEqual(0.2M, resultBoth["key2"]);
            Assert.AreEqual(0.3M, resultBoth["key3"]);
            Assert.AreEqual(0.5M, resultBoth["???"]);
        }
        

        /// <summary>
        /// Если у эмитента и бумаги нет одного из обязательных аттрибутов,
        /// должно быть исключение.
        /// </summary>
        [TestMethod]
        public void TestIssuerAndSecurity()
        {
            #region Xml text.
            var xmlText_Full = @"
                <root>
                <issuer name = 'full attributes issuer' 
                    country = 'issuer country' 
                    sector = '1'
                >
                    <country key = 'issuer inner country 1' value = '20' />
                    <country key = 'issuer inner country 2' value = '80' />

                    <sector key = 'issuer inner sector 1' value = '20' />
                    <sector key = 'issuer inner sector 2' value = '80' />

                    <security 
                        isin = 'full attributes etf' 
                        sec_type = 'etf' 
                        currency = 'etf currency' 
                        country = 'US' 
                        sector = '2' 
                        what_inside = 'share'
                    >
                        <currency key = 'CUR1' value = '10' />
                        <country key = 'CN' value = '20' />
                        <sector key = '3' value = '30' />
                        <what_inside  key = 'bond' value = '40' />
                    </security>

                    <security isin = 'empty attributes etf' ticker = 'EMPTY' sec_type = 'etf' />

                </issuer>
                </root>
            ";
            #endregion

            SecuritiesManager.ParseXmlText(xmlText_Full);

            // У эмитента свои страны и сектора, приоритет у вложенных тегов перед аттрибутами.
            Assert.AreEqual(1, SecuritiesManager.Issuers.Count);
            var issuer = SecuritiesManager.Issuers[0];
            Assert.AreEqual("full attributes issuer", issuer.Name);
            Assert.AreEqual(2, issuer.Countries.Count);
            Assert.IsTrue(issuer.Countries.ContainsKey("issuer inner country 1"));
            Assert.IsTrue(issuer.Countries.ContainsKey("issuer inner country 2"));
            Assert.AreEqual(0.2M, issuer.Countries["issuer inner country 1"]);
            Assert.AreEqual(0.8M, issuer.Countries["issuer inner country 2"]);

            // С заполненной бумагой аналогично, у неё свои характеристики,
            // переопределяющие эмитента.
            Assert.AreEqual(2, SecuritiesManager.Securities.Count);
            var security = SecuritiesManager.SecuritiesByIsin["full attributes etf"];
            Assert.IsInstanceOfType(security, typeof(ETF));
            var fullEtf = security as ETF;

            Assert.AreEqual(2, fullEtf.Currencies.Count);
            Assert.IsTrue(fullEtf.Currencies.ContainsKey("CUR1"));
            Assert.IsTrue(fullEtf.Currencies.ContainsKey("???"));
            Assert.AreEqual(0.1M, fullEtf.Currencies["CUR1"]);
            Assert.AreEqual(0.9M, fullEtf.Currencies["???"]);

            Assert.AreEqual(2, fullEtf.Countries.Count);
            Assert.IsTrue(fullEtf.Countries.ContainsKey("CN"));
            Assert.IsTrue(fullEtf.Countries.ContainsKey("???"));
            Assert.AreEqual(0.2M, fullEtf.Countries["CN"]);
            Assert.AreEqual(0.8M, fullEtf.Countries["???"]);

            Assert.AreEqual(2, fullEtf.Sectors.Count);
            Assert.IsTrue(fullEtf.Sectors.ContainsKey(SectorsManager.ById["3"]));
            Assert.IsTrue(fullEtf.Sectors.ContainsKey(SectorsManager.DefaultSector));
            Assert.AreEqual(0.3M, fullEtf.Sectors[SectorsManager.ById["3"]]);
            Assert.AreEqual(0.7M, fullEtf.Sectors[SectorsManager.DefaultSector]);

            Assert.AreEqual(2, fullEtf.WhatInside.Count);
            Assert.IsTrue(fullEtf.WhatInside.ContainsKey("bond"));
            Assert.IsTrue(fullEtf.WhatInside.ContainsKey("???"));
            Assert.AreEqual(0.4M, fullEtf.WhatInside["bond"]);
            Assert.AreEqual(0.6M, fullEtf.WhatInside["???"]);

            var emptyEtf = SecuritiesManager.SecuritiesByTicker["EMPTY"];

        }

        /// <summary>
        /// Проверка приоритета аттрибута и вложенных тегов.
        /// </summary>
        [TestMethod]
        public void TestOverrideSecurityAttributes()
        {
            SecuritiesManager.ParseXmlFile(@"data\info\securities.xml");
            var security = SecuritiesManager.SecuritiesByIsin["test override security"] as ETF;
            Assert.AreEqual(security.Countries.Count, 4);
        }
    }
}
