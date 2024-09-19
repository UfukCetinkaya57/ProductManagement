using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace ProductManagement.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Oluşan hatayı logla
            Log.Error(context.Exception, "An unhandled exception occurred during the request.");

            // İstemciye uygun bir hata yanıtı döndür
            context.Result = new ObjectResult(new
            {
                StatusCode = 500,
                Message = "An unhandled exception occurred. Please try again later."
            })
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}
