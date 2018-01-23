using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_SpotifyAPI
{
    public enum Scope
    {
        [Description("")]
        None = 0,

        [Description("playlist-read-private")]
        PlaylistReadPrivate = 1,

        [Description("playlist-read-collaborative")]
        PlaylistReadCollaborative = 2,

        [Description("playlist-modify-public")]
        PlaylistModifyPublic = 3,

        [Description("playlist-modify-private")]
        PlaylistModifyPrivate = 4,

        [Description("streaming")]
        Steaming = 5,

        [Description("ugc-image-upload")]
        UgcImageUpload = 6,

        [Description("user-follow-modify")]
        UserFollowModify = 7,

        [Description("user-follow-read")]
        UserFollowRead = 8,

        [Description("user-library-read")]
        UserLibraryRead = 9,

        [Description("user-library-modify")]
        UserLibraryModify = 10,

        [Description("user-read-private")]
        UserReadPrivate = 11,

        [Description("user-read-birthdate")]
        UserReadBirthdate = 12,

        [Description("user-read-email")]
        UserReadEmail = 13,

        [Description("user-top-read")]
        UserTopRead = 14,

        [Description("user-read-playback-state")]
        UserReadPlaybackState = 15,

        [Description("user-modify-playback-state")]
        UserModifyPlaybackState = 16,

        [Description("user-read-currently-playing")]
        UserReadCurrentlyPlaying = 17,

        [Description("user-read-recently-played")]
        UserReadRecentlyPlayed = 18

    }

    public static class ScopeExtensions
    {

        public static string GetDescription(this Scope scope)
        {
            FieldInfo fieldInfo = scope.GetType().GetField(scope.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
                        
        }

    }

}
