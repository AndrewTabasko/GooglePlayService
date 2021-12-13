using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Refit;

namespace GoogleApps.Agent.Refit
{
    public interface IGoogleAppMetadataProvider
    {
        [Get("/api/apps/{query}")]
        Task<string> AppMetadataRequestApi([Query]string query);// attribute query or pathewq
    }
}
