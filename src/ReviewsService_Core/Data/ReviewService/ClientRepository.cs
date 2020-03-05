using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReviewsService_Core.Data.ReviewService
{
    public class ClientRepository : BaseRepository<Client, ClientModel, int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ClientRepository(ReviewContext context) : base(context)
        {
        }
        /// <summary>
        /// IQueryable Client Entity Search
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Client> Search(string name = "")
        {
            var table = Query();
            if (!string.IsNullOrEmpty(name))
            {
                table = table.Where(x => x.Name == name);
            }

            return table;
        }

        /// <summary>
        /// Paged Client Model Search
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        ///<param name="pageSize"></param>
        ///<param name="sort"></param>
        /// <returns></returns>
        public Page<ClientModel> SearchView(string name = "",
            long page = 1, long pageSize = 10, string sort = "Id")
        {
            var sql = " where Id > 0 ";
            var c = 0;

            if (!string.IsNullOrEmpty(name))
            {
                sql += $" and Name = @{c} ";
                AddParam("name", name);
                c++;
            }

            sql += ApplySort(sort);
            if (page <= 0) return QueryView(sql);

            return PagedView(sql, page, pageSize);

        }

        /// <summary>
        /// Check exists
        /// </summary>
        /// <param name="name"></param>
        /// 
        /// <param name="Id"></param>
        /// <returns></returns>
        //public bool ItemExists(string name = "", int? Id = null)
        //{
        //    var check = Search(name);
        //    if (Id != null)
        //    {
        //        check = check.Where(x => x.Id != Id);
        //    }
        //    return check.Any();
        //}

        /// <summary>
        /// check exists
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool ItemExists(ClientModel model, int? Id = null)
        {
            var check = Search(model.Name);
            if (Id != null)
            {
                check = check.Where(x => x.Id != Id);
            }
            return check.Any();
        }
    }
}
