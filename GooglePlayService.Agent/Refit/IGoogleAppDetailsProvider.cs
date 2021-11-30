using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;

namespace GoogleApps.Agent.Refit
{
    public interface IGoogleAppDetailsProvider
    {
        [Get("/api/apps/{query}/")]
        string GetDetailApp(string query);
    }
}
