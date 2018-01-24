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
        private string _responseType = "token";
        private string _redirectUri;
        private string _state;
        private Scope _scope;
        private bool _showDialgog;
        private string _url;
        
        public Authentication(string clientID, string redirectUri, string state, Scope scope, bool showDialog)
        {
            _clientID = clientID;
            _redirectUri = redirectUri;
            _state = state;
            _scope = scope;
            _showDialgog = showDialog;

            BuildUrl();
        }

        private string BuildUrl()
        {
            StringBuilder builder = new StringBuilder("https://accounts.spotify.com/authorize?");
            builder.Append("client_id=" + _clientID);
            builder.Append("&redirect_uri=" + _redirectUri);
            builder.Append("&scope=" + _scope.GetDescription());
            builder.Append("&response_type=" + _responseType);
            builder.Append("&state=" + _state);
            builder.Append("&show_dialog=" + _showDialgog);

            return builder.ToString();
        }

        public string Authenticate()
        {
            string authCode = null;

            //create server with specific port
            SimpleHttpServer myServer = new SimpleHttpServer(62177, AuthType.Implicit);
            myServer.Listen();

            Process.Start(_url);

            myServer.OnAuth += e =>
            {
                authCode = e.Code;
            };

            return authCode;
        }
    }
}
