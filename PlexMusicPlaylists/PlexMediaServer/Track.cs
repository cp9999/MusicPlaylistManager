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

    public string DurationStr
    {
      get
      {
        return String.Format("{0}:{1}", Minutes.ToString("00"), Seconds.ToString("00"));
      }
    }

    private float _order;
    public float Order 
    {
      get { return _order; }
      set { SetPropertyField("Órder", ref _order, value); }
    }

    private string _trackType;
    public string TrackType 
    {
      get { return _trackType; }
      set { SetPropertyField("TrackType", ref _trackType, value); }
    }

    public Track() : base()
    {
    }

    public Track(IList<Track> _ownerList)
      : base()
    {
      ownerList = _ownerList;
    }

    public void setOwnerList(IList<Track> _ownerList)
    {
      ownerList = _ownerList;
    }

  }
}
