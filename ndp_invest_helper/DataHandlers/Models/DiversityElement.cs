using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ndp_invest_helper.Models
{
    /// <summary>
    /// Критерий диверсификации: валюта, сектор экономики, страна, тип актива...
    /// </summary>
    public abstract class DiversityElement : IEquatable<DiversityElement>
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
            return Id == ((DiversityElement)obj).Id;
        }

        public bool Equals(DiversityElement other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
