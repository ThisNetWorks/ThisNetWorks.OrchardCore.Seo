using System;
using System.Collections.Generic;
using System.Text;

namespace ThisNetWorks.OrchardCore.Seo.OpenGraphMeta.Models
{
    public class OpenGraphMusicSongMetadata
    {        //types meta
        public int Duration { get; set; } //- integer >=1 - The song's length in seconds.
             //public music:album - music.album array - The album this song is from.
             public int Disc { get; set; }
             //music:album:disc - integer >=1 - Which disc of the album this song is on.
             //music:album:track - integer >=1 - Which track this song is.
             //music:musician - profile array - The musician that made this song.
    }
}
