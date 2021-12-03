using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;

namespace GoogleApps.Agent.Refit
{
    public interface IGoogleAppMetadataProvider
    {
        [Get("/api/apps/{query}/")]
        string AppMetadataRequestApi(string query);
    }
}
