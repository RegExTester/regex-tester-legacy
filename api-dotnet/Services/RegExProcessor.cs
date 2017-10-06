using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using RegExTester.Api.DotNet.Models;

namespace RegExTester.Api.DotNet.Services
{
    public interface IRegExProcessor
    {
        dynamic Matches(string pattern, string text, RegExTesterOptions options);
    }

    public class RegExProcessor : IRegExProcessor
    {
        public dynamic Matches(string pattern, string text, RegExTesterOptions options)
        {
            var showCaptures = options.HasFlag(RegExTesterOptions.ShowCaptures);
            var regexOptions = showCaptures ? (RegexOptions)(options - RegExTesterOptions.ShowCaptures) : (RegexOptions)options;
            var regex = new Regex(pattern, regexOptions);
            var matches = regex.Matches(text);
            var result = new {
                //matches_count = matches.Count,
                matches = new List<dynamic>()
            };

            foreach (Match match in matches)
            {
                var matchItem = new {
                    name = match.Name,
                    index = match.Index,
                    length = match.Length,
                    value = match.Value,
                    //groups_count = match.Groups.Count,
                    groups = new List<dynamic>(),
                    //captures_count = showCaptures ? (int?)match.Captures.Count : null,
                    captures = showCaptures ? GetCaptures(match.Captures) : null
                };

                foreach (Group group in match.Groups)
                {
                    var groupItem = new {
                        name = group.Name,
                        index = group.Index,
                        length = group.Length,
                        value = group.Value,
                        //captures_count = showCaptures ? (int?)group.Captures.Count : null,
                        captures = showCaptures ? GetCaptures(group.Captures) : null
                    };

                    matchItem.groups.Add(groupItem);
                }

                result.matches.Add(matchItem);
            }

            return result;
        }

        private List<dynamic> GetCaptures(CaptureCollection captures)
        {
            var result = new List<dynamic>();
            foreach (Capture capture in captures)
            {
                result.Add(new {
                    index = capture.Index,
                    length = capture.Length,
                    value = capture.Value
                });
            }
            return result;
        }
    }
}
