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
        Album = 1,

        [Description("single")]
        Single = 2,

        [Description("appears_on")]
        ApppearsOn = 3,

        [Description("compilation")]
        Compilation = 4, 
    }
}
