using System;
using System.Collections.Generic;
using System.Linq;

namespace RegExTester.Api.DotNet.Models
{
    public class Input
    {
        public bool ShowCaptures { get; set; }
        public string Pattern { get; set; }
        public string Text { get; set; }
    }
}
