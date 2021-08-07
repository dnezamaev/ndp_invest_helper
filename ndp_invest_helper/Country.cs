using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace ndp_invest_helper
{
    public static class CountriesManager
    {
        public static Dictionary<string, string> Countries = 
            new Dictionary<string, string>();

        public static Dictionary<string, HashSet<string>> ByDevelopment =
            new Dictionary<string, HashSet<string>>();

        public static Dictionary<string, HashSet<string>> ByRegion =
            new Dictionary<string, HashSet<string>>();

        public static void ParseXmlFile(string filePath)
        {
            var xRoot = XElement.Parse(File.ReadAllText(filePath));

            foreach (var xCountry in xRoot.Element("countries").Elements("country"))
            {
                Countries[xCountry.Attribute("key").Value] = 
                    xCountry.Attribute("name").Value;
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
    }
}
