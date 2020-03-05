using ReviewsService_Core.Data;
using ReviewsService_Core.Domain;
using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Form;
using ReviewsService_Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsService_Core.Logic.ReviewService
{
    /// <summary>
    /// ReviewType Logic
    /// </summary>
    public partial class ReviewTypeLogic : BaseLogic
    {

    }


    /// <summary>
    /// ReviewType Logic
    /// </summary>
    public partial class ReviewTypeLogic : BaseLogic
    {

        private readonly IDataModule Data;
        private readonly IFactoryModule Factory;

        public ReviewTypeLogic(IDataModule data, IFactoryModule factory)
        {
            Data = data;
            Factory = factory;
        }


        /// <summary>
        /// IQueryable ReviewType Entity Search
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<ReviewType> Search(string name = "")
        {
            return Data.ReviewTypes.Search(name);
        }



        /// <summary>
        /// Paged ReviewType Model Search
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        ///<param name="pageSize"></param>
        ///<param name="sort"></param>
        /// <returns></returns>
        public Page<ReviewTypeModel> SearchEf(string name = "",
            long page = 1, long pageSize = 10, string sort = "Id")
        {
            return Data.ReviewTypes.SearchEF(name, page, pageSize, sort);
        }


        /// <summary>
        /// IEnumerable ReviewType Model Search
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<ReviewTypeModel> SearchModel(string name = "")
        {
            return Data.ReviewTypes.Search(name)
                .Select(Factory.ReviewTypes.CreateModel);
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
            long page = 1, long pageSize = 10, string sort = "")
        {
            return Data.ReviewTypes.SearchView(name, page, pageSize, sort);
        }

        /// <summary>
        /// Create ReviewType Model from ReviewType Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ReviewTypeModel Create(ReviewType entity)
        {
            return Factory.ReviewTypes.CreateModel(entity);
        }

        /// <summary>
        /// Create ReviewType Model from ReviewType Form
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ReviewTypeModel Create(ReviewTypeForm form)
        {
            return Factory.ReviewTypes.CreateModel(form);
        }

        /// <summary>
        /// Create ReviewType Entity from ReviewType Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReviewType Create(ReviewTypeModel model)
        {
            return Factory.ReviewTypes.CreateEntity(model);
        }

        /// <summary>
        /// Check Uniqueness of ReviewType before creation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateExists(ReviewTypeModel model)
        {
            return Data.ReviewTypes.ItemExists(model);
        }

        /// <summary>
        /// Delete ReviewType
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(ReviewType entity)
        {
            return Data.ReviewTypes.DeleteNpoco(entity);
        }

        /// <summary>
        /// Get ReviewType Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReviewType Get(long id)
        {
            return Data.ReviewTypes.Get(id);
        }

        /// <summary>
        /// Get ReviewType Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ReviewTypeModel> GetModel(long id)
        {
            return await Data.ReviewTypes.GetModel(id);
        }

        /// <summary>
        /// Insert new ReviewType to DB
        /// </summary>
        /// <param name="model"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public async Task<ReviewTypeModel> Insert(ReviewTypeModel model, bool check = true)
        {
            if (check)
            {
                var routeSearch = Data.ReviewTypes.ItemExists(model);
                if (routeSearch)
                {
                    throw new Exception("ReviewType Name already exists");
                }
            }
            var entity = Factory.ReviewTypes.CreateEntity(model);
            entity.RecordStatus = ReviewsService_Core.Domain.Enum.RecordStatus.Active;
            await Data.ReviewTypes.Insert(entity);
            return Factory.ReviewTypes.CreateModel(entity);
        }

        /// <summary>
        /// Update a ReviewType Entity with a ReviewType Model with selected fields
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public ReviewType Patch(ReviewType entity, ReviewTypeModel model, string fields)
        {
            return Factory.ReviewTypes.Patch(entity, model, fields);
        }

        /// <summary>
        /// Update ReviewType, with Patch Options Optional
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public ReviewTypeModel Update(ReviewType entity, ReviewTypeModel model = null, string fields = "")
        {
            if (model != null)
            {
                entity = Patch(entity, model, fields);
            }
            return Factory.ReviewTypes.CreateModel(Data.ReviewTypes.UpdateNpoco(entity));
        }

        /// <summary>
        /// Check Uniqueness of ReviewType before update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateExists(ReviewTypeModel model)
        {
            return Data.ReviewTypes.ItemExists(model, model.Id);
        }

    }
}
