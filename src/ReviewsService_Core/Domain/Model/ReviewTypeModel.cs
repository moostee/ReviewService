using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Model
{
    /// <summary>
    /// ReviewType View Model
    /// </summary> 
    public class ReviewTypeModel : BaseModel<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

    }
}
