using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace ndp_invest_helper
{
    class MoexShareInfo
    {
        public string Isin;
        public decimal Price;
    }

    class Moex
    {
        public List<MoexShareInfo> Shares;
        public void ParseSharesXmlFile(string filePath)
        {
            Shares = new List<MoexShareInfo>();

            var xRoot = XElement.Parse(File.ReadAllText(filePath));

            foreach (var xShare in xRoot.Elements("row"))
            {
                var share = new MoexShareInfo
                {
                    Isin = xShare.Attribute("ISIN").Value,
                    Price = decimal.Parse(xShare.Attribute("PREVPRICE").Value),
                };

                Shares.Add(share);
            }
        }
    }
}
