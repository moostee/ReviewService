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
    public class AppClientsController : BaseApiController
    {
        private readonly ILogicModule Logic;

        public AppClientsController(ILogicModule logic)
        {
            Logic = logic;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(AppClientForm form)
        {
            var response = Utilities.InitializeResponse();
            try
            {
                var model = Logic.AppClients.Create(form);
                var check = Logic.AppClients.CreateExists(model);
                if (check)
                {
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "AppClient already exists"));
                }
                response.Data = await Logic.AppClients.Insert(model);
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
