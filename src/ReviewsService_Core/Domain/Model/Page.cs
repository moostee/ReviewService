using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain.Model
{
    public class Page<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public Page()
        {

        }
        /// <summary>
        /// Current page of search
        /// </summary>
        public long CurrentPage { get; set; }
        /// <summary>
        /// Total number of pages in the search
        /// </summary>
        public long TotalPages { get; set; }
        /// <summary>
        /// Total number of items in the search
        /// </summary>
        public long TotalItems { get; set; }
        /// <summary>
        /// Number of items per page
        /// </summary>
        public long ItemsPerPage { get; set; }
        /// <summary>
        /// The search results
        /// </summary>
        public List<T> Items { get; set; }

    }
}
