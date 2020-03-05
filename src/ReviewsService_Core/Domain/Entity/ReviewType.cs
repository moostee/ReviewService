using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Entity
{/// <summary>
 /// ReviewType Class
 /// </summary>
    public class ReviewType : BaseEntity<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

    }

}
