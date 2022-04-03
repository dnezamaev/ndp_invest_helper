using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Linq;

using Country = ndp_invest_helper.Models.Country;
using ndp_invest_helper.Models;

namespace ndp_invest_helper
{
    public class CountriesManager : DiversityManager
    {
        public Dictionary<string, HashSet<string>> ByDevelopment { get; private set; }
            = new Dictionary<string, HashSet<string>>();

        public Dictionary<string, HashSet<string>> ByRegion { get; private set; } 
            = new Dictionary<string, HashSet<string>>();

        public override void LoadFromDatabase()
        {
            Items = DatabaseManager.GetFullTable<Country>("Countries")
                .Cast<DiversityItem>().ToList();

            FillDictionaries();

            ByDevelopment = DatabaseManager
                .GetFullTable<CountryDevelopmentLevel>("CountryDevelopmentLevels")
                .ToDictionary(
                x => x.NameRus,
                x => Items
                    .Where(c => ((Country)c).DevelopmentLevel == x.Id)
                    .Select(c => c.Code)
                    .ToHashSet()
                    );

            ByRegion = DatabaseManager
                .GetFullTable<CountryRegion>("CountryRegions").
                ToDictionary(
                x => x.NameRus,
                x => Items
                    .Where(c => ((Country)c).RegionId == x.Id)
                    .Select(c => c.Code)
                    .ToHashSet()
                    );
        }

        public override void LoadFromXmlText(string text)
        {
            Items.Clear();

            var xRoot = XElement.Parse(text);

            foreach (var xCountry in xRoot.Element("countries").Elements("country"))
            {
                var country = new Country 
                { 
                    Id = int.Parse(xCountry.Attribute("number").Value),
                    Code = xCountry.Attribute("alpha2").Value,
                };

                var xNameEng = xCountry.Attribute("name_en");
                country.NameEng = xNameEng == null ? "" : xNameEng.Value;

                var xNameRus = xCountry.Attribute("name_ru");
                country.NameRus = xNameRus == null ? "" : xNameRus.Value;

                var xCode3 = xCountry.Attribute("alpha3");
                country.Code3 = xCode3 == null ? "" : xCode3.Value;

                var xNameRusFull = xCountry.Attribute("name_ru_full");
                country.NameRusFull = xNameRusFull == null ? "" : xNameRusFull.Value;

                Items.Add(country);
            }

            FillDictionaries();

            foreach (var xRegion in xRoot.Element("by_region").Elements())
            {
                ByRegion[xRegion.Attribute("key").Value] =
                    xRegion.Elements().Select(x => x.Attribute("key").Value).ToHashSet();
            }

            foreach (var xLevel in xRoot.Element("by_development_level").Elements())
            {
                ByDevelopment[xLevel.Attribute("key").Value] =
                    xLevel.Elements().Select(x => x.Attribute("key").Value).ToHashSet();
            }
        }

        public override void LoadFromXmlFile(string filePath)
        {
            LoadFromXmlText(File.ReadAllText(filePath));
        }

        public override void HandleXml(XElement xTag, Dictionary<DiversityItem, decimal> destination)
        {
            InnerHandleXml(xTag, "country", destination);
        }
    }
}
