using System;
using GoogleApps.Interfaces.Entities;
using GoogleApps.Interfaces.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApps.Backned
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppDetailsController : ControllerBase
    {
        private readonly IAppDbProvider appDbProvider;
        private readonly AppDetails.AppDetailsClient grpcClient;

        public AppDetailsController(IAppDbProvider appProvider, AppDetails.AppDetailsClient grpcClient)
        {
            appDbProvider = appProvider;
            this.grpcClient = grpcClient;
        }


        [Route("[action]/{Url}")]
        [HttpPost]
        public async Task<IActionResult> SaveApp(string Url)
        {
            var guid = await SaveUrlDataToDb(Url);
            await grpcClient.LoadAppDataAsync(new AppGuid 
            {
                Guid = guid.ToString()
            });
            return Ok();
        }


        [Route("[action]/{guid}")]
        [HttpGet]
        public IActionResult GetAppDetails(string guid)
        {
            var app = appDbProvider.GetAppByGuid(Guid.Parse(guid));
            if (app == null)
            {
                return StatusCode(404, "Not exist app details");
            }
            var appJson = JsonConvert.SerializeObject(app);
            return Ok(appJson);
        }


        private async Task<Guid> SaveUrlDataToDb(string Url)
        {
            Uri uri;
            Guid guid = default;

            if (Uri.TryCreate(Url, UriKind.Absolute, out uri))
            {
                var query = uri.Query.Split('&');

                var app = new App()
                {
                    GooglePlayId = query[0].Substring(4),
                    Hl = query[1],
                    Gl = query[2]
                };
                guid = app.Guid;

                await appDbProvider.RegisterApp(app);                
            }
            return guid;
        }
    }
}
