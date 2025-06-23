using BookStore.Library;
using Newtonsoft.Json;
using System.Net;

namespace BookStoreDemo
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleException(httpContext, HttpStatusCode.Unauthorized, "Yetkisiz işlem yapılamaz.", ex);
            }
            catch (TimeoutException ex)
            {
                await HandleException(httpContext, HttpStatusCode.RequestTimeout, "İstek zaman aşımına uğradı.", ex);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, HttpStatusCode.InternalServerError, "Sistemsel bir hata oluştu.", ex);
            }
        }

        private static async Task HandleException(HttpContext context, HttpStatusCode statusCode, string userMessage, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = new FunctionResult
            {
                IsSuccess = false,
                Message = userMessage,
                ExceptionMessage = ex.Message
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }

    }
}
