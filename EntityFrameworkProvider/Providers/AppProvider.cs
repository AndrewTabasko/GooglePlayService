using System;
using System.Threading.Tasks;
using GoogleApps.Interfaces.Entities;
using GoogleApps.Interfaces.Interfaces;

namespace EntityFrameworkProvider.Providers
{
    public class AppDbProvider : IAppDbProvider
    {
        private readonly IAppEFRepository repository;

        public AppDbProvider(IAppEFRepository repository)
        {
            this.repository = repository;
        }

        public App GetAppByGuid(Guid guid)
        {
            return repository.ReadAppByGuid(guid);
        }

        public async Task RegisterApp(App app)
        {
            await repository.InsertApp(app);
        }

        public async Task UpdateApp(App app)
        {
            await repository.UpdateApp(app);
        }
    }
}
