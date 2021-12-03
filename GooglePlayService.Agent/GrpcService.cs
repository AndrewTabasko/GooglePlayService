using System;
using System.Threading.Tasks;
using GoogleApps.Agent.Refit;
using GoogleApps.Interfaces.Entities;
using GoogleApps.Interfaces.Interfaces;
using Grpc.Core;
using Newtonsoft.Json;
using Refit;

namespace GoogleApps.Agent
{
    public class GrpcService : Greeter.GreeterBase
    {
        private readonly IAppDbRepository appDbRepository;
        private readonly IGoogleAppMetadataProvider appMetadataProvider;

        public GrpcService(IAppDbRepository dbProvider, IGoogleAppMetadataProvider appDetailsProvider)
        {
            appDbRepository = dbProvider;
            appMetadataProvider = appDetailsProvider;

        }
        public async override Task<HelloReply> LoadAppMetadata(HelloRequest guid, ServerCallContext context)
        {
            App app = default;
            try
            {
                app = appDbRepository.ReadAppByGuid(Guid.Parse(guid.Guid));
            }
            catch
            {
                throw new ArgumentException("Guid");
            }

            var appData = RequestAppData(app);
            if (app != null)
            {
                app.Name = appData.title;
                app.Downloads = appData.maxInstalls;

                await appDbRepository.UpdateApp(app);
            }

            return await Task.FromResult(new HelloReply
            {
                Message = "successfully received"
            });
        }


        private AppMetadataDto RequestAppData(App app)
        {
            var query = String.Concat(app.GooglePlayId, "?lang=", app.Hl);
            AppMetadataDto appMetaDto = default;
            try
            {
                var apiResponce = appMetadataProvider.AppMetadataRequestApi(query);
                appMetaDto = JsonConvert.DeserializeObject<AppMetadataDto>(apiResponce);
            }
            catch (ApiException e)
            {
                throw new Exception("Status code:" + e.StatusCode.ToString());
            }

            return appMetaDto;
        }
    }
}
