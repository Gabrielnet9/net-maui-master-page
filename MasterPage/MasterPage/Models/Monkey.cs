using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterPage.Models
{
    public class Monkey
    {

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("Details")]
        public string Details { get; set; }

        [JsonProperty("Image")]
        public string Image { get; set; }

        [JsonProperty("Population")]
        public int Population { get; set; }
    }
}
