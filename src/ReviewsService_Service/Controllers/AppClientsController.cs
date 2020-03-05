using Microsoft.AspNetCore.Mvc;
using ReviewsService_Core.Common;
using ReviewsService_Core.Domain.Form;
using ReviewsService_Core.Domain.Model;
using ReviewsService_Core.Domain.Model.Helper;
using ReviewsService_Core.Logic;
using ReviewsService_Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReviewsService_Service.Controllers
{
    public class AppClientsController : BaseApiController
    {
        private readonly ILogicModule Logic;

        public AppClientsController(ILogicModule logic)
        {
            Logic = logic;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(AppClientForm form)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var model = Logic.AppClients.Create(form);
                var check = Logic.AppClients.CreateExists(model);
                if (check)
                {
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "AppClient already exists"));
                }
                response.Data = await Logic.AppClients.Insert(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }

        [Route("Search", Name = "AppClientApi")]
        [HttpGet]
        public IActionResult Get(string sort = "id", int appId = 0, int clientId = 0, string clientSecret = "", long page = 1, long pageSize = 10, string fields = "", int draw = 1)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var items = Logic.AppClients.SearchView(appId, clientId, clientSecret, page, pageSize, sort);

                if (page > items.TotalPages) page = items.TotalPages;
                var jo = new JObjectHelper();
                jo.Add("appId", appId);
                jo.Add("clientId", clientId);
                jo.Add("clientSecret", clientSecret);

                jo.Add("fields", fields);
                jo.Add("sort", sort);
                //var urlHelper = new UrlHelper(Request);
                var linkBuilder = new PageLinkBuilder(jo, page, pageSize, items.TotalItems, draw);
                AddHeader("X-Pagination", linkBuilder.PaginationHeader);
                var dto = new List<AppClientModel>();
                if (items.TotalItems <= 0) return Ok(dto);
                response.Data = items.Items.ShapeList(fields);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }

        [HttpGet]
        [Route("Detail")]
        public async Task<IActionResult> Get(long id)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var item = await Logic.AppClients.GetModel(id);
                if (item == null)
                {
                    return NotFound(Utilities.UnsuccessfulResponse(response, "AppClient does not exist"));
                }
                response.Data = item;
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }
    }
}
