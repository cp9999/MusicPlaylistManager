using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class SectionLocation
  {
    protected MainSection m_owner = null;

    public SectionLocation(MainSection _owner)
      : base()
    {
      m_owner = _owner;
      PlexLocation = "";
      MappedLocation = "";
    }
    public SectionLocation()
      : base()
    {
      m_owner = null;
      PlexLocation = "";
      MappedLocation = "";
    }

    public MainSection Owner()
    {
      return m_owner;
    }

    public string PlexLocation { get; set; }

    public string MappedLocation { get; set; }
    public string MainSectionName
    {
      get
      {
        return m_owner != null ? m_owner.Title : "";
      }
    }
  }
}
