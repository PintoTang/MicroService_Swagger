using DbContext.Lib;
using Swagger.Lib.Abstractions;
using Swagger.Lib.Entity;
using Swagger.Lib.Implemention;
using Swagger.Lib.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Swagger.Lib
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFengSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            SwaggerOption option = new SwaggerOption();
            configuration.GetSection("Swagger").Bind(option);
            //services.Configure<SwaggerOption>(configuration.GetSection("Swagger"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(option.DocName, new Info
                {
                    Title = option.Title,
                    Version = option.Version,
                    Description = option.Description,
                    Contact = new Contact
                    {
                        Name = option.ContactName,
                        Email = option.ContactEmail
                    }
                });
                c.OperationFilter<HeadOperationFilter>();

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, option.XmlFile));
                // Swagger验证部分
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "请输入带有Bearer的Token", Name = "Authorization", Type = "apiKey" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string>() } });
            });
            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            return services;
        }

        public static IServiceCollection AddFengGatewaySwagger(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            SwaggerOption option = new SwaggerOption();
            configuration.GetSection("Swagger").Bind(option);
            //services.Configure<SwaggerOption>(configuration.GetSection("Swagger"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(option.DocName, new Info
                {
                    Title = option.Title,
                    Version = option.Version
                });
                
            });

            services.AddSingleton<ISwaggerProjectRepository>(imp => {
                return new MySqlSwaggerProjectRepository(imp.GetRequiredService<IDbRepository<ProjectInfo>>());
            });

            return services;
        }

    }
}
