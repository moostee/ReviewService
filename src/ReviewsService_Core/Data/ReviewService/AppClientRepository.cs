using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReviewsService_Core.Data.ReviewService
{
    /// </summary>
    public class AppClientRepository : BaseRepository<AppClient, AppClientModel, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AppClientRepository(ReviewContext context) : base(context)
        {
        }
        /// <summary>
        /// IQueryable AppClient Entity Search
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public IQueryable<AppClient> Search(int appId = 0, int clientId = 0, string clientSecret = "")
        {
            var table = Query();
            if (appId > 0)
            {
                table = table.Where(x => x.AppId == appId);
            }
            if (clientId > 0)
            {
                table = table.Where(x => x.ClientId == clientId);
            }
            if (!string.IsNullOrEmpty(clientSecret))
            {
                table = table.Where(x => x.ClientSecret == clientSecret);
            }

            return table;
        }

        /// <summary>
        /// Paged AppClient Model Search
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="page"></param>
        ///<param name="pageSize"></param>
        ///<param name="sort"></param>
        /// <returns></returns>
        public Page<AppClientModel> SearchView(int appId = 0, int clientId = 0, string clientSecret = "",
            long page = 1, long pageSize = 10, string sort = "Id")
        {
            var sql = " where Id > 0 ";
            var c = 0;

            if (appId > 0)
            {
                sql += $" and AppId = @{c} ";
                AddParam("appId", appId);
                c++;
            }
            if (clientId > 0)
            {
                sql += $" and ClientId = @{c} ";
                AddParam("clientId", clientId);
                c++;
            }
            if (!string.IsNullOrEmpty(clientSecret))
            {
                sql += $" and ClientSecret = @{c} ";
                AddParam("clientSecret", clientSecret);
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
        public bool ItemExists(AppClientModel model, long? Id = null)
        {
            var check = Search(model.AppId, model.ClientId, model.ClientSecret);
            if (Id != null)
            {
                check = check.Where(x => x.Id != Id);
            }
            return check.Any();
        }
    }

}
