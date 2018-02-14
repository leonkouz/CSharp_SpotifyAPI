using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_SpotifyAPI;
using CSharp_SpotifyAPI.Enums;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string clientID = "305dbadf23cd4d9688868eb01857b54b";
            string redirectID = "http%3A%2F%2Flocalhost%3A62177";
            string state = "123";
            List<Scope> scope = new List<Scope>()
            {
                Scope.UserReadPrivate, Scope.UserReadBirthdate, Scope.UserModifyPlaybackState, Scope.UserModifyPlaybackState, Scope.UserFollowRead, Scope.UserFollowModify, Scope.UserReadRecentlyPlayed, Scope.UserReadPlaybackState
            };

            SpotifyAPI api = new SpotifyAPI(clientID, redirectID, state, scope, true);

            /*
            string[] albumIds = new string[2] {"1FpEcjbwwsSKIeCBzNKZdc", "2YDNDwQvsU0njt7Kq0xNRY"};

            Console.WriteLine(api.GetSeveralAlbums(albumIds, Markets.AD));*/

            /*List<AlbumType> albumTypes = new List<AlbumType>()
            {
                AlbumType.Single, AlbumType.Album, AlbumType.ApppearsOn, AlbumType.Compilation
            };

            Console.WriteLine(api.GetArtistsAlbums("2zFkwocH9Ah8KpUzydbcrO", albumTypes, 20, 0));*/

            //Console.WriteLine(api.GetAlbumTracks("3tqWiJf0QKpxX2IItsAl24", Market.AD, 20, 0));

            //Console.WriteLine(api.GetArtist("4F7Q5NV6h5TSwCainz8S5A"));

            /*List<string> artistIds = new List<string>()
            {
                "34W7ZCX0LZeJd8q6boKGOk", "4PDpGtF16XpqvXxsrFwQnN", "3HJzLaMbS0jMabxS3wttGk"
            };

            Console.WriteLine(api.GetSeveralArtists(artistIds));*/

            //Console.WriteLine(api.GetArtistsTopTracks("6Zq5ky484xYTgxE6dQ8yHh", Market.AD));

            //Console.WriteLine(api.GetArtistsRelatedArtists("34W7ZCX0LZeJd8q6boKGOk"));

            //Console.WriteLine(api.GetTrack("0hz62SbhrP77G1cajlwaEH", Market.ES));

             List<string> trackids = new List<string>()
             {
                 "250RLekaiL1q9qZer975Eg", "0pf9ik9MHYZnxEgDw3NYp0", "250RLekaiL1q9qZer975Eg", "0pf9ik9MHYZnxEgDw3NYp0"
             };

            /*Console.WriteLine(api.GetSeveralTracks(trackids, Market.ES));*/

            //Console.WriteLine(api.GetAudioFeaturesForTrack("184r9uzj1Coildl16EK5Qz"));

            //Console.WriteLine(api.GetAudioFeaturesForSeveralTracks(trackids));

            //Console.WriteLine(api.GetAudioAnalysisForTrack("4r1zlgDyILnZq9fEaSwFGd"));

            /*List<SearchType> searchTypes = new List<SearchType>()
            {
                SearchType.playlist, SearchType.album
            };

            Console.WriteLine(api.Search("roadhouse%20OR%20blues", searchTypes, Market.AD, 20, 0));*/

            //Console.WriteLine(api.GetUsersPlaylists("name", 20, 0));

            //Console.WriteLine(api.GetCurrentUsersPlaylists(10, 0));

            //Console.WriteLine(api.GetPlaylist("leonkouz", "5WzFmpYAKQ8frPXw5yIJtz", Market.AD, "description,uri"));

            //Console.WriteLine(api.GetPlaylistsTracks("leonkouz", "5kIg55zaJKaFKgqrwOKffb", 20, 0));

            //Console.WriteLine(api.CreatePlaylist("leonkouz", "test", false, "Name", false));

            //Console.WriteLine(api.AddTrackToPlaylist("leonkouz", "1npLUC4yTNWePi0qGwbWN8", trackids));

            //Console.WriteLine(api.RemoveTrackFromPlaylist("leonkouz", "1npLUC4yTNWePi0qGwbWN8", "0pf9ik9MHYZnxEgDw3NYp0"));

            /*Dictionary<string, int> trackIds = new Dictionary<string, int>()
            {
                {"0pf9ik9MHYZnxEgDw3NYp0", 1 },
            };*/

            //Console.WriteLine(api.RemoveTracksFromPlaylist("leonkouz", "1npLUC4yTNWePi0qGwbWN8", trackIds, "y43RmddChW8L88cIjgqDdbTIdYl9bsb8i6u9JzO91F8wM3lUMCFsF2LIqLoQui4A"));

            //Console.WriteLine(api.ReorderPlaylistTracks("leonkouz", "1npLUC4yTNWePi0qGwbWN8", 0, 2, 3));

            //Console.WriteLine(api.ChangePlaylistDetails("leonkouz", "1npLUC4yTNWePi0qGwbWN8", "new name", null, null, null));

            //Console.WriteLine(api.GetUserProfile("leonkouz"));

            //Console.WriteLine(api.GetCurrentUserProfile());

            //Console.WriteLine(api.GetCurrentUsersSavedTracks(Market.ES, 0, 0));

            //Console.WriteLine(api.CheckCurrentUsersSavedTracks(trackids));

            //Console.WriteLine(api.SaveTracksForCurrentUser(trackids));

            //Console.WriteLine(api.RemoveTracksForCurrentUser(trackids));

            //Console.WriteLine(api.GetCurrentUsersSavedAlbums(Market.AD));

            //Console.WriteLine(api.CheckUsersSavedAlbums("02S1126Q5E4gUEtpTI6W38"));

            /*List<string> albumIds = new List<string>()
            {
                "4lVR2fg3DAUQpGVJ6DciHW", "7J0uECwRkAFLiZljgYFq1w"
            };*/

            //Console.WriteLine(api.SaveAlbumsForCurrentUser(albumIds));

            //Console.WriteLine(api.RemoveAlbumsForCurrentUser(albumIds));

            //Console.WriteLine(api.GetCurrentUsersTopTracks(20, 0, TimeRange.LongTerm));

            //Console.WriteLine(api.GetCurrentUsersTopArtists(20, 0, TimeRange.LongTerm));

            //Console.WriteLine(api.GetListOfNewReleases(20, 0));

            //Console.WriteLine(api.GetFeaturedPlaylists("2018-02-01T09:01:02",  20, 0));

            //Console.WriteLine(api.GetBrowseCategories(Market.BR, 20, 0));

            //Console.WriteLine(api.GetCategoryPlaylist("dinner",Market.EC ,20, 0));

            //Console.WriteLine(api.GetCurrentUsersFollowedArtists());

            List<string> userIds = new List<string>()
            {
                "tezzbian", "biggboris"
            };

            List<string> artistIds = new List<string>()
            {
                "41MozSoPIsD1dJM0CLPjZF", "6gzXCdfYfFe5XKhPKkYqxV"
            };

            //Console.WriteLine(api.CheckIfCurrentUserIsFollowingArtist("41MozSoPIsD1dJM0CLPjZF"));

            //Console.WriteLine(api.CheckIfCurrentUserIsFollowingArtists(artistIds));

            //Console.WriteLine(api.CheckIfCurrentUserIsFollowingUser("tezzbian"));

            //Console.WriteLine(api.CheckIfCurrentUserIsFollowingUsers(userIds));

            //Console.WriteLine(api.FollowUser("biggboris"));

            //Console.WriteLine(api.FollowUsers(userIds));

            //Console.WriteLine(api.FollowArtist("3Nrfpe0tUJi4K4DXYWgMUX"));

            //Console.WriteLine(api.FollowArtists(artistIds));

            //Console.WriteLine(api.UnfollowUser("tezzbian"));

            //Console.WriteLine(api.UnfollowUsers(userIds));

            //Console.WriteLine(api.UnfollowArtist("31ID15xoalmnSgwPhHJZrR"));

            //Console.WriteLine(api.UnfollowArtists(artistIds));

            //Console.WriteLine(api.CheckIfUsersFollowPlaylist("leonkouz", "5dbkqx1RxAiTf64YCfxocS", artistOrUserId));

            //Console.WriteLine(api.FollowPlaylist("spotify", "37i9dQZF1DWVbckf5vh03w", false));

            //Console.WriteLine(api.UnfollowPlaylist("spotify", "37i9dQZF1DWVbckf5vh03w"));

            //Console.WriteLine(api.GetCurrentUsersRecentlyPlayedTracks(20, Time.Before, "1518343202525"));

            //Console.WriteLine(api.GetCurrentPlaybackInformation(Market.AR));

            //Console.WriteLine(api.TransferUsersPlayback("bad41f473b836de526e5c1cbac1b9f63ebe283d5", true));

            //Console.WriteLine(api.GetUsersAvailableDevices());

            //Console.WriteLine(api.GetUsersCurrentPlayingTrack(Market.AT));

            //Console.WriteLine(api.Resume("bad41f473b836de526e5c1cbac1b9f63ebe283d5"));

            //Console.WriteLine(api.PlayTracks(trackids, 1));

            //Console.WriteLine(api.PlayAlbum("5f8pMn2A5d5lKMDapYbCmp", 5));

            //Console.WriteLine(api.PlayPlaylist("5LXyV8imf7zQ5eCXI5CQ8s", "tezzbian", 5));

            //Console.WriteLine(api.PlayArtist("7ohlPA8dRBtCf92zaZCaaB"));

            //Console.WriteLine(api.Pause("13c53822b6a6f324e088becda0f6f052737d7d5c"));

            //Console.WriteLine(api.NextTrack("13c53822b6a6f324e088becda0f6f052737d7d5c"));

            //Console.WriteLine(api.PreviousTrack("13c53822b6a6f324e088becda0f6f052737d7d5c"));

            Console.WriteLine(api.Seek(2500000, "13c53822b6a6f324e088becda0f6f052737d7d5c"));

            Console.ReadLine();

        }
    }
}


