using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_SpotifyAPI.Enums
{
    public static class ScopeExtensions
    {
        public static string GetDescription(this Scope scope)
        {
            FieldInfo fieldInfo = scope.GetType().GetField(scope.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }

        public static string GetDescription(this AlbumType scope)
        {
            FieldInfo fieldInfo = scope.GetType().GetField(scope.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }




    }
}
