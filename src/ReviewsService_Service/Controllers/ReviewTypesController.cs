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
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewTypesController : BaseApiController
    {
        private readonly ILogicModule Logic;

        public ReviewTypesController(ILogicModule logic)
        {
            Logic = logic;
        }

        /// <summary>
        /// Add ReviewType
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        [Produces(typeof(ReviewTypeModel))]
        public async Task<IActionResult> Create(ReviewTypeForm form)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var model = Logic.ReviewTypeLogic.Create(form);
                var check = Logic.ReviewTypeLogic.CreateExists(model);
                if (check)
                {
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "Review type already exists"));
                }
                response.Data = await Logic.ReviewTypeLogic.Insert(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }

        /// <summary>
        /// Update ReviewType
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPost]
        [Produces(typeof(ReviewTypeModel))]
        public IActionResult Update(long id, ReviewTypeForm form)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                form.Id = id;
                var model = Logic.ReviewTypeLogic.Create(form);
                if (id != model.Id)
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "Route Parameter does not match model ID"));
                var found = Logic.ReviewTypeLogic.Get(id);
                if (found == null)
                    return NotFound(Utilities.UnsuccessfulResponse(response, "ReviewType not found"));
                var check = Logic.ReviewTypeLogic.UpdateExists(model);
                if (check)
                    return BadRequest(Utilities.UnsuccessfulResponse(response,  "ReviewType configuration already exists"));
                response.Data = Logic.ReviewTypeLogic.Update(found, model,
                    "Name,RecordStatus");
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }


        /// <summary>
        /// Get ReviewType by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Detail")]
        [Produces(typeof(ReviewTypeModel))]
        public async Task<IActionResult> Get(long id)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var item = await Logic.ReviewTypeLogic.GetModel(id);
                if (item == null)
                {
                    return NotFound(Utilities.UnsuccessfulResponse(response, "ReviewType not found"));
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }



        /// <summary>
        /// Search, Page, filter and Shaped ReviewTypes
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="fields"></param>
        /// <param name="draw"></param>
        /// <returns></returns>
        [Produces(typeof(IEnumerable<ReviewTypeModel>))]
        [Route("Search", Name = "ReviewTypeApi")]
        [HttpGet]
        public IActionResult Get(string sort = "Id", string name = "", long page = 1, long pageSize = 10, string fields = "", int draw = 1)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var items = Logic.ReviewTypeLogic.SearchView(name, page, pageSize, sort);

                if (page > items.TotalPages) page = items.TotalPages;
                var jo = new JObjectHelper();
                jo.Add("name", name);

                jo.Add("fields", fields);
                jo.Add("sort", sort);
                var linkBuilder = new PageLinkBuilder(jo, page, pageSize, items.TotalItems, draw);
                AddHeader("X-Pagination", linkBuilder.PaginationHeader);
                var dto = new List<ReviewTypeModel>();
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

        /// <summary>
        /// Delete ReviewType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Delete")]
        [HttpPost]
        [Produces(typeof(ReviewTypeModel))]
        public IActionResult Delete(long id)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var found = Logic.ReviewTypeLogic.Get(id);
                if (found == null)
                    return NotFound(Utilities.UnsuccessfulResponse(response, "ReviewType not found"));
                Logic.ReviewTypeLogic.Delete(found);
                response.Data = found;
                return Ok(response);
            }
            catch(Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }

    }
}