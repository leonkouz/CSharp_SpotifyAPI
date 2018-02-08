using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_SpotifyAPI.Enums
{
    public enum TimeRange
    {
        [Description("long_term")]
        LongTerm = 0,

        [Description("medium_term")]
        MediumTerm = 1,

        [Description("short_term")]
        ShortTerm = 2
    }
}
