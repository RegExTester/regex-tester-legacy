using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;

namespace RegExTester.Api.DotNet.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        // GET /
        [HttpGet]
        public RedirectResult Get()
        {
            return Redirect("https://regextester.github.io/");
        }

        // GET api/version
        [HttpGet]
        [Route("/api/version")]
        public ActionResult Version()
        {
            return Json(new {
                os = Environment.OSVersion.VersionString,
                platform = Environment.OSVersion.Platform.ToString(),
                framework = PlatformServices.Default.Application.RuntimeFramework.FullName
            });
        }
    }
}
