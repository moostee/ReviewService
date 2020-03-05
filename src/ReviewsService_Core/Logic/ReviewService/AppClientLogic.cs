using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewsService_Core.Common;
using ReviewsService_Core.Data;
using ReviewsService_Core.Domain;
using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Form;
using ReviewsService_Core.Domain.Model;


namespace ReviewsService_Core.Logic.ReviewService
{
    
    public partial class AppClientLogic : BaseLogic
    {

    }
    public partial class AppClientLogic : BaseLogic
    {
        private readonly IDataModule Data;
        private readonly IFactoryModule Factory;

        public AppClientLogic(IDataModule data, IFactoryModule factory)
        {
            Data = data;
            Factory = factory;
        }
        public IQueryable<AppClient> Search(int appId = 0, int clientId = 0, string clientSecret = "")
        {
            return Data.AppClients.Search(appId, clientId, clientSecret);
        }
        public IEnumerable<AppClientModel> SearchModel(int appId = 0, int clientId = 0, string clientSecret = "")
        {
            return Data.AppClients.Search(appId, clientId, clientSecret)
                .Select(Factory.AppClients.CreateModel);
        }

        public Page<AppClientModel> SearchView(int appId = 0, int clientId = 0, string clientSecret = "",
            long page = 1, long pageSize = 10, string sort = "")
        {
            return Data.AppClients.SearchView(appId, clientId, clientSecret, page, pageSize, sort);
        }
        public AppClientModel Create(AppClient entity)
        {
            return Factory.AppClients.CreateModel(entity);
        }
        public AppClientModel Create(AppClientForm form)
        {
            return Factory.AppClients.CreateModel(form);
        }
        public AppClient Create(AppClientModel model)
        {
            return Factory.AppClients.CreateEntity(model);
        }
        public bool CreateExists(AppClientModel model)
        {
            return Data.AppClients.ItemExists(model);
        }
        public int Delete(AppClient entity)
        {
            return Data.AppClients.Delete(entity);
        }
        public AppClient Get(long id)
        {
            return Data.AppClients.Get(id);
        }
        public async Task<AppClientModel> GetModel(long id)
        {
            return await Data.AppClients.GetModel(id);
        }
        public async Task<AppClientModel> Insert(AppClientModel model, bool check = true)
        {
            if (check)
            {
                var routeSearch = Data.AppClients.ItemExists(model);
                if (routeSearch)
                {
                    throw new Exception("AppClient Name already exists");
                }
            }
            var entity = Factory.AppClients.CreateEntity(model);
            entity.ClientSecret = StringUtility.GenerateAPISecret();
            entity.RecordStatus = Domain.Enum.RecordStatus.Active;
            await Data.AppClients.Insert(entity);
            return Factory.AppClients.CreateModel(entity);
        }

        public AppClient Patch(AppClient entity, AppClientModel model, string fields)
        {
            return Factory.AppClients.Patch(entity, model, fields);
        }
        public AppClientModel Update(AppClient entity, AppClientModel model = null, string fields = "")
        {
            if (model != null)
            {
                entity = Patch(entity, model, fields);
            }
            return Factory.AppClients.CreateModel(Data.AppClients.Update(entity));
        }
        public bool UpdateExists(AppClientModel model)
        {
            return Data.AppClients.ItemExists(model, model.Id);
        }

    }
}
