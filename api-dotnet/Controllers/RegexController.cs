using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RegExTester.Api.DotNet.Models;
using RegExTester.Api.DotNet.Services;

namespace RegExTester.Api.DotNet.Controllers
{
    [Route("api/regex")]
    public class RegExController : Controller
    {
        public IRegExProcessor RegExProcessor { get; set; }

        public RegExController(IRegExProcessor regExProcessor)
        {
            this.RegExProcessor = regExProcessor;
        }

        // GET api/regex?p=*&t=*&c=1
        [HttpGet]
        public ActionResult Post(string p, string t, int? c)
        {
            var pattern = Uri.UnescapeDataString(p);
            var text = Uri.UnescapeDataString(t);
            var result = RegExProcessor.Matches(pattern, text, c.HasValue && c != 0);
            return Json(result);
        }

        // POST api/regex
        [HttpPost]
        public ActionResult Post([FromBody] Input model)
        {
            var processor = new RegExProcessor();
            var result = RegExProcessor.Matches(model.Pattern, model.Text, model.ShowCaptures);
            return Json(result);
        }
    }
}
