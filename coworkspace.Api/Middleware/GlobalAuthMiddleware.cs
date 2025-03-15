using Coworkspace.Api.Config;

namespace Coworkspace.Api.Middleware
{
    public class GlobalAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!AuthConfig.IsPublicEndpoint(context.Request.Path))
            {
                if (!context.User.Identity?.IsAuthenticated ?? true)
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }

            await _next(context);
        }
    }

    public static class GlobalAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalAuth(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalAuthMiddleware>();
        }
    }
}