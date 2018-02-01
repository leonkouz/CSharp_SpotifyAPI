using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_SpotifyAPI.Enums;
using System.Web;

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

        /// <summary>
        /// Get Spotify catalog information about an album’s tracks.
        /// </summary>
        /// <param name="id">The Spotify ID for the album.</param>
        /// <param name="limit">The maximum number of tracks to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object). Use with limit to get the next set of tracks.</param>
        /// <returns></returns>
        public dynamic GetAlbumTracks(string id, int limit, int offset)
        {
            string endpointUrl = "albums/" + id + "/tracks?limit=" + limit.ToString() + "&offset=" + offset.ToString();

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information about an album’s tracks.
        /// </summary>
        /// <param name="id">The Spotify ID for the album.</param>
        /// <param name="market">>Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="limit">The maximum number of tracks to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object). Use with limit to get the next set of tracks.</param>
        /// <returns></returns>
        public dynamic GetAlbumTracks(string id, Market market, int limit, int offset)
        {
            string endpointUrl = "albums/" + id + "/tracks?market=" + market + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.DownloadData(endpointUrl);
        }

        #endregion

        #region Artists

        /// <summary>
        /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="id">The Spotify ID for the artist.</param>
        /// <returns></returns>
        public dynamic GetArtist(string id)
        {
            string endpointUrl = "artists/" + id;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information for several artists based on their Spotify IDs.
        /// </summary>
        /// <param name="ids">List of the Spotify IDs for the artists. Maximum: 50 IDs.</param>
        /// <returns></returns>
        public dynamic GetSeveralArtists(ICollection<string> ids)
        {
            string artistIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "artists?ids=" + artistIds;

            return HttpMethods.DownloadData(endpointUrl);

        }

        /// <summary>
        /// Get Spotify catalog information about an artist’s albums.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="limit">The number of album objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first album to return. Default: 0 (i.e., the first album). Use with limit to get the next set of albums. </param>
        /// <returns>JSON response</returns>
        public dynamic GetArtistsAlbums(string id, int limit, int offset)
        {
            string endpointUrl = "artists/" + id + "/albums?limit=" + limit.ToString() + "&offset=" + offset.ToString();

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information about an artist’s albums.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="albumType">Used to filter the response. If not supplied, all album types will be returned.</param>
        /// <param name="limit">The number of album objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first album to return. Default: 0 (i.e., the first album). Use with limit to get the next set of albums. </param>
        /// <returns>JSON response</returns>
        public dynamic GetArtistsAlbums(string id, AlbumType albumType, int limit, int offset)
        {
            string endpointUrl = "artists/" + id + "/albums?album_type=" + albumType.GetDescription() + "&limit=" + limit.ToString() + "&offset=" + offset.ToString();

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information about an artist’s albums.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="albumType">Used to filter the response. If not supplied, all album types will be returned.</param>
        /// <param name="limit">The number of album objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first album to return. Default: 0 (i.e., the first album). Use with limit to get the next set of albums. </param>
        /// <returns>JSON response</returns>
        public dynamic GetArtistsAlbums(string id, ICollection<AlbumType> albumType, int limit, int offset)
        {
            string albumTypeDelimited = StringUtil.AggregateEnumsWithDescription(albumType);

            string endpointUrl = "artists/" + id + "/albums?album_type=" + albumTypeDelimited + "&limit=" + limit.ToString() + "&offset=" + offset.ToString();

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information about an artist’s albums.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="albumType">Used to filter the response. If not supplied, all album types will be returned.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="limit">The number of album objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first album to return. Default: 0 (i.e., the first album). Use with limit to get the next set of albums.</param>
        /// <returns>JSON response</returns>
        public dynamic GetArtistsAlbums(string id, ICollection<AlbumType> albumType, Market market, int limit, int offset)
        {
            string albumTypeDelimited = StringUtil.AggregateEnumsWithDescription(albumType);

            string endpointUrl = "artists/" + id + "/albums?album_type=" + albumTypeDelimited + "&market=" + market + "&limit=" + limit.ToString() + "&offset=" + offset.ToString();

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information about an artist’s top tracks by country.
        /// </summary>
        /// <param name="id">The Spotify ID for the artist.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <returns></returns>
        public dynamic GetArtistsTopTracks(string id, Market market)
        {
            string endpointUrl = "artists/" + id + "/top-tracks?country=" + market;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. Similarity is based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="id">The Spotify ID for the artist.</param>
        /// <returns></returns>
        public dynamic GetArtistsRelatedArtists(string id)
        {
            string endpointUrl = "artists/" + id + "/related-artists";

            return HttpMethods.DownloadData(endpointUrl);
        }

        #endregion

        #region Track

        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="id">The Spotify ID for the track.</param>
        /// <returns></returns>
        public dynamic GetTrack(string id)
        {
            string endpointUrl = "tracks/" + id;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="id">The Spotify ID for the track.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <returns></returns>
        public dynamic GetTrack(string id, Market market)
        {
            string endpointUrl = "tracks/" + id + "?market=" + market;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="ids">The Spotify ID for the track.</param>
        /// <returns></returns>
        public dynamic GetSeveralTracks(ICollection<string> ids)
        {
            string trackIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "tracks?ids=" + trackIds;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="ids">The Spotify ID for the track.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <returns></returns>
        public dynamic GetSeveralTracks(ICollection<string> ids, Market market)
        {
            string trackIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "tracks?ids=" + trackIds + "&market=" + market;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get audio feature information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="id">The Spotify ID for the track.</param>
        /// <returns></returns>
        public dynamic GetAudioFeaturesForTrack(string id)
        {
            string endpointUrl = "audio-features/" + id;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get audio features for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="ids">List of the Spotify IDs for the tracks. Maximum: 100 IDs.</param>
        /// <returns></returns>
        public dynamic GetAudioFeaturesForSeveralTracks(ICollection<string> ids)
        {
            string trackIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "audio-features?ids=" + trackIds;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get a detailed audio analysis for a single track identified by its unique Spotify ID. Note: This can take some time depending on the song and internet connection.
        /// </summary>
        /// <param name="id">The Spotify ID for the track.</param>
        /// <returns></returns>
        public dynamic GetAudioAnalysisForTrack(string id)
        {
            string endpointUrl = "audio-analysis/" + id;

            Console.WriteLine("Downloading Audio Analysis for: " + id);

            return HttpMethods.DownloadData(endpointUrl);
        }

        #endregion

        #region Search

        /// <summary>
        /// Get Spotify catalog information about artists, albums, tracks or playlists that match a keyword string.
        /// </summary>
        /// <param name="query">The search query's keywords (and optional field filters and operators), for example: roadhouse%20blues</param>
        /// <param name="type">List of item types to search across</param>
        /// <param name="limit">The maximum number of results to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first result to return. Default: 0 (i.e., the first result). Maximum offset: 100</param>
        /// <returns></returns>
        public dynamic Search(string query, SearchType type, int limit, int offset)
        {
            string endpointUrl = "search?q=" + query + "&type=" + type + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information about artists, albums, tracks or playlists that match a keyword string.
        /// </summary>
        /// <param name="query">The search query's keywords (and optional field filters and operators), for example: roadhouse%20blues</param>
        /// <param name="types">List of item types to search across</param>
        /// <param name="limit">The maximum number of results to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first result to return. Default: 0 (i.e., the first result). Maximum offset: 100</param>
        /// <returns></returns>
        public dynamic Search(string query, ICollection<SearchType> types, int limit, int offset)
        {
            string searchTypesDelimited = StringUtil.AggregateEnums(types);

            string endpointUrl = "search?q=" + query + "&type=" + searchTypesDelimited + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information about artists, albums, tracks or playlists that match a keyword string.
        /// </summary>
        /// <param name="query">The search query's keywords (and optional field filters and operators), for example: roadhouse%20blues</param>
        /// <param name="types">List of item types to search across</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="limit">The maximum number of results to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first result to return. Default: 0 (i.e., the first result). Maximum offset: 100</param>
        /// <returns></returns>
        public dynamic Search(string query, ICollection<SearchType> types, Market market, int limit, int offset)
        {
            string searchTypesDelimited = StringUtil.AggregateEnums(types);

            string endpointUrl = "search?q=" + query + "&type=" + searchTypesDelimited + "&market=" + market + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.DownloadData(endpointUrl);
        }

        #endregion

        #region Playlists

        /// <summary>
        /// Get a list of the playlists owned or followed by a Spotify user.
        /// </summary>
        /// <param name="id">The user's Spotify user ID.</param>
        /// <param name="limit">The maximum number of playlists to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first playlist to return. Default: 0 (the first object). Maximum offset: 100.</param>
        /// <returns></returns>
        public dynamic GetUsersPlaylists(string id, int limit, int offset)
        {
            string endpointUrl = "users/" + id + "/playlists?limit=" + limit + "&offset=" + offset;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get a list of the playlists owned or followed by a Spotify user.
        /// </summary>
        /// <returns></returns>
        public dynamic GetCurrentUsersPlaylists()
        {
            string endpointUrl = "me/playlists";

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get a list of the playlists owned or followed by a Spotify user.
        /// </summary>
        /// <param name="limit">The maximum number of playlists to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first playlist to return. Default: 0 (the first object). Maximum offset: 100.</param>
        /// <returns></returns>
        public dynamic GetCurrentUsersPlaylists(int limit, int offset)
        {
            string endpointUrl = "me/playlists?limit=" + limit + "&offset=" + offset;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <returns></returns>
        public dynamic GetPlaylist(string userId, string playlistId)
        {
            string endpointUrl = "users/" + userId + "/playlists/" + playlistId;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <returns></returns>
        public dynamic GetPlaylist(string userId, string playlistId, Market market)
        {
            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "?market=" + market;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="fields">Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. If the requested field is invalid, an empty string will be returned</param>
        /// <returns></returns>
        public dynamic GetPlaylist(string userId, string playlistId, string fields)
        {
            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "?fields=" + fields;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="fields">Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. If the requested field is invalid, an empty string will be returned</param>
        /// <returns></returns>
        public dynamic GetPlaylist(string userId, string playlistId, Market market, string fields)
        {
            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "?market=" + market + "&fields=" + fields;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <returns></returns>
        public dynamic GetPlaylistsTracks(string userId, string playlistId)
        {
            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks";

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="limit">The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object).</param>
        /// <returns></returns>
        public dynamic GetPlaylistsTracks(string userId, string playlistId, int limit, int offset)
        {
            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks?limit=" + limit + "&offset=" + offset;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="limit">The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object).</param>
        /// <returns></returns>
        public dynamic GetPlaylistsTracks(string userId, string playlistId, Market market, int limit, int offset)
        {
            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks?market=" + market + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="fields">Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. If the fields requested are invalid, an empty string will be returned</param>
        /// <param name="limit">The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object).</param>
        /// <returns></returns>
        public dynamic GetPlaylistsTracks(string userId, string playlistId, string fields, int limit, int offset)
        {
            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks?fields=" + fields + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="fields">Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. If the fields requested are invalid, an empty string will be returned</param>
        /// <param name="limit">The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object).</param>
        /// <returns></returns>
        public dynamic GetPlaylistsTracks(string userId, string playlistId, Market market, string fields, int limit, int offset)
        {
            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks?market=" + market + "&fields=" + fields + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.DownloadData(endpointUrl);
        }

        /// <summary>
        /// Create a playlist for a Spotify user. (The playlist will be empty until you add tracks.)
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="description">Value for playlist description as displayed in Spotify Clients and in the Web API.</param>
        /// <param name="Public">Default true. If true the playlist will be public, if false it will be private. To be able to create private playlists, the user must have granted the playlist-modify-private scope.</param>
        /// <param name="name">The name for the new playlist, for example "Your Coolest Playlist". This name does not need to be unique; a user may have several playlists with the same name.</param>
        /// <param name="collaborative">Default false. If true the playlist will be collaborative. Note that to create a collaborative playlist you must also set public to false. To create collaborative playlists you must have granted playlist-modify-private and playlist-modify-public scopes.</param>
        /// <returns></returns>
        public dynamic CreatePlaylist(string userId, string description, bool Public, string name, bool collaborative)
        {
            string endpointUrl = "users/" + userId + "/playlists";

            string publicBool = Public.ToString();
            string collaborativeBool = collaborative.ToString();

            string jsonData = String.Format("{{\"description\":\"{0}\",\"public\":{1},\"name\":\"{2}\",\"collaborative\":{3}}}", description, publicBool.ToLower(), name, collaborativeBool.ToLower());

            return HttpMethods.SendPostRequest(endpointUrl, jsonData);
        }

        public dynamic AddTrackToPlaylist(string userId, string playlistId, string trackId)
        {
            string trackIdUri = "spotify:track:" + trackId;
            string trackIdUriEncoded = HttpUtility.UrlEncode(trackIdUri);

            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks?uris=" + trackIdUriEncoded;

            return HttpMethods.SendPostRequest(endpointUrl);
        }

        public dynamic AddTrackToPlaylist(string userId, string playlistId, ICollection<string> trackId)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
