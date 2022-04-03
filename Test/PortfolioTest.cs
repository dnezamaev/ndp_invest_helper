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
            CommonData.Sectors.LoadFromXmlFile(@"data\info\sectors.xml");
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

            CommonData.Currencies.LoadFromXmlText
            ( @"
                <root>
                <currency number='0' code='???' rate='1' name_en='unknown' />
                <currency number='1' code='CUR1' rate='1' name_en='unknown' />
                <currency number='2' code='issuer currency' rate='1' name_en='' />
                <currency number='3' code='issuer inner currency 1' rate='1' name_en='' />
                <currency number='4' code='issuer inner currency 2' rate='1' name_en='' />
                <currency number='5' code='bond currency' rate='1' name_en='' />
                </root>
            " );

            CommonData.Countries.LoadFromXmlText
            ( @"
                <root>
                  <countries>
                    <country number='0' alpha2='???' name=''/>
                    <country number='1' alpha2='US' name=''/>
                    <country number='2' alpha2='CN' name=''/>
                    <country number='3' alpha2='issuer country' name=''/>
                    <country number='4' alpha2='issuer inner country 1' name=''/>
                    <country number='5' alpha2='issuer inner country 2' name=''/>
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
                CommonData.Currencies.ByCode["issuer inner currency 1"]));
            Assert.IsTrue(issuer.Currencies.Keys.Contains(
                CommonData.Currencies.ByCode["issuer inner currency 2"]));
            Assert.AreEqual(0.2M, issuer.Currencies[
                CommonData.Currencies.ByCode["issuer inner currency 1"]]);
            Assert.AreEqual(0.8M, issuer.Currencies[
                CommonData.Currencies.ByCode["issuer inner currency 2"]]);

            Assert.AreEqual(2, issuer.Countries.Count);
            Assert.IsTrue(issuer.Countries.ContainsKey(
                CommonData.Countries.ByCode["issuer inner country 1"]));
            Assert.IsTrue(issuer.Countries.ContainsKey(
                CommonData.Countries.ByCode["issuer inner country 2"]));
            Assert.AreEqual(0.2M, issuer.Countries[
                CommonData.Countries.ByCode["issuer inner country 1"]]);
            Assert.AreEqual(0.8M, issuer.Countries[
                CommonData.Countries.ByCode["issuer inner country 2"]]);

            Assert.AreEqual(2, issuer.Sectors.Count);
            Assert.IsTrue(issuer.Sectors.ContainsKey(CommonData.Sectors.ById[2]));
            Assert.IsTrue(issuer.Sectors.ContainsKey(CommonData.Sectors.ById[3]));
            Assert.AreEqual(0.2M, issuer.Sectors[CommonData.Sectors.ById[2]]);
            Assert.AreEqual(0.8M, issuer.Sectors[CommonData.Sectors.ById[3]]);

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
                CommonData.Currencies.ByCode["CUR1"]));
            Assert.IsTrue(fullEtf.Currencies.ContainsKey(
                CommonData.Currencies.ByCode["???"]));
            Assert.AreEqual(0.1M, fullEtf.Currencies[
                CommonData.Currencies.ByCode["CUR1"]]);
            Assert.AreEqual(0.9M, fullEtf.Currencies[
                CommonData.Currencies.ByCode["???"]]);

            Assert.AreEqual(2, fullEtf.Countries.Count);
            Assert.IsTrue(fullEtf.Countries.ContainsKey(
                CommonData.Countries.ByCode["CN"]));
            Assert.IsTrue(fullEtf.Countries.ContainsKey(
                CommonData.Countries.ByCode["???"]));
            Assert.AreEqual(0.2M, fullEtf.Countries[
                CommonData.Countries.ByCode["CN"]]);
            Assert.AreEqual(0.8M, fullEtf.Countries[
                CommonData.Countries.ByCode["???"]]);

            Assert.AreEqual(2, fullEtf.Sectors.Count);
            Assert.IsTrue(fullEtf.Sectors.ContainsKey(CommonData.Sectors.ById[5]));
            Assert.IsTrue(fullEtf.Sectors.ContainsKey(CommonData.Sectors.DefaultSector));
            Assert.AreEqual(0.3M, fullEtf.Sectors[CommonData.Sectors.ById[5]]);
            Assert.AreEqual(0.7M, fullEtf.Sectors[CommonData.Sectors.DefaultSector]);

            Assert.AreEqual(2, fullEtf.WhatInside.Count);
            Assert.IsTrue(fullEtf.WhatInside.ContainsKey(AssetType.Bond));
            Assert.IsTrue(fullEtf.WhatInside.ContainsKey(AssetType.Unknown));
            Assert.AreEqual(0.4M, fullEtf.WhatInside[AssetType.Bond]);
            Assert.AreEqual(0.6M, fullEtf.WhatInside[AssetType.Unknown]);

            // У пустой бумаги характеристики берутся от эмитента.
            var emptySec = SecuritiesManager.SecuritiesByTicker["EMPTY"];

            Assert.AreEqual(2, emptySec.Countries.Count);
            Assert.IsTrue(emptySec.Countries.ContainsKey(
                CommonData.Countries.ByCode["issuer inner country 1"]));
            Assert.IsTrue(emptySec.Countries.ContainsKey(
                CommonData.Countries.ByCode["issuer inner country 2"]));
            Assert.AreEqual(0.2M, emptySec.Countries[
                CommonData.Countries.ByCode["issuer inner country 1"]]);
            Assert.AreEqual(0.8M, emptySec.Countries[
                CommonData.Countries.ByCode["issuer inner country 2"]]);

            Assert.AreEqual(2, emptySec.Sectors.Count);
            Assert.IsTrue(emptySec.Sectors.ContainsKey(CommonData.Sectors.ById[2]));
            Assert.IsTrue(emptySec.Sectors.ContainsKey(CommonData.Sectors.ById[3]));
            Assert.AreEqual(0.2M, emptySec.Sectors[CommonData.Sectors.ById[2]]);
            Assert.AreEqual(0.8M, emptySec.Sectors[CommonData.Sectors.ById[3]]);

            var currencyBond = SecuritiesManager.SecuritiesByIsin["bond with currency"];

            Assert.AreEqual(1, currencyBond.Currencies.Count);
            Assert.IsTrue(currencyBond.Currencies.ContainsKey(
                CommonData.Currencies.ByCode["bond currency"]));
            Assert.AreEqual(1M, currencyBond.Currencies[
                CommonData.Currencies.ByCode["bond currency"]]);
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

            CommonData.Currencies.LoadFromXmlText
            ( @"
                <root>
                <currency number='1' code='???' rate='1' name_en='unknown' />
                <currency number='2' code='cur1' rate='1' name_en='cur1' />
                <currency number='3' code='cur2' rate='1' name_en='cur2' />
                </root>
            " );

            CommonData.Countries.LoadFromXmlText
            ( @"
                <root>
                  <countries>
                    <country alpha2='???' number='1' name='unknown'/>
                    <country alpha2='cnt1' number='2' name='cnt1'/>
                    <country alpha2='cnt2' number='3' name='cnt2'/>
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

            var currency1 = (Currency)CommonData.Currencies.ByCode["cur1"];
            var currency2 = (Currency)CommonData.Currencies.ByCode["cur2"];

            var cashDict = new Dictionary<Currency, decimal>
                {
                    { currency1, 100 },
                    { currency2, 100 },
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
            Assert.IsTrue(anCurrency.ContainsKey(CommonData.Currencies.ByCode["cur1"]));
            Assert.IsTrue(anCurrency.ContainsKey(CommonData.Currencies.ByCode["cur2"]));
            Assert.IsTrue(anCurrency.ContainsKey(CommonData.Currencies.ByCode["???"]));
            Assert.AreEqual(
                (100 + 10 * 1000 * 1 + 10 * 1000 * 0.1M) / total, 
                anCurrency[CommonData.Currencies.ByCode["cur1"]].Part);
            Assert.AreEqual(
                (100 + 10 * 1000 * 0.5M) / total, 
                anCurrency[CommonData.Currencies.ByCode["cur2"]].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.4M) / total, 
                anCurrency[CommonData.Currencies.ByCode["???"]].Part);

            var country1 = CommonData.Countries.ByCode["cnt1"];
            var country2 = CommonData.Countries.ByCode["cnt2"];
            var countryUnknown = CommonData.Countries.ByCode["???"];

            Assert.AreEqual(3, anCountry.Count);
            Assert.IsTrue(anCountry.ContainsKey(country1));
            Assert.IsTrue(anCountry.ContainsKey(country2));
            Assert.IsTrue(anCountry.ContainsKey(countryUnknown));
            Assert.AreEqual(
                (10 * 1000 * 1 + 10 * 1000 * 0.1M) / secTotal,
                anCountry[country1].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.5M) / secTotal,
                anCountry[country2].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.4M) / secTotal,
                anCountry[countryUnknown].Part);

            var sector1 = CommonData.Sectors.ById[1];
            var sector2 = CommonData.Sectors.ById[2];
            var sector900 = CommonData.Sectors.ById[900];

            Assert.AreEqual(3, anSector.Count);
            Assert.IsTrue(anSector.ContainsKey(sector1));
            Assert.IsTrue(anSector.ContainsKey(sector2));
            Assert.IsTrue(anSector.ContainsKey(sector900));
            Assert.AreEqual(
                (10 * 1000 * 1 + 10 * 1000 * 0.1M) / secTotal,
                anSector[sector1].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.5M) / secTotal,
                anSector[sector2].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.4M) / secTotal,
                anSector[sector900].Part);

            Assert.AreEqual(5, anType.Count);
            Assert.IsTrue(anType.ContainsKey(AssetType.Share));
            Assert.IsTrue(anType.ContainsKey(AssetType.Bond));
            Assert.IsTrue(anType.ContainsKey(AssetType.Gold));
            Assert.IsTrue(anType.ContainsKey(AssetType.Cash));
            Assert.IsTrue(anType.ContainsKey(AssetType.Unknown));
            Assert.AreEqual(
                (10 * 1000 * 1 + 10 * 1000 * 0.1M) / total,
                anType[AssetType.Share].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.5M) / total,
                anType[AssetType.Bond].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.1M) / total,
                anType[AssetType.Gold].Part);
            Assert.AreEqual(
                (100 + 100M) / total,
                anType[AssetType.Cash].Part);
            Assert.AreEqual(
                (10 * 1000 * 0.3M) / total,
                anType[AssetType.Unknown].Part);
        }

    }
}
