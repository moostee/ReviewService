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
    }
}