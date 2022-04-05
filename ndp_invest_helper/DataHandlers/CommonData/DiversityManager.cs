using ndp_invest_helper.DataHandlers;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ndp_invest_helper
{
    /// <summary>
    /// Base type for common data managers.
    /// </summary>
    public abstract class DiversityManager
    {
        private DataConnector DataConnector { get; set; }

        public List<DiversityItem> Items { get; protected set; }
            = new List<DiversityItem>();

        public Dictionary<int, DiversityItem> ById { get; protected set; }
            = new Dictionary<int, DiversityItem>();

        public Dictionary<string, DiversityItem> ByCode { get; protected set; }
            = new Dictionary<string, DiversityItem>();

        /// <summary>
        /// Item for unknown category. Useful when information is incomplete.
        /// </summary>
        public virtual DiversityItem Unknown { get => ById[0]; }

        public abstract void LoadFromDatabase();

        public abstract void LoadFromXmlText(string text);

        public abstract void LoadFromXmlFile(string filePath);

        protected virtual void FillDictionaries()
        {
            ById = Items.ToDictionary(x => x.Id, x => x);
            ByCode = Items.ToDictionary(x => x.Code, x => x);
        }

        public abstract void HandleXml(
            System.Xml.Linq.XElement xTag,
            Dictionary<DiversityItem, decimal> destination);

        protected void InnerHandleXml(
            System.Xml.Linq.XElement xTag,
            string attributeName,
            Dictionary<DiversityItem, decimal> destination)
        {
            var foundAttributes = Utils.HandleComplexStringXmlAttribute(
                xTag, attributeName, true, Unknown.Code.ToLower());

            foreach (var kvp in foundAttributes)
            {
                var item = Items.Find(x => x.Code == kvp.Key);

                if (item == null)
                {
                    throw new ArgumentException(string.Format(
                        "Обнаружен неизвестный элемент {0} в теге {1}",
                        kvp.Key, xTag.ToString()));
                }

                destination[item] = kvp.Value;
            }
        }

        public void UpdateItem(DiversityItem item)
        {
            InnerUpdateItem(item);
        }

        protected virtual void InnerUpdateItem(DiversityItem item)
        {

        }
    }
}
