﻿using System.Diagnostics.CodeAnalysis;
using Asp.Versioning.ApiExplorer;
using Driver.Common.Abstraction.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Driver.Api.Extensions
{
    /// <summary>
    /// Pipeline Extensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ConfigureMiddlewareExtension
    {
        /// <summary>
        /// General Configuration Method
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IApplicationBuilder Configure(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.ConfigureCors();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.SwaggerConfig(provider);
            app.UseHealthChecks("/probe");
            app.MigrateDatabase();
            return app;
        }
        /// <summary>
        /// Configure Cors
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }


        /// <summary>
        /// Migrate Database
        /// </summary>
        /// <param name="app"></param>
        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var driverRepository = scope.ServiceProvider.GetRequiredService<IRepository<Domain.Entities.Driver>>();
            // Ensure the Drivers table is created
            driverRepository.CreateDriversTable();
        }


        /// <summary>
        /// User Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        private static void SwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant()
                    );
                }
                options.DocExpansion(DocExpansion.List);
            });
        }

    
    }
}
