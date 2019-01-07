using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharp_SpotifyAPI.Models
{
    public class Albums
    {
        [JsonProperty("albums")]
        public List<Album> AlbumsList { get; set; }
    }
}