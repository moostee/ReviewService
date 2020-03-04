using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Entity
{

    /// <summary>
    /// App Class
    /// </summary>
    public class App : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
