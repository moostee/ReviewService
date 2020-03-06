using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Form
{
    /// <summary>
    /// ReviewVote Form
    /// </summary>
    public class ReviewVoteForm : BaseForm<Guid>
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

    }
}
