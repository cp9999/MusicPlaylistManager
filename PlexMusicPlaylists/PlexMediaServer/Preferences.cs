using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class Preferences
  {
    // prefs
    public const string KEY = "key";
    public const string TITLE = "title";
    protected const string PREFS__ALLOW_VIDEO = "allow_video";

    public bool AllowVideos { get; set; }


    public void loadFromList(IEnumerable<XElement> _preferences)
    {
      if (_preferences != null)
      {
        try
        {
          XElement pref = _preferences.First(elem => elem.Attribute(KEY).Value.Equals(PREFS__ALLOW_VIDEO));
          AllowVideos = pref != null && pref.Attribute(TITLE).Value.Equals("True", StringComparison.OrdinalIgnoreCase);
        }
        catch { }        
      }
    }
  }
}
