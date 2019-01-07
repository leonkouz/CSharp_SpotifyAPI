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
        /// Sends a HTTP method with an authorisation header and body
        /// </summary>
        /// <param name="url">The URL the request is sent to </param>
        /// <param name="AuthCode">Autorisation Code</param>
        /// <param name="method">Specifies the HTTP method to use</param>
        /// <param name="body">The JSON string to include in the body of the POST request</param>
        /// <returns></returns>
        private static string HttpMethodWithAuthHeader(string url, string AuthCode, HttpMethod method, string body)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.Method = method.ToString();

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
        /// Sends a HTTP method with an authorisation header and body
        /// </summary>
        /// <param name="url">The URL the request is sent to </param>
        /// <param name="AuthCode">Autorisation Code</param>
        /// <param name="method">Specifies the HTTP method to use</param>
        /// <returns></returns>
        private static string HttpMethodWithAuthHeader(string url, string AuthCode, HttpMethod method)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.Method = method.ToString();

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
            catch (WebException wex)
            {
                string errorJson;

                using (var errorResponse = (HttpWebResponse)wex.Response)
                using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                {
                    errorJson = reader.ReadToEnd();
                };

                string errorMessage = null;

                try
                {
                    //string gymnastics to get error message from Json
                    dynamic deserialisedResponse = JsonConvert.DeserializeObject(errorJson);
                    string deserialisedJson = deserialisedResponse.ToString();
                    string charRemoved = StringUtil.RemoveAllInstanceOfCharacter('"', deserialisedJson);
                    var splitJson = charRemoved.Split(new string[] { "message:" }, StringSplitOptions.None);
                    errorMessage = splitJson[1].Split('\r')[0];
                    errorMessage.Trim(' ');
                }
                catch
                {
                    errorMessage = wex.Message;
                }
                

                throw new ArgumentException(errorMessage);
            }
        }

        /// <summary>
        /// Downloads JSON from Spotify API
        /// </summary>
        /// <param name="url">The Spotify API endpoint url</param>
        /// <returns></returns>
        public static dynamic SendGetRequest(string endpointUrl)
        {
            string url = Constants.baseUrl + endpointUrl;

            var json = HttpMethodWithAuthHeader(url, Constants.AuthCode, HttpMethod.GET);

            return json;
        }

        /// <summary>
        /// Sends a POST request to the spotify API with a body
        /// </summary>
        /// <param name="endpointUrl">The Spotify API endpoint url</param>
        /// <param name="jsonData">The body of the POST request</param>
        /// <returns></returns>
        public static dynamic SendPostRequest(string endpointUrl, string jsonData)
        {
            string url = Constants.baseUrl + endpointUrl;

            var json = HttpMethodWithAuthHeader(url, Constants.AuthCode, HttpMethod.POST, jsonData);

            return json;
        }

        /// <summary>
        /// Sends a POST request to the spotify API
        /// </summary>
        /// <param name="endpointUrl">The Spotify API endpoint url</param>
        /// <returns></returns>
        public static dynamic SendPostRequest(string endpointUrl)
        {
            string url = Constants.baseUrl + endpointUrl;

            var json = HttpMethodWithAuthHeader(url, Constants.AuthCode, HttpMethod.POST);

            return json;
        }

        /// <summary>
        /// Sends a DELETE request to the Spotify API
        /// </summary>
        /// <param name="endpointUrl">The Spotify API endpoint url</param>
        /// <param name="body">The body of the POST request</param>
        /// <returns></returns>
        public static dynamic SendDeleteRequest(string endpointUrl, string body)
        {
            string url = Constants.baseUrl + endpointUrl;

            var json = HttpMethodWithAuthHeader(url, Constants.AuthCode, HttpMethod.DELETE, body);

            return json;
        }

        /// <summary>
        /// Sends a DELETE request to the Spotify API
        /// </summary>
        /// <param name="endpointUrl">The Spotify API endpoint url</param>
        /// <returns></returns>
        public static dynamic SendDeleteRequest(string endpointUrl)
        {
            string url = Constants.baseUrl + endpointUrl;

            var json = HttpMethodWithAuthHeader(url, Constants.AuthCode, HttpMethod.DELETE);

            return json;
        }

        /// <summary>
        /// Sends a PUT request to the Spotify API
        /// </summary>
        /// <param name="endpointUrl">The Spotify API endpoint url</param>
        /// <param name="body">The body of the POST request</param>
        /// <returns></returns>
        public static dynamic SendPutRequest(string endpointUrl, string body)
        {
            string url = Constants.baseUrl + endpointUrl;

            var json = HttpMethodWithAuthHeader(url, Constants.AuthCode, HttpMethod.PUT, body);

            return json;
        }

        /// <summary>
        /// Sends a PUT request to the Spotify API
        /// </summary>
        /// <param name="endpointUrl">The Spotify API endpoint url</param>
        /// <returns></returns>
        public static dynamic SendPutRequest(string endpointUrl)
        {
            string url = Constants.baseUrl + endpointUrl;

            var json = HttpMethodWithAuthHeader(url, Constants.AuthCode, HttpMethod.PUT);

            return json;
        }

        private enum HttpMethod
        {
            POST = 0,

            GET = 1,

            DELETE = 2,

            PUT = 3,
        }


    }
}
