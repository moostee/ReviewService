using Microsoft.AspNetCore.Http;
using ReviewsService_Core.Data;
using System.Threading.Tasks;

namespace ReviewsService_Service.Middlewares
{
    public class ClientSecretMiddleware
    {
        private readonly RequestDelegate _Next;
        public ClientSecretMiddleware(RequestDelegate next)
        {
            _Next = next;
        }

        public async Task Invoke(HttpContext context, IDataModule dataModule)
        {
            //var _data = dataModule.AppClients;
            //var response = Utilities.InitializeResponse();
            //context.Response.ContentType = "application/json";
            //context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //var reqAuthToken = context.Request.Headers["X-ClientSecret"];

            //if (string.IsNullOrEmpty(reqAuthToken))
            //{
            //    await context.Response.WriteAsync(
            //        SerializeUtility.SerializeJSON(Utilities.UnsuccessfulResponse(response, "X-ClientSecret header is required")),
            //        Encoding.UTF8);
            //}
            //else
            //if (_data.Search(reqAuthToken).FirstOrDefault()?.ClientSecret != reqAuthToken)
            //{
            //    await context.Response.WriteAsync(
            //        SerializeUtility.SerializeJSON(Utilities.UnsuccessfulResponse(response, "Invalid access token")),
            //        Encoding.UTF8);
            //}
            //else
            //{
            //    await _Next(context);

            //}

        }
    }

}
