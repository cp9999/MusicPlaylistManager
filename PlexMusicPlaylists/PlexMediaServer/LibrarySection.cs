using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class LibrarySection : PlaylistBase
  {
    private IEnumerable<XElement> m_tracks = null;

    public string SectionUrl { get; set; }

    public bool HasTracks { get; set; }

    public bool IsMusic { get; set; }

    public bool IsEpisode { get; set; }

    public string TrackType
    {
      get { return IsMusic ? PMSBase.TYPE_TRACK : IsEpisode ? PMSBase.TYPE_SHOW : PMSBase.TYPE_MOVIE; }
    }

    public IEnumerable<XElement> tracks(PMSServer _pmsServer)
    {
      if (m_tracks == null && _pmsServer != null)
      {
        m_tracks = _pmsServer.getSectionTracks(this);
      }
      return m_tracks;
    }
  }
}
