using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Gestalt.Api.Middlewares
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandler(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case Constants.NotAuthorizedExceptionMessage:
                        context.Response.StatusCode = 401;
                        break;
                    default:
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync($"Unexpexted error:{Environment.NewLine}{ex.Message}");
                        break;
                }
            }
        }
    }
}