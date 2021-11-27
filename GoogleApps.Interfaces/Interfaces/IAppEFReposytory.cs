using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoogleApps.Interfaces.Entities;

namespace GoogleApps.Interfaces.Interfaces
{
    public interface IAppEFReposytory
    {
        Task InsertApp(App app);
        App ReadAppByGuid(Guid guid);
        Task UpdateApp(App app);

    }
}
