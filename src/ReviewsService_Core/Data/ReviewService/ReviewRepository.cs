using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReviewsService_Core.Data.ReviewService
{
    /// <summary>
    /// 
    /// </summary>
    public class ReviewRepository : BaseRepository<Review, ReviewModel, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ReviewRepository(ReviewContext context) : base(context)
        {
        }
        /// <summary>
        /// IQueryable Review Entity Search
        /// </summary>
        /// <param name="appClientId"></param>
        /// <param name="comment"></param>
        /// <param name="rating"></param>
        /// <param name="appFeature"></param>
        /// <param name="userId"></param>
        /// <param name="isActive"></param>
        /// <param name="reviewTypeId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IQueryable<Review> Search(long appClientId = 0, string comment = "", int rating = 0, string appFeature = "", string userId = "", bool? isActive = null, long reviewTypeId = 0, int parentId = 0)
        {
            var table = Query();
            if (appClientId > 0)
            {
                table = table.Where(x => x.AppClientId == appClientId);
            }
            if (!string.IsNullOrEmpty(comment))
            {
                table = table.Where(x => x.Comment == comment);
            }
            if (rating > 0)
            {
                table = table.Where(x => x.Rating == rating);
            }
            if (!string.IsNullOrEmpty(appFeature))
            {
                table = table.Where(x => x.AppFeature == appFeature);
            }
            if (!string.IsNullOrEmpty(userId))
            {
                table = table.Where(x => x.UserId == userId);
            }
            if (isActive.HasValue)
            {
                var isActiveVal = isActive.GetValueOrDefault();
                table = table.Where(x => x.IsActive == isActiveVal);
            }
            if (reviewTypeId > 0)
            {
                table = table.Where(x => x.ReviewTypeId == reviewTypeId);
            }
            if (parentId > 0)
            {
                table = table.Where(x => x.ParentId == parentId);
            }

            return table;
        }

        /// <summary>
        /// Paged Review Model Search
        /// </summary>
        /// <param name="appClientId"></param>
        /// <param name="comment"></param>
        /// <param name="rating"></param>
        /// <param name="appFeature"></param>
        /// <param name="userId"></param>
        /// <param name="isActive"></param>
        /// <param name="reviewTypeId"></param>
        /// <param name="parentId"></param>
        /// <param name="page"></param>
        ///<param name="pageSize"></param>
        ///<param name="sort"></param>
        /// <returns></returns>
        public Page<ReviewModel> SearchEF(long appClientId = 0, string comment = "", int rating = 0, string appFeature = "", string userId = "", bool? isActive = null, long reviewTypeId = 0, int parentId = 0,
            long page = 1, long pageSize = 10, string sort = "Id")
        {
            var table = QueryModel();

            if (appClientId > 0)
            {
                table = table.Where(x => x.AppClientId == appClientId);
            }
            if (!string.IsNullOrEmpty(comment))
            {
                table = table.Where(x => x.Comment == comment);
            }
            if (rating > 0)
            {
                table = table.Where(x => x.Rating == rating);
            }
            if (!string.IsNullOrEmpty(appFeature))
            {
                table = table.Where(x => x.AppFeature == appFeature);
            }
            if (!string.IsNullOrEmpty(userId))
            {
                table = table.Where(x => x.UserId == userId);
            }
            if (isActive.HasValue)
            {
                var isActiveVal = isActive.GetValueOrDefault();
                table = table.Where(x => x.IsActive == isActiveVal);
            }
            if (reviewTypeId > 0)
            {
                table = table.Where(x => x.ReviewTypeId == reviewTypeId);
            }
            if (parentId > 0)
            {
                table = table.Where(x => x.ParentId == parentId);
            }

            return Paged(table, pageSize, page, sort);

        }

        /// <summary>
        /// Paged Review Model Search
        /// </summary>
        /// <param name="appClientId"></param>
        /// <param name="comment"></param>
        /// <param name="rating"></param>
        /// <param name="appFeature"></param>
        /// <param name="userId"></param>
        /// <param name="isActive"></param>
        /// <param name="reviewTypeId"></param>
        /// <param name="parentId"></param>
        /// <param name="page"></param>
        ///<param name="pageSize"></param>
        ///<param name="sort"></param>
        /// <returns></returns>
        public Page<ReviewModel> SearchView(long appClientId = 0, string comment = "", int rating = 0, string appFeature = "", string userId = "", bool? isActive = null, long reviewTypeId = 0, int parentId = 0,
            long page = 1, long pageSize = 10, string sort = "Id")
        {
            var sql = " where Id > \'00000000-0000-0000-0000-000000000000\' ";
            var c = 0;

            if (appClientId > 0)
            {
                sql += $" and AppClientId = @{c} ";
                AddParam("appClientId", appClientId);
                c++;
            }
            if (!string.IsNullOrEmpty(comment))
            {
                sql += $" and Comment = @{c} ";
                AddParam("comment", comment);
                c++;
            }
            if (rating > 0)
            {
                sql += $" and Rating = @{c} ";
                AddParam("rating", rating);
                c++;
            }
            if (!string.IsNullOrEmpty(appFeature))
            {
                sql += $" and AppFeature = @{c} ";
                AddParam("appFeature", appFeature);
                c++;
            }
            if (!string.IsNullOrEmpty(userId))
            {
                sql += $" and UserId = @{c} ";
                AddParam("userId", userId);
                c++;
            }
            if (isActive.HasValue)
            {
                var isActiveVal = isActive.GetValueOrDefault();
                sql += $" and IsActive = @{c} ";
                AddParam("isActive", isActiveVal);
                c++;
            }
            if (reviewTypeId > 0)
            {
                sql += $" and ReviewTypeId = @{c} ";
                AddParam("reviewTypeId", reviewTypeId);
                c++;
            }
            if (parentId > 0)
            {
                sql += $" and ParentId = @{c} ";
                AddParam("parentId", parentId);
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
        public bool ItemExists(ReviewModel model, Guid? Id = null)
        {
            var search = SearchView(model.AppClientId, model.Comment, model.Rating, model.AppFeature, model.UserId, model.IsActive, model.ReviewTypeId, model.ParentId);
            var check = search.Items.AsEnumerable();
            if (Id != null)
            {
                check = check.Where(x => x.Id != Id);
            }
            return check.Any();
        }
    }

}
