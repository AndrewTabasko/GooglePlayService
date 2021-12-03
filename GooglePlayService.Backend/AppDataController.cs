using System;
using GoogleApps.Interfaces.Entities;
using GoogleApps.Interfaces.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace GoogleApps.Backend
{
    [ApiController]
    public class AppDataController : ControllerBase
    {
        private readonly IAppDbRepository appDbRepository;
        private readonly Greeter.GreeterClient grpcClient;
        private readonly ILogger logger;

        public AppDataController(IAppDbRepository appProvider, Greeter.GreeterClient grpcClient, ILogger logger)
        {
            appDbRepository = appProvider;
            this.grpcClient = grpcClient;
            this.logger = logger;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> LoadAppData(string Url)
        {
            var guid = await SaveUrlDataToDb(Url);
            await grpcClient.LoadAppMetadataAsync(new HelloRequest
            {
                Guid = guid.ToString()
            });
            return Ok();
        }


        [Route("[action]/{guid}")]
        [HttpGet]
        public IActionResult GetAppData(string guid)
        {
            var guidResult = Guid.TryParse(guid, out var parsedGuid);
            if (!guidResult)
            {
                return StatusCode(500, "Incorrect guid format");
            }

            var app = appDbRepository.ReadAppByGuid(parsedGuid);
            if (app == null)
            {
                return StatusCode(404, "Not found app data");
            }

            var appJson = JsonConvert.SerializeObject(app);
            return Ok(appJson);
        }


        private async Task<Guid> SaveUrlDataToDb(string Url)
        {
            if ((Uri.TryCreate(Url, UriKind.RelativeOrAbsolute, out var uri) && (uri.Host.Equals("play.google.com"))))
            {
                var query = uri.Query.Split('&');

                var app = new App()
                {
                    GooglePlayId = query[0].Substring(4),
                    Hl = query[1],
                    Gl = query[2]
                };
                var guid = app.Guid;

                await appDbRepository.InsertApp(app);
                return guid;
            }
            else
            {
                logger.Information("Not valid URL");
                throw new UriFormatException();
            }
        }
    }
}
