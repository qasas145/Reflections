using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflections
{
    internal class HomeController
    {
        public HomeController() => HomeData = new Dictionary<string, string>()
            {
                ["Name"] = "Mohamed Elsayed",
                ["Second Name"] = "Ahmed Elqasas"
            };
        [Data]
        public IDictionary<string, string> HomeData { get; set; }
    }
}
