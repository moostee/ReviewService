using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Entity
{
    /// <summary>
    /// Review Class
    /// </summary>
    public class Review : BaseEntity<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public long AppClientId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Rating { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AppFeature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long ReviewTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ParentId { get; set; }

    }
}
