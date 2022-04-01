using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class EconomySector
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string NameEng { get; set; }
        public string NameRus { get; set; }
        public int ParentId { get; set; }
    }
}
