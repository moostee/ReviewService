using ReviewsService_Core.Data;
using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Model;
using System.Linq;
/// <summary>
/// 
/// </summary>
public class AppRepository : BaseRepository<App, AppModel, int>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public AppRepository(ReviewContext context) : base(context)
    {
    }
    /// <summary>
    /// IQueryable App Entity Search
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public IQueryable<App> Search(string name = "", string description = "")
    {
        var table = Query();
        if (!string.IsNullOrEmpty(name))
        {
            table = table.Where(x => x.Name == name);
        }
        if (!string.IsNullOrEmpty(description))
        {
            table = table.Where(x => x.Description == description);
        }

        return table;
    }

    /// <summary>
    /// Paged App Model Search
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="page"></param>
    ///<param name="pageSize"></param>
    ///<param name="sort"></param>
    /// <returns></returns>
    public Page<AppModel> SearchEF(string name = "", string description = "",
        long page = 1, long pageSize = 10, string sort = "Id")
    {
        var table = QueryModel();

        if (!string.IsNullOrEmpty(name))
        {
            table = table.Where(x => x.Name == name);
        }

        if (!string.IsNullOrEmpty(description))
        {
            table = table.Where(x => x.Description == description);
        }

        return Paged(table, pageSize, page, sort);

    }

    /// <summary>
    /// Paged App Model Search
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="page"></param>
    ///<param name="pageSize"></param>
    ///<param name="sort"></param>
    /// <returns></returns>
    public Page<AppModel> SearchView(string name = "", string description = "",
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
        if (!string.IsNullOrEmpty(description))
        {
            sql += $" and Description = @{c} ";
            AddParam("description", description);
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
    public bool ItemExists(AppModel model, int? Id = null)
    {
        var search = SearchView(model.Name, model.Description);
        var check = search.Items.AsEnumerable();
        if (Id != null)
        {
            check = check.Where(x => x.Id != Id);
        }
        return check.Any();
    }
}