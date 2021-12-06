using System;
using EntityFrameworkProvider;
using EntityFrameworkProvider.Repositories;
using GoogleApps.Agent.Refit;
using GoogleApps.Interfaces.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace GoogleApps.Agent
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
            #region DB
            services.AddDbContextPool<AppsDataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("AppsDb")));
            services.AddTransient<IAppDbRepository, AppDbRepository>();
            #endregion

            #region Refit
            services.AddRefitClient<IGoogleAppMetadataProvider>().ConfigureHttpClient(x => x.BaseAddress = new Uri(Configuration.GetSection("RefitConfig:Uri").Value));
            #endregion

            #region gRPC
            services.AddGrpc();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcService>();
            });
        }
    }
}
