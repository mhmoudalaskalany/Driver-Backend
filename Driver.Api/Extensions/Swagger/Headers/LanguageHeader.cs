using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Driver.Api.Extensions.Swagger.Headers
{
    /// <summary>
    /// Language Header
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class LanguageHeader : IOperationFilter
    {
        /// <summary>
        /// Apply Filter
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Description = "Accept-Language",
                Required = false
            });
        }
    }
}
