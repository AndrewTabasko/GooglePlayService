using System;
using EntityFrameworkProvider;
using EntityFrameworkProvider.Repositories;
using GoogleApps.Interfaces.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            #region DB
            services.AddDbContextPool<AppsDataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("AppsDb")));
            services.AddTransient<IAppDbRepository, AppDbRepository>();
            #endregion

            #region Grpc
            services.AddGrpc();
            services.AddGrpcClient<Greeter.GreeterClient>(o => o.Address = new Uri(Configuration.GetSection("Grpc:Uri").Value)); 
            #endregion
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
