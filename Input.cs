using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTool
{
    public class Input
    {
        public string Method { get; set; }

        public string Apikey { get; set; }

        public JObject Parameters { get; set; }
    }
}
