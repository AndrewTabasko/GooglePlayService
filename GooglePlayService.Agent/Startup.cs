using System;
using EntityFrameworkProvider.Providers;
using EntityFrameworkProvider.Repositories;
using GoogleApps.Agent.Refit;
using GoogleApps.Interfaces.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddTransient<IAppEFRepository, AppEFRepository>();
            services.AddTransient<IAppDbProvider, AppDbProvider>();

            services.AddRefitClient<IGoogleAppDetailsProvider>().ConfigureHttpClient(x => x.BaseAddress = new Uri(Configuration.GetSection("RefitConfig:Uri").Value));
                        
            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcService>();
            });
        }
    }
}
