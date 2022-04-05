using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_invest_helper.DataHandlers
{
    /// <summary>
    /// Base class for read/write data sources like XML files, SQL DB.
    /// </summary>
    public abstract class DataConnector
    {
        // TODO: add GetItem methods - by id, by code, all items...

        /// <summary>
        /// Save data to storage.
        /// </summary>
        /// <param name="item"></param>
        public abstract void UpdateItem(DiversityItem item);
    }
}
