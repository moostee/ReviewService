using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReviewsService_Core.Data.ReviewService
{
    public class ReviewVoteRepository : BaseRepository<ReviewVote, ReviewVoteModel, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ReviewVoteRepository(ReviewContext context) : base(context)
        {
        }
        /// <summary>
        /// IQueryable ReviewVote Entity Search
        /// </summary>
        /// <param name="reviewId"></param>
        /// <param name="userId"></param>
        /// <param name="reviewVoteTypeId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IQueryable<ReviewVote> Search(string reviewId = "", string userId = "", int reviewVoteTypeId = 0, bool? isActive = null)
        {
            var table = Query();
            if (!string.IsNullOrEmpty(reviewId))
            {
                table = table.Where(x => x.ReviewId.ToString() == reviewId);
            }
            if (!string.IsNullOrEmpty(userId))
            {
                table = table.Where(x => x.UserId == userId);
            }
            if (reviewVoteTypeId > 0)
            {
                table = table.Where(x => x.ReviewVoteTypeId == reviewVoteTypeId);
            }
            if (isActive.HasValue)
            {
                var isActiveVal = isActive.GetValueOrDefault();
                table = table.Where(x => x.IsActive == isActiveVal);
            }

            return table;
        }

        /// <summary>
        /// Paged ReviewVote Model Search
        /// </summary>
        /// <param name="reviewId"></param>
        /// <param name="userId"></param>
        /// <param name="reviewVoteTypeId"></param>
        /// <param name="isActive"></param>
        /// <param name="page"></param>
        ///<param name="pageSize"></param>
        ///<param name="sort"></param>
        /// <returns></returns>
        public Page<ReviewVoteModel> SearchView(string reviewId = "", string userId = "", int reviewVoteTypeId = 0, bool? isActive = null,
            long page = 1, long pageSize = 10, string sort = "Id")
        {
            var sql = "where \"Id\" != \'00000000-0000-0000-0000-000000000000\' ";
            var c = 0;

            if (!string.IsNullOrEmpty(reviewId))
            {
                var reviewIdVal = reviewId;
                sql += $" and ReviewId = @{c} ";
                AddParam("reviewId", reviewIdVal);
                c++;
            }
            if (!string.IsNullOrEmpty(reviewId))
            {
                sql += $" and UserId = @{c} ";
                AddParam("userId", userId);
                c++;
            }
            if (reviewVoteTypeId > 0)
            {
                sql += $" and ReviewVoteTypeId = @{c} ";
                AddParam("reviewVoteTypeId", reviewVoteTypeId);
                c++;
            }
            if (isActive.HasValue)
            {
                var isActiveVal = isActive.GetValueOrDefault();
                sql += $" and IsActive = @{c} ";
                AddParam("isActive", isActiveVal);
                c++;
            }


            sql += ApplySort(sort);
            if (page <= 0) return QueryView(sql);

            return PagedView(sql, page, pageSize);
        }

        /// <summary>
        /// check exists
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool ItemExists(ReviewVoteModel model, Guid? Id = null)
        {
            var check = Search(model.ReviewId.ToString(), model.UserId, model.ReviewVoteTypeId, model.IsActive);
            if (Id != null)
            {
                check = check.Where(x => x.Id != Id);
            }
            return check.Any();
        }
    }
}
