﻿using System;
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

            foreach(var value in trackIdAndPosition)
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

        #endregion
    }
}
