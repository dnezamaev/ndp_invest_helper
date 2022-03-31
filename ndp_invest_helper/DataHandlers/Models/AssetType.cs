using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public enum AssetTypes
    {
        Share = 1, Bond, Etf, Gold, Cash
    }

    public partial class AssetType
    {
        public long Id { get; set; }
        public string NameEng { get; set; }
        public string NameRus { get; set; }

        public static AssetType[] Predefined
            = new AssetType[]
            {
                new AssetType
                {
                    Id = (long)AssetTypes.Share,
                    NameEng = "Share",
                    NameRus = "Акция"
                },
                new AssetType
                {
                    Id = (long)AssetTypes.Bond,
                    NameEng = "Bond",
                    NameRus = "Облигация"
                },
                new AssetType
                {
                    Id = (long)AssetTypes.Etf,
                    NameEng = "ETF",
                    NameRus = "Фонд"
                },
                new AssetType
                {
                    Id = (long)AssetTypes.Gold,
                    NameEng = "Gold",
                    NameRus = "Золото"
                },
                new AssetType
                {
                    Id = (long)AssetTypes.Cash,
                    NameEng = "Cash",
                    NameRus = "Деньги"
                },
            };
    }
}
