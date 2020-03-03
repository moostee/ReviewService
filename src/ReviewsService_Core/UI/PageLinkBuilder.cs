using ReviewsService_Core.Common;
using System;

namespace ReviewsService_Core.UI
{
    public class PageLinkBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        public string FirstPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NextPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PreviousPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long totalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long totalPages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int draw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PagingInfo PaginationHeader
        {
            get
            {
                int _totalCount = Convert.ToInt32(totalCount);
                int _totalPages = Convert.ToInt32(totalPages);
                int _page = Convert.ToInt32(page);
                int _pageSize = Convert.ToInt32(pageSize);


                return new PagingInfo(_totalCount, _totalPages, _page, _pageSize, PreviousPage, NextPage, draw, summary);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSizeNo"></param>
        /// <param name="totalRecordCount"></param>
        /// <param name="draw"></param>
        /// <param name="summary"></param>
        public PageLinkBuilder(JObjectHelper args, long pageNo, long pageSizeNo,
        long totalRecordCount, int draw = 1, string summary = "")
        {
            this.draw = draw;
            this.summary = summary;
            page = pageNo;
            pageSize = pageSizeNo;
            totalCount = totalRecordCount;
            totalPages = totalRecordCount > 0 ? (int)Math.Ceiling(totalRecordCount / (double)pageSize) : 0;
            args.Add("pageSize", pageSize);
            args.Add("page", 1);
            var p1 = args.ToObject();
            args.Add("page", page - 1);
            var p2 = args.ToObject();
            args.Add("page", page + 1);
            var p3 = args.ToObject();
            args.Add("page", totalPages);
            var p4 = args.ToObject();

        }
    }

}
