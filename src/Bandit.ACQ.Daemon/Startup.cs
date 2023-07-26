using Bandit.ACQ.Daemon.Configuration;
using Bandit.ACQ.Daemon.Extensions;
using Bandit.ACQ.Daemon.Helpers;
using Bandit.ACQ.Daemon.Services;
using Bandit.ACS.Daemon.Services;

namespace Bandit.ACQ.Daemon
{
    public class Startup
    {
        private IConfiguration _configuration;
        private DaemonConfiguration _parsedConfiguration;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _parsedConfiguration = ConfigurationParser.Parse(_configuration, environment.IsDevelopment());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddSingleton(_parsedConfiguration)
                .AddEndpointsApiExplorer()
                .AddCorsHandling()
                .AddSwaggerService(_parsedConfiguration.API)
                .AddLogging(b =>
                {
                    b.AddConfiguration(_configuration.GetSection("Logging"));
                    b.AddConsole();
                })
                .AddSingleton<ICertificateHelper, LocalCertificateHelper>()
                .AddScoped<IBankService, BankService>()
                .AddScoped<IPaymentService, PaymentService>()
                .AddScoped<IAnalyticsService, AnalyticsService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.DocumentationPath = _parsedConfiguration.API.ErrorDocumentationUri;
            });

            app.UseCors("ALLOW_ANY");

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
