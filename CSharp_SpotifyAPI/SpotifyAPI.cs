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
        public SpotifyAPI(string clientID, string redirectUri, string state, List<Scope> scopes, bool showDialog)
        {
            Authentication auth = new Authentication(clientID, redirectUri, state, scopes, showDialog);

            Constants.AuthCode = auth.Authenticate();
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

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="id">The Spotify ID for the album.</param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public dynamic GetAlbum(string id, Market market)
        {
            string endpointUrl = "albums/" + id + "?market=" + market;

            return HttpMethods.DownloadData(endpointUrl);
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

            string endpointUrl = "albums/?ids=" + albumIds;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
        /// </summary>
        /// <param name="ids">List of the Spotify IDs for the albums</param>
        /// <param name="market">Specifies a market to retrieve information from</param>
        /// <returns></returns>
        public dynamic GetSeveralAlbums(ICollection<string> ids, Market market)
        {
            //Concatenates all ids from the collection into a string
            string albumIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "albums/?ids=" + albumIds + "&market=" + market;

            return HttpMethods.DownloadData(endpointUrl);
        }
        
        #endregion

        #region Artists

        public dynamic GetAlbumByArtist(string artistId)
        {
            string endpointUrl = "artists/" + artistId + "/albums";

            return HttpMethods.DownloadData(endpointUrl);
        }

        public dynamic GetAlbumByArtist(string artistId, ICollection<AlbumType> albumType)
        {
            string albumTypeDelimited = StringUtil.AggregateCollection(albumType);

            string endpointUrl = "artists/" + artistId + "/albums?album_type=" + albumTypeDelimited;

            return HttpMethods.DownloadData(endpointUrl);
        }

        public dynamic GetAlbumByArtist(string artistId, AlbumType albumType)
        {
            string endpointUrl = "artists/" + artistId + "/albums?album_type=" + albumType.GetDescription();

            return HttpMethods.DownloadData(endpointUrl);
        }

        public dynamic GetAlbumByArtist(string artistId, ICollection<AlbumType> albumType, Market market)
        {
            string albumTypeDelimited = StringUtil.AggregateCollection(albumType);

            string endpointUrl = "artists/" + artistId + "/albums?album_type=" + albumTypeDelimited + "&market=" + market;

            return HttpMethods.DownloadData(endpointUrl);
        }


        #endregion
    }
}
