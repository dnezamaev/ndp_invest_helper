using System.Collections.Generic;
using System.Xml.Linq;

namespace ndp_invest_helper.DataHandlers
{
    public class AssetTypesManager : DiversityManager
    {
        public override DiversityItem Unknown { get; } = new AssetType
        {
            Id = (int)AssetTypeEnum.Unknown,
            NameEng = "Unknown",
            NameRus = "Неизвестно"
        };

        public DiversityItem Share { get; } = new AssetType
        {
            Id = (int)AssetTypeEnum.Share,
            NameEng = "Share",
            NameRus = "Акция"
        };

        public DiversityItem Bond { get; } = new AssetType
        {
            Id = (int)AssetTypeEnum.Bond,
            NameEng = "Bond",
            NameRus = "Облигация"
        };

        public DiversityItem Etf { get; } = new AssetType
        {
            Id = (int)AssetTypeEnum.Etf,
            NameEng = "ETF",
            NameRus = "Фонд"
        };

        public DiversityItem Gold { get; } = new AssetType
        {
            Id = (int)AssetTypeEnum.Gold,
            NameEng = "Gold",
            NameRus = "Золото"
        };

        public DiversityItem Cash { get; } = new AssetType
        {
            Id = (int)AssetTypeEnum.Cash,
            NameEng = "Cash",
            NameRus = "Деньги"
        };

        public Dictionary<AssetTypeEnum, DiversityItem> ByType { get; private set; }

        public Dictionary<AssetTypeEnum, DiversityItem> Securities { get; private set; }

        public AssetTypesManager()
        {
            Items = new List<DiversityItem>
                { Unknown, Share, Bond, Etf, Gold, Cash };

            ByType = new Dictionary<AssetTypeEnum, DiversityItem>()
            {
                { AssetTypeEnum.Unknown, Unknown },
                { AssetTypeEnum.Share, Share },
                { AssetTypeEnum.Bond, Bond },
                { AssetTypeEnum.Etf, Etf },
                { AssetTypeEnum.Gold, Gold },
                { AssetTypeEnum.Cash, Cash }
            };

            Securities = new Dictionary<AssetTypeEnum, DiversityItem>()
            {
                { AssetTypeEnum.Share, Share },
                { AssetTypeEnum.Bond, Bond },
                { AssetTypeEnum.Etf, Etf },
            };

            FillDictionaries();
        }

        // Dummy methods, not required for Asset.
        public override void LoadFromDatabase()
        {
        }

        public override void LoadFromXmlText(string text)
        {
        }

        public override void LoadFromXmlFile(string filePath)
        {
        }

        public override void HandleXml(XElement xTag, Dictionary<DiversityItem, decimal> destination)
        {
            InnerHandleXml(xTag, "what_inside", destination);
        }
    }
}
