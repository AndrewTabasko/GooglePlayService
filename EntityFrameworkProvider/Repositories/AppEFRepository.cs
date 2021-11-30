using System;
using System.Linq;
using System.Threading.Tasks;
using GoogleApps.Interfaces.Entities;
using GoogleApps.Interfaces.Interfaces;
using Npgsql;

namespace EntityFrameworkProvider.Repositories
{
    public class AppEFRepository : IAppEFRepository
    {
        private readonly AppsDataContext context;
        public AppEFRepository(AppsDataContext context)
        {
            this.context = context;
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
                throw new ApplicationException(e.Message);
            }
        }

        public App ReadAppByGuid(Guid guid)
        {
            try
            {
                return context.Apps.FirstOrDefault(app => app.Guid.Equals(guid));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task UpdateApp(App app)
        {
            try
            {
                context.Apps.Update(app);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}
