using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace ndp_invest_helper
{
    class Sector
    {
        /// <summary>
        /// Уникальный идентификатор сектора.
        /// По идее должен быть int, но для единообразия в группировке строка.
        /// Секторов немного, так что проблем со скоростью/памятью не должно возникнуть.
        /// Если будет сильно тормозить, можно заменить на свойство с кэшируемым значением.
        /// </summary>
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
        public string ParentId;

        public string Name;
    }

    static class SectorsManager
    {
        public static List<Sector> Sectors;

        public static Dictionary<string, Sector> ById;

        public static int LevelsCount;

        public static Sector GetParent(Sector sector)
        {
            return ById[sector.ParentId];
        }

        public static Sector GetParent(Sector sector, int parentLevel)
        {
            while (parentLevel < sector.Level)
                sector = GetParent(sector);

            return sector;
        }

        public static void ParseXmlFile(string filePath)
        {
            Sectors = new List<Sector>();
            ById = new Dictionary<string, Sector>();
            LevelsCount = 0;

            var xRoot = XElement.Parse(File.ReadAllText(filePath));

            foreach (var xSector in xRoot.Elements("row"))
            {
                var sector = new Sector
                {
                    Id = xSector.Attribute("id").Value,
                    Level = int.Parse(xSector.Attribute("level").Value),
                    ParentId = xSector.Attribute("parent_id").Value,
                    Name = xSector.Attribute("name").Value
                };

                Sectors.Add(sector);
                ById[sector.Id] = sector;

                LevelsCount = Math.Max(LevelsCount, sector.Level);
            }
        }
    }

}
