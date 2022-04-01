using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Linq;

using Country = ndp_invest_helper.Models.Country;

namespace ndp_invest_helper
{
    public static class CountriesManager
    {
        public static List<Country> Countries;

        public static Dictionary<int, Country> ById;

        public static Dictionary<string, Country> ByCode;

        public static Dictionary<string, HashSet<string>> ByDevelopment;

        public static Dictionary<string, HashSet<string>> ByRegion;

        private static void Init()
        {
            Countries = new List<Country>();
            ById = new Dictionary<int, Country>();
            ByCode = new Dictionary<string, Country>();
            ByDevelopment = new Dictionary<string, HashSet<string>>();
            ByRegion = new Dictionary<string, HashSet<string>>();
        }

        public static void LoadFromDatabase()
        {
            Init();

            Countries = DatabaseManager.GetFullTable<Country>("Countries");

            ByCode = Countries.ToDictionary(x => x.Code, x => x);

            ById = Countries.ToDictionary(x => x.Id, x => x);

            ByDevelopment = DatabaseManager
                .GetFullTable<Models.CountryDevelopmentLevel>("CountryDevelopmentLevels")
                .ToDictionary(
                x => x.NameRus,
                x => Countries
                    .Where(c => c.DevelopmentLevel == x.Id)
                    .Select(c => c.Code)
                    .ToHashSet()
                    );

            ByRegion = DatabaseManager
                .GetFullTable<Models.CountryRegion>("CountryRegions").
                ToDictionary(
                x => x.NameRus,
                x => Countries
                    .Where(c => c.RegionId == x.Id)
                    .Select(c => c.Code)
                    .ToHashSet()
                    );
        }

        public static void LoadFromXmlText(string text)
        {
            Init();

            var xRoot = XElement.Parse(text);

            foreach (var xCountry in xRoot.Element("countries").Elements("country"))
            {
                var country = new Country 
                { 
                    Id = int.Parse(xCountry.Attribute("number").Value),
                    Code = xCountry.Attribute("alpha2").Value,
                    Code3 = xCountry.Attribute("alpha3").Value,
                    NameRus = xCountry.Attribute("name_ru").Value,
                    NameRusFull = xCountry.Attribute("name_ru_full").Value,
                    NameEng = xCountry.Attribute("name_en").Value
                };

                Countries.Add(country);
                ByCode[country.Code] = country;
            }

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

        public static void LoadFromXmlFile(string filePath)
        {
            LoadFromXmlText(File.ReadAllText(filePath));
        }
    }
}
