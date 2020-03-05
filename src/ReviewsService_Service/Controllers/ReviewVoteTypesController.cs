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

        
    }
}
