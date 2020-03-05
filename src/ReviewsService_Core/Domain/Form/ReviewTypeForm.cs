using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Form
{
    /// <summary>
    /// ReviewType Form
    /// </summary>
    public class ReviewTypeForm : BaseForm<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

    }
}
