using Microsoft.AspNetCore.Builder;

namespace Gestalt.Api.Middlewares
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandler>();
        }
    }
}
