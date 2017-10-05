using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
    }
}
