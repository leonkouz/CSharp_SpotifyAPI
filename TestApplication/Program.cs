﻿using System;
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

            List<AlbumType> albumTypes = new List<AlbumType>()
            {
                AlbumType.Single
            };

            Console.WriteLine(api.GetAlbumByArtist("2zFkwocH9Ah8KpUzydbcrO", albumTypes, Market.AD));


            Console.ReadLine();

        }
    }
}
