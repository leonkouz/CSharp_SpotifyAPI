using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharp_SpotifyAPI.Models
{
    public class Album
    {
        [JsonProperty(PropertyName = "album_type")]
        public string AlbumType { get; set; }

        [JsonProperty(PropertyName = "artists")]
        public List<Artist> Artists { get; set; }

        [JsonProperty(PropertyName = "available_markets")]
        public List<object> AvailableMarkets { get; set; }

        [JsonProperty(PropertyName = "copyrights")]
        public List<Copyright> Copyrights { get; set; }

        [JsonProperty(PropertyName = "external_ids")]
        public ExternalIds ExternalIds { get; set; }

        [JsonProperty(PropertyName = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty(PropertyName = "genres")]
        public List<object> Genres { get; set; }

        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "images")]
        public List<Image> Images { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "popularity")]
        public int Popularity { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty(PropertyName = "release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        [JsonProperty(PropertyName = "total_tracks")]
        public int TotalTracks { get; set; }

        [JsonProperty(PropertyName = "tracks")]
        public AlbumTracks Tracks { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
    }
}