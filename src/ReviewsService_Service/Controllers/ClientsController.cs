using Microsoft.AspNetCore.Mvc;
using ReviewsService_Core.Domain.Form;
using ReviewsService_Core.Domain.Model.Helper;
using ReviewsService_Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsService_Service.Controllers
{
    public class ClientsController : BaseApiController
    {
        private readonly ILogicModule Logic;

        public ClientsController(ILogicModule logic)
        {
            Logic = logic;
        }
        
        /// <summary>
        /// Add Client
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ClientForm form)
        {

            var response = Utilities.InitializeResponse();
            try
            {
                var model = Logic.Clients.Create(form);
                var check = Logic.Clients.CreateExists(model);
                if (check)
                {
                    return BadRequest(Utilities.UnsuccessfulResponse(response, "Client already exists"));
                }
                var dto = await Logic.Clients.Insert(model);
                response.Data = dto;
                return Ok(response);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return BadRequest(Utilities.CatchException(response));
            }
        }

       
    }



}
