using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewsService_Core.Common;

namespace ReviewsService_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        protected void AddHeader(string key, object data)
        {

            HttpContext.Response.Headers.Add(key, SerializeUtility.SerializeJSON(data));
        }
    }
}
