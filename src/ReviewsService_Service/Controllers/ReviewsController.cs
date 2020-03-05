using Microsoft.AspNetCore.Mvc;
using ReviewsService_Core.Common;
using ReviewsService_Core.Domain.Form;
using ReviewsService_Core.Domain.Model;
using ReviewsService_Core.Domain.Model.Helper;
using ReviewsService_Core.Logic;
using System;
using System.Threading.Tasks;

namespace ReviewsService_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
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

    }
}