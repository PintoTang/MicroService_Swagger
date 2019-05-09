using Swagger.Lib.Abstractions;
using Swagger.Lib.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Swagger.Lib
{
    public static class SwaggerExtensions
    {

        public static IApplicationBuilder UseFengSwagger(this IApplicationBuilder app, IConfiguration configuration) {
            SwaggerOption option = new SwaggerOption();
            configuration.GetSection("Swagger").Bind(option);
            app.UseFengSwagger(option);
            return app;
        }
        public static IApplicationBuilder UseFengSwagger(this IApplicationBuilder app, SwaggerOption options)
        {
            if (options == null)
                throw new ArgumentException("Missing Dependency", nameof(options));

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "doc/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/doc/{options.DocName}/swagger.json",
                    $"{options.Title} {options.Version}");
            });

            return app;
        }
        public static IApplicationBuilder UseFengGatewaySwagger(this IApplicationBuilder app)
        {
            var swaggerProject = app.ApplicationServices.GetRequiredService<ISwaggerProjectRepository>() ??
               throw new ArgumentException("Missing Dependency", nameof(ISwaggerProjectRepository));

            var apiList = swaggerProject.GetProject().GetAwaiter().GetResult();
            
            app.UseSwagger()
                .UseSwaggerUI(options =>
                {

                    apiList.ForEach(apiItem =>
                    {
                        options.SwaggerEndpoint($"/doc/{apiItem.RouteKey}_api/swagger.json", $"{apiItem.Name}");
                    });
                });

            return app;
        }
    }
}
