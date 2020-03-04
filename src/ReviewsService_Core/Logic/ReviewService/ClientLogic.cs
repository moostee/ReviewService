using ReviewsService_Core.Data;
using ReviewsService_Core.Domain;
using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Form;
using ReviewsService_Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsService_Core.Logic
{
    public partial class ClientLogic : BaseLogic
    {

    }


    /// <summary>
    /// Client Service
    /// </summary>
    public partial class ClientLogic : BaseLogic
    {

        private readonly IDataModule Data;
        private readonly IFactoryModule Factory;

        public ClientLogic(IDataModule data, IFactoryModule factory)
        {
            Data = data;
            Factory = factory;
        }
        /// <summary>
        /// IQueryable Client Entity Search
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Client> Search(string name = "")
        {
            return Data.Clients.Search(name);
        }


        /// <summary>
        /// IEnumerable Client Model Search
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<ClientModel> SearchModel(string name = "")
        {
            return Data.Clients.Search(name)
                .Select(Factory.Clients.CreateModel);
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
            long page = 1, long pageSize = 10, string sort = "")
        {
            return Data.Clients.SearchView(name, page, pageSize, sort);
        }

        /// <summary>
        /// Create Client Model from Client Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ClientModel Create(Client entity)
        {
            return Factory.Clients.CreateModel(entity);
        }

        /// <summary>
        /// Create Client Model from Client Form
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ClientModel Create(ClientForm form)
        {
            return Factory.Clients.CreateModel(form);
        }

        /// <summary>
        /// Create Client Entity from Client Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Client Create(ClientModel model)
        {
            return Factory.Clients.CreateEntity(model);
        }

        /// <summary>
        /// Check Uniqueness of Client before creation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateExists(ClientModel model)
        {
            return Data.Clients.ItemExists(model);
        }

        /// <summary>
        /// Delete Client
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(Client entity)
        {
            return Data.Clients.Delete(entity);
        }

        /// <summary>
        /// Get Client Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Client Get(int id)
        {
            return Data.Clients.Get(id);
        }



        /// <summary>
        /// Get Client Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ClientModel> GetModel(int id)
        {
            return await Data.Clients.GetModel(id);
        }

        /// <summary>
        /// Insert new Client to DB
        /// </summary>
        /// <param name="model"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public async Task<ClientModel> Insert(ClientModel model, bool check = true)
        {
            if (check)
            {
                var routeSearch = Data.Clients.ItemExists(model);
                if (routeSearch)
                {
                    throw new Exception("Client Name already exists");
                }
            }
            var entity = Factory.Clients.CreateEntity(model);
            entity.RecordStatus = Domain.Enum.RecordStatus.Active;
            await Data.Clients.Insert(entity);
            return Factory.Clients.CreateModel(entity);
        }

        /// <summary>
        /// Update a Client Entity with a Client Model with selected fields
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public Client Patch(Client entity, ClientModel model, string fields)
        {
            return Factory.Clients.Patch(entity, model, fields);
        }

        /// <summary>
        /// Update Client, with Patch Options Optional
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public ClientModel Update(Client entity, ClientModel model = null, string fields = "")
        {
            if (model != null)
            {
                entity = Patch(entity, model, fields);
            }
            return Factory.Clients.CreateModel(Data.Clients.Update(entity));
        }

        /// <summary>
        /// Check Uniqueness of Client before update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateExists(ClientModel model)
        {
            return Data.Clients.ItemExists(model, model.Id);
        }

    }
}
