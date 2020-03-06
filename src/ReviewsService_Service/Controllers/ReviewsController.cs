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
    public class ReviewsController : BaseApiController
    {
        private readonly ILogicModule Logic;

        public ReviewsController(ILogicModule logic)
        {
            Logic = logic;
        }

        /// <summary>
        /// Add Review
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        [Produces(typeof(ReviewModel))]
        public async Task<IActionResult> Create(ReviewForm form)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var model = Logic.ReviewLogic.Create(form);
                var check = Logic.ReviewLogic.CreateExists(model);
                if (check)
                {
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "Review already exists"));
                }
                response.Data = await Logic.ReviewLogic.Insert(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }


        /// <summary>
        /// Update Review
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPost]
        [Produces(typeof(ReviewModel))]
        public IActionResult Update(Guid id, ReviewForm form)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                form.Id = id;
                var model = Logic.ReviewLogic.Create(form);
                if (id != model.Id)
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "Route Parameter does not match model ID"));
                var found = Logic.ReviewLogic.Get(id);
                if (found == null)
                    return NotFound(Utilities.UnsuccessfulResponse(response, "Review not found"));
                var check = Logic.ReviewLogic.UpdateExists(model);
                if (check)
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "Review configuration already exists"));
                response.Data = Logic.ReviewLogic.Update(found, model,
                    "AppClientId,Comment,Rating,AppFeature,UserId,IsActive,ReviewTypeId,ParentId,RecordStatus");
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }

        /// <summary>
        /// Delete Review
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Delete")]
        [HttpPost]
        [Produces(typeof(ReviewModel))]
        public IActionResult Delete(Guid id)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var found = Logic.ReviewLogic.Get(id);
                if (found == null)
                    return NotFound(Utilities.UnsuccessfulResponse(response, "Review not found"));
                Logic.ReviewLogic.Delete(found);
                response.Data = found;
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(Utilities.CatchException(response, ex.Message));
            }
        }


        /// <summary>
        /// Search, Page, filter and Shaped Reviews
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="appClientId"></param>
        /// <param name="comment"></param>
        /// <param name="rating"></param>
        /// <param name="appFeature"></param>
        /// <param name="userId"></param>
        /// <param name="isActive"></param>
        /// <param name="reviewTypeId"></param>
        /// <param name="parentId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="fields"></param>
        /// <param name="draw"></param>
        /// <returns></returns>
        [Produces(typeof(IEnumerable<ReviewModel>))]
        [Route("Search", Name = "ReviewApi")]
        [HttpGet]
        public IActionResult Get(string sort = "Id", long appClientId = 0, string comment = "", int rating = 0, string appFeature = "", string userId = "", bool? isActive = null, long reviewTypeId = 0, int parentId = 0, long page = 1, long pageSize = 10, string fields = "", int draw = 1)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var items = Logic.ReviewLogic.SearchView(appClientId, comment, rating, appFeature, userId, isActive, reviewTypeId, parentId, page, pageSize, sort);

                if (page > items.TotalPages) page = items.TotalPages;
                var jo = new JObjectHelper();
                jo.Add("appClientId", appClientId);
                jo.Add("comment", comment);
                jo.Add("rating", rating);
                jo.Add("appFeature", appFeature);
                jo.Add("userId", userId);
                jo.Add("isActive", isActive);
                jo.Add("reviewTypeId", reviewTypeId);
                jo.Add("parentId", parentId);

                jo.Add("fields", fields);
                jo.Add("sort", sort);
                var linkBuilder = new PageLinkBuilder(jo, page, pageSize, items.TotalItems, draw);
                AddHeader("X-Pagination", linkBuilder.PaginationHeader);
                var dto = new List<ReviewModel>();
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
        /// Get Review by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Detail")]
        [Produces(typeof(ReviewModel))]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var item = await Logic.ReviewLogic.GetModel(id);
                if (item == null)
                {
                    return NotFound(Utilities.UnsuccessfulResponse(response, "Review not found"));
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