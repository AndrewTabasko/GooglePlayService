using System;
using System.Threading.Tasks;
using GoogleApps.Interfaces.Entities;

namespace GoogleApps.Interfaces.Interfaces
{
    public interface IAppDbProvider
    {
        Task RegisterApp(App app);
        App GetAppByGuid(Guid guid);
        Task UpdateApp(App app);
    }
}
