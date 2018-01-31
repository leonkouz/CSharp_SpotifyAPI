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
                Scope.UserLibraryRead, Scope.PlaylistReadPrivate
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

            /*List<string> trackids = new List<string>()
            {
                "4r1zlgDyILnZq9fEaSwFGd", "184r9uzj1Coildl16EK5Qz"
            };

            Console.WriteLine(api.GetSeveralTracks(trackids, Market.ES));*/

            Console.WriteLine(api.GetAudioFeaturesForTrack("184r9uzj1Coildl16EK5Qz"));

            Console.ReadLine();

        }
    }
}
