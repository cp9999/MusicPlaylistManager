using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class PLUser : PlaylistBase
  {
    public const string USER_UNKNOWN = "Unknown";

    public string Name 
    {
      get { return this.Key; }
      set { this.Key = value; } 
    }


    public bool isUnknownUser()
    {
      return Name.Equals(USER_UNKNOWN, StringComparison.OrdinalIgnoreCase);
    }
  }
}
