using System;
using System.Linq;
using System.Threading.Tasks;
using GoogleApps.Interfaces.Entities;
using GoogleApps.Interfaces.Interfaces;
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
                await context.apps.AddAsync(app);
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
                return context.apps.FirstOrDefault(app => app.guid.Equals(guid));
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return null;
            }
        }

        public async Task UpdateApp(App app)
        {
            try
            {
                context.apps.Update(app);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
        }
    }
}
