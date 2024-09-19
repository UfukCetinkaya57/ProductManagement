using Microsoft.AspNetCore.Http;
using Newtonsoft.Json; // JSON serileştirme için
using Serilog;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var logContext = new
            {
                RequestMethod = context.Request.Method,
                RequestPath = context.Request.Path,
                RequestBody = await ReadRequestBodyAsync(context)
            };

            try
            {
                // Orijinal yanıt akışını yedekle
                var originalBodyStream = context.Response.Body;

                // Yanıtı yakalayabilmek için bir MemoryStream kullan
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    // Sonraki middleware'e geç
                    await _next(context);

                    // Yanıtı işleyip logla
                    await LogResponseAsync(context, logContext);

                    // Orijinal yanıt akışını geri yükleyin
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                // Yakalanamayan istisnaları logla
                await HandleExceptionAsync(context, ex, logContext);
            }
        }

        private async Task<string> ReadRequestBodyAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            context.Request.Body.Position = 0;
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0; // Body'yi tekrar okumak için başa döndür
            return body;
        }

        private async Task LogResponseAsync(HttpContext context, object logContext)
        {
            // Yanıtı oku ve logla
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            // Logu tek bir noktada logla
            if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
            {
                Log.Information("Request completed successfully: {@LogContext} with status code {StatusCode}. Response: {ResponseBodyText}", logContext, context.Response.StatusCode, responseBodyText);
            }
            else if (context.Response.StatusCode >= 400 && context.Response.StatusCode < 500)
            {
                Log.Warning("Request failed: {@LogContext} with status code {StatusCode}. Response: {ResponseBodyText}", logContext, context.Response.StatusCode, responseBodyText);
            }
            else if (context.Response.StatusCode >= 500)
            {
                Log.Error("Server error: {@LogContext} with status code {StatusCode}. Response: {ResponseBodyText}", logContext, context.Response.StatusCode, responseBodyText);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, object logContext)
        {
            var errorResponse = new
            {
                Message = "An unexpected error occurred.",
                Details = exception.Message,
                StackTrace = exception.StackTrace
            };

            // Hataları tek satırda logla
            Log.ForContext("ExceptionType", exception.GetType().Name)
               .ForContext("ExceptionMessage", exception.Message)
               .ForContext("StackTrace", exception.StackTrace)
               .Error(exception, "An unexpected error occurred while processing the request: {@LogContext}. Error Details: {@ErrorResponse}", logContext, errorResponse);

            // Hata yanıtını ayarla
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorJson = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(errorJson);
        }
    }
}
