using System;

namespace ndp_invest_helper.DataHandlers
{
    /// <summary>
    /// Критерий диверсификации: валюта, сектор экономики, страна, тип актива...
    /// </summary>
    public abstract class DiversityItem : IEquatable<DiversityItem>
    {
        public int Id { get; set; }

        public virtual string Code { get; set; }

        public string NameRus { get; set; }

        public string NameEng { get; set; }

        public string FriendlyName
        {
            get
            {
                if (!string.IsNullOrEmpty(NameRus))
                {
                    return NameRus;
                }
                else if (!string.IsNullOrEmpty(NameEng))
                {
                    return NameEng;
                }
                else if (!string.IsNullOrEmpty(Code))
                {
                    return Code;
                }

                return Id.ToString();
            }
        }

        public override bool Equals(object obj)
        {
            return Id == ((DiversityItem)obj).Id;
        }

        public bool Equals(DiversityItem other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
