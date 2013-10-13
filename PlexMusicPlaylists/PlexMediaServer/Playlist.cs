using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class Playlist : PlaylistBase
  {
    public string Description { get; set; }

    public bool canRenameTo(string _newTitle)
    {
      _newTitle = (_newTitle ?? "").Trim();
      return !String.IsNullOrEmpty(_newTitle) && Title.Trim() != _newTitle;
    }
  }
}
