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
    protected const string FOLDER = "PlexMusicPlaylists";
    protected const string LOGFOLDER = "Logs";
    protected const string FILENAME = "PlaylistSettings.xml";
    public string IP { get; set; }
    public int Port { get; set; }
    public static XmlSerializer xs;

    static PlaylistSettings()
    {
      xs = new XmlSerializer(typeof(PlaylistSettings));
      initDataFolder();
    }

    static void initDataFolder()
    {
      try
      {
        if (!Directory.Exists(SettingsFolder))
        {
          // Create our settings folder
          DirectoryInfo dir = Directory.CreateDirectory(SettingsFolder);
          // Copy existing settings from old to new storage location
          string oldSettingsFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FILENAME);
          if (File.Exists(oldSettingsFileName))
          {
            File.Copy(oldSettingsFileName, SettingsFileName);
          }
        }
        if (!Directory.Exists(LogFolder))
        {
          // Create our logs folder
          DirectoryInfo dir = Directory.CreateDirectory(LogFolder);
        }
      }
      catch { }
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
        return Path.Combine(SettingsFolder, FILENAME);
      }
    }

    public static string SettingsFolder
    {
      get
      {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FOLDER);
      }
    }

    public static string LogFolder
    {
      get
      {
        return Path.Combine(PlaylistSettings.SettingsFolder, LOGFOLDER);
      }
    }

  }
}
