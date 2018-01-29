using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_SpotifyAPI.Enums;

namespace CSharp_SpotifyAPI
{
    public class SpotifyAPI
    {
        private string AuthCode;

        private string baseUrl = "https://api.spotify.com/v1/";

        public SpotifyAPI(string clientID, string redirectUri, string state, List<Scope> scopes, bool showDialog)
        {
            Authentication auth = new Authentication(clientID, redirectUri, state, scopes, showDialog);

            AuthCode = auth.Authenticate();
        }

        #region Albums

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="id">The Spotify ID for the album.</param>
        /// <returns></returns>
        public dynamic GetAlbum(string id)
        {
            string endpointUrl = "albums/" + id;

            string url = baseUrl + endpointUrl;

            var json = HttpMethods.HttpGetWithAuthHeader(url, AuthCode);

            return json;
        }

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="id">The Spotify ID for the album.</param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public dynamic GetAlbum(string id, Markets market)
        {
            string endpointUrl = "albums/" + id + "?market=" + market;

            string url = baseUrl + endpointUrl;

            var json = HttpMethods.HttpGetWithAuthHeader(url, AuthCode);

            return json;
        }

        /// <summary>
        /// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
        /// </summary>
        /// <param name="ids">List of the Spotify IDs for the albums</param>
        /// <returns></returns>
        public dynamic GetSeveralAlbums(ICollection<string> ids)
        {
            //Concatenates all ids from the collection into a string
            string albumIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointURl = "albums/?ids=" + albumIds;

            string url = baseUrl + endpointURl;

            var json = HttpMethods.HttpGetWithAuthHeader(url, AuthCode);

            return json;
        }

        /// <summary>
        /// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
        /// </summary>
        /// <param name="ids">List of the Spotify IDs for the albums</param>
        /// <param name="market">Specifies a market to retrieve information from</param>
        /// <returns></returns>
        public dynamic GetSeveralAlbums(ICollection<string> ids, Markets market)
        {
            //Concatenates all ids from the collection into a string
            string albumIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointURl = "albums/?ids=" + albumIds + "&market=" + market;

            string url = baseUrl + endpointURl;

            var json = HttpMethods.HttpGetWithAuthHeader(url, AuthCode);

            return json;
        }
        
        #endregion

        #region Artists

        public dynamic GetAlbumByArtist(string artistId)
        {
            string endpointUrl = "artists/" + artistId + "/albums";

            string url = baseUrl + endpointUrl;

            var json = HttpMethods.HttpGetWithAuthHeader(url, AuthCode);

            return json;
        }


        #endregion
    }
}
