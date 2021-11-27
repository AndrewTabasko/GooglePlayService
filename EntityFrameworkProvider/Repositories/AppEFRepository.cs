using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoogleApps.Interfaces.Entities;
using GoogleApps.Interfaces.Interfaces;
using Npgsql;

namespace EntityFrameworkProvider.Repositories
{
    public class AppEFRepository : IAppEFReposytory
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
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public App ReadAppByGuid(Guid guid)
        {
            return context.Apps.FirstOrDefaultAsync(app => app.Guid)
        }

        public Task UpdateApp(App app)
        {
            throw new NotImplementedException();
        }
    }
}
