using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public enum AssetTypeEnum
    {
        Unknown = 0, Share, Bond, Etf, Gold, Cash
    }

    public partial class AssetType : DiversityItem
    {
        public override string Code { get => NameEng.ToLower(); }

        public static AssetType Unknown = new AssetType
        {
            Id = (int)AssetTypeEnum.Unknown,
            NameEng = "Unknown",
            NameRus = "Неизвестно"
        };

        public static AssetType Share = new AssetType
        {
            Id = (int)AssetTypeEnum.Share,
            NameEng = "Share",
            NameRus = "Акция"
        };

        public static AssetType Bond = new AssetType
        {
            Id = (int)AssetTypeEnum.Bond,
            NameEng = "Bond",
            NameRus = "Облигация"
        };

        public static AssetType Etf = new AssetType
        {
            Id = (int)AssetTypeEnum.Etf,
            NameEng = "ETF",
            NameRus = "Фонд"
        };

        public static AssetType Gold = new AssetType
        {
            Id = (int)AssetTypeEnum.Gold,
            NameEng = "Gold",
            NameRus = "Золото"
        };

        public static AssetType Cash = new AssetType
        {
            Id = (int)AssetTypeEnum.Cash,
            NameEng = "Cash",
            NameRus = "Деньги"
        };

        public static Dictionary<AssetTypeEnum, AssetType> All
            = new Dictionary<AssetTypeEnum, AssetType>()
            {
                {
                    AssetTypeEnum.Unknown,
                    AssetType.Unknown
                },
                {
                    AssetTypeEnum.Share,
                    AssetType.Share
                },
                {
                    AssetTypeEnum.Bond,
                    AssetType.Bond
                },
                {
                    AssetTypeEnum.Etf,
                    AssetType.Etf
                },
                {
                    AssetTypeEnum.Gold,
                    AssetType.Gold
                },
                {
                    AssetTypeEnum.Cash,
                    AssetType.Cash
                }
            };

        public static Dictionary<AssetTypeEnum, AssetType> Securities
            = new Dictionary<AssetTypeEnum, AssetType>()
            {
                {
                    AssetTypeEnum.Share,
                    AssetType.Share
                },
                {
                    AssetTypeEnum.Bond,
                    AssetType.Bond
                },
                {
                    AssetTypeEnum.Etf,
                    AssetType.Etf
                },
            };

        // In case Predefined will be loaded from database.

        //public static AssetType Unknown { get => Predefined[AssetTypeEnum.Unknown]; }

        //public static AssetType Share { get => Predefined[AssetTypeEnum.Share]; }

        //public static AssetType Bond { get => Predefined[AssetTypeEnum.Bond]; }

        //public static AssetType Etf { get => Predefined[AssetTypeEnum.Etf]; }

        //public static AssetType Gold { get => Predefined[AssetTypeEnum.Gold]; }

        //public static AssetType Cash { get => Predefined[AssetTypeEnum.Cash]; }

    }
}
