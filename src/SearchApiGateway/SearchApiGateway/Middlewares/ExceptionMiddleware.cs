using System.Net;
using SearchService.Exceptions;

namespace SearchApiGateway.Middlewares
{
	public class ExceptionMiddleware
	{
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                switch(e)
                {
                    case BusinessException businessException:
                        await Response(context, HttpStatusCode.BadRequest, businessException.Message);
                        break;
                    default:
                        throw e;
                }
                    
            }
        }

        private async Task Response(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(message);
        }
    }

    public static class Middlewares
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

