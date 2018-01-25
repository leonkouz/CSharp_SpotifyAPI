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
    }
}
