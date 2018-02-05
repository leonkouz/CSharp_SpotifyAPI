using CSharp_SpotifyAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CSharp_SpotifyAPI
{
    class StringUtil
    {
        public static string RemoveAllInstanceOfCharacter(char CharacterToRemove, string StringToRemoveFrom)
        {
            StringToRemoveFrom = StringToRemoveFrom.Replace(CharacterToRemove.ToString(), string.Empty);

            return StringToRemoveFrom;
        }

        public static string AggregateEnumsWithDescription<T>(ICollection<T> col)
        {
            string scopeContents = null;

            foreach (T item in col)
            {
                scopeContents += item.GetDescription() + ",";
            }
            //Removes the extra comma at the end
            scopeContents = scopeContents.Remove(scopeContents.Length - 1);

            return scopeContents;
        }

        public static string AggregateEnums<T>(ICollection<T> col)
        {
            string scopeContents = null;

            foreach (T item in col)
            {
                scopeContents += item.ToString() + ",";
            }
            //Removes the extra comma at the end
            scopeContents = scopeContents.Remove(scopeContents.Length - 1);

            return scopeContents;
        }

        /// <summary>
        /// Creates a HTTP encoded Spotify Track URI from the Spotify Track ID
        /// </summary>
        /// <param name="id">Sptofiy Track ID</param>
        /// <returns></returns>
        public static string CreateSpotifyURI(string id)
        {
            string trackIdUri = "spotify:track:" + id;

            string trackIdUriEncoded = HttpUtility.UrlEncode(trackIdUri);

            return trackIdUriEncoded;
        }

        /// <summary>
        /// Creates a aggregated string of HTTP encoded Spotify Track URIs from the Spotify Track IDs
        /// </summary>
        /// <param name="ids">List of Spofity Track IDs</param>
        /// <returns></returns>
        public static string CreateSpotifyURI(ICollection<string> ids)
        {
            List<string> uris = new List<string>();
            foreach(string str in ids)
            {
                string uri = "spotify:track:" + str;

                string encodedUri = HttpUtility.UrlEncode(uri);

                uris.Add(encodedUri);
            }

            string trackUris = uris.Aggregate((i, j) => i + ',' + j);
            return trackUris;
        }

        public static string StringifyJson(dynamic json)
        {
            string jsonString = json.ToString();

            return jsonString = jsonString.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(" ", "");
        }

    }
}
