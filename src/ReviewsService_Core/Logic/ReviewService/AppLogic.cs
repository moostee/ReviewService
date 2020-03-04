using ReviewsService_Core.Logic;
using ReviewsService_Core.Data;
using ReviewsService_Core.Domain;
using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Form;
using ReviewsService_Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public partial class AppLogic : BaseLogic
{

}


public partial class AppLogic : BaseLogic
{

    private readonly IDataModule Data;
    private readonly IFactoryModule Factory;

    public AppLogic(IDataModule data, IFactoryModule factory)
    {
        Data = data;
        Factory = factory;
    }


    /// <summary>
    /// IQueryable App Entity Search
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public IQueryable<App> Search(string name = "", string description = "")
    {
        return Data.Apps.Search(name, description);
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
    public Page<AppModel> SearchEf(string name = "", string description = "",
        long page = 1, long pageSize = 10, string sort = "Id")
    {
        return Data.Apps.SearchEF(name, description, page, pageSize, sort);
    }


    /// <summary>
    /// IEnumerable App Model Search
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public IEnumerable<AppModel> SearchModel(string name = "", string description = "")
    {
        return Data.Apps.Search(name, description)
            .Select(Factory.Apps.CreateModel);
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
        long page = 1, long pageSize = 10, string sort = "")
    {
        return Data.Apps.SearchView(name, description, page, pageSize, sort);
    }

    /// <summary>
    /// Create App Model from App Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public AppModel Create(App entity)
    {
        return Factory.Apps.CreateModel(entity);
    }

    /// <summary>
    /// Create App Model from App Form
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public AppModel Create(AppForm form)
    {
        return Factory.Apps.CreateModel(form);
    }

    /// <summary>
    /// Create App Entity from App Model
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public App Create(AppModel model)
    {
        return Factory.Apps.CreateEntity(model);
    }

    /// <summary>
    /// Check Uniqueness of App before creation
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public bool CreateExists(AppModel model)
    {
        return Data.Apps.ItemExists(model);
    }

    /// <summary>
    /// Delete App
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public int Delete(App entity)
    {
        return Data.Apps.DeleteNpoco(entity);
    }

    /// <summary>
    /// Get App Entity
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public App Get(int id)
    {
        return Data.Apps.Get(id);
    }

    /// <summary>
    /// Get App Model
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<AppModel> GetModel(int id)
    {
        return await Data.Apps.GetModel(id);
    }

    /// <summary>
    /// Insert new App to DB
    /// </summary>
    /// <param name="model"></param>
    /// <param name="check"></param>
    /// <returns></returns>
    public async Task<AppModel> Insert(AppModel model, bool check = true)
    {
        if (check)
        {
            var routeSearch = Data.Apps.ItemExists(model);
            if (routeSearch)
            {
                throw new Exception("App Name already exists");
            }
        }
        var entity = Factory.Apps.CreateEntity(model);
        entity.RecordStatus = ReviewsService_Core.Domain.Enum.RecordStatus.Active;
        await Data.Apps.Insert(entity);
        return Factory.Apps.CreateModel(entity);
    }

    /// <summary>
    /// Update a App Entity with a App Model with selected fields
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="model"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public App Patch(App entity, AppModel model, string fields)
    {
        return Factory.Apps.Patch(entity, model, fields);
    }

    /// <summary>
    /// Update App, with Patch Options Optional
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="model"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public AppModel Update(App entity, AppModel model = null, string fields = "")
    {
        if (model != null)
        {
            entity = Patch(entity, model, fields);
        }
        return Factory.Apps.CreateModel(Data.Apps.UpdateNpoco(entity));
    }

    /// <summary>
    /// Check Uniqueness of App before update
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public bool UpdateExists(AppModel model)
    {
        return Data.Apps.ItemExists(model, model.Id);
    }

}

