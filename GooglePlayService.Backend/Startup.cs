using EntityFrameworkProvider;
using EntityFrameworkProvider.Repositories;
using GoogleApps.Interfaces.Interfaces;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace GoogleApps.Backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOpenApiDocument(services => services.Title = "GooglePlay info");

            #region DB
            services.AddDbContextPool<AppsDataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("AppsDb")));
            services.AddTransient<IAppDbRepository, AppDbRepository>();
            #endregion

            #region Grpc
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            services.AddGrpc();
            services.AddGrpcClient<Greeter.GreeterClient>(o => o.Address = new Uri(Configuration.GetSection("Grpc:Uri").Value));
            #endregion
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3(x =>
                {
                    x.DocExpansion = "title";
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
