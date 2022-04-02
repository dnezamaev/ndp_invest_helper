using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Linq;

using ndp_invest_helper.Models;

namespace ndp_invest_helper
{
    public static class SectorsManager
    {
        public static List<EconomySector> Sectors;

        public static Dictionary<int, EconomySector> ById;

        /// <summary>
        /// Количество уровней вложенности.
        /// </summary>
        public static int LevelsCount;

        /// <summary>
        /// Номер сектора по умолчанию для неизвестных секторов.
        /// Первый уровень вложенности.
        /// </summary>
        public const int DefaultSectorIdLevel1 = 900;

        /// <summary>
        /// Номер сектора по умолчанию для неизвестных секторов.
        /// Второй уровень вложенности.
        /// </summary>
        public const int DefaultSectorIdLevel2 = 999;

        /// <summary>
        /// Сектор по умолчанию для неизвестных секторов.
        /// </summary>
        public static EconomySector DefaultSector { get => ById[DefaultSectorIdLevel2]; }

        /// <summary>
        /// Получить родительский сектор на один уровень выше.
        /// </summary>
        /// <param name="sector">Сектор, для которого ищем родителя.</param>
        /// <returns>Родительский сектор.</returns>
        public static EconomySector GetParent(EconomySector sector)
        {
            return ById[sector.ParentId];
        }

        /// <summary>
        /// Получить родительский сектор указанного уровеня.
        /// </summary>
        /// <param name="sector">Сектор, для которого ищем родителя.</param>
        /// <param name="parentLevel">Уровень родителя.</param>
        /// <returns>Родительский сектор.</returns>
        public static EconomySector GetParent(EconomySector sector, int parentLevel)
        {
            while (parentLevel < sector.Level)
                sector = GetParent(sector);

            return sector;
        }

        private static void Init()
        {
            Sectors = new List<EconomySector>();
            ById = new Dictionary<int, EconomySector>();
            LevelsCount = 0;
        }

        public static void LoadFromDatabase()
        {
            Init();

            Sectors = DatabaseManager.GetFullTable<EconomySector>("EconomySectors");

            ById = Sectors.ToDictionary(k => k.Id, v => v);
        }

        public static void LoadFromXmlFile(string filePath)
        {
            Init();

            var xRoot = XElement.Parse(File.ReadAllText(filePath));

            foreach (var xSector in xRoot.Elements("row"))
            {
                var sector = new EconomySector
                {
                    Id = int.Parse(xSector.Attribute("id").Value),
                    Level = int.Parse(xSector.Attribute("level").Value),
                    ParentId = int.Parse(xSector.Attribute("parent_id").Value),
                    NameRus = xSector.Attribute("name_ru").Value,
                    NameEng = xSector.Attribute("name_en").Value
                };

                Sectors.Add(sector);
                ById[sector.Id] = sector;

                LevelsCount = Math.Max(LevelsCount, sector.Level);
            }
        }
    }

}
