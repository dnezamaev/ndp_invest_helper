using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_invest_helper.DataHandlers.Models
{
    interface IDevirsityElement
    {
        int Id { get; }

        string NameRus { get; }

        string NameEng { get; }
    }
}
