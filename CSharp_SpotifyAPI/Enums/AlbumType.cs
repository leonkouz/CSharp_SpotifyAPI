using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_SpotifyAPI.Enums
{
    public enum AlbumType
    {
        [Description("album")]
        Album = 0,

        [Description("single")]
        Single = 1,

        [Description("appears_on")]
        ApppearsOn = 2,

        [Description("compilation")]
        Compilation = 3, 
    }
}
