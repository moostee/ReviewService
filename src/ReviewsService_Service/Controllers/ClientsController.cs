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
using System.Threading.Tasks;

namespace ReviewsService_Service.Controllers
{
    public class ClientsController : BaseApiController
    {
        private readonly ILogicModule Logic;

        public ClientsController(ILogicModule logic)
        {
            Logic = logic;
        }
        
        /// <summary>
        /// Add Client
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ClientForm form)
        {

            var response = Utilities.InitializeResponse();
            try
            {
                var model = Logic.Clients.Create(form);
                var check = Logic.Clients.CreateExists(model);
                if (check)
                {
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "Client already exists"));
                }
                var dto = await Logic.Clients.Insert(model);
                response.Data = dto;
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response));
            }
        }

        [Route("Search")]
        [HttpGet]
        public IActionResult Get(string sort = "Name", string name = "", long page = 1, long pageSize = 10, string fields = "", int draw = 1)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var items = Logic.Clients.SearchView(name, page, pageSize, sort);
                if (page > items.TotalPages) page = items.TotalPages;
                var jo = new JObjectHelper();
                jo.Add("name", name);

                jo.Add("fields", fields);
                jo.Add("sort", sort);
                var linkBuilder = new PageLinkBuilder(jo, page, pageSize, items.TotalItems, draw);
                AddHeader("X-Pagination", linkBuilder.PaginationHeader);
                var dto = new List<ClientModel>();
                if (items.TotalItems <= 0) return Ok(dto);
                var dtos = items.Items.ShapeList(fields);
                response.Data = dtos;
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response));
            }
        }

        [HttpGet]
        [Route("Detail")]
        public async Task<IActionResult> Get(int id)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var item = await Logic.Clients.GetModel(id);
                if (item == null)
                {
                    return NotFound(Utilities.UnsuccessfulResponse(response, "Client does not exist"));
                }
                response.Data = item;
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response));
            }
        }

        [Route("Delete")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var found = Logic.Clients.Get(id);
                if (found == null)
                    return NotFound(Utilities.UnsuccessfulResponse(response, "Client does not exist"));
                Logic.Clients.Delete(found);
                response.Data = found;
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response));
            }
        }

        [Route("Update")]
        [HttpPost]
        public IActionResult Update(int id, ClientForm form)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                //form.Id = id;
                var model = Logic.Clients.Create(form);
                if (id != model.Id)
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "Route Parameter does not match model ID"));
                var found = Logic.Clients.Get(id);
                if (found == null)
                    return NotFound(Utilities.UnsuccessfulResponse(response, "Client does not exist"));
                var check = Logic.Clients.UpdateExists(model);
                if (check)
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "Client already exists"));
                var dto = Logic.Clients.Update(found, model,
                    "Name");
                response.Data = dto;
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response));
            }
        }

    }
}
