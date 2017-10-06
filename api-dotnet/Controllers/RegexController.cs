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

        // GET api/regex?p=*&t=*&o=0
        [HttpGet]
        public ActionResult Get(string p, string t, int? o)
        {
            var pattern = Uri.UnescapeDataString(p);
            var text = Uri.UnescapeDataString(t);
            var options = o.HasValue ? (RegExTesterOptions)o : RegExTesterOptions.None;
            var result = RegExProcessor.Matches(pattern, text, options);
            return Json(result);
        }

        // POST api/regex
        [HttpPost]
        public ActionResult Post([FromBody] Input model)
        {
            var processor = new RegExProcessor();
            var result = RegExProcessor.Matches(model.Pattern, model.Text, model.Options);
            return Json(result);
        }
    }
}
