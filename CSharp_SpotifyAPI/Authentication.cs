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
        
        private string _clientID;
        private Scope _scope;




        public Authentication()
        {


        }

        public void Authenticate()
        {
            

        }



        //https://developer.spotify.com/web-api/authorization-guide/#implicit_grant_flow

        string urlToAuthenticateWith = "https://accounts.spotify.com/authorize?client_id=305dbadf23cd4d9688868eb01857b54b&redirect_uri=http%3A%2F%2Flocalhost%3A62177&scope=user-read-private%20user-read-email&response_type=token&state=123";

    }
}
