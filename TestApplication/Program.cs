using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_SpotifyAPI;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string clientID = "305dbadf23cd4d9688868eb01857b54b";
            string redirectID = "http%3A%2F%2Flocalhost%3A62177";
            string state = "123";
            Scope scope = Scope.UserLibraryRead;

            SpotifyAPI api = new SpotifyAPI(clientID, redirectID, state, scope, true);

            Console.WriteLine(api.GetAlbum("02S1126Q5E4gUEtpTI6W38", "ES"));

            Console.ReadLine();



        }
    }
}
