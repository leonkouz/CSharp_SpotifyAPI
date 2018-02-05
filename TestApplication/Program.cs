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
                Scope.UserReadPrivate, Scope.UserReadBirthdate, Scope.UserReadEmail, Scope.UserLibraryModify
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

            //Console.WriteLine(api.GetUsersPlaylists("beanzu", 20, 0));

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

            Console.WriteLine(api.RemoveTracksForCurrentUser(trackids));

            Console.ReadLine();

        }
    }
}
