using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_SpotifyAPI.Enums
{
    public enum Scope
    {
        /// <summary>
        /// If no scope is specified, access is permitted only to publicly available information: that is, only information normally visible to normal logged-in users of the Spotify desktop, web, 
        /// and mobile clients (e.g. public playlists).
        /// </summary>
        [Description("")]
        None = 0,

        /// <summary>
        /// Read access to user's private playlists.
        /// </summary>
        [Description("playlist-read-private")]
        PlaylistReadPrivate = 1,

        /// <summary>
        /// Include collaborative playlists when requesting a user's playlists.
        /// </summary>
        [Description("playlist-read-collaborative")]
        PlaylistReadCollaborative = 2,

        /// <summary>
        /// Write access to a user's public playlists.
        /// </summary>
        [Description("playlist-modify-public")]
        PlaylistModifyPublic = 3,

        /// <summary>
        /// Write access to a user's private playlists.
        /// </summary>
        [Description("playlist-modify-private")]
        PlaylistModifyPrivate = 4,

        /// <summary>
        /// Control playback of a Spotify track. This scope is currently only available to Spotify native SDKs (for example, the iOS SDK and the Android SDK). The user must have a Spotify Premium account.	
        /// </summary>
        [Description("streaming")]
        Steaming = 5,

        /// <summary>
        /// Upload playlist cover image.
        /// </summary>
        [Description("ugc-image-upload")]
        UgcImageUpload = 6,

        /// <summary>
        /// Write/delete access to the list of artists and other users that the user follows.
        /// </summary>
        [Description("user-follow-modify")]
        UserFollowModify = 7,

        /// <summary>
        /// Read access to the list of artists and other users that the user follows.
        /// </summary>
        [Description("user-follow-read")]
        UserFollowRead = 8,

        /// <summary>
        /// Read access to a user's "Your Music" library.
        /// </summary>
        [Description("user-library-read")]
        UserLibraryRead = 9,

        /// <summary>
        /// Write/delete access to a user's "Your Music" library.
        /// </summary>
        [Description("user-library-modify")]
        UserLibraryModify = 10,

        /// <summary>
        /// Read access to user’s subscription details (type of user account).	
        /// </summary>
        [Description("user-read-private")]
        UserReadPrivate = 11,

        /// <summary>
        /// Read access to the user's birthdate.
        /// </summary>
        [Description("user-read-birthdate")]
        UserReadBirthdate = 12,

        /// <summary>
        /// Read access to user’s email address.	
        /// </summary>
        [Description("user-read-email")]
        UserReadEmail = 13,

        /// <summary>
        /// Read access to a user's top artists and tracks.
        /// </summary>
        [Description("user-top-read")]
        UserTopRead = 14,

        /// <summary>
        /// Read access to a user's playback state.
        /// </summary>
        [Description("user-read-playback-state")]
        UserReadPlaybackState = 15,

        /// <summary>
        /// Control playback on Spotify clients and Spotify Connect devices.
        /// </summary>
        [Description("user-modify-playback-state")]
        UserModifyPlaybackState = 16,

        /// <summary>
        /// Read access to a user's currently playing track.
        /// </summary>
        [Description("user-read-currently-playing")]
        UserReadCurrentlyPlaying = 17,

        /// <summary>
        /// Read access to a user's recently played items.
        /// </summary>
        [Description("user-read-recently-played")]
        UserReadRecentlyPlayed = 18

    }
}
