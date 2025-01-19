using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflections
{
    internal class AboutController
    {
        public AboutController() => AboutData = new Dictionary<string, string>()
        {
            ["Name"] = "Muhammad Elqasas",
            ["Address"] = "Egypt"
        };
        [Data]
        public IDictionary<string, string> AboutData { get; set; }
    }
}
