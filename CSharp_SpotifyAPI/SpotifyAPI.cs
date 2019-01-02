using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_SpotifyAPI.Enums;
using Newtonsoft.Json;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace CSharp_SpotifyAPI
{
    public class SpotifyAPI
    {
        private Authentication auth;

        public event EventHandler Authenticated;

        #region Authentication

        /// <summary>
        /// Initalise the Spotify API
        /// </summary>
        /// <param name="clientID">The client ID provided to you by Spotify when you register your application.</param>
        /// <param name="redirectUri">The URI to redirect to after the user grants/denies permission. This URI needs to be entered in the URI whitelist that you specify when you register your application.</param>
        /// <param name="state">The state can be useful for correlating requests and responses. Because your redirect_uri can be guessed, using a state value can increase your assurance that an incoming connection 
        /// is the result of an authentication request. If you generate a random string or encode the hash of some client state (e.g., a cookie) in this state variable, you can validate the response to 
        /// additionally ensure that the request and response originated in the same browser. This provides protection against attacks such as cross-site request forgery.</param>
        /// <param name="scopes">A space-separated list of scopes</param>
        /// <param name="showDialog">Whether or not to force the user to approve the app again if they’ve already done so. If false (default), a user who has already approved the application may be automatically 
        /// redirected to the URI specified by redirect_uri. If true, the user will not be automatically redirected and will have to approve the app again.</param>
        /// <param name="launchBrowser">Decide whether you would like the browser to launch directly to the webpage or if you would like to navigate to the page. If false is selected the URL is printed to the Console</param>
        public SpotifyAPI(string clientID, string redirectUri, string state, List<Scope> scopes, bool showDialog)
        {
            auth = new Authentication(clientID, redirectUri, state, scopes, showDialog);
        }

        /// <summary>
        /// Begin the authentication process
        /// </summary>
        /// <param name="launchBrowser"></param>
        public void Authenticate(bool launchBrowser)
        {
            Constants.AuthCode = auth.Authenticate(launchBrowser);

            OnAuthentication(EventArgs.Empty);
        }

        protected void OnAuthentication(EventArgs e)
        {
            Authenticated?.Invoke(this, e);
        }

        /// <summary>
        /// Get the URL to authenticate spotify
        /// </summary>
        /// <returns></returns>
        public string GetAuthenticationUrl()
        {
            return auth.BuildUrl();
        }

        #endregion

        #region Albums

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="id">The Spotify ID for the album.</param>
        /// <returns></returns>
        public dynamic GetAlbum(string id)
        {
            string endpointUrl = "albums/" + id;

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);

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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. Similarity is based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="id">The Spotify ID for the artist.</param>
        /// <returns></returns>
        public dynamic GetArtistsRelatedArtists(string id)
        {
            string endpointUrl = "artists/" + id + "/related-artists";

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get audio feature information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="id">The Spotify ID for the track.</param>
        /// <returns></returns>
        public dynamic GetAudioFeaturesForTrack(string id)
        {
            string endpointUrl = "audio-features/" + id;

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of the playlists owned or followed by a Spotify user.
        /// </summary>
        /// <returns></returns>
        public dynamic GetCurrentUsersPlaylists()
        {
            string endpointUrl = "me/playlists";

            return HttpMethods.SendGetRequest(endpointUrl);
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

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <returns></returns>
        public dynamic GetPlaylist(string playlistId)
        {
            string endpointUrl = "playlists/" + playlistId;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <returns></returns>
        public dynamic GetPlaylist(string playlistId, Market market)
        {
            string endpointUrl = "playlists/" + playlistId + "?market=" + market;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="fields">Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. If the requested field is invalid, an empty string will be returned</param>
        /// <returns></returns>
        public dynamic GetPlaylist(string playlistId, string fields)
        {
            string endpointUrl = "playlists/" + playlistId + "?fields=" + fields;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="fields">Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. If the requested field is invalid, an empty string will be returned</param>
        /// <returns></returns>
        public dynamic GetPlaylist(string playlistId, Market market, string fields)
        {
            string endpointUrl = "playlists/" + playlistId + "?market=" + market + "&fields=" + fields;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get full details of the tracks of a playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <returns></returns>
        public dynamic GetPlaylistsTracks(string playlistId)
        {
            string endpointUrl = "playlists/" + playlistId + "/tracks";

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get full details of the tracks of a playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="limit">The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object).</param>
        /// <returns></returns>
        public dynamic GetPlaylistsTracks(string playlistId, int limit, int offset)
        {
            string endpointUrl = "playlists/" + playlistId + "/tracks?limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get full details of the tracks of a playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="limit">The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object).</param>
        /// <returns></returns>
        public dynamic GetPlaylistsTracks(string playlistId, Market market, int limit, int offset)
        {
            string endpointUrl = "playlists/" + playlistId + "/tracks?market=" + market + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get full details of the tracks of a playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="fields">Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. If the fields requested are invalid, an empty string will be returned</param>
        /// <param name="limit">The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object).</param>
        /// <returns></returns>
        public dynamic GetPlaylistsTracks(string playlistId, string fields, int limit, int offset)
        {
            string endpointUrl = "playlists/" + playlistId + "/tracks?fields=" + fields + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get full details of the tracks of a playlist.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="fields">Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. If the fields requested are invalid, an empty string will be returned</param>
        /// <param name="limit">The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object).</param>
        /// <returns></returns>
        public dynamic GetPlaylistsTracks(string playlistId, Market market, string fields, int limit, int offset)
        {
            string endpointUrl = "playlists/" + playlistId + "/tracks?market=" + market + "&fields=" + fields + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
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

        /// <summary>
        /// Add one or more tracks to a user’s playlist.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="trackIds">Spotify Track ID to add</param>
        /// <returns></returns>
        public dynamic AddTrackToPlaylist(string userId, string playlistId, string trackId)
        {
            string trackUri = StringUtil.CreateSpotifyURI(trackId);

            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks?uris=" + trackUri;

            return HttpMethods.SendPostRequest(endpointUrl);
        }

        /// <summary>
        /// Add one or more tracks to a user’s playlist.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="trackIds">List of Spotify track URIs to add.</param>
        /// <returns></returns>
        public dynamic AddTrackToPlaylist(string userId, string playlistId, ICollection<string> trackIds)
        {
            string trackUris = StringUtil.CreateSpotifyURI(trackIds);

            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks?uris=" + trackUris;

            return HttpMethods.SendPostRequest(endpointUrl);
        }

        /// <summary>
        /// Add one or more tracks to a user’s playlist.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="trackIds">Spotify Track ID to add</param>
        /// <param name="position">The position to insert the tracks, a zero-based index. For example, to insert the tracks in the first position: position=0; to insert the tracks in the third position: position=2. If omitted, the tracks will be appended to the playlist. 
        /// Tracks are added in the order they appear in the uris array.</param>
        /// <returns></returns>
        public dynamic AddTrackToPlaylist(string userId, string playlistId, string trackId, int position)
        {
            string trackUri = StringUtil.CreateSpotifyURI(trackId);

            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks?position=" + position + "&uris=" + trackUri;

            return HttpMethods.SendPostRequest(endpointUrl);
        }

        /// <summary>
        /// Add one or more tracks to a user’s playlist.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="trackIds">List of Spotify track URIs to add.</param>
        /// <param name="position">The position to insert the tracks, a zero-based index. For example, to insert the tracks in the first position: position=0; to insert the tracks in the third position: position=2. If omitted, the tracks will be appended to the playlist. 
        /// Tracks are added in the order they appear in the uris array.</param>
        /// <returns></returns>
        public dynamic AddTrackToPlaylist(string userId, string playlistId, ICollection<string> trackIds, int position)
        {
            string trackUris = StringUtil.CreateSpotifyURI(trackIds);

            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks?position=" + position + "&uris=" + trackUris;

            return HttpMethods.SendPostRequest(endpointUrl);
        }

        /// <summary>
        /// Remove all occurences of specific track from a playlist
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="trackId">Spotify track ID</param>
        /// <returns></returns>
        public dynamic RemoveTrackFromPlaylist(string userId, string playlistId, string trackId)
        {
            string trackUri = "spotify:track:" + trackId;

            JObject json =
                new JObject(
                    new JProperty("tracks",
                    new JArray(
                        new JObject(
                            new JProperty("uri", trackUri)
                            ))));

            string jsonString = StringUtil.StringifyJson(json);

            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks";

            return HttpMethods.SendDeleteRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Remove a specific track from a specific position in a playlist
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="trackId">Spotify track ID</param>
        /// <param name="position">Position to remove the track from</param>
        /// <returns></returns>
        public dynamic RemoveTrackFromPlaylist(string userId, string playlistId, string trackId, int position)
        {
            string trackUri = "spotify:track:" + trackId;

            JObject json =
           new JObject(
               new JProperty("tracks",
               new JArray(
                   new JObject(
                       new JProperty("positions", new JArray(position)),
                       new JProperty("uri", trackUri)
                       ))));

            string jsonString = StringUtil.StringifyJson(json);

            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks";

            return HttpMethods.SendDeleteRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Remove all occurences of multiple tracks from a playlist
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="trackIds">Collection of Spotify Track IDs</param>
        /// <returns></returns>
        public dynamic RemoveTracksFromPlaylist(string userId, string playlistId, ICollection<string> trackIds)
        {
            List<string> trackUris = new List<string>();

            foreach (string str in trackIds)
            {
                string uri = "spotify:track:" + str;
                trackUris.Add(uri);
            }

            JObject json =
                new JObject(
                    new JProperty("tracks",
                    new JArray(
                         new JObject(
                            new JProperty("uri", trackUris[0])
                            ))));


            //Remove the first element since we already added it in the json
            trackUris[0].Remove(0);

            foreach (string str in trackUris)
            {
                json["tracks"].First().AddAfterSelf(new JObject(new JProperty("uri", str)));
            }

            string jsonString = StringUtil.StringifyJson(json);

            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks";

            return HttpMethods.SendDeleteRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Remove all occurences of multiple tracks from a playlist
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="trackIdAndPosition">Dictionary of the track to remove and the specific position to remove it from</param>
        /// <param name="playlistSnapshot">The Spotify ID for the playlist snapshot to remove from. This is used to help avoid errors. You can retreive the id when adding to or creating playlists.</param>
        /// <returns></returns>
        public dynamic RemoveTracksFromPlaylist(string userId, string playlistId, Dictionary<string, int> trackIdAndPosition, string playlistSnapshot)
        {
            List<string> trackUris = new List<string>();
            List<int> positions = new List<int>();

            foreach (var value in trackIdAndPosition)
            {
                string uri = "spotify:track:" + value.Key;
                trackUris.Add(uri);
                positions.Add(value.Value);
            }

            JObject json =
                new JObject(
                    new JProperty("tracks", new JArray(new JObject(
                            new JProperty("positions", new JArray(positions[0])),
                            new JProperty("uri", trackUris[0])
                            ))),
                    new JProperty("snapshot_id", playlistSnapshot));

            var firstValue = trackIdAndPosition.First();
            trackIdAndPosition.Remove(firstValue.Key);

            int index = 0;
            foreach (string str in trackUris)
            {
                json["tracks"].First().AddAfterSelf(new JObject(new JProperty("positions", new JArray(positions[index])), new JProperty("uri", str)));
                index++;
            }

            string jsonString = StringUtil.StringifyJson(json);

            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks";

            return HttpMethods.SendDeleteRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Reorder a track or a group of tracks in a playlist.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="rangeStart">The position of the first track to be reordered.</param>
        /// <param name="rangeLength">The amount of tracks to be reordered. Defaults to 1 if not set.</param>
        /// <param name="insertBefore">The position where the tracks should be inserted. </param>
        /// <returns></returns>
        public dynamic ReorderPlaylistTracks(string userId, string playlistId, int rangeStart, int rangeLength, int insertBefore)
        {
            JObject json = new JObject(
                new JProperty("range_start", rangeStart),
                new JProperty("range_length", rangeLength),
                new JProperty("insert_before", insertBefore));

            string jsonString = StringUtil.StringifyJson(json);

            string endpointUrl = "users/" + userId + "/playlists/" + playlistId + "/tracks";

            return HttpMethods.SendPutRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Change a playlist’s name and public/private state. (The user must, of course, own the playlist.). Use null if you do not wish to change a specific parameter. E.g. ChangePlaylistDetails(id, playlistId, "New Playlist Name", null, "new description"). Note: User Id and Playlist ID cannot be null
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="name">The new name for the playlist, for example "My New Playlist Title".</param>
        /// <param name="Public">If true the playlist will be public, if false it will be private.</param>
        /// <param name="collaborative">f true, the playlist will become collaborative and other users will be able to modify the playlist in their Spotify client. Note: You can only set collaborative to true on non-public playlists.</param>
        /// <param name="description">Value for playlist description as displayed in Spotify Clients and in the Web API.</param>
        /// <returns></returns>
        public dynamic ChangePlaylistDetails(string userId, string playlistId, string name, bool? Public, bool? collaborative, string description)
        {
            string endpointUrl = "users/" + userId + "/playlists/" + playlistId;

            JObject json = new JObject();

            if (name != null)
                json.Add(new JProperty("name", name));
            if (Public != null)
                json.Add(new JProperty("public", Public));
            if (collaborative != null)
                json.Add(new JProperty("collaborative", collaborative));
            if (description != null)
                json.Add(new JProperty("description", description));

            string jsonString = StringUtil.StringifyJson(json);

            HttpMethods.SendPutRequest(endpointUrl, jsonString);

            return "Playlist details changed successfully"; //return this as the api does return any data from this endpoint
        }

        #endregion

        #region User Profiles

        /// <summary>
        /// Get public profile information about a Spotify user.
        /// </summary>
        /// <param name="id">The user's Spotify user ID.</param>
        /// <returns></returns>
        public dynamic GetUserProfile(string id)
        {
            string endpointUrl = "users/" + id;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get detailed profile information about the current user (including the current user’s username).
        /// </summary>
        /// <returns></returns>
        public dynamic GetCurrentUserProfile()
        {
            string endpointUrl = "me";

            return HttpMethods.SendGetRequest(endpointUrl);
        }


        #endregion

        #region User Library

        /// <summary>
        /// Get a list of the songs saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <returns></returns>
        public dynamic GetCurrentUsersSavedTracks()
        {
            string endpointUrl = "me/tracks";

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of the songs saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <returns></returns>
        public dynamic GetCurrentUsersSavedTracks(Market market)
        {
            string endpointUrl = "me/tracks?market=" + market;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of the songs saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="limit">The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns></returns>
        public dynamic GetCurrentUsersSavedTracks(int limit, int offset)
        {
            string endpointUrl = "me/tracks?limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of the songs saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="limit">The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns></returns>
        public dynamic GetCurrentUsersSavedTracks(Market market, int limit, int offset)
        {
            string endpointUrl = "me/tracks?market=" + market + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Check if one or more tracks is already saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="id">The Spotify track ID</param>
        /// <returns></returns>
        public dynamic CheckCurrentUsersSavedTracks(string id)
        {
            string endpointUrl = "me/tracks/contains?ids=" + id;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Check if one or more tracks is already saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="ids">List of the Spotify IDs for the tracks. Maximum: 50 IDs.</param>
        /// <returns></returns>
        public dynamic CheckCurrentUsersSavedTracks(ICollection<string> ids)
        {
            string trackIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "me/tracks/contains?ids=" + trackIds;

            return HttpMethods.SendGetRequest(endpointUrl);

        }

        /// <summary>
        /// Save one or more tracks to the current user’s “Your Music” library.
        /// </summary>
        /// <param name="id">The Spotify Track ID</param>
        /// <returns></returns>
        public dynamic SaveTracksForCurrentUser(string id)
        {
            string endpointUrl = "me/tracks?ids=" + id;

            HttpMethods.SendPutRequest(endpointUrl);

            return "Track saved successfully"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Save one or more tracks to the current user’s “Your Music” library.
        /// </summary>
        /// <param name="ids">List of the Spotify Track IDs. Maximum: 50 IDs.</param>
        /// <returns></returns>
        public dynamic SaveTracksForCurrentUser(ICollection<string> ids)
        {
            string trackIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "me/tracks?ids=" + trackIds;

            HttpMethods.SendPutRequest(endpointUrl);

            return "Tracks saved successfully"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Remove one or more tracks from the current user’s “Your Music” library.
        /// </summary>
        /// <param name="id">The Spotify Track ID</param>
        /// <returns></returns>
        public dynamic RemoveTracksForCurrentUser(string id)
        {
            string endpointUrl = "me/tracks?ids=" + id;

            HttpMethods.SendDeleteRequest(endpointUrl);

            return "Tracks removed successfully"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Remove one or more tracks from the current user’s “Your Music” library.
        /// </summary>
        /// <param name="ids">List of Spotify Track IDs. Maximum: 50 IDs.</param>
        /// <returns></returns>
        public dynamic RemoveTracksForCurrentUser(ICollection<string> ids)
        {
            string trackIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "me/tracks?ids=" + trackIds;

            HttpMethods.SendDeleteRequest(endpointUrl);

            return "Tracks removed successfully"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Get a list of the albums saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <returns></returns>
        public dynamic GetCurrentUsersSavedAlbums()
        {
            string endpointUrl = "me/albums";

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of the albums saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <returns></returns>
        public dynamic GetCurrentUsersSavedAlbums(Market market)
        {
            string endpointUrl = "me/albums?market=" + market;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of the albums saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="limit">The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns></returns>
        public dynamic GetCurrentUsersSavedAlbums(int limit, int offset)
        {
            string endpointUrl = "me/albums?limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of the albums saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="limit">The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns></returns>
        public dynamic GetCurrentUsersSavedAlbums(Market market, int limit, int offset)
        {
            string endpointUrl = "me/albums?market=" + market + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Check if one or more albums is already saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="id">The Spotify ID for the album.</param>
        /// <returns></returns>
        public dynamic CheckUsersSavedAlbums(string id)
        {
            string endpointUrl = "me/albums/contains?ids=" + id;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Check if one or more albums is already saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="ids">List of the Spotify IDs for the albums. Maximum: 50 IDs.</param>
        /// <returns></returns>
        public dynamic CheckUsersSavedAlbums(ICollection<string> ids)
        {
            string albumIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "me/albums/contains?ids=" + albumIds;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Save one or more albums to the current user’s “Your Music” library.
        /// </summary>
        /// <param name="id">The Spotify Album ID.</param>
        /// <returns></returns>
        public dynamic SaveAlbumsForCurrentUser(string id)
        {
            string endpointUrl = "me/albums?ids=" + id;

            HttpMethods.SendPutRequest(endpointUrl);

            return "Album saved successfully"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Save one or more albums to the current user’s “Your Music” library.
        /// </summary>
        /// <param name="ids">List of Spotify Album IDs. Maximum: 50 IDs.</param>
        /// <returns></returns>
        public dynamic SaveAlbumsForCurrentUser(ICollection<string> ids)
        {
            string albumIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "me/albums?ids=" + albumIds;

            HttpMethods.SendPutRequest(endpointUrl);

            return "Albums saved successfully"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Remove one or more albums from the current user’s “Your Music” library.
        /// </summary>
        /// <param name="id">The Spotify Album ID.</param>
        /// <returns></returns>
        public dynamic RemoveAlbumsForCurrentUser(string id)
        {
            string endpointUrl = "me/albums?ids=" + id;

            HttpMethods.SendDeleteRequest(endpointUrl);

            return "Album removed successfully"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Remove one or more albums from the current user’s “Your Music” library.
        /// </summary>
        /// <param name="ids">List of Spotify Album IDs. Maximum: 50 IDs.</param>
        /// <returns></returns>
        public dynamic RemoveAlbumsForCurrentUser(ICollection<string> ids)
        {
            string albumIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "me/albums?ids=" + albumIds;

            HttpMethods.SendDeleteRequest(endpointUrl);

            return "Albums saved successfully"; //Added as the endpoint does not return a message
        }

        #endregion

        #region Personalisation

        /// <summary>
        /// Get the current user’s tracks based on calculated affinity.
        /// </summary>
        /// <param name="limit">The number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first entity to return. Default: 0 (i.e., the first track). Use with limit to get the next set of entities.</param>
        /// <returns></returns>
        public dynamic GetCurrentUsersTopTracks(int limit, int offset)
        {
            string endpointUrl = "me/top/tracks?limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get the current user’s tracks based on calculated affinity.
        /// </summary>
        /// <param name="limit">The number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first entity to return. Default: 0 (i.e., the first track). Use with limit to get the next set of entities.</param>
        /// <param name="timeRange">Over what time frame the affinities are computed. Long term is calculated from several years of data and including all new data as it becomes available.
        /// Medium term approximately last 6 months. Short term approximately lasts 4 weeks</param>
        /// <returns></returns>
        public dynamic GetCurrentUsersTopTracks(int limit, int offset, TimeRange timeRange)
        {
            string endpointUrl = "me/top/tracks?time_range=" + timeRange.GetDescription() + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get the current user’s top artists based on calculated affinity.
        /// </summary>
        /// <param name="limit">The number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first entity to return. Default: 0 (i.e., the first track). Use with limit to get the next set of entities.</param>
        /// <returns></returns>
        public dynamic GetCurrentUsersTopArtists(int limit, int offset)
        {
            string endpointUrl = "me/top/artists?limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get the current user’s top artists based on calculated affinity.
        /// </summary>
        /// <param name="limit">The number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first entity to return. Default: 0 (i.e., the first track). Use with limit to get the next set of entities.</param>
        /// <param name="timeRange">Over what time frame the affinities are computed. Long term is calculated from several years of data and including all new data as it becomes available.
        /// Medium term approximately last 6 months. Short term approximately lasts 4 weeks</param>
        /// <returns></returns>
        public dynamic GetCurrentUsersTopArtists(int limit, int offset, TimeRange timeRange)
        {
            string endpointUrl = "me/top/artists?time_range=" + timeRange.GetDescription() + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        #endregion

        #region Browse

        /// <summary>
        /// Get a list of new album releases featured in Spotify (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items. </param>
        /// <returns></returns>
        public dynamic GetNewReleases(int limit, int offset)
        {
            string endpointUrl = "browse/new-releases?limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of new album releases featured in Spotify (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items. </param>
        /// <returns></returns>
        public dynamic GetNewReleases(Market market, int limit, int offset)
        {
            string endpointUrl = "browse/new-releases?country=" + market + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of Spotify featured playlists (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items.</param>
        /// <returns></returns>
        public dynamic GetFeaturedPlaylists(int limit, int offset)
        {
            string endpointUrl = "browse/featured-playlists?limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of Spotify featured playlists (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="timeStamp">A timestamp in format: yyyy-MM-ddTHH:mm:ss. Use this parameter to specify the user's local time to get results tailored for that specific date and time in the day. If not provided, the response defaults to the current UTC time.</param>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items.</param>
        /// <returns></returns>
        public dynamic GetFeaturedPlaylists(string timeStamp, int limit, int offset)
        {
            string endpointUrl = "browse/featured-playlists?timestamp=" + timeStamp + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of Spotify featured playlists (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items.</param>
        /// <returns></returns>
        public dynamic GetFeaturedPlaylists(Market market, int limit, int offset)
        {
            string endpointUrl = "browse/featured-playlists?country=" + market + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of Spotify featured playlists (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="timeStamp">A timestamp in format: yyyy-MM-ddTHH:mm:ss. Use this parameter to specify the user's local time to get results tailored for that specific date and time in the day. If not provided, the response defaults to the current UTC time.</param>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items.</param>
        /// <returns></returns>
        public dynamic GetFeaturedPlaylists(Market market, string timeStamp, int limit, int offset)
        {
            string endpointUrl = "browse/featured-playlists?country=" + market + "&timestamp=" + timeStamp + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of categories used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="limit">The maximum number of categories to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of categories.</param>
        /// <returns></returns>
        public dynamic GetBrowseCategories(int limit, int offset)
        {
            string endpointUrl = "browse/categories?limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of categories used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="limit">The maximum number of categories to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of categories.</param>
        /// <returns></returns>
        public dynamic GetBrowseCategories(Market market, int limit, int offset)
        {
            string endpointUrl = "browse/categories?country=" + market + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a single category used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="id">The Spotify category ID for the category.</param>
        /// <returns></returns>
        public dynamic GetBrowseCategories(string id)
        {
            string endpointUrl = "browse/categories/" + id;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a single category used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="id">The Spotify category ID for the category.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <returns></returns>
        public dynamic GetBrowseCategories(string id, Market market)
        {
            string endpointUrl = "browse/categories/" + id + "?country=" + market;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of Spotify playlists tagged with a particular category.
        /// </summary>
        /// <param name="id">The Spotify category ID for the category.</param>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items. </param>
        /// <returns></returns>
        public dynamic GetCategoryPlaylist(string id, int limit, int offset)
        {
            string endpointUrl = "browse/categories/" + id + "/playlists?limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get a list of Spotify playlists tagged with a particular category.
        /// </summary>
        /// <param name="id">The Spotify category ID for the category.</param>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items. </param>
        /// <returns></returns>
        public dynamic GetCategoryPlaylist(string id, Market market, int limit, int offset)
        {
            string endpointUrl = "browse/categories/" + id + "/playlists?country=" + market + "&limit=" + limit + "&offset=" + offset;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        #endregion

        #region Follow

        /// <summary>
        /// Get the current user’s followed artists.
        /// </summary>
        /// <returns></returns>
        public dynamic GetCurrentUsersFollowedArtists()
        {
            string endpointUrl = "me/following?type=artist";

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Check to see if the current user is following one or more artists.
        /// </summary>
        /// <param name="id">The artist Spotify IDs to check.</param>
        /// <returns></returns>
        public dynamic CheckIfCurrentUserIsFollowingArtist(string id)
        {
            string endpointUrl = "me/following/contains?type=artist&ids=" + id;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Check to see if the current user is following one or more artists.
        /// </summary>
        /// <param name="ids">List of the artist Spotify IDs to check.</param>
        /// <returns></returns>
        public dynamic CheckIfCurrentUserIsFollowingArtists(ICollection<string> ids)
        {
            string trackIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "me/following/contains?type=artist&ids=" + trackIds;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Check to see if the current user is following one or more other Spotify users.
        /// </summary>
        /// <param name="id">The user Spotify IDs to check.</param>
        /// <returns></returns>
        public dynamic CheckIfCurrentUserIsFollowingUser(string id)
        {
            string endpointUrl = "me/following/contains?type=user&ids=" + id;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Check to see if the current user is following one or more other Spotify users.
        /// </summary>
        /// <param name="ids">List of the user Spotify IDs to check.</param>
        /// <returns></returns>
        public dynamic CheckIfCurrentUserIsFollowingUsers(ICollection<string> ids)
        {
            string trackIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "me/following/contains?type=user&ids=" + trackIds;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Add the current user as a follower or other Spotify users.
        /// </summary>
        /// <param name="id">The user's Spotify ID. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns></returns>
        public dynamic FollowUser(string id)
        {
            string endpointUrl = "me/following?type=user&ids=" + id;

            HttpMethods.SendPutRequest(endpointUrl);

            return "Followed user"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Add the current user as a follower or other Spotify users.
        /// </summary>
        /// <param name="ids">List of user's Spotify ID. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns></returns>
        public dynamic FollowUsers(ICollection<string> ids)
        {
            string trackIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "me/following?type=user&ids=" + trackIds;

            HttpMethods.SendPutRequest(endpointUrl);

            return "Followed users"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Add the current user as a follower of one or more artists.
        /// </summary>
        /// <param name="id">The artist's Spotify ID. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns></returns>
        public dynamic FollowArtist(string id)
        {
            string endpointUrl = "me/following?type=artist&ids=" + id;

            HttpMethods.SendPutRequest(endpointUrl);

            return "Followed artist"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Add the current user as a follower of one or more artists.
        /// </summary>
        /// <param name="ids">List of artist's Spotify ID. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns></returns>
        public dynamic FollowArtists(ICollection<string> ids)
        {
            string trackIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "me/following?type=artist&ids=" + trackIds;

            HttpMethods.SendPutRequest(endpointUrl);

            return "Followed artists"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Add the current user as a follower of one other Spotify users.
        /// </summary>
        /// <param name="id">The user's Spotify ID</param>
        /// <returns></returns>
        public dynamic UnfollowUser(string id)
        {
            string endpointUrl = "me/following?type=user&ids=" + id;

            HttpMethods.SendDeleteRequest(endpointUrl);

            return "Unfollowed user"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Add the current user as a follower of one or more other Spotify users.
        /// </summary>
        /// <param name="ids">List of users Spotify ID</param>
        /// <returns></returns>
        public dynamic UnfollowUsers(ICollection<string> ids)
        {
            string trackIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "me/following?type=user&ids=" + trackIds;

            HttpMethods.SendDeleteRequest(endpointUrl);

            return "Unfollowed users"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Add the current user as a follower of one artists. 
        /// </summary>
        /// <param name="id">The artist's Spotify ID</param>
        /// <returns></returns>
        public dynamic UnfollowArtist(string id)
        {
            string endpointUrl = "me/following?type=artist&ids=" + id;

            HttpMethods.SendDeleteRequest(endpointUrl);

            return "Unfollowed artist"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Add the current user as a follower of one or more artists.
        /// </summary>
        /// <param name="ids">List of artists Spotify ID</param>
        /// <returns></returns>
        public dynamic UnfollowArtists(ICollection<string> ids)
        {
            string trackIds = ids.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "me/following?type=artist&ids=" + trackIds;

            HttpMethods.SendDeleteRequest(endpointUrl);

            return "Unfollowed artists"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Check to see if one or more Spotify users are following a specified playlist.
        /// </summary>
        /// <param name="ownerId">The Spotify user ID of the person who owns the playlist.</param>
        /// <param name="playlistId">The Spotify ID of the playlist.</param>
        /// <param name="userId">The ids of the users that you want to check to see if they follow the playlist.</param>
        /// <returns></returns>
        public dynamic CheckIfUsersFollowPlaylist(string ownerId, string playlistId, string userId)
        {
            string endpointUrl = "users/" + ownerId + "/playlists/" + playlistId + "/followers/contains?ids=" + userId;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Check to see if one or more Spotify users are following a specified playlist.
        /// </summary>
        /// <param name="ownerId">The Spotify user ID of the person who owns the playlist.</param>
        /// <param name="playlistId">The Spotify ID of the playlist.</param>
        /// <param name="userIds">List of Spotify User IDs; The ids of the users that you want to check to see if they follow the playlist. Maximum: 5 ids.</param>
        /// <returns></returns>
        public dynamic CheckIfUsersFollowPlaylist(string ownerId, string playlistId, ICollection<string> userIds)
        {
            string userIdsAggregated = userIds.Aggregate((i, j) => i + ',' + j);

            string endpointUrl = "users/" + ownerId + "/playlists/" + playlistId + "/followers/contains?ids=" + userIdsAggregated;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Add the current user as a follower of a playlist.
        /// </summary>
        /// <param name="ownerId">The Spotify user ID of the person who owns the playlist.</param>
        /// <param name="playlistId">The Spotify ID of the playlist. Any playlist can be followed, regardless of its public/private status, as long as you know its playlist ID.</param>
        /// <returns></returns>
        public dynamic FollowPlaylist(string ownerId, string playlistId)
        {
            string endpointUrl = "users/" + ownerId + "/playlists/" + playlistId + "/followers";

            HttpMethods.SendPutRequest(endpointUrl);

            return "Playlist followed"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Add the current user as a follower of a playlist.
        /// </summary>
        /// <param name="ownerId">The Spotify user ID of the person who owns the playlist.</param>
        /// <param name="playlistId">The Spotify ID of the playlist. Any playlist can be followed, regardless of its public/private status, as long as you know its playlist ID.</param>
        /// <param name="Public">Default true. If true the playlist will be included in user's public playlists, if false it will remain private. To be able to follow playlists privately, the user must have granted the playlist-modify-private scope.</param>
        /// <returns></returns>
        public dynamic FollowPlaylist(string ownerId, string playlistId, bool Public)
        {
            string endpointUrl = "users/" + ownerId + "/playlists/" + playlistId + "/followers";

            JObject json =
                new JObject(
                    new JProperty("public", Public));

            string jsonString = StringUtil.StringifyJson(json);

            HttpMethods.SendPutRequest(endpointUrl, jsonString);

            return "Playlist followed"; //Added as the endpoint does not return a message
        }

        /// <summary>
        /// Remove the current user as a follower of a playlist.
        /// </summary>
        /// <param name="ownerId">The Spotify user ID of the person who owns the playlist.</param>
        /// <param name="playlistId">The Spotify ID of the playlist that is to be no longer followed.</param>
        /// <returns></returns>
        public dynamic UnfollowPlaylist(string ownerId, string playlistId)
        {
            string endpointUrl = "users/" + ownerId + "/playlists/" + playlistId + "/followers";

            HttpMethods.SendDeleteRequest(endpointUrl);

            return "Playlist unfollowed"; //Added as the endpoint does not return a message
        }

        #endregion

        #region Player

        /// <summary>
        /// Get tracks from the current user’s recently played tracks. Returns the most recent 50 tracks played by a user. Note that a track currently playing will not be visible in play history until it has completed. 
        /// A track must be played for more than 30 seconds to be included in play history.Any tracks listened to while the user had “Private Session” enabled in their client will not be returned in the list of recently played tracks.
        /// The endpoint uses a bidirectional cursor for paging. Follow the next field with the before parameter to move back in time, or use the after parameter to move forward in time.
        /// </summary>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <returns></returns>
        public dynamic GetCurrentUsersRecentlyPlayedTracks(int limit)
        {
            string endpointUrl = "me/player/recently-played";

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get tracks from the current user’s recently played tracks. Returns the most recent 50 tracks played by a user. Note that a track currently playing will not be visible in play history until it has completed. 
        /// A track must be played for more than 30 seconds to be included in play history.Any tracks listened to while the user had “Private Session” enabled in their client will not be returned in the list of recently played tracks.
        /// The endpoint uses a bidirectional cursor for paging. Follow the next field with the before parameter to move back in time, or use the after parameter to move forward in time.
        /// </summary>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="time">Specify a timestamp to return all items before or after. Returns all items before or after the timestamp but not including</param>
        /// <param name="timeStamp">A Unix timestamp in milliseconds.</param>
        /// <returns></returns>
        public dynamic GetCurrentUsersRecentlyPlayedTracks(int limit, Time time, string timeStamp)
        {
            string endpointUrl;

            if (time == Time.After)
                endpointUrl = "me/player/recently-played?after=" + timeStamp;
            else
                endpointUrl = "me/player/recently-played?before=" + timeStamp;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get information about the user’s current playback state, including track, track progress, and active device.
        /// </summary>
        /// <returns></returns>
        public dynamic GetCurrentPlaybackInformation()
        {
            string endpointUrl = "me/player";

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get information about the user’s current playback state, including track, track progress, and active device.
        /// </summary>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <returns></returns>
        public dynamic GetCurrentPlaybackInformation(Market market)
        {
            string endpointUrl = "me/player?market=" + market;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Transfer playback to a new device and determine if it should start playing.
        /// </summary>
        /// <param name="id">The ID of the device on which playback should be started/transferred.</param>
        /// <returns></returns>
        public dynamic TransferUsersPlayback(string id)
        {
            string endpointUrl = "me/player";

            JObject json =
                new JObject(
                    new JProperty("device_ids", new JArray(id)));

            string jsonString = StringUtil.StringifyJson(json);

            return HttpMethods.SendPutRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Transfer playback to a new device and determine if it should start playing.
        /// </summary>
        /// <param name="id">The ID of the device on which playback should be started/transferred.</param>
        /// <param name="play">True: ensure playback happens on new device. False: keep the current playback state. Note that a value of false for 
        /// the play parameter when also transferring to another device_id will not pause playback. To ensure that playback is paused on the new device you 
        /// should send a pause command to the currently active device before transferring to the new device_id.</param>
        /// <returns></returns>
        public dynamic TransferUsersPlayback(string id, bool play)
        {
            string endpointUrl = "me/player";

            JObject json =
                new JObject(
                    new JProperty("device_ids", new JArray(id)),
                    new JProperty("play", play));

            string jsonString = StringUtil.StringifyJson(json);

            return HttpMethods.SendPutRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Get information about a user’s available devices.
        /// </summary>
        /// <returns></returns>
        public dynamic GetUsersAvailableDevices()
        {
            string endpointUrl = "me/player/devices";

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get the object currently being played on the user’s Spotify account.
        /// </summary>
        /// <returns></returns>
        public dynamic GetUsersCurrentPlayingTrack()
        {
            string endpointUrl = "me/player/currently-playing";

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Get the object currently being played on the user’s Spotify account.
        /// </summary>
        /// <param name="market">Supply this parameter to limit the response to one particular geographical market.</param>
        /// <returns></returns>
        public dynamic GetUsersCurrentPlayingTrack(Market market)
        {
            string endpointUrl = "me/player/currently-playing?market=" + market;

            return HttpMethods.SendGetRequest(endpointUrl);
        }

        /// <summary>
        /// Resume the current playback
        /// </summary>
        /// <returns>A successful request will return a 204 NO CONTENT response code.
        /// When the device is temporarily unavailable the request will return a 202 ACCEPTED response code and the client should retry the request after 5 seconds, but no more than at most 5 retries.
        /// If the device is not found, the request will return 404 NOT FOUND response code.
        /// If the user making the request is non-premium, a 403 FORBIDDEN response code will be returned.</returns>
        public dynamic Resume()
        {
            string endpointUrl = "me/player/play";

            return HttpMethods.SendPutRequest(endpointUrl);
        }

        /// <summary>
        /// Resume the current playback on a specific device
        /// </summary>
        /// <param name="deviceId">The device id</param>
        /// <returns>A successful request will return a 204 NO CONTENT response code.
        /// When the device is temporarily unavailable the request will return a 202 ACCEPTED response code and the client should retry the request after 5 seconds, but no more than at most 5 retries.
        /// If the device is not found, the request will return 404 NOT FOUND response code.
        /// If the user making the request is non-premium, a 403 FORBIDDEN response code will be returned.</returns>
        public dynamic Resume(string deviceId)
        {
            string endpointUrl = "me/player/play?device_id=" + deviceId;

            return HttpMethods.SendPutRequest(endpointUrl);
        }

        /// <summary>
        /// Play a track
        /// </summary>
        /// <param name="id">The Spotify Track ID</param>
        /// <returns>A successful request will return a 204 NO CONTENT response code.
        /// When the device is temporarily unavailable the request will return a 202 ACCEPTED response code and the client should retry the request after 5 seconds, but no more than at most 5 retries.
        /// If the device is not found, the request will return 404 NOT FOUND response code.
        /// If the user making the request is non-premium, a 403 FORBIDDEN response code will be returned.</returns>
        public dynamic PlayTrack(string id)
        {
            string trackUri = "spotify:track:" + id;

            string endpointUrl = "me/player/play";

            JObject json =
                new JObject(
                    new JProperty("uris", new JArray(trackUri)));

            string jsonString = StringUtil.StringifyJson(json);

            return HttpMethods.SendPutRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Play one or more tracks. If more than one track is specified, the remaining tracks will be queued.
        /// </summary>
        /// <param name="ids">List of Spotify Track IDs</param>
        /// <returns>A successful request will return a 204 NO CONTENT response code.
        /// When the device is temporarily unavailable the request will return a 202 ACCEPTED response code and the client should retry the request after 5 seconds, but no more than at most 5 retries.
        /// If the device is not found, the request will return 404 NOT FOUND response code.
        /// If the user making the request is non-premium, a 403 FORBIDDEN response code will be returned.</returns>
        public dynamic PlayTracks(ICollection<string> ids)
        {
            List<string> trackUris = new List<string>();

            foreach (string str in ids)
            {
                string uri = "spotify:track:" + str;
                trackUris.Add(uri);
            }

            string endpointUrl = "me/player/play";

            JObject json =
                new JObject(
                    new JProperty("uris", new JArray(trackUris)));

            string jsonString = StringUtil.StringifyJson(json);

            return HttpMethods.SendPutRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Play one or more tracks. If more than one track is specified, the remaining tracks will be queued.
        /// </summary>
        /// <param name="ids">List of Spotify Track IDs</param>
        /// <param name="offset">Indicates where in the album playback should start. Cannot be negative.</param>
        /// <returns>A successful request will return a 204 NO CONTENT response code.
        /// When the device is temporarily unavailable the request will return a 202 ACCEPTED response code and the client should retry the request after 5 seconds, but no more than at most 5 retries.
        /// If the device is not found, the request will return 404 NOT FOUND response code.
        /// If the user making the request is non-premium, a 403 FORBIDDEN response code will be returned.</returns>
        public dynamic PlayTracks(ICollection<string> ids, int offset)
        {
            List<string> trackUris = new List<string>();

            foreach (string str in ids)
            {
                string uri = "spotify:track:" + str;
                trackUris.Add(uri);
            }

            string endpointUrl = "me/player/play";

            JObject json =
                new JObject(
                    new JProperty("uris", new JArray(trackUris)),
                    new JProperty("offset", new JObject(new JProperty("position", offset))));

            string jsonString = StringUtil.StringifyJson(json);

            return HttpMethods.SendPutRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Start playback of an album
        /// </summary>
        /// <param name="id">The Spotify Album ID</param>
        /// <returns>A successful request will return a 204 NO CONTENT response code.
        /// When the device is temporarily unavailable the request will return a 202 ACCEPTED response code and the client should retry the request after 5 seconds, but no more than at most 5 retries.
        /// If the device is not found, the request will return 404 NOT FOUND response code.
        /// If the user making the request is non-premium, a 403 FORBIDDEN response code will be returned.</returns>
        public dynamic PlayAlbum(string id)
        {
            string albumUri = "spotify:album:" + id;

            string endpointUrl = "me/player/play";

            JObject json =
                new JObject(
                    new JProperty("context_uri", albumUri));

            string jsonString = StringUtil.StringifyJson(json);

            return HttpMethods.SendPutRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Start playback of an album at a specific position
        /// </summary>
        /// <param name="id">The Spotify Album ID</param>
        /// <param name="offset">Indicates where in the album playback should start. Cannot be negative.</param>
        /// <returns>A successful request will return a 204 NO CONTENT response code.
        /// When the device is temporarily unavailable the request will return a 202 ACCEPTED response code and the client should retry the request after 5 seconds, but no more than at most 5 retries.
        /// If the device is not found, the request will return 404 NOT FOUND response code.
        /// If the user making the request is non-premium, a 403 FORBIDDEN response code will be returned.</returns>
        public dynamic PlayAlbum(string id, int offset)
        {
            string albumUri = "spotify:album:" + id;

            string endpointUrl = "me/player/play";

            JObject json =
                new JObject(
                    new JProperty("context_uri", albumUri),
                    new JProperty("offset", new JObject(new JProperty("position", offset))));

            string jsonString = StringUtil.StringifyJson(json);

            return HttpMethods.SendPutRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Start playback of a playlist
        /// </summary>
        /// <param name="playlistId">The Spotify Playlist ID</param>
        /// <param name="userId">The Spotify User ID that the playlist belongs to</param>
        /// <returns>A successful request will return a 204 NO CONTENT response code.
        /// When the device is temporarily unavailable the request will return a 202 ACCEPTED response code and the client should retry the request after 5 seconds, but no more than at most 5 retries.
        /// If the device is not found, the request will return 404 NOT FOUND response code.
        /// If the user making the request is non-premium, a 403 FORBIDDEN response code will be returned.</returns>
        public dynamic PlayPlaylist(string playlistId, string userId)
        {
            string playlistUri = "spotify:user:" + userId + ":playlist:" + playlistId;

            string endpointUrl = "me/player/play";

            JObject json =
                new JObject(
                    new JProperty("context_uri", playlistUri));

            string jsonString = StringUtil.StringifyJson(json);

            return HttpMethods.SendPutRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Start playback of a playlist at specific position
        /// </summary>
        /// <param name="playlistId">The Spotify Playlist ID</param>
        /// <param name="userId">The Spotify User ID that the playlist belongs to</param>
        /// <param name="offset">Indicates where in the album playback should start. Cannot be negative.</param>
        /// <returns>A successful request will return a 204 NO CONTENT response code.
        /// When the device is temporarily unavailable the request will return a 202 ACCEPTED response code and the client should retry the request after 5 seconds, but no more than at most 5 retries.
        /// If the device is not found, the request will return 404 NOT FOUND response code.
        /// If the user making the request is non-premium, a 403 FORBIDDEN response code will be returned.</returns>
        public dynamic PlayPlaylist(string playlistId, string userId, int offset)
        {
            string playlistUri = "spotify:user:" + userId + ":playlist:" + playlistId;

            string endpointUrl = "me/player/play";

            JObject json =
                new JObject(
                    new JProperty("context_uri", playlistUri),
                    new JProperty("offset", new JObject(new JProperty("position", offset))));

            string jsonString = StringUtil.StringifyJson(json);

            return HttpMethods.SendPutRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Start playback of an artists songs
        /// </summary>
        /// <param name="id">The Spotify Artist ID</param>
        /// <returns></returns>
        /// <returns>A successful request will return a 204 NO CONTENT response code.
        /// When the device is temporarily unavailable the request will return a 202 ACCEPTED response code and the client should retry the request after 5 seconds, but no more than at most 5 retries.
        /// If the device is not found, the request will return 404 NOT FOUND response code.
        /// If the user making the request is non-premium, a 403 FORBIDDEN response code will be returned.</returns>
        public dynamic PlayArtist(string id)
        {
            string artistUri = "spotify:artist:" + id;

            string endpointUrl = "me/player/play";

            JObject json =
                new JObject(
                    new JProperty("context_uri", artistUri));

            string jsonString = StringUtil.StringifyJson(json);

            return HttpMethods.SendPutRequest(endpointUrl, jsonString);
        }

        /// <summary>
        /// Pause playback on the user’s account.
        /// </summary>
        /// <returns></returns>
        public dynamic Pause()
        {
            string endpointUrl = "me/player/pause";

            return HttpMethods.SendPutRequest(endpointUrl);
        }

        /// <summary>
        /// Pause playback on the specified device.
        /// </summary>
        /// <param name="deviceId">The id of the device this command is targeting.</param>
        /// <returns></returns>
        public dynamic Pause(string deviceId)
        {
            string endpointUrl = "me/player/pause?device_id=" + deviceId;

            return HttpMethods.SendPutRequest(endpointUrl);
        }

        /// <summary>
        /// Skips to next track in the user’s queue.
        /// </summary>
        /// <returns></returns>
        public dynamic NextTrack()
        {
            string endpointUrl = "me/player/next";

            return HttpMethods.SendPostRequest(endpointUrl);
        }

        /// <summary>
        /// Skips to next track in the user’s queue on the specified device.
        /// </summary>
        /// <param name="deviceId">The id of the device this command is targeting.</param>
        /// <returns></returns>
        public dynamic NextTrack(string deviceId)
        {
            string endpointUrl = "me/player/next?device_id=" + deviceId;

            return HttpMethods.SendPostRequest(endpointUrl);
        }

        /// <summary>
        /// Skips to previous track in the user’s queue. Note that this will ALWAYS skip to the previous track, regardless of the current track’s progress. Returning to the start of the current track should be performed using the Seek method.
        /// </summary>
        /// <returns></returns>
        public dynamic PreviousTrack()
        {
            string endpointUrl = "me/player/previous";

            return HttpMethods.SendPostRequest(endpointUrl);
        }

        /// <summary>
        /// Skips to previous track in the user’s queue. Note that this will ALWAYS skip to the previous track, regardless of the current track’s progress. Returning to the start of the current track should be performed using the Seek method.
        /// </summary>
        /// <param name="deviceId">The id of the device this command is targeting.</param>
        /// <returns></returns>
        public dynamic PreviousTrack(string deviceId)
        {
            string endpointUrl = "me/player/previous?device_id=" + deviceId;

            return HttpMethods.SendPostRequest(endpointUrl);
        }

        /// <summary>
        /// Seeks to the given position in the user’s currently playing track.
        /// </summary>
        /// <param name="milliseconds">The position in milliseconds to seek to. Must be a positive number. Passing in a position that is greater than the length of the track will cause the player to start playing the next song.</param>
        /// <returns></returns>
        public dynamic Seek(int milliseconds)
        {
            string endpointUrl = "me/player/seek?position_ms=" + milliseconds;

            return HttpMethods.SendPutRequest(endpointUrl);
        }

        /// <summary>
        /// Seeks to the given position in the user’s currently playing track.
        /// </summary>
        /// <param name="milliseconds">The position in milliseconds to seek to. Must be a positive number. Passing in a position that is greater than the length of the track will cause the player to start playing the next song.</param>
        /// <param name="deviceId">The id of the device this command is targeting.</param>
        /// <returns></returns>
        public dynamic Seek(int milliseconds, string deviceId)
        {
            string endpointUrl = "me/player/seek?position_ms=" + milliseconds + "&device_id=" + deviceId;

            return HttpMethods.SendPutRequest(endpointUrl);
        }

        /// <summary>
        /// Set the repeat mode for the user’s playback.
        /// </summary>
        /// <param name="state">Options are repeat-track, repeat-context, and off.</param>
        /// <returns></returns>
        public dynamic SetRepeatMode(RepeatState state)
        {
            string endpointUrl = "me/player/repeat?state=" + state;

            return HttpMethods.SendPutRequest(endpointUrl);
        }

        /// <summary>
        /// Set the repeat mode for the user’s playback. 
        /// </summary>
        /// <param name="state">Options are repeat-track, repeat-context, and off.</param>
        /// <param name="deviceId">The id of the device this command is targeting.</param>
        /// <returns></returns>
        public dynamic SetRepeatMode(RepeatState state, string deviceId)
        {
            string endpointUrl = "me/player/repeat?state=" + state + "&device_id=" + deviceId;

            return HttpMethods.SendPutRequest(endpointUrl);
        }

        /// <summary>
        /// Set the volume for the user’s current playback device.
        /// </summary>
        /// <param name="volumePercentage">The volume to set. Must be a value from 0 to 100 inclusive.</param>
        /// <returns></returns>
        public dynamic SetVolume(int volumePercentage)
        {
            string endpointUrl = "me/player/volume?volume_percent=" + volumePercentage;

            return HttpMethods.SendPutRequest(endpointUrl);
        }

        /// <summary>
        /// Set the volume for the user’s current playback device.
        /// </summary>
        /// <param name="volumePercentage">The volume to set. Must be a value from 0 to 100 inclusive.</param>
        /// <param name="deviceId">The id of the device this command is targeting.</param>
        /// <returns></returns>
        public dynamic SetVolume(int volumePercentage, string deviceId)
        {
            string endpointUrl = "me/player/volume?volume_percent=" + volumePercentage + "&device_id=" + deviceId;

            return HttpMethods.SendPutRequest(endpointUrl);
        }

        /// <summary>
        /// Toggle shuffle on or off for user’s playback.
        /// </summary>
        /// <param name="state">true: shuffle user's playback. false: do not shuffle user's playback</param>
        /// <returns></returns>
        public dynamic SetShuffleStatus(bool state)
        {
            string endpointUrl = "me/player/shuffle?state=" + state;

            return HttpMethods.SendPutRequest(endpointUrl);
        }

        /// <summary>
        /// Toggle shuffle on or off for user’s playback.
        /// </summary>
        /// <param name="state">true: shuffle user's playback. false: do not shuffle user's playback</param>
        /// <param name="deviceId">The id of the device this command is targeting.</param>
        /// <returns></returns>
        public dynamic SetShuffleStatus(bool state, string deviceId)
        {
            string endpointUrl = "me/player/shuffle?state=" + state + "&device_id" + deviceId;

            return HttpMethods.SendPutRequest(endpointUrl);
        }

        #endregion

    }
}
