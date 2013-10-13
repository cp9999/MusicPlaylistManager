using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class PlaylistSettings
  {
    protected const string FILENAME = "PlaylistSettings.xml";
    public string IP { get; set; }
    public int Port { get; set; }
    public static XmlSerializer xs;

    static PlaylistSettings()
    {
      xs = new XmlSerializer(typeof(PlaylistSettings));
    }

    protected PlaylistSettings()
    {
      IP = "";
      Port = 32400;
    }

    public void SaveToFile(string _filename)
    {
      try
      {
        using (StreamWriter sw = new StreamWriter(_filename))
        {
          xs.Serialize(sw, this);
        }
      }
      catch
      {
      }
    }

    public static PlaylistSettings theSettings()
    {
      return ReadFromFile(SettingsFileName);
    }

    public void Save()
    {
      SaveToFile(SettingsFileName);
    }

    public static PlaylistSettings ReadFromFile(string _fileName)
    {
      try
      {
        using (StreamReader sr = new StreamReader(_fileName))
        {
          return xs.Deserialize(sr) as PlaylistSettings;
        }
      }
      catch
      {
        return new PlaylistSettings();
      }
    }

    public static string SettingsFileName
    {
      get
      {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FILENAME);
      }
    }
  }
}
