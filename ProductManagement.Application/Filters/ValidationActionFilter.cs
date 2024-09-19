using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace ProductManagement.API.Filters
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // ModelState hatalarını logla
                Log.Error("ModelState validation errors: {@ModelStateErrors}", context.ModelState);

                // Hata yanıtını ayarla
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
