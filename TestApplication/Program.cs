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
            //create server with auto assigned port
            SimpleHttpServer myServer = new SimpleHttpServer(62177, AuthType.Implicit);
            myServer.Listen();

            Console.WriteLine("Listening on 62177");

            Console.ReadLine();


        }
    }
}
