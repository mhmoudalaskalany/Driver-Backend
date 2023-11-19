using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Asp.Versioning;
using Driver.Api.Extensions.Swagger.Headers;
using Driver.Api.Extensions.Swagger.Options;
using Driver.Application.Mapping;
using Driver.Application.Services.Driver;
using Driver.Common.Abstraction.UnitOfWork;
using Driver.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Driver.Api.Extensions
{
    /// <summary>
    /// Dependency Extensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ConfigureDependencyExtension
    {
        private const string ConnectionStringName = "Default";
        /// <summary>
        /// Register Extensions
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.RegisterDbConfiguration(configuration);
            services.AddLocalizationServices();
            services.AddServices();
            services.RegisterInfrastructure();
            services.RegisterAutoMapper();
            services.RegisterApiMonitoring();
            services.AddControllers();
            services.RegisterApiVersioning();
            services.RegisterLowerCaseUrls();
            services.RegisterSwaggerConfig();
            return services;
        }

        /// <summary>
        /// Add Register Db Configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void RegisterDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped((_) => new SqlConnection(configuration.GetConnectionString(ConnectionStringName)));
            services.AddScoped<IDbTransaction>(s =>
            {
                SqlConnection conn = s.GetRequiredService<SqlConnection>();
                conn.Open();
                return conn.BeginTransaction();
            });
        }


        /// <summary>
        /// register auto-mapper
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingService));

        }

        /// <summary>
        /// Register localization
        /// </summary>
        /// <param name="services"></param>
        private static void AddLocalizationServices(this IServiceCollection services)
        {
            services.AddLocalization();
        }

        /// <summary>
        /// Register Application Services Dependencies
        /// </summary>
        /// <param name="services"></param>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDriverService, DriverService>();
        }


        /// <summary>
        /// Add Health Checks
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterApiMonitoring(this IServiceCollection services)
        {
            services.AddHealthChecks();
        }




        /// <summary>
        /// Register Infrastructure
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        /// <summary>
        /// Register Api Versioning
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterApiVersioning(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddApiVersioning(config =>
                {
                    config.DefaultApiVersion = new ApiVersion(1, 0);
                    config.AssumeDefaultVersionWhenUnspecified = true;
                    config.ReportApiVersions = true;
                })
                .AddApiExplorer(config =>
                {
                    config.GroupNameFormat = "'v'VVV";
                    config.SubstituteApiVersionInUrl = true;
                });
        }


        /// <summary>
        /// Lower Case Urls
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterLowerCaseUrls(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
        }
        /// <summary>
        /// Swagger Config
        /// </summary>
        /// <param name="services"></param>

        private static void RegisterSwaggerConfig(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            services.AddSwaggerGen(options =>
            {
                var security = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }

                    }
                };
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(security);
                options.OperationFilter<LanguageHeader>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
