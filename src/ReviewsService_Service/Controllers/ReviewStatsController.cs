using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewsService_Core.Common;
using ReviewsService_Core.Domain.Model;
using ReviewsService_Core.Domain.Model.Helper;
using ReviewsService_Core.Logic;
using ReviewsService_Core.UI;

namespace ReviewsService_Service.Controllers
{
    public class ReviewStatsController : BaseApiController
    {
        private readonly ILogicModule Logic;

        public ReviewStatsController(ILogicModule logic)
        {
            Logic = logic;
        }

        [Route("Search", Name = "ReviewStatsApi")]
        [HttpGet]
        public IActionResult Get(string appFeature)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var clientSecret = HttpContext.Request.Headers["X-ClientSecret"];
                var appClientId = Logic.AppClients.Search(0, 0, clientSecret).FirstOrDefault().Id;
                var items = Logic.ReviewLogic.Search(appClientId, "", 0, appFeature, "", true);
                ReviewStatsModel reviewStatsModel = new ReviewStatsModel();
                foreach (var item in items)
                {
                    switch (item.Rating)
                    {
                        case 5:
                            reviewStatsModel.Rating5 += 1;
                            reviewStatsModel.TotalUsers += 1;
                            break;
                        case 4:
                            reviewStatsModel.Rating4 += 1;
                            reviewStatsModel.TotalUsers += 1;
                            break;
                        case 3:
                            reviewStatsModel.Rating3 += 1;
                            reviewStatsModel.TotalUsers += 1;
                            break;
                        case 2:
                            reviewStatsModel.Rating2 += 1;
                            reviewStatsModel.TotalUsers += 1;
                            break;
                        case 1:
                            reviewStatsModel.Rating1 += 1;
                            reviewStatsModel.TotalUsers += 1;
                            break;
                    }
                }

                if(reviewStatsModel.TotalUsers == 0)
                {
                    response.Data = reviewStatsModel;
                    return Ok(response);
                }

                reviewStatsModel.AverageRatings = ((5 * reviewStatsModel.Rating5 + 4 * reviewStatsModel.Rating4 + 3 * reviewStatsModel.Rating3 + 2 * reviewStatsModel.Rating2 + reviewStatsModel.Rating1) / reviewStatsModel.TotalUsers);

                response.Data = reviewStatsModel;

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