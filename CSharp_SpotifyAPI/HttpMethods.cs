using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_SpotifyAPI
{
    class HttpMethods
    {
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Sends a HTTP POST method
        /// <param name="url">The URL the request is sent to</param>
        /// <param name="dictionary">The dictionary containing the headers for the request</param>
        /// </summary>
        public static async Task<string> AsyncPost(string url, Dictionary<string, string> values)
        {
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        /// <summary>
        /// Sends a HTTP Get method
        /// <param name="url">The URL the request is sent to</param>
        /// </summary>
        public static string HttpGet(string url)
        {
            string json;

            //Creates a GET request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            //Sends the GET request
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }

        /// <summary>
        /// Sends a HTTP Get method with headers
        /// <param name="url">The URL the request is sent to</param>
        /// </summary>
        public static string HttpGet(string url, Dictionary<string, string> headers)
        {
            string json;

            //Creates a GET request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            //Add the headers to the header collection
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            //Sends the GET request
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }

        public static string HttpGetWithAuthHeader(string url, string AuthCode)
        {
            string json;

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Authorization: Bearer", AuthCode }
            };

            //Creates a GET request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            //Add the headers to the header collection
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            //Sends the GET request
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }

    }
}
