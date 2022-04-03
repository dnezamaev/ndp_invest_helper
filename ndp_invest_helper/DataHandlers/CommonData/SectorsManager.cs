using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Linq;

using ndp_invest_helper.Models;

namespace ndp_invest_helper
{
    public class SectorsManager : DiversityManager
    {
        /// <summary>
        /// Количество уровней вложенности.
        /// </summary>
        public int LevelsCount { get; private set; }

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

        public override DiversityItem Unknown => ById[DefaultSectorIdLevel2];

        /// <summary>
        /// Сектор по умолчанию для неизвестных секторов.
        /// </summary>
        public EconomySector DefaultSector { get => (EconomySector)Unknown; }

        /// <summary>
        /// Получить родительский сектор на один уровень выше.
        /// </summary>
        /// <param name="sector">Сектор, для которого ищем родителя.</param>
        /// <returns>Родительский сектор.</returns>
        public EconomySector GetParent(EconomySector sector)
        {
            return (EconomySector)ById[sector.ParentId];
        }

        /// <summary>
        /// Получить родительский сектор указанного уровеня.
        /// </summary>
        /// <param name="sector">Сектор, для которого ищем родителя.</param>
        /// <param name="parentLevel">Уровень родителя.</param>
        /// <returns>Родительский сектор.</returns>
        public EconomySector GetParent(EconomySector sector, int parentLevel)
        {
            while (parentLevel < sector.Level)
                sector = GetParent(sector);

            return sector;
        }

        private void Init()
        {
            LevelsCount = 0;
        }

        public override void LoadFromDatabase()
        {
            Items = DatabaseManager.GetFullTable<EconomySector>("EconomySectors")
                .Cast<DiversityItem>().ToList();

            FillDictionaries();

            LevelsCount = Items.Max(x => ((EconomySector)x).Level);
        }

        public override void LoadFromXmlText(string text)
        {
            Items.Clear();

            var xRoot = XElement.Parse(text);

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

                Items.Add(sector);

                LevelsCount = Math.Max(LevelsCount, sector.Level);
            }

            FillDictionaries();
        }

        public override void LoadFromXmlFile(string filePath)
        {
            LoadFromXmlText(File.ReadAllText(filePath));
        }

        public override void HandleXml(XElement xTag, Dictionary<DiversityItem, decimal> destination)
        {
            InnerHandleXml(xTag, "sector", destination);
        }
    }
}
