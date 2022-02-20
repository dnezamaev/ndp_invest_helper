using System;
using System.Collections.Generic;

namespace ndp_invest_helper.ReportHandlers
{
    /// <summary>
    /// Запись об акциях в справке для госслужащих.
    /// </summary>
    public class OfficerReportShareInfo
    {
        public string Issuer { get; set; }

        public string ShareTypeString { get; set; }

        public string Address { get; set; }

        public string AuthorizedCapital { get; set; }

        public string Price { get; set; }

        public string Count { get; set; }

        public Models.ShareType TypeOfShare { get; set; }
            = Models.ShareType.Unknown;

        public Models.IssuerType TypeOfIssuer { get; set; }
            = Models.IssuerType.Unknown;
    }

    public class OfficerReport
    {
        public string FilePath { get; private set; }

        public List<OfficerReportShareInfo> Shares { get; private set; }

        public virtual void ParseFile(string filePath)
        {
            FilePath = filePath;
            Shares = new List<OfficerReportShareInfo>();
        }
    }
}
