using ReviewsService_Core.Data;
using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Model;
using System.Linq;
/// <summary>
/// 
/// </summary>
public class ReviewTypeRepository : BaseRepository<ReviewType, ReviewTypeModel, long>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public ReviewTypeRepository(ReviewContext context) : base(context)
    {
    }
    /// <summary>
    /// IQueryable ReviewType Entity Search
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IQueryable<ReviewType> Search(string name = "")
    {
        var table = Query();
        if (!string.IsNullOrEmpty(name))
        {
            table = table.Where(x => x.Name == name);
        }

        return table;
    }

    /// <summary>
    /// Paged ReviewType Model Search
    /// </summary>
    /// <param name="name"></param>
    /// <param name="page"></param>
    ///<param name="pageSize"></param>
    ///<param name="sort"></param>
    /// <returns></returns>
    public Page<ReviewTypeModel> SearchEF(string name = "",
        long page = 1, long pageSize = 10, string sort = "Id")
    {
        var table = QueryModel();

        if (!string.IsNullOrEmpty(name))
        {
            table.Where(x => x.Name == name);
        }

        return Paged(table, pageSize, page, sort);

    }

    /// <summary>
    /// Paged ReviewType Model Search
    /// </summary>
    /// <param name="name"></param>
    /// <param name="page"></param>
    ///<param name="pageSize"></param>
    ///<param name="sort"></param>
    /// <returns></returns>
    public Page<ReviewTypeModel> SearchView(string name = "",
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
    /// check exists
    /// </summary>
    /// <param name="model"></param>
    /// <param name="Id"></param>
    /// <returns></returns>
    public bool ItemExists(ReviewTypeModel model, long? Id = null)
    {
        var search = SearchView(model.Name);
        var check = search.Items.AsEnumerable();
        if (Id != null)
        {
            check = check.Where(x => x.Id != Id);
        }
        return check.Any();
    }
}
