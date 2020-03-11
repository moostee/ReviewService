using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Model
{
    public class ReviewStatsModel
    {
        public int Rating5 { get; set; }
        public int Rating4 { get; set; }
        public int Rating3 { get; set; }
        public int Rating2 { get; set; }
        public int Rating1 { get; set; }
        public int TotalUsers { get; set; }
        public decimal AverageRatings { get; set; }
    }
}
