using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharp_SpotifyAPI.Models
{
    public class ExternalIds
    {
        [JsonProperty(PropertyName = "upc")]
        public string Upc { get; set; }
    }
}