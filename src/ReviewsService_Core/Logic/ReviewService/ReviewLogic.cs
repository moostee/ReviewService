using ReviewsService_Core.Data;
using ReviewsService_Core.Domain;
using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Form;
using ReviewsService_Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsService_Core.Logic.ReviewService
{

    /// <summary>
    /// Review Logic
    /// </summary>
    public partial class ReviewLogic : BaseLogic
    {

    }


    /// <summary>
    /// Review Logic
    /// </summary>
    public partial class ReviewLogic : BaseLogic
    {

        private readonly IDataModule Data;
        private readonly IFactoryModule Factory;

        public ReviewLogic(IDataModule data, IFactoryModule factory)
        {
            Data = data;
            Factory = factory;
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
            return Data.Reviews.Search(appClientId, comment, rating, appFeature, userId, isActive, reviewTypeId, parentId);
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
        public Page<ReviewModel> SearchEf(long appClientId = 0, string comment = "", int rating = 0, string appFeature = "", string userId = "", bool? isActive = null, long reviewTypeId = 0, int parentId = 0,
            long page = 1, long pageSize = 10, string sort = "Id")
        {
            return Data.Reviews.SearchEF(appClientId, comment, rating, appFeature, userId, isActive, reviewTypeId, parentId, page, pageSize, sort);
        }


        /// <summary>
        /// IEnumerable Review Model Search
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
        public IEnumerable<ReviewModel> SearchModel(long appClientId = 0, string comment = "", int rating = 0, string appFeature = "", string userId = "", bool? isActive = null, long reviewTypeId = 0, int parentId = 0)
        {
            return Data.Reviews.Search(appClientId, comment, rating, appFeature, userId, isActive, reviewTypeId, parentId)
                .Select(Factory.Reviews.CreateModel);
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
            long page = 1, long pageSize = 10, string sort = "")
        {
            return Data.Reviews.SearchView(appClientId, comment, rating, appFeature, userId, isActive, reviewTypeId, parentId, page, pageSize, sort);
        }

        /// <summary>
        /// Create Review Model from Review Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ReviewModel Create(Review entity)
        {
            return Factory.Reviews.CreateModel(entity);
        }

        /// <summary>
        /// Create Review Model from Review Form
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ReviewModel Create(ReviewForm form)
        {
            return Factory.Reviews.CreateModel(form);
        }

        /// <summary>
        /// Create Review Entity from Review Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Review Create(ReviewModel model)
        {
            return Factory.Reviews.CreateEntity(model);
        }

        /// <summary>
        /// Check Uniqueness of Review before creation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateExists(ReviewModel model)
        {
            return Data.Reviews.ItemExists(model);
        }

        /// <summary>
        /// Delete Review
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(Review entity)
        {
            return Data.Reviews.DeleteNpoco(entity);
        }

        /// <summary>
        /// Get Review Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Review Get(Guid id)
        {
            return Data.Reviews.Get(id);
        }

        /// <summary>
        /// Get Review Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ReviewModel> GetModel(Guid id)
        {
            return await Data.Reviews.GetModel(id);
        }

        /// <summary>
        /// Insert new Review to DB
        /// </summary>
        /// <param name="model"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public async Task<ReviewModel> Insert(ReviewModel model, bool check = true)
        {
            if (check)
            {
                var routeSearch = Data.Reviews.ItemExists(model);
                if (routeSearch)
                {
                    throw new Exception("Review Name already exists");
                }
            }
            var entity = Factory.Reviews.CreateEntity(model);
            entity.RecordStatus = ReviewsService_Core.Domain.Enum.RecordStatus.Active;
            await Data.Reviews.Insert(entity);
            return Factory.Reviews.CreateModel(entity);
        }

        /// <summary>
        /// Update a Review Entity with a Review Model with selected fields
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public Review Patch(Review entity, ReviewModel model, string fields)
        {
            return Factory.Reviews.Patch(entity, model, fields);
        }

        /// <summary>
        /// Update Review, with Patch Options Optional
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public ReviewModel Update(Review entity, ReviewModel model = null, string fields = "")
        {
            if (model != null)
            {
                entity = Patch(entity, model, fields);
            }
            return Factory.Reviews.CreateModel(Data.Reviews.UpdateNpoco(entity));
        }

        /// <summary>
        /// Check Uniqueness of Review before update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateExists(ReviewModel model)
        {
            return Data.Reviews.ItemExists(model, model.Id);
        }
    }

}
