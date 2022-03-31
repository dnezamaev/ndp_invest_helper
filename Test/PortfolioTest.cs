using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ndp_invest_helper;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

using ndp_invest_helper.Models;

namespace Test
{
    [TestClass]
    public class PortfolioTest
    {
        [TestInitialize]
        public void Initialize()
        {
            SectorsManager.LoadFromXmlFile(@"data\info\sectors.xml");
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

        [TestMethod]
        public void TestIssuerAndSecurity()
        {
            #region Xml text.
            var xmlText = @"
                <root>
                <issuer name = 'full attributes issuer' 
                    country = 'issuer country' 
                    currency = 'issuer currency' 
                    sector = '1'
                >

                    <country key = 'issuer inner country 1' value = '20' />
                    <country key = 'issuer inner country 2' value = '80' />

                    <currency key = 'issuer inner currency 1' value = '20' />
                    <currency key = 'issuer inner currency 2' value = '80' />

                    <sector key = '2' value = '20' />
                    <sector key = '3' value = '80' />

                    <security 
                        isin = 'full attributes etf' 
                        sec_type = 'etf' 
                        currency = 'etf currency' 
                        country = 'US' 
                        sector = '4' 
                        what_inside = 'share'
                    >
                        <currency key = 'CUR1' value = '10' />
                        <country key = 'CN' value = '20' />
                        <sector key = '5' value = '30' />
                        <what_inside  key = 'bond' value = '40' />
                    </security>

                    <security isin = 'empty attributes bond' ticker = 'EMPTY' sec_type = 'bond' />

                    <security isin = 'bond with currency' sec_type = 'bond' currency = 'bond currency' />

                </issuer>
                </root>
            ";
            #endregion

            CurrenciesManager.LoadFromXmlText
            ( @"
                <root>
                <currency code='???' rate='1' name_en='unknown' />
                <currency code='CUR1' rate='1' name_en='unknown' />
                <currency code='issuer currency' rate='1' name_en='' />
                <currency code='issuer inner currency 1' rate='1' name_en='' />
                <currency code='issuer inner currency 2' rate='1' name_en='' />
                <currency code='bond currency' rate='1' name_en='' />
                </root>
            " );

            CountriesManager.LoadFromXmlText
            ( @"
                <root>
                  <countries>
                    <country key='???' name=''/>
                    <country key='US' name=''/>
                    <country key='CN' name=''/>
                    <country key='issuer country' name=''/>
                    <country key='issuer inner country 1' name=''/>
                    <country key='issuer inner country 2' name=''/>
                  </countries>
                  <by_region />
                  <by_development_level />
                </root>
            " );

            SecuritiesManager.ParseXmlText(xmlText);

            // У эмитента свои страны и сектора, приоритет у вложенных тегов перед аттрибутами.
            Assert.AreEqual(1, SecuritiesManager.Issuers.Count);
            var issuer = SecuritiesManager.Issuers[0];
            Assert.AreEqual("full attributes issuer", issuer.NameRus);

            Assert.AreEqual(2, issuer.Currencies.Count);
            Assert.IsTrue(issuer.Currencies.Keys.Contains(
                CurrenciesManager.ByCode["issuer inner currency 1"]));
            Assert.IsTrue(issuer.Currencies.Keys.Contains(
                CurrenciesManager.ByCode["issuer inner currency 2"]));
            Assert.AreEqual(0.2M, issuer.Currencies[
                CurrenciesManager.ByCode["issuer inner currency 1"]]);
            Assert.AreEqual(0.8M, issuer.Currencies[
                CurrenciesManager.ByCode["issuer inner currency 2"]]);

            Assert.AreEqual(2, issuer.Countries.Count);
            Assert.IsTrue(issuer.Countries.ContainsKey(
                CountriesManager.ByCode["issuer inner country 1"]));
            Assert.IsTrue(issuer.Countries.ContainsKey(
                CountriesManager.ByCode["issuer inner country 2"]));
            Assert.AreEqual(0.2M, issuer.Countries[
                CountriesManager.ByCode["issuer inner country 1"]]);
            Assert.AreEqual(0.8M, issuer.Countries[
                CountriesManager.ByCode["issuer inner country 2"]]);

            Assert.AreEqual(2, issuer.Sectors.Count);
            Assert.IsTrue(issuer.Sectors.ContainsKey(SectorsManager.ById["2"]));
            Assert.IsTrue(issuer.Sectors.ContainsKey(SectorsManager.ById["3"]));
            Assert.AreEqual(0.2M, issuer.Sectors[SectorsManager.ById["2"]]);
            Assert.AreEqual(0.8M, issuer.Sectors[SectorsManager.ById["3"]]);

            // С заполненным фондом аналогично, у него свои характеристики,
            // переопределяющие эмитента.
            const int SecuritiesCount = 3;
            Assert.AreEqual(SecuritiesCount, SecuritiesManager.Securities.Count);
            Assert.AreEqual(SecuritiesCount, issuer.Securities.Count);

            var security = SecuritiesManager.SecuritiesByIsin["full attributes etf"];
            Assert.IsInstanceOfType(security, typeof(ETF));
            var fullEtf = security as ETF;

            Assert.AreEqual(2, fullEtf.Currencies.Count);
            Assert.IsTrue(fullEtf.Currencies.ContainsKey(
                CurrenciesManager.ByCode["CUR1"]));
            Assert.IsTrue(fullEtf.Currencies.ContainsKey(
                CurrenciesManager.ByCode["???"]));
            Assert.AreEqual(0.1M, fullEtf.Currencies[
                CurrenciesManager.ByCode["CUR1"]]);
            Assert.AreEqual(0.9M, fullEtf.Currencies[
                CurrenciesManager.ByCode["???"]]);

            Assert.AreEqual(2, fullEtf.Countries.Count);
            Assert.IsTrue(fullEtf.Countries.ContainsKey(
                CountriesManager.ByCode["CN"]));
            Assert.IsTrue(fullEtf.Countries.ContainsKey(
                CountriesManager.ByCode["???"]));
            Assert.AreEqual(0.2M, fullEtf.Countries[
                CountriesManager.ByCode["CN"]]);
            Assert.AreEqual(0.8M, fullEtf.Countries[
                CountriesManager.ByCode["???"]]);

            Assert.AreEqual(2, fullEtf.Sectors.Count);
            Assert.IsTrue(fullEtf.Sectors.ContainsKey(SectorsManager.ById["5"]));
            Assert.IsTrue(fullEtf.Sectors.ContainsKey(SectorsManager.DefaultSector));
            Assert.AreEqual(0.3M, fullEtf.Sectors[SectorsManager.ById["5"]]);
            Assert.AreEqual(0.7M, fullEtf.Sectors[SectorsManager.DefaultSector]);

            Assert.AreEqual(2, fullEtf.WhatInside.Count);
            Assert.IsTrue(fullEtf.WhatInside.ContainsKey("bond"));
            Assert.IsTrue(fullEtf.WhatInside.ContainsKey("???"));
            Assert.AreEqual(0.4M, fullEtf.WhatInside["bond"]);
            Assert.AreEqual(0.6M, fullEtf.WhatInside["???"]);

            // У пустой бумаги характеристики берутся от эмитента.
            var emptySec = SecuritiesManager.SecuritiesByTicker["EMPTY"];

            Assert.AreEqual(2, emptySec.Countries.Count);
            Assert.IsTrue(emptySec.Countries.ContainsKey(
                CountriesManager.ByCode["issuer inner country 1"]));
            Assert.IsTrue(emptySec.Countries.ContainsKey(
                CountriesManager.ByCode["issuer inner country 2"]));
            Assert.AreEqual(0.2M, emptySec.Countries[
                CountriesManager.ByCode["issuer inner country 1"]]);
            Assert.AreEqual(0.8M, emptySec.Countries[
                CountriesManager.ByCode["issuer inner country 2"]]);

            Assert.AreEqual(2, emptySec.Sectors.Count);
            Assert.IsTrue(emptySec.Sectors.ContainsKey(SectorsManager.ById["2"]));
            Assert.IsTrue(emptySec.Sectors.ContainsKey(SectorsManager.ById["3"]));
            Assert.AreEqual(0.2M, emptySec.Sectors[SectorsManager.ById["2"]]);
            Assert.AreEqual(0.8M, emptySec.Sectors[SectorsManager.ById["3"]]);

            var currencyBond = SecuritiesManager.SecuritiesByIsin["bond with currency"];

            Assert.AreEqual(1, currencyBond.Currencies.Count);
            Assert.IsTrue(currencyBond.Currencies.ContainsKey(
                CurrenciesManager.ByCode["bond currency"]));
            Assert.AreEqual(1M, currencyBond.Currencies[
                CurrenciesManager.ByCode["bond currency"]]);
        }

        [TestMethod]
        public void TestGroupping()
        {
            #region Xml text.
            var xmlText_Full = @"
                <root>
                <issuer name = 'issuer' >

                    <security 
                        isin = 'etf1' 
                        sec_type = 'etf' 
                        currency = 'cur1' 
                        country = 'cnt1' 
                        sector = '1' 
                        what_inside = 'share'
                    />

                    <security isin = 'etf2' sec_type = 'etf' >
                        <currency key = 'cur1' value = '10' />
                        <currency key = 'cur2' value = '50' />
                        <country key = 'cnt1' value = '10' />
                        <country key = 'cnt2' value = '50' />
                        <sector key = '1' value = '10' />
                        <sector key = '2' value = '50' />
                        <what_inside key = 'share' value = '10' />
                        <what_inside key = 'bond' value = '50' />
                        <what_inside key = 'gold' value = '10' />
                    </security>

                </issuer>

                <issuer name = 'empty'>
                    <security isin = 'empty' sec_type = 'share'/>
                </issuer>

                </root>
            ";
            #endregion

            CurrenciesManager.LoadFromXmlText
            ( @"
                <root>
                <currency code='???' rate='1' name_en='unknown' />
                <currency code='cur1' rate='1' name_en='cur1' />
                <currency code='cur2' rate='1' name_en='cur2' />
                </root>
            " );

            CountriesManager.LoadFromXmlText
            ( @"
                <root>
                  <countries>
                    <country key='???' name='unknown'/>
                    <country key='cnt1' name='cnt1'/>
                    <country key='cnt2' name='cnt2'/>
                  </countries>
                  <by_region />
                  <by_development_level />
                </root>
            " );

            SecuritiesManager.ParseXmlText(xmlText_Full);
            var etf1 = SecuritiesManager.SecuritiesByIsin["etf1"];
            var etf2 = SecuritiesManager.SecuritiesByIsin["etf2"];

            var secDict = 
                new Dictionary<Security, SecurityInfo>
                {
                    { etf1, new SecurityInfo { Count = 10, Price = 1000 } },
                    { etf2, new SecurityInfo { Count = 10, Price = 1000 } }
                };

            var cashDict =
                new Dictionary<string, decimal>
                {
                    { "cur1", 100 },
                    { "cur2", 100 },
                };

            var portfolio = new Portfolio(secDict, cashDict);

            var cashTotal = cashDict.Sum(x => x.Value);
            var secTotal = secDict.Sum(x => x.Value.Total);
            var total = cashTotal + secTotal;

            Assert.AreEqual(cashTotal, portfolio.CashTotal);
            Assert.AreEqual(total, portfolio.Total);

            Assert.AreEqual(secDict.Count, portfolio.Securities.Count);
            Assert.AreEqual(cashDict.Count, portfolio.Cash.Count);

            var group = new GrouppingResults(portfolio);
            var grCurrency = group.ByCurrency;
            var grCountry = group.ByCountry;
            var grType = group.ByType;
            var grSector = group.BySector;

            var anCurrency = group.ByCurrency.Analytics;
            var anCountry = group.ByCountry.Analytics;
            var anType = group.ByType.Analytics;
            var anSector = group.BySector.Analytics;

            Assert.AreEqual(3, anCurrency.Count);
            Assert.IsTrue(anCurrency.ContainsKey("cur1"));
            Assert.IsTrue(anCurrency.ContainsKey("cur2"));
            Assert.IsTrue(anCurrency.ContainsKey("???"));
            Assert.AreEqual(
                (100 + 10 * 1000 * 1 + 10 * 1000 * 0.1M) / total, 
                anCurrency["cur1"].Part);
            Assert.AreEqual(
                (100 + 10 * 1000 * 0.5M) / total, 
                anCurrency["cur2"].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.4M) / total, 
                anCurrency["???"].Part);

            Assert.AreEqual(3, anCountry.Count);
            Assert.IsTrue(anCountry.ContainsKey("cnt1"));
            Assert.IsTrue(anCountry.ContainsKey("cnt2"));
            Assert.IsTrue(anCountry.ContainsKey("???"));
            Assert.AreEqual(
                (10 * 1000 * 1 + 10 * 1000 * 0.1M) / secTotal,
                anCountry["cnt1"].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.5M) / secTotal,
                anCountry["cnt2"].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.4M) / secTotal,
                anCountry["???"].Part);

            Assert.AreEqual(3, anSector.Count);
            Assert.IsTrue(anSector.ContainsKey("1"));
            Assert.IsTrue(anSector.ContainsKey("2"));
            Assert.IsTrue(anSector.ContainsKey("900"));
            Assert.AreEqual(
                (10 * 1000 * 1 + 10 * 1000 * 0.1M) / secTotal,
                anSector["1"].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.5M) / secTotal,
                anSector["2"].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.4M) / secTotal,
                anSector["900"].Part);

            Assert.AreEqual(5, anType.Count);
            Assert.IsTrue(anType.ContainsKey("share"));
            Assert.IsTrue(anType.ContainsKey("bond"));
            Assert.IsTrue(anType.ContainsKey("gold"));
            Assert.IsTrue(anType.ContainsKey("cash"));
            Assert.IsTrue(anType.ContainsKey("???"));
            Assert.AreEqual(
                (10 * 1000 * 1 + 10 * 1000 * 0.1M) / total,
                anType["share"].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.5M) / total,
                anType["bond"].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.1M) / total,
                anType["gold"].Part);
            Assert.AreEqual(
                (100 + 100M) / total,
                anType["cash"].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.3M) / total,
                anType["???"].Part);

        }

    }
}
