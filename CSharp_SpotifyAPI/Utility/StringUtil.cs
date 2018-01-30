using CSharp_SpotifyAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_SpotifyAPI
{
    class StringUtil
    {
        public static string RemoveAllInstanceOfCharacter(char CharacterToRemove, string StringToRemoveFrom)
        {
            StringToRemoveFrom = StringToRemoveFrom.Replace(CharacterToRemove.ToString(), string.Empty);

            return StringToRemoveFrom;
        }

        public static string AggregateCollection<T>(ICollection<T> col)
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

    }
}
