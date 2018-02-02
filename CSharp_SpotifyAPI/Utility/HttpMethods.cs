using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


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
        /// Sends a HTTP POST method with an authorisation header and body
        /// </summary>
        /// <param name="url">The URL the request is sent to </param>
        /// <param name="AuthCode">Autorisation Code</param>
        /// <param name="body">The JSON string to include in the body of the POST request</param>
        /// <returns></returns>
        public static string HttpPostWithAuthHeader(string url, string AuthCode, string body)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.Method = "POST";

            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization + ": Bearer " + AuthCode);
            httpWebRequest.ContentType = "application/json";

            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(body);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        return result;
                    }
                }
            }
            catch(WebException wex)
            {
                string errorJson;

                using (var errorResponse = (HttpWebResponse)wex.Response)
                using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                {
                    errorJson = reader.ReadToEnd();
                };

                //string gymnastics to get error message
                dynamic deserialisedResponse = JsonConvert.DeserializeObject(errorJson);
                string deserialisedJson = deserialisedResponse.ToString();
                string charRemoved = StringUtil.RemoveAllInstanceOfCharacter('"', deserialisedJson);
                var splitJson = charRemoved.Split(new string[] { "message:" }, StringSplitOptions.None);
                string errorMessage = splitJson[1].Split('\r')[0];

                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// Sends a HTTP POST method with an authorisation header
        /// </summary>
        /// <param name="url">The URL the request is sent to </param>
        /// <param name="AuthCode">Autorisation Code</param>
        /// <returns></returns>
        public static string HttpPostWithAuthHeader(string url, string AuthCode)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.Method = "POST";

            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization + ": Bearer " + AuthCode);
            httpWebRequest.ContentType = "application/json";

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
            catch(WebException wex)
            {
                string errorJson;

                using (var errorResponse = (HttpWebResponse)wex.Response)
                using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                {
                    errorJson = reader.ReadToEnd();
                };

                //string gymnastics to get error message
                dynamic deserialisedResponse = JsonConvert.DeserializeObject(errorJson);
                string deserialisedJson = deserialisedResponse.ToString();
                string charRemoved = StringUtil.RemoveAllInstanceOfCharacter('"', deserialisedJson);
                var splitJson = charRemoved.Split(new string[] { "message:" }, StringSplitOptions.None);
                string errorMessage = splitJson[1].Split('\r')[0];

                throw new Exception(errorMessage);
            }
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

        /// <summary>
        /// Sends a HTTP Get request with an authorisation header
        /// </summary>
        /// <param name="url">The URL the request is sent to </param>
        /// <param name="AuthCode">Autorisation Code</param>
        /// <returns></returns>
        public static string HttpGetWithAuthHeader(string url, string AuthCode)
        {
            string json = null;
            
            //Creates a GET request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            //Add the headers to the header collection
            request.Headers.Add(HttpRequestHeader.Authorization + ": Bearer " + AuthCode);

            //Sends the GET request
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }
            catch(WebException wex)
            {
                string errorJson;

                using (var errorResponse = (HttpWebResponse)wex.Response)
                using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                {
                    errorJson = reader.ReadToEnd();
                };

                //string gymnastics to get error message
                dynamic deserialisedResponse = JsonConvert.DeserializeObject(errorJson);
                string deserialisedJson = deserialisedResponse.ToString();
                string charRemoved = StringUtil.RemoveAllInstanceOfCharacter('"', deserialisedJson);
                var splitJson = charRemoved.Split(new string[] { "message:"}, StringSplitOptions.None);
                string errorMessage = splitJson[1].Split('\r')[0];

                throw new Exception(errorMessage);
            }

            return json;
        }

        /// <summary>
        /// Downloads JSON from Spotify API
        /// </summary>
        /// <param name="url">The Spotify API endpoint url</param>
        /// <returns></returns>
        public static dynamic DownloadData(string endpointUrl)
        {
            string url = Constants.baseUrl + endpointUrl;

            var json = HttpGetWithAuthHeader(url, Constants.AuthCode);

            return json;
        }

        /// <summary>
        /// Summary sends a POST request to the spotify API with a body
        /// </summary>
        /// <param name="endpointUrl">The Spotify API endpoint url</param>
        /// <param name="jsonData">The body of the POST request</param>
        /// <returns></returns>
        public static dynamic SendPostRequest(string endpointUrl, string jsonData)
        {
            string url = Constants.baseUrl + endpointUrl;

            var json = HttpPostWithAuthHeader(url, Constants.AuthCode, jsonData);

            return json;
        }

        /// <summary>
        /// Summary sends a POST request to the spotify API
        /// </summary>
        /// <param name="endpointUrl">The Spotify API endpoint url</param>
        /// <returns></returns>
        public static dynamic SendPostRequest(string endpointUrl)
        {
            string url = Constants.baseUrl + endpointUrl;

            var json = HttpPostWithAuthHeader(url, Constants.AuthCode);

            return json;
        }


    }
}
