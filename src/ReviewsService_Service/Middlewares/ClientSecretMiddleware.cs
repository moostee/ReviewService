using Microsoft.AspNetCore.Http;
using ReviewsService_Core.Common;
using ReviewsService_Core.Data;
using ReviewsService_Core.Domain.Model.Helper;
using System.Linq;
using System.Net;
using System.Text;
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

            if (context.Request.Path.ToUriComponent().Contains("Reviews"))
            {
                var _data = dataModule.AppClients;
                var response = Utilities.InitializeResponse();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                var reqAuthToken = context.Request.Headers["X-ClientSecret"];

                if (string.IsNullOrEmpty(reqAuthToken))
                {
                    await context.Response.WriteAsync(
                        SerializeUtility.SerializeJSON(Utilities.UnsuccessfulResponse(response, "X-ClientSecret header is required" + $"{context.Request.Path}")),
                        Encoding.UTF8);
                }
                else
                if (_data.Search(0, 0, reqAuthToken).FirstOrDefault()?.ClientSecret != reqAuthToken)
                {
                    await context.Response.WriteAsync(
                        SerializeUtility.SerializeJSON(Utilities.UnsuccessfulResponse(response, "Invalid access token")),
                        Encoding.UTF8);
                }
                else
                {
                    await _Next(context);
                }
            }
            else
            {
                await _Next(context);
            }
            
        }
    }

}
