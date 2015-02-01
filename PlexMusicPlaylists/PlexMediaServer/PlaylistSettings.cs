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
    protected const string PLAYLISTFOLDER = "Playlists";
    protected const string SQLPLAYLISTFOLDER = "Sql";
    protected const string FILENAME = "PlaylistSettings.xml";
    public string IP { get; set; }
    public int Port { get; set; }
    public string ChannelDataFolder { get; set; }
    public bool ChannelPreferDataFolder { get; set; }
    public string PlaylistDB { get; set; }
    public bool DatabaseCreateSqlFiles { get; set; }
    public bool DatabaseDirectUpdate { get; set; }
    public bool DatabaseModifiedTracksOnly { get; set; }
    public PlaylistManager.PlaylistMode GUIPlaylistMode { get; set; }
    private static XmlSerializer xs;
    private static PlaylistSettings m_playlistSettings = null;

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
        if (!Directory.Exists(PlaylistFolder))
        {
          // Create our logs folder
          DirectoryInfo dir = Directory.CreateDirectory(PlaylistFolder);
        }
        if (!Directory.Exists(SqlPlaylistFolder))
        {
          // Create our logs folder
          DirectoryInfo dir = Directory.CreateDirectory(SqlPlaylistFolder);
        }
      }
      catch { }
    }

    protected PlaylistSettings()
    {
      IP = "";
      Port = 32400;
      ChannelDataFolder = "";
      ChannelPreferDataFolder = false;
      PlaylistDB = "";
      DatabaseCreateSqlFiles = true;
      DatabaseDirectUpdate = false;
      DatabaseModifiedTracksOnly = true;
      GUIPlaylistMode = PlaylistManager.PlaylistMode.PlexNative;
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

    public static bool ValidChannelDataFolder
    {
      get { return m_playlistSettings != null && !String.IsNullOrEmpty(m_playlistSettings.ChannelDataFolder) && Directory.Exists(m_playlistSettings.ChannelDataFolder); }
    }

    public static PlaylistSettings theSettings()
    {
      if (m_playlistSettings == null)
      {
        m_playlistSettings = ReadFromFile(SettingsFileName);
      }
      return m_playlistSettings;
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

    public static string PlaylistFolder
    {
      get
      {
        return Path.Combine(PlaylistSettings.SettingsFolder, PLAYLISTFOLDER);
      }
    }

    public static string SqlPlaylistFolder
    {
      get
      {
        return Path.Combine(PlaylistFolder, SQLPLAYLISTFOLDER);
      }
    }

  }
}
