using System;
using System.Collections.Generic;

namespace ndp_invest_helper.Models
{
    public partial class EconomySector : DiversityElement
    {
        /// <summary>
        /// Уровень вложенности. 
        /// 1 - самый верхний, глобальные сектора экономики.
        /// 2 - более мелкое деление. И т.д.
        /// </summary>
        public int Level { get; set; }

        public override string Code { get => Id.ToString(); }

        /// <summary>
        /// Идентификатор родительского сектора для секторов с Level > 1.
        /// Если это сектор верхнего уровня, то 0.
        /// </summary>
        public int ParentId { get; set; }
    }
}
