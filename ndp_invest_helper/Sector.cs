using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace ndp_invest_helper
{
    class Sector
    {
        public string Id;

        /// <summary>
        /// Уровень вложенности. 
        /// 1 - самый верхний, глобальные сектора экономики.
        /// 2 - более мелкое деление. И т.д.
        /// </summary>
        public int Level;

        /// <summary>
        /// Идентификатор родительского сектора для секторов с Level > 1.
        /// Если это сектор верхнего уровня, то 0.
        /// </summary>
        public int ParentId;

        public string Name;
    }

    class SectorsManager
    {
        public List<Sector> Sectors;

        public int LevelsCount;

        public static SectorsManager FromXmlFile(string filePath)
        {
            var sectorsManager = new SectorsManager();
            sectorsManager.ParseXmlFile(filePath);
            return sectorsManager;
        }

        public void ParseXmlFile(string filePath)
        {
            Sectors = new List<Sector>();
            LevelsCount = 0;

            var xRoot = XElement.Parse(File.ReadAllText(filePath));

            foreach (var xSector in xRoot.Elements("row"))
            {
                var sector = new Sector
                {
                    Id = xSector.Attribute("id").Value,
                    Level = int.Parse(xSector.Attribute("level").Value),
                    ParentId = int.Parse(xSector.Attribute("parent_id").Value),
                    Name = xSector.Attribute("name").Value
                };

                Sectors.Add(sector);

                LevelsCount = Math.Max(LevelsCount, sector.Level);
            }
        }

    }

}
