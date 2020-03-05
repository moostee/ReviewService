using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Model
{
    public class ReviewVoteTypeModel : BaseModel<int>
    {
        public string Name { get; set; }

    }
}
