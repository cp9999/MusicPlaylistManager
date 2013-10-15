using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class PlaylistBase
  {

    public string Key { get; set; }

    public string Title { get; set; }

    public int Duration { get; set; }

    protected int Hours
    {
      get
      {
        return Duration / 3600;
      }
    }
    protected int Minutes
    {
      get
      {
        return (Duration % 3600) / 60;
      }
    }
    protected int Seconds
    {
      get
      {
        return Duration % 60;
      }
    }

  }
}
