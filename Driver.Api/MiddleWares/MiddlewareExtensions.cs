using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;

namespace Driver.Api.MiddleWares
{
    /// <summary>
    /// Extend Middleware
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<LanguageMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
