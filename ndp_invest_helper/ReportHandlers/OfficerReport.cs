using System;
using System.Linq;
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

        public string AddressFull { get; set; }

        public string AddressIndex 
        {
            get
            {
                var splittedAddress = AddressFull.Split(',');

                if // index is digits, ends with comma
                (
                    splittedAddress.Length == 1 || 
                    false == splittedAddress[0].All(x => char.IsDigit(x))
                )
                {
                    return null; // no comma, or not all digits in 1st split
                }

                return splittedAddress[0];
            } 
        }

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
