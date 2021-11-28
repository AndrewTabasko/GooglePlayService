using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EntityFrameworkProvider.Providers;
using GoogleApps.Interfaces.Entities;
using GoogleApps.Interfaces.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoogleApps.Backned
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppLinkController : ControllerBase
    {
        private readonly IAppProvider appProvider;

        public AppLinkController(IAppProvider appProvider)
        {
            this.appProvider = appProvider;
        }
    

        [Route("[action]/{Url}")]
        [HttpPost] 
        public IActionResult SaveApp(string Url)
        {
            appProvider.RegisterApp(
                new App
                {
                    URL = Url
                });
            return Ok();
        }

        [Route("[action]/{guid}")]
        [HttpGet]
        public IActionResult GetAppDetails(string guid)
        {
            var app = appProvider.GetAppByGuid(Guid.Parse(guid));
            if (app == null)
            {
                return StatusCode(404, "Not exist app details");
            }
            var appJson = JsonConvert.SerializeObject(app);
            return Ok(appJson);
        }
    }
}
