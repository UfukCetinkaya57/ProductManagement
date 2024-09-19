using Microsoft.AspNetCore.Builder;
using ProductManagement.API.Middlewares;

namespace ProductManagement.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
