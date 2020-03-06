using Microsoft.AspNetCore.Mvc;
using ReviewsService_Core.Common;
using ReviewsService_Core.Domain.Form;
using ReviewsService_Core.Domain.Model;
using ReviewsService_Core.Domain.Model.Helper;
using ReviewsService_Core.Logic;
using ReviewsService_Core.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewsService_Service.Controllers
{
    public class AppsController : BaseApiController
    {

        private readonly ILogicModule Logic;
        public AppsController(ILogicModule logic)
        {
            Logic = logic;
        }
        /// <summary>
        /// Search, Page, filter and Shaped Apps
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="fields"></param>
        /// <param name="draw"></param>
        /// <returns></returns>
        [Route("Search", Name = "AppApi")]
        [HttpGet]
        public IActionResult Get(string sort = "Id", string name = "", string description = "", long page = 1, long pageSize = 10, string fields = "", int draw = 1)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var items = Logic.AppLogic.SearchView(name, description, page, pageSize, sort);

                if (page > items.TotalPages) page = items.TotalPages;
                var jo = new JObjectHelper();
                jo.Add("name", name);
                jo.Add("description", description);

                jo.Add("fields", fields);
                jo.Add("sort", sort);

                var linkBuilder = new PageLinkBuilder(jo, page, pageSize, items.TotalItems, draw);
                AddHeader("X-Pagination", linkBuilder.PaginationHeader);
                var dto = new List<AppModel>();
                if (items.TotalItems <= 0) return Ok(response);
                response.Data = items.Items.ShapeList(fields);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response,ex.Message));
            }
        }

        /// <summary>
        /// Get App by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Detail")]
        public async Task<IActionResult> Get(int id)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var item = await Logic.AppLogic.GetModel(id);
                if (item == null)
                {
                    return NotFound(Utilities.UnsuccessfulResponse(response,"App not found"));
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

        /// <summary>
        /// Add App
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(AppForm form)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var model = Logic.AppLogic.Create(form);
                var check = Logic.AppLogic.CreateExists(model);
                if (check)
                {
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "App already exists"));
                }
                response.Data = await Logic.AppLogic.Insert(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }


        /// <summary>
        /// Update App
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPost]
        public IActionResult Update(int id, AppForm form)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                form.Id = id;
                var model = Logic.AppLogic.Create(form);
                if (id != model.Id)
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "Route Parameter does not match model ID"));
                var found = Logic.AppLogic.Get(id);
                if (found == null)
                    return NotFound(Utilities.UnsuccessfulResponse(response, "App not found"));
                var check = Logic.AppLogic.UpdateExists(model);
                if (check)
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "App configuration already exists"));
                response.Data = Logic.AppLogic.Update(found, model,
                    "Name,Description,RecordStatus");
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }

        /// <summary>
        /// Delete App
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var found = Logic.AppLogic.Get(id);
                if (found == null)
                    return NotFound(Utilities.UnsuccessfulResponse(response, "App not found"));
                Logic.AppLogic.Delete(found);
                response.Data = found;
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