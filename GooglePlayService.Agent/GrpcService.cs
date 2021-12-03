using System;
using System.Threading.Tasks;
using GoogleApps.Agent.Refit;
using GoogleApps.Interfaces.Entities;
using GoogleApps.Interfaces.Interfaces;
using Grpc.Core;
using Newtonsoft.Json;
using Refit;
using Serilog;

namespace GoogleApps.Agent
{
    public class GrpcService : Greeter.GreeterBase
    {
        private readonly IAppDbRepository appDbRepository;
        private readonly IGoogleAppMetadataProvider appMetadataProvider;
        private readonly ILogger logger;

        public GrpcService(IAppDbRepository dbProvider, IGoogleAppMetadataProvider appDetailsProvider, ILogger logger)
        {
            appDbRepository = dbProvider;
            appMetadataProvider = appDetailsProvider;
            this.logger = logger;
        }
        public async override Task<HelloReply> LoadAppMetadata(HelloRequest guid, ServerCallContext context)
        {
            if (Guid.TryParse(guid.Guid, out var parsedGuid))
            {
                var app = appDbRepository.ReadAppByGuid(parsedGuid);

                var appData = await RequestAppData(app);
                if (app != null)
                {
                    app.name = appData.title;
                    app.downloads = appData.maxInstalls;

                    await appDbRepository.UpdateApp(app);
                }
            }
            else
            {
                logger.Error("guid is not valid");
            }

            return await Task.FromResult(new HelloReply
            {
                Message = "successfully received"
            });
        }


        private async Task<AppMetadataDto> RequestAppData(App app)
        {
            var query = app.hl == null ? app.googleplayid : String.Concat(app.googleplayid, "?lang=", app.hl);
            AppMetadataDto appMetaDto = default;
            try
            {
                var apiResponce = await appMetadataProvider.AppMetadataRequestApi(query);
                appMetaDto = JsonConvert.DeserializeObject<AppMetadataDto>(apiResponce);
            }
            catch (ApiException e)
            {
                logger.Error("Google play api returned status code:" + e.StatusCode.ToString());
            }

            return appMetaDto;
        }
    }
}
