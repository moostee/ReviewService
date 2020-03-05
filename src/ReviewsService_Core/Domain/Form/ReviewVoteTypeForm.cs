using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Form
{
    public class ReviewVoteTypeForm : BaseForm<int>
    {
        public string Name { get; set; }

    }

}
