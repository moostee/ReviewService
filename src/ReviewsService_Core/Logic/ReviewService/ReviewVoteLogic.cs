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
    /// ReviewVote Service
    /// </summary>
    public partial class ReviewVoteLogic : BaseLogic
    {

    }


    /// <summary>
    /// ReviewVote Service
    /// </summary>
    public partial class ReviewVoteLogic : BaseLogic
    {

        private readonly IDataModule Data;
        private readonly IFactoryModule Factory;

        public ReviewVoteLogic(IDataModule data, IFactoryModule factory)
        {
            Data = data;
            Factory = factory;
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
            return Data.ReviewVotes.Search(reviewId, userId, reviewVoteTypeId, isActive);
        }


        /// <summary>
        /// IEnumerable ReviewVote Model Search
        /// </summary>
        /// <param name="reviewId"></param>
        /// <param name="userId"></param>
        /// <param name="reviewVoteTypeId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<ReviewVoteModel> SearchModel(string reviewId = "", string userId = "", int reviewVoteTypeId = 0, bool? isActive = null)
        {
            return Data.ReviewVotes.Search(reviewId, userId, reviewVoteTypeId, isActive)
                .Select(Factory.ReviewVotes.CreateModel);
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
            long page = 1, long pageSize = 10, string sort = "")
        {
            return Data.ReviewVotes.SearchView(reviewId, userId, reviewVoteTypeId, isActive, page, pageSize, sort);
        }

        /// <summary>
        /// Create ReviewVote Model from ReviewVote Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ReviewVoteModel Create(ReviewVote entity)
        {
            return Factory.ReviewVotes.CreateModel(entity);
        }

        /// <summary>
        /// Create ReviewVote Model from ReviewVote Form
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ReviewVoteModel Create(ReviewVoteForm form)
        {
            return Factory.ReviewVotes.CreateModel(form);
        }

        /// <summary>
        /// Create ReviewVote Entity from ReviewVote Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReviewVote Create(ReviewVoteModel model)
        {
            return Factory.ReviewVotes.CreateEntity(model);
        }

        /// <summary>
        /// Check Uniqueness of ReviewVote before creation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateExists(ReviewVoteModel model)
        {
            return Data.ReviewVotes.ItemExists(model);
        }

        /// <summary>
        /// Delete ReviewVote
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(ReviewVote entity)
        {
            return Data.ReviewVotes.Delete(entity);
        }

        /// <summary>
        /// Get ReviewVote Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReviewVote Get(Guid id)
        {
            return Data.ReviewVotes.Get(id);
        }



        /// <summary>
        /// Get ReviewVote Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ReviewVoteModel> GetModel(Guid id)
        {
            return await Data.ReviewVotes.GetModel(id);
        }

        /// <summary>
        /// Insert new ReviewVote to DB
        /// </summary>
        /// <param name="model"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public async Task<ReviewVoteModel> Insert(ReviewVoteModel model, bool check = true)
        {
            if (check)
            {
                var routeSearch = Data.ReviewVotes.ItemExists(model);
                if (routeSearch)
                {
                    throw new Exception("ReviewVote Name already exists");
                }
            }
            var entity = Factory.ReviewVotes.CreateEntity(model);
            // Check if there is an active ReviewVote
            var isActiveReviewVote = Data.ReviewVotes.Search(entity.ReviewId.ToString(), entity.UserId, 0, true).FirstOrDefault();
            // disable it if found
            if(isActiveReviewVote != null)
            {
                isActiveReviewVote.IsActive = false;
            }
            // then make this new one active
            entity.IsActive = true;
            entity.RecordStatus = Domain.Enum.RecordStatus.Active;
            await Data.ReviewVotes.Insert(entity);
            return Factory.ReviewVotes.CreateModel(entity);
        }

        /// <summary>
        /// Update a ReviewVote Entity with a ReviewVote Model with selected fields
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public ReviewVote Patch(ReviewVote entity, ReviewVoteModel model, string fields)
        {
            return Factory.ReviewVotes.Patch(entity, model, fields);
        }

        /// <summary>
        /// Update ReviewVote, with Patch Options Optional
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public ReviewVoteModel Update(ReviewVote entity, ReviewVoteModel model = null, string fields = "")
        {
            if (model != null)
            {
                entity = Patch(entity, model, fields);
            }
            return Factory.ReviewVotes.CreateModel(Data.ReviewVotes.Update(entity));
        }

        /// <summary>
        /// Check Uniqueness of ReviewVote before update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateExists(ReviewVoteModel model)
        {
            return Data.ReviewVotes.ItemExists(model, model.Id);
        }

    }
}
