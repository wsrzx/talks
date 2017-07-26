using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodePooper.Model
{
    public class Pooper
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "howMuch")]
        public int HowMuch { get; set; }
    }
}
