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
    public class ReviewVoteTypesController : BaseApiController
    {
        private readonly ILogicModule Logic;

        public ReviewVoteTypesController(ILogicModule logic)
        {
            Logic = logic;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ReviewVoteTypeForm form)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var model = Logic.ReviewVoteTypes.Create(form);
                var check = Logic.ReviewVoteTypes.CreateExists(model);
                if (check)
                {
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "ReviewVotetype already exists"));
                }
                response.Data = await Logic.ReviewVoteTypes.Insert(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }

        [Route("Search", Name = "ReviewVoteTypeApi")]
        [HttpGet]
        public IActionResult Get(string sort = "id",string name = "", long page = 1, long pageSize = 10, string fields = "", int draw = 1)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var items = Logic.ReviewVoteTypes.SearchView(name, page, pageSize, sort);

                if (page > items.TotalPages) page = items.TotalPages;
                var jo = new JObjectHelper();
                jo.Add("name", name);

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
        public async Task<IActionResult> Get(int id)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var item = await Logic.ReviewVoteTypes.GetModel(id);
                if (item == null)
                {
                    return NotFound(Utilities.UnsuccessfulResponse(response, "ReviewVoteType does not exist"));
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

        [Route("Update")]
        [HttpPost]
        public IActionResult Update(int id, ReviewVoteTypeForm form)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                form.Id = id;
                var model = Logic.ReviewVoteTypes.Create(form);
                if (id != model.Id)
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "Route Parameter does mot match model ID"));
                var found = Logic.ReviewVoteTypes.Get(id);
                if (found == null)
                    return NotFound(Utilities.UnsuccessfulResponse(response, "ReviewVoteType does not exist"));
                var check = Logic.ReviewVoteTypes.UpdateExists(model);
                if (Logic.ReviewVoteTypes.UpdateExists(model))
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "ReviewVoteType configuration already exists"));
                response.Data = Logic.ReviewVoteTypes.Update(found, model,
                    "name");
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }

        [Route("Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var found = Logic.ReviewVoteTypes.Get(id);
                if (found == null)
                    return NotFound(Utilities.UnsuccessfulResponse(response, "ReviewVoteType does not exist"));
                Logic.ReviewVoteTypes.Delete(found);
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
