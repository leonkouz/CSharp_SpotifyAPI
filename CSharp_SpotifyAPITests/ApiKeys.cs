using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharp_SpotifyAPITests
{
    public class ApiKeys
    {
        /// <summary>
        /// Read the Spotify.txt file for the Spotify API client Id in %appdata%\MusicBotPlayerCache.
        /// </summary>
        /// <returns>The Spotify client ID.</returns>
        public static string GetSpotifyClientIdFromAppData()
        {
            string appDataPathTextFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MusicBotPlayerCache\\SpotifyToken.txt";

            if (File.Exists(appDataPathTextFile))
            {
                string text = File.ReadAllText(appDataPathTextFile);

                return text;
            }
            else
            {
                return null;
            }
        }

    }
}