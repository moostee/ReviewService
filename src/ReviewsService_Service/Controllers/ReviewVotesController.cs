using Microsoft.AspNetCore.Mvc;
using ReviewsService_Core.Common;
using ReviewsService_Core.Domain.Entity;
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
    /// <summary>
    /// ReviewVotes CRUD
    /// </summary>
    public class ReviewVotesController : BaseApiController
    {
        private readonly ILogicModule Logic;

        public ReviewVotesController(ILogicModule logic)
        {
            Logic = logic;
        }
        /// <summary>
        /// Add ReviewVote
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        [Produces(typeof(ReviewVoteModel))]
        public async Task<IActionResult> Create(ReviewVoteForm form)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var model = Logic.ReviewVotes.Create(form);
                var check = Logic.ReviewVotes.CreateExists(model);
                if (check)
                {
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "Review vote already exists"));
                }
                response.Data = await Logic.ReviewVotes.Insert(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        [Route("EditHistory")]
        [HttpGet]
        public IActionResult Get(Guid reviewId, string userId)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var reviewVotes = Logic.ReviewVotes.Search(reviewId.ToString(), userId);
                var dto = new List<ReviewVoteModel>();
                foreach (var reviewVote in reviewVotes)
                {
                    dto.Add(Logic.ReviewVotes.Create(reviewVote));
                };
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
