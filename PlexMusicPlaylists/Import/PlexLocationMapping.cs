using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using PlexMusicPlaylists.PlexMediaServer;

namespace PlexMusicPlaylists.Import
{
  public class PlexLocationMapping
  {
    public char DirectorySeparator { get; set; }
    public List<SectionLocation> Locations { get; set; }
    public bool ShownOnEmptyMappedlocation { get; set; }
    public static XmlSerializer xs;

    static PlexLocationMapping()
    {
      xs = new XmlSerializer(typeof(PlexLocationMapping));
    }

    public PlexLocationMapping()
    {
      DirectorySeparator = PMSBase.FORWARD_SLASH;
      ShownOnEmptyMappedlocation = false;
    }

    public void SaveToFile(string _filename)
    {
      try
      {
        using (StreamWriter sw = new StreamWriter(SettingsFileName(_filename)))
        {
          xs.Serialize(sw, this);
        }
      }
      catch
      {
      }
    }

    public void Save(string _fileName)
    {
      SaveToFile(_fileName);
    }

    public static PlexLocationMapping ReadFromFile(string _fileName)
    {
      try
      {
        using (StreamReader sr = new StreamReader(SettingsFileName(_fileName)))
        {
          return xs.Deserialize(sr) as PlexLocationMapping;
        }
      }
      catch
      {
        return new PlexLocationMapping();
      }
    }

    public static string SettingsFileName(string _fileName)
    {
      return Path.Combine(PlaylistSettings.SettingsFolder, _fileName);
    }
  }
}
