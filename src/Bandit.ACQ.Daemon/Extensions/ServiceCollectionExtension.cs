using Bandit.ACQ.Daemon.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Bandit.ACQ.Daemon.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddSwaggerService(this IServiceCollection services, APIConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = config.Title,
                        Description = config.Description,
                        Version = "v1"
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IServiceCollection AddCorsHandling(this IServiceCollection services) => services.AddCors(o =>
        {
            o.AddPolicy("ALLOW_ANY", b =>
            {
                b.AllowAnyMethod().
                AllowAnyHeader().
                AllowCredentials().
                SetIsOriginAllowed(hostName => true);
            });
        });


        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app, Action<ExceptionHandlerMiddlewareOptions> configureOptions)
        {
            var options = new ExceptionHandlerMiddlewareOptions();
            configureOptions(options);
            return app.UseMiddleware<ExceptionHandlerMiddleware>(options);
        }


    }
}
