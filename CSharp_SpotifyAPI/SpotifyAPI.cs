using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_SpotifyAPI
{
    public class SpotifyAPI
    {
        private string AuthCode;

        private string baseUrl = "https://api.spotify.com/v1/";

        public SpotifyAPI(string clientID, string redirectUri, string state, List<Scope> scope, bool showDialog)
        {
            Authentication auth = new Authentication(clientID, redirectUri, state, scope, showDialog);

            AuthCode = auth.Authenticate();
        }

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="id">The Spotify ID for the album.</param>
        /// <returns></returns>
        public dynamic GetAlbum(string id)
        {
            string endpointUrl = "albums/" + id;

            var url = baseUrl + endpointUrl;

            var json = HttpMethods.HttpGetWithAuthHeader(url, AuthCode);

            return json;
        }

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="id">The Spotify ID for the album.</param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public dynamic GetAlbum(int id, string market)
        {
            throw new NotImplementedException();

        }

    }
}
