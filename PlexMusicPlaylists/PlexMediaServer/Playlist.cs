﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class Playlist : PlaylistBase
  {
    public string Description { get; set; }
    public Guid UniqueId { get; set; }
    public int AccountId { get; set; }

    public Playlist()
      : base()
    {
      UniqueId = Guid.Empty;
    }

    public string DurationStr
    {
      get
      {
        return String.Format("{0}:{1}:{2}", Hours.ToString("00"), Minutes.ToString("00"), Seconds.ToString("00"));
      }
    }

    public bool canRenameTo(string _newTitle, string _newDescription)
    {
      _newTitle = (_newTitle ?? "").Trim();
      _newDescription = (_newDescription ?? "").Trim();
      return (!String.IsNullOrEmpty(_newTitle) && Title.Trim() != _newTitle) || (Description.Trim() != _newDescription);
    }
  }
}
