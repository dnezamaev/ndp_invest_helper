using System;
using System.Collections.Generic;

namespace ndp_invest_helper.DataHandlers
{
    /// <summary>
    /// Тип актива.
    /// </summary>
    public enum AssetTypeEnum
    {
        Unknown = 0, Share, Bond, Etf, Gold, Cash
    }

    /// <summary>
    /// Содержимое фонда.
    /// </summary>
    public partial class AssetType : DiversityItem
    {
        public override string Code { get => NameEng.ToLower(); }
    }
}
