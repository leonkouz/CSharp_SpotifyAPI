using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_SpotifyAPI
{
    class Authentication
    {
        //https://developer.spotify.com/web-api/authorization-guide/#implicit_grant_flow

        private string _clientID;
        private string _responseType = "token"; //always set to "token"
        private string _redirectUri;
        private string _state;
        private Scope _scope;
        private bool _showDialgog;
        private string _url;

        /// <summary>
        /// Authenticate with the Spotify API
        /// </summary>
        /// <param name="clientID">The client ID provided to you by Spotify when you register your application.</param>
        /// <param name="redirectUri">The URI to redirect to after the user grants/denies permission. This URI needs to be entered in the URI whitelist that you specify when you register your application.</param>
        /// <param name="state">The state can be useful for correlating requests and responses. Because your redirect_uri can be guessed, using a state value can increase your assurance that an incoming connection 
        /// is the result of an authentication request. If you generate a random string or encode the hash of some client state (e.g., a cookie) in this state variable, you can validate the response to 
        /// additionally ensure that the request and response originated in the same browser. This provides protection against attacks such as cross-site request forgery.</param>
        /// <param name="scope">A space-separated list of scopes</param>
        /// <param name="showDialog">Whether or not to force the user to approve the app again if they’ve already done so. If false (default), a user who has already approved the application may be automatically 
        /// redirected to the URI specified by redirect_uri. If true, the user will not be automatically redirected and will have to approve the app again.</param>
        public Authentication(string clientID, string redirectUri, string state, Scope scope, bool showDialog)
        {
            //Need to change the scope to a list to allow for multiple scopes to be used
            
            _clientID = clientID;
            _redirectUri = redirectUri;
            _state = state;
            _scope = scope;
            _showDialgog = showDialog;

            BuildUrl();
        }

        private void BuildUrl()
        {
            StringBuilder builder = new StringBuilder("https://accounts.spotify.com/authorize?");
            builder.Append("client_id=" + _clientID);
            builder.Append("&redirect_uri=" + _redirectUri);
            builder.Append("&scope=" + _scope.GetDescription());
            builder.Append("&response_type=" + _responseType);
            builder.Append("&state=" + _state);
            builder.Append("&show_dialog=" + _showDialgog);

            _url = builder.ToString();
        }

        public string Authenticate()
        {
            string authCode = null;

            //create server with specific port
            SimpleHttpServer myServer = new SimpleHttpServer(62177, AuthType.Implicit);
            myServer.Listen();
            Console.WriteLine("Listening on 62177");

            Process.Start(_url);

            myServer.OnAuth += e =>
            {
                authCode = e.Code;
                Console.WriteLine(e.Code);
            };

            return authCode;
        }
    }
}
