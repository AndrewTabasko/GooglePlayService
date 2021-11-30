using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GoogleApps;
using GoogleApps.Interfaces.Interfaces;
using GoogleApps.Agent.Refit;
using System.Text;
using GoogleApps.Interfaces.Exceptions;
using GoogleApps.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace GoogleApps.Agent
{
    public class GrpcService : Greeter.GreeterBase
    {
        private readonly IAppDbProvider dbProvider;
        private readonly IGoogleAppDetailsProvider appDetailsProvider;

        public GrpcService(IAppDbProvider dbProvider, IGoogleAppDetailsProvider appDetailsProvider)
        {
            this.dbProvider = dbProvider;
            this.appDetailsProvider = appDetailsProvider;
        }

        public override Task<HelloReply> SayHello(HelloRequest guid, ServerCallContext context)
        {
            App app = default;
            try
            {
                app = dbProvider.GetAppByGuid(Guid.Parse(guid.Guid));
            }
            catch(Exception e)
            {
                throw new DbException(e.Message);
            }

            var details = GetDetailsApp(app);
            app.Name = details.title;
            app.Downloads = details.maxInstalls;

            dbProvider.UpdateApp(app);


            return Task.FromResult(new HelloReply
            {
                Message = "successfully received"
            });
        }



        private Root GetDetailsApp(App app)
        {
            string detailsJson = default;
            var query = String.Concat(app.GooglePlayId, app.Hl, app.Gl);
            try
            {
                detailsJson = appDetailsProvider.GetDetailApp(query);
            }
            catch(Exception e)
            {
                throw new GoogleApiException(e.Message);
            }
            var details = JsonConvert.DeserializeObject<Root>(detailsJson);
            return details;
        }
    }
}
