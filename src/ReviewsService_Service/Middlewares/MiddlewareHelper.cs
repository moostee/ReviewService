using Microsoft.AspNetCore.Builder;

namespace ReviewsService_Service.Middlewares
{
    public static class MiddlewareHelper
    {
        public static IApplicationBuilder UseClientSecretMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ClientSecretMiddleware>();
        }
    }
}
