using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_SpotifyAPI
{
    class SpotifyAPI
    {
        private string AuthCode;

        public SpotifyAPI(string clientID, string redirectUri, string state, Scope scope, bool showDialog)
        {
            Authentication auth = new Authentication(clientID, redirectUri, state, scope, showDialog);

            AuthCode = auth.Authenticate();
        }


    }
}
