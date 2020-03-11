using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Model
{
    /// <summary>
    /// ReviewVote View Model
    /// </summary> 
    public class ReviewVoteModel : BaseModel<Guid>
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
        public bool IsActive { get; set; } = true;

    }
}
