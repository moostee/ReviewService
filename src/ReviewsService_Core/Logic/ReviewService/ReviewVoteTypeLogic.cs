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
    public partial class ReviewVoteTypeLogic : BaseLogic
    {

    }
    public partial class ReviewVoteTypeLogic : BaseLogic
    {
        private readonly IDataModule Data;
        private readonly IFactoryModule Factory;

        public ReviewVoteTypeLogic(IDataModule data, IFactoryModule factory)
        {
            Data = data;
            Factory = factory;
        }
        public IQueryable<ReviewVoteType> Search(string name = "")
        {
            return Data.ReviewVoteTypes.Search(name);
        }
        public IEnumerable<ReviewVoteTypeModel> SearchModel(string name = "")
        {
            return Data.ReviewVoteTypes.Search(name)
                .Select(Factory.ReviewVoteTypes.CreateModel);
        }

        public Page<ReviewVoteTypeModel> SearchView(string name = "",
            long page = 1, long pageSize = 10, string sort = "")
        {
            return Data.ReviewVoteTypes.SearchView(name, page, pageSize, sort);
        }
        public ReviewVoteTypeModel Create(ReviewVoteType entity)
        {
            return Factory.ReviewVoteTypes.CreateModel(entity);
        }
        public ReviewVoteTypeModel Create(ReviewVoteTypeForm form)
        {
            return Factory.ReviewVoteTypes.CreateModel(form);
        }
        public ReviewVoteType Create(ReviewVoteTypeModel model)
        {
            return Factory.ReviewVoteTypes.CreateEntity(model);
        }
        public bool CreateExists(ReviewVoteTypeModel model)
        {
            return Data.ReviewVoteTypes.ItemExists(model);
        }
        public int Delete(ReviewVoteType entity)
        {
            return Data.ReviewVoteTypes.Delete(entity);
        }
        public ReviewVoteType Get(int id)
        {
            return Data.ReviewVoteTypes.Get(id);
        }
        public async Task<ReviewVoteTypeModel> GetModel(int id)
        {
            return await Data.ReviewVoteTypes.GetModel(id);
        }
        public async Task<ReviewVoteTypeModel> Insert(ReviewVoteTypeModel model, bool check = true)
        {
            if (check)
            {
                var routeSearch = Data.ReviewVoteTypes.ItemExists(model);
                if (routeSearch)
                {
                    throw new Exception("ReviewVoteType Name already exists");
                }
            }
            var entity = Factory.ReviewVoteTypes.CreateEntity(model);
            entity.RecordStatus = Domain.Enum.RecordStatus.Active;
            await Data.ReviewVoteTypes.Insert(entity);
            return Factory.ReviewVoteTypes.CreateModel(entity);
        }

        public ReviewVoteType Patch(ReviewVoteType entity, ReviewVoteTypeModel model, string fields)
        {
            return Factory.ReviewVoteTypes.Patch(entity, model, fields);
        }
        public ReviewVoteTypeModel Update(ReviewVoteType entity, ReviewVoteTypeModel model = null, string fields = "")
        {
            if (model != null)
            {
                entity = Patch(entity, model, fields);
            }
            return Factory.ReviewVoteTypes.CreateModel(Data.ReviewVoteTypes.Update(entity));
        }
        public bool UpdateExists(ReviewVoteTypeModel model)
        {
            return Data.ReviewVoteTypes.ItemExists(model, model.Id);
        }

    }
}
