using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Entity
{
    public class ReviewVoteType : BaseEntity<int>
    {
        public string Name { get; set; }

    }
}
