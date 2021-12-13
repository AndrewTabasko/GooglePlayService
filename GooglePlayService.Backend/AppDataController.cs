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
            var reply = await grpcClient.LoadAppMetadataAsync(new HelloRequest
            {
                Guid = guid.ToString()
            });

            logger.Information(reply.Message);
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
                var app = new App();

                var query = uri.Query.Split('&');
                foreach (var q in query)
                {
                    switch (q)
                    {
                        case string hl when hl.StartsWith("hl="):
                            app.hl = hl.Trim("hl=".ToCharArray());
                            break;

                        case string gl when gl.StartsWith("gl="):
                            app.gl = gl.Trim("gl=".ToCharArray());
                            break;

                        case string id when id.StartsWith("?id="):
                            app.googleplayid = id.Trim("?id=".ToCharArray());
                            break;
                    }
                }
                
                await appDbRepository.InsertApp(app);
                return app.guid;
            }
            else
            {
                logger.Information("Not valid URL");
                throw new UriFormatException();
            }
        }


    }
}
