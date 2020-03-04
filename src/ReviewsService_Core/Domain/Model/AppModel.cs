using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Model
{
    public class AppModel : BaseModel<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
