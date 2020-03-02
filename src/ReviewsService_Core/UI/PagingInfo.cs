using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class PagingInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ItemStart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ItemEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PreviousPageLink { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NextPageLink { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Draw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Summary { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="totalPages"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="previousPageLink"></param>
        /// <param name="nextPageLink"></param>
        /// <param name="draw"></param>
        /// <param name="summary"></param>
        public PagingInfo(int totalCount, int totalPages, int currentPage,
            int pageSize, string previousPageLink, string nextPageLink, int draw = 1, string summary = "")
        {
            this.TotalCount = totalCount;
            this.TotalPages = totalPages;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.PreviousPageLink = previousPageLink;
            this.NextPageLink = nextPageLink;
            this.Draw = draw;
            this.Summary = summary;

            SetPageItems();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static PagingInfo Default()
        {
            return new PagingInfo(1, 1, 1, 1, "", "");
        }


        /// <summary>
        /// 
        /// </summary>
        public void SetPageItems()
        {
            ItemEnd = PageSize * CurrentPage;
            ItemStart = ItemEnd - PageSize + 1;
            if (ItemEnd > TotalCount)
            {
                ItemEnd = TotalCount;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SummaryPage
        {
            get
            {
                var msg = string.Format("Showing {0} to {1} of {2} records | Total Pages: {3}",
                    ItemStart, ItemEnd, TotalCount, TotalPages);
                if (TotalCount > 0) return msg;
                else return "";
            }
        }
    }
}
