using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RegExTester.Api.DotNet.Controllers
{
    [Route("api")]
    public class HomeController : Controller
    {
        // GET api/
        [HttpGet]
        public string Get()
        {
            return "RegExTester.Api.DotNet";
        }
    }
}
