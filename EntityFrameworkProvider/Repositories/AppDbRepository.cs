using System;
using System.Linq;
using System.Threading.Tasks;
using GoogleApps.Interfaces.Entities;
using GoogleApps.Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EntityFrameworkProvider.Repositories
{
    public class AppDbRepository : IAppDbRepository
    {
        private readonly AppsDataContext context;
        private readonly ILogger logger;
        public AppDbRepository(AppsDataContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task InsertApp(App app)
        {
            try
            {
                await context.Apps.AddAsync(app);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
        }

        public App ReadAppByGuid(Guid guid)
        {
            try
            {
                return context.Apps.FirstOrDefault(app => app.Guid.Equals(guid));
            }
            catch
            {
                return null;//log           
            }
        }

        public async Task UpdateApp(App app)
        {
            try
            {
                context.Apps.Update(app);
                await context.SaveChangesAsync();
            }
            catch
            {
                //log
            }
        }
    }
}
