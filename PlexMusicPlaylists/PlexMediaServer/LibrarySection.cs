using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class LibrarySection : PlaylistBase
  {

    public string SectionUrl { get; set; }

    public bool HasTracks { get; set; }

    public bool IsMusic { get; set; }

    public string TrackType
    {
      get { return IsMusic ? "track" : "movie"; }
    }
  }
}
