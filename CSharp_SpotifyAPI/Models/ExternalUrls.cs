using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharp_SpotifyAPI.Models
{
    public class ExternalUrls
    {
        [JsonProperty(PropertyName = "spotify")]
        public string Spotify { get; set; }
    }
}