using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Entity
{
    /// <summary>
    /// ReviewVote Class
    /// </summary>
    public class ReviewVote : BaseEntity<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ReviewId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ReviewVoteTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

    }

}
