using ReviewsService_Core.Common;
using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Enum;
using ReviewsService_Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using NPoco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsService_Core.Data
{
    public class BaseRepository<TEntity, TModel, TKey>
       where TEntity : BaseEntity<TKey>
       where TModel : BaseModel<TKey>
    {
        public ReviewContext _context;
        public BaseRepository(ReviewContext context)
        {
            _context = context;
        }

        public Domain.Model.Page<T> Paged<T>(IQueryable<T> items, long pageSize, long page = 1, string sort = "",
            bool extended = false, string included = "")
        {

            int _pageSize = Convert.ToInt32(pageSize);
            int _page = Convert.ToInt32(page);
            items = items.ApplySort(sort);
            var t = items.Count();

            if (!extended)
            {
                items = _pageSize <= 0 ? items : items.Skip(_pageSize * (_page - 1)).Take(_pageSize);
            }
            else
            {
               // items = pageSize <= 0 ? items.Include(included) : items.Include(included).Skip(pageSize * (page - 1)).Take(pageSize);
            }
            //return items.AsEnumerable();

            return new Domain.Model.Page<T>()
            {
                CurrentPage = page,
                Items = items.ToList(),
                ItemsPerPage = pageSize,
                TotalItems = t,
                TotalPages = (int)Math.Ceiling((double)t / pageSize)
            };
        }


        public virtual TEntity Update(TEntity obj)
        {
            try
            {
                obj.UpdatedAt = DateTime.UtcNow;
                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public virtual int Delete(TEntity obj)
        {
            try
            {
                obj.UpdatedAt = DateTime.UtcNow;
                obj.RecordStatus = RecordStatus.Deleted;
                _context.Entry(obj).State = EntityState.Modified;
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                     

        public virtual async Task<TEntity> Insert(TEntity obj)
        {
            try
            {
                obj.UpdatedAt = DateTime.UtcNow;
                obj.CreatedAt = DateTime.UtcNow;
                _context.Set<TEntity>().Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public virtual async Task<int> DeleteFinally(TEntity obj)
        {
            try
            {
                _context.Set<TEntity>().Remove(obj);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public virtual int Count()
        {
            try
            {
                return Query().Count();



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public virtual IQueryable<TEntity> Query(string includeProperties = "")
        {
            try
            {
                return All(includeProperties)
                    .Where(x => x.RecordStatus != RecordStatus.Deleted && x.RecordStatus != RecordStatus.Archive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IQueryable<TModel> QueryModel(string includeProperties = "")
        {
            try
            {

                return AllModel(includeProperties)
                    .Where(x => x.RecordStatus != RecordStatus.Deleted && x.RecordStatus != RecordStatus.Archive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual IQueryable<TEntity> All(string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = _context.Set<TEntity>();
                if (!String.IsNullOrEmpty(includeProperties))
                {
                    foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }




        }


        public virtual IQueryable<TModel> AllModel(string includeProperties = "")
        {
            try
            {
                IQueryable<TModel> query = _context.Set<TModel>();
                if (!String.IsNullOrEmpty(includeProperties))
                {
                    foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }




        }


        public bool Exists(TKey id)
        {
            try
            {
                var i = _context.Set<TEntity>().Find(id);
                if (i != null)
                {
                    if (i.RecordStatus == RecordStatus.Deleted || i.RecordStatus == RecordStatus.Archive)
                    {
                        i = null;
                    }
                }
                return i != null;
            }



            catch (Exception ex)
            {
                throw ex;
            }
        }



        public TEntity Get(TKey id)
        {
            try
            {
                var i = _context.Set<TEntity>().Find(id);
                if (i != null)
                {
                    if (i.RecordStatus == RecordStatus.Deleted || i.RecordStatus == RecordStatus.Archive)
                    {
                        i = null;
                    }
                    else
                    {
                        //_context.Entry<TEntity>(i).Reload();
                    }
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public virtual TEntity DeleteUndo(TEntity obj)
        {
            try
            {
                obj.UpdatedAt = DateTime.Now;
                obj.RecordStatus = RecordStatus.Active;
                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Npoco Crud

        public IDatabase Connection
        {
            get
            {
                return ReviewPoco.DbFactory.GetDatabase();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual TEntity UpdateNpoco(TEntity obj)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    obj.UpdatedAt = DateUtility.CurrentDateTime();
                    db.Update(obj);
                    return obj;
                }
                catch (Exception e)
                {
                    var _e = new Exception(SerializeUtility.SerializeJSON(obj), e);
                    Log.Error(_e);
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int DeleteNpoco(TEntity obj)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    obj.RecordStatus = RecordStatus.Deleted;
                    obj.UpdatedAt = DateUtility.CurrentDateTime();
                    return db.Update(obj);
                }
                catch (Exception e)
                {
                    var _e = new Exception(SerializeUtility.SerializeJSON(obj), e);
                    Log.Error(_e);
                    return -1;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual TEntity InsertNpoco(TEntity obj)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    obj.UpdatedAt = DateUtility.CurrentDateTime();
                    obj.CreatedAt = DateUtility.CurrentDateTime();
                    db.Insert<TEntity>(obj);
                    return obj;
                }
                catch (Exception e)
                {
                    var _e = new Exception(SerializeUtility.SerializeJSON(obj), e);
                    Log.Error(_e);
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int DeleteFinallyNpoco(TEntity obj)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    return db.Delete(obj);
                }
                catch (Exception e)
                {
                    var _e = new Exception(SerializeUtility.SerializeJSON(obj), e);
                    Log.Error(_e);
                    return -1;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int DeleteFinally(TKey id)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    return db.Delete(id);
                }
                catch (Exception e)
                {
                    var _e = new Exception(SerializeUtility.SerializeJSON(id), e);
                    Log.Error(_e);
                    return -1;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual TEntity DeleteUndoNpoco(TEntity obj)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    obj.RecordStatus = RecordStatus.Active;
                    obj.UpdatedAt = DateTime.UtcNow;
                    db.Update(obj);
                    return obj;
                }
                catch (Exception e)
                {
                    var _e = new Exception(SerializeUtility.SerializeJSON(obj), e);
                    Log.Error(_e);
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<TEntity> QueryNpoco(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Fetch<TEntity>(sql);
                    }
                    var t = db.Fetch<TEntity>(sql, prms.ToArray());
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    var _e = new Exception(sql, e);
                    Log.Error(_e);
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public string ApplySort(string sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                return "";
            }
            var lst = new List<string>();
            var lstSort = sort.Split(',');
            foreach (var sortOption in lstSort.Reverse())
            {
                if (sortOption.StartsWith("-"))
                {
                    lst.Add("\"" + sortOption.Remove(0, 1) + "\"" + " desc");
                }
                else
                {
                    lst.Add("\"" + sortOption + "\"");
                }
            }
            return " Order by " + string.Join(",", lst);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="all"></param>
        /// <returns></returns>
        public TEntity Get(TKey id, bool all = false)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    var k = db.SingleOrDefaultById<TEntity>(id);
                    if (k != null)
                    {
                        if (k.RecordStatus == RecordStatus.Deleted || k.RecordStatus == RecordStatus.Archive)
                        {
                            if (!all) return default;
                        }
                    }
                    return k;
                }
                catch (Exception e)
                {
                    var _e = new Exception(id.ToString(), e);
                    Log.Error(_e);
                    return null;
                }
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public TEntity Get(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Fetch<TEntity>(sql).FirstOrDefault();
                    }
                    var t = db.Fetch<TEntity>(sql, prms.ToArray()).FirstOrDefault();
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    var _e = new Exception(sql, e);
                    Log.Error(_e);
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public TModel GetModel(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Fetch<TModel>(sql).FirstOrDefault();
                    }
                    var t = db.Fetch<TModel>(sql, prms.ToArray()).FirstOrDefault();
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    var _e = new Exception(sql, e);
                    Log.Error(_e);
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TModel> GetModel(TKey id)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    return await db.SingleOrDefaultByIdAsync<TModel>(id);
                }
                catch (Exception e)
                {
                    var _e = new Exception(id.ToString(), e);
                    Log.Error(_e);
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<TModel> GetModels(string ids)
        {
            using (IDatabase db = Connection)
            {
                var str = StringUtility.ToStringQuery(ids, "number");
                var sql = "where id in (" + str + ")";

                try
                {
                    return db.Fetch<TModel>(sql);
                }
                catch (Exception e)
                {
                    var _e = new Exception(sql, e);
                    Log.Error(_e);
                    return null;
                }
            }
        }

        //public bool Exists(TKey id)
        //{
        //    var i = _context.Set<TEntity>().Find(id);
        //    if (i != null)
        //    {
        //        if (i.RecordStatus == RecordStatus.Deleted || i.RecordStatus == RecordStatus.Archive)
        //        {
        //            i = null;
        //        }
        //    }
        //    return i != null;
        //}

        private List<object> prms;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void AddParam(string key, object obj)
        {
            if (prms == null)
            {
                prms = new List<object>();
            }
            prms.Add(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Domain.Model.Page<TModel> QueryView(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return ToPaged(db.Fetch<TModel>(sql));
                    }
                    var t = db.Fetch<TModel>(sql, prms.ToArray());
                    prms = null;
                    return ToPaged(t);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public Domain.Model.Page<TModel> PagedView(string sql, long page, long pagesize)
        {
            using (IDatabase db = Connection)
            {

                try
                {
                    if (prms == null)
                    {
                        return ToPaged(db.Page<TModel>(page, pagesize, sql));
                    }
                    var t = db.Page<TModel>(page, pagesize, sql, prms.ToArray());
                    prms = null;
                    return ToPaged(t);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="k"></param>
        /// <returns></returns>
        public Domain.Model.Page<T> ToPaged<T>(NPoco.Page<T> k)
        {
            return new ReviewsService_Core.Domain.Model.Page<T>()
            {
                CurrentPage = k.CurrentPage,
                Items = k.Items,
                ItemsPerPage = k.ItemsPerPage,
                TotalItems = k.TotalItems,
                TotalPages = k.TotalPages
            };
        }



        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="k"></param>
        /// <returns></returns>
        public Domain.Model.Page<T> ToPaged<T>(List<T> k)
        {
            var t = k.Count();
            return new ReviewsService_Core.Domain.Model.Page<T>()
            {
                CurrentPage = 1,
                Items = k,
                ItemsPerPage = t,
                TotalItems = t,
                TotalPages = 1
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public NPoco.Page<TModel> SearchViewSQL(string sql, long page, long pagesize)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Page<TModel>(page, pagesize, sql);
                    }
                    var t = db.Page<TModel>(page, pagesize, sql, prms.ToArray());
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual T UpdateRecord<T>(T obj)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    db.Update(obj);
                    return obj;
                }
                catch (Exception e)
                {
                    var _e = new Exception(SerializeUtility.SerializeJSON(obj), e);
                    Log.Error(_e);
                    throw e;
                }
            }
        }



        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected T SelectRecord<T>(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Fetch<T>(sql).FirstOrDefault();
                    }
                    var t = db.Fetch<T>(sql, prms.ToArray()).FirstOrDefault();
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> SelectRows<T>(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Fetch<T>(sql);
                    }
                    var t = db.Fetch<T>(sql, prms.ToArray());
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T SelectScalar<T>(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.ExecuteScalar<T>(sql);
                    }
                    var t = db.ExecuteScalar<T>(sql, prms.ToArray());
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int Execute(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Execute(sql);
                    }
                    var t = db.Execute(sql, prms.ToArray());
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    var _e = new Exception(SerializeUtility.SerializeJSON(sql), e);
                    Log.Error(_e);
                    return -1;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteProc(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Execute(sql, CommandType.StoredProcedure);
                    }
                    var t = db.Execute(sql, CommandType.StoredProcedure, prms.ToArray());
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtm"></param>
        /// <param name="alldata"></param>
        /// <returns></returns>
        public List<TEntity> SearchLastUpdated(DateTime dtm, bool alldata = false)
        {
            var sql = "where updatedat >= @0 ";
            if (!alldata)
            {
                sql += " and Recordstatus != 3 and RecordStatus != 4";
            }
            AddParam("updatedat", dtm);
            return QueryNpoco(sql);
        }

        #endregion


    }
}
