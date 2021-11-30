using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GoogleApps;


namespace GoogleApps.Agent
{
    public class GreeterService : AppDetails.AppDetailsBase
    {
        public override Task<Reply> LoadAppData(AppGuid Id, ServerCallContext context)
        {
            
            return Task.FromResult(new Reply
            {
                Message = "successfully received"
            });
        }         
    }
}
