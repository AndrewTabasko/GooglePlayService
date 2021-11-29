using EntityFrameworkProvider;
using EntityFrameworkProvider.Providers;
using EntityFrameworkProvider.Repositories;
using GoogleApps.Interfaces.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GoogleApps.Backned
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
            services.AddDbContextPool<AppsDataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DBApps")));
            #endregion

            services.AddTransient<IAppEFRepository, AppEFRepository>();
            services.AddTransient<IAppProvider, AppProvider>();

            services.AddGrpc();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
                endpoints.MapControllers();
            });
        }
    }
}
