using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class LibrarySection : PlaylistBase
  {

    public string Prompt { get; set; }

    public string Search { get; set; }

    public string SectionUrl { get; set; }

    public bool HasTracks { get; set; }
  }
}
