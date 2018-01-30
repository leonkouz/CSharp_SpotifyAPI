﻿using CSharp_SpotifyAPI.Enums;
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

        public static string AggregateEnums<T>(ICollection<T> scopes)
        {
            string scopeContents = null;

            foreach (T item in scopes)
            {
                scopeContents += item.GetDescription() + "%20";
            }
            scopeContents = scopeContents.Remove(scopeContents.Length - 3);

            return scopeContents;

        }

    }
}
