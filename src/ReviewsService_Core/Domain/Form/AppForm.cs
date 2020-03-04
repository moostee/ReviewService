using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Form
{
    public class AppForm : BaseForm<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
