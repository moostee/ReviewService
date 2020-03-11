using ReviewsService_Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Model
{
    /// <summary>
    /// Review View Model
    /// </summary> 
    public class ReviewModel : BaseModel<Guid>
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

        public int ReviewUpVotes { get; set; }

        public int ReviewDownVotes { get; set; }

    }
}
