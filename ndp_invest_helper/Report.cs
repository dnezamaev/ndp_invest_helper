using System;
using System.Collections.Generic;
using System.Text;

namespace ndp_invest_helper
{
    abstract class Report
    {
        public Dictionary<Security, SecurityInfo> Securities = 
            new Dictionary<Security, SecurityInfo>();

        abstract public void ParseXmlFile(string xmlReportFilePath);
    }
}
