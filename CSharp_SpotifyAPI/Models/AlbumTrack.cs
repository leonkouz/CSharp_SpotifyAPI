using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharp_SpotifyAPI.Models
{
    public class AlbumTrack
    {
        [JsonProperty(PropertyName = "artists")]
        public List<Artist> Artists { get; set; }

        [JsonProperty(PropertyName = "available_markets")]
        public List<object> AvailableMarkets { get; set; }

        [JsonProperty(PropertyName = "disc_number")]
        public int DiscNumber { get; set; }

        [JsonProperty(PropertyName = "duration_ms")]
        public int DurationMs { get; set; }

        [JsonProperty(PropertyName = "@explicit")]
        public bool Explicit { get; set; }

        [JsonProperty(PropertyName = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "is_local")]
        public bool IsLocal { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "preview_url")]
        public object PreviewUrl { get; set; }

        [JsonProperty(PropertyName = "track_number")]
        public int TrackNumber { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
    }
}