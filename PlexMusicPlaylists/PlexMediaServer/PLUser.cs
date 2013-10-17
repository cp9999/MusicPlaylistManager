using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class PLUser : PlaylistBase
  {
    public string Name 
    {
      get { return this.Key; }
      set { this.Key = value; } 
    }

  }
}
