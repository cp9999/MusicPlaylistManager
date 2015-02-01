using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class PlaylistBase
  {
    private bool _propertyModified;

    private int _id;
    public int Id
    {
      get { return _id; }
      set { SetPropertyField("Id", ref _id, value); }
    }

    private string _key;
    public string Key
    {
      get { return _key; }
      set { SetPropertyField("Key", ref _key, value); }
    }

    private string _title;
    public string Title 
    {
      get { return _title; }
      set { SetPropertyField("Title", ref _title, value); }
    }

    private int _duration;
    public int Duration 
    {
      get { return _duration; }
      set { SetPropertyField("Duration", ref _duration, value); }
    }

    private bool _modified;
    public bool Modified
    {
      get { return _modified || _propertyModified; }
      set
      {
        _modified = value;
        if (!_modified)
        {
          this.resetPropertyModified();
        }
      }
    }

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

    public void resetPropertyModified()
    {
      _propertyModified = false;
    }


    protected void SetPropertyField<T>(string propertyName, ref T field, T newValue)
    {
      if (!EqualityComparer<T>.Default.Equals(field, newValue))
      {
        field = newValue;
        _propertyModified = true;
      }
    }
  }
}
