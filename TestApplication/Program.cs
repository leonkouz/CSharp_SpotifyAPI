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
            string clientID = "";
            string redirectID = "http%3A%2F%2Flocalhost%3A62177";
            string state = "123";
            Scope scope = Scope.UserLibraryRead;

            SpotifyAPI api = new SpotifyAPI(clientID, redirectID, state, scope, false);

            Console.ReadLine();



        }
    }
}
