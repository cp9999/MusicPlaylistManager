using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class Track : PlaylistBase
  {
    protected IList<Track> ownerList = null;

    public int Index
    {
      get
      {
        if (ownerList != null)
        {
          return ownerList.IndexOf(this) + 1;
        }
        return 0;
      }
    }

    public Track() : base()
    {
    }

    public Track(IList<Track> _ownerList)
      : base()
    {
      ownerList = _ownerList;
    }
  }
}
