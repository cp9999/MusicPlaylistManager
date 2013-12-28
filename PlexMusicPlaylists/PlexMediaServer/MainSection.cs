using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using PlexMusicPlaylists.Import;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class MainSection : LibrarySection
  {
    protected const string SEARCH = "search";
    protected const string PROMPT = "prompt";
    protected const string PATH = "path";
    protected const string FOLDER = "folder";
    private List<SearchSection> m_searchSections = new List<SearchSection>();
    private List<string> m_locations = new List<string>();
    private List<LibrarySection> m_folders = new List<LibrarySection>();

    public List<SearchSection> searchSections { get { return m_searchSections; } }
    public List<string> Locations { get { return m_locations; } }
    public List<LibrarySection> folders { get { return m_folders; } }

    public PMSServer Owner { get; set; }

    public bool canBeSearched
    {
      get { return m_searchSections != null ? m_searchSections.Count() > 0 : false; }
    }

    public void addSearchSections()
    {
      m_searchSections.Clear();

      XElement sectionElements = Utils.elementFromURL(SectionUrl);

      var elements =
        from element in sectionElements.Elements(PMSBase.DIRECTORY)
        where element.Attribute(SEARCH) != null
        select element;
      foreach (XElement searchSection in elements)
      {
        m_searchSections.Add(new SearchSection(this)
        {
          Search = PMSBase.attributeValue(searchSection, PMSBase.KEY),
          Prompt = PMSBase.attributeValue(searchSection, PROMPT),
          Title = PMSBase.attributeValue(searchSection, PMSBase.TITLE)
        });
      }
    }

    public SearchSection TitleSearchSection()
    {
      var sections =
        from section in searchSections
        where section.Key.Contains("?type=10")
        select section;
      return sections.FirstOrDefault();
    }

    public void addLocations(XElement _sectionElement)
    {
      if (_sectionElement != null)
      {
        var locations =
          from location in _sectionElement.Elements("Location")
          select location;

        foreach (XElement locationSection in locations)
        {
          m_locations.Add(PMSBase.attributeValue(locationSection, PATH));
        }
      }
    }

    protected void addFolder(string _url, string _baseUrl, string _parentTitle, ImportManager.ProgressEventHandler _progressMessage)
    {
      if (!String.IsNullOrEmpty(_url))
      {
        if (_progressMessage != null)
        {
          _progressMessage(_parentTitle, false);
        }
        XElement folderElements = Utils.elementFromURL(_url);
        var elements =
          from element in folderElements.Elements(PMSBase.DIRECTORY)
          select element;
        foreach (XElement folderSection in elements)
        {
          LibrarySection librarySection = new LibrarySection()
          {
            Key = PMSBase.attributeValue(folderSection, PMSBase.KEY),
            Title = _parentTitle + PMSBase.attributeValue(folderSection, PMSBase.TITLE),
            IsMusic = this.IsMusic
          };
          librarySection.SectionUrl = Utils.getSectionUrl(librarySection.Key, _url, _baseUrl, "");
          m_folders.Add(librarySection);
          Debug.WriteLine(String.Format("{0} - {1}", librarySection.Key, librarySection.Title));
          addFolder(Utils.getSectionUrl(librarySection.Key, _url, _baseUrl, ""), _baseUrl, String.Format("{0}{1}", librarySection.Title, PMSServer.DirectorySeparator), _progressMessage);
        }
      }
    }

    public void loadFolders(string _baseUrl, ImportManager.ProgressEventHandler _progressMessage)
    {
      if (m_folders.Count == 0)
      {
        m_folders.Clear();
        string sectionUrl = String.Format("{0}{1}", SectionUrl, FOLDER);
        LibrarySection librarySection = new LibrarySection()
        {
          Key = sectionUrl.Remove(0, _baseUrl.Length),
          Title = "",
          IsMusic = this.IsMusic
        };
        librarySection.SectionUrl = Utils.getSectionUrl(librarySection.Key, sectionUrl, _baseUrl, "");
        m_folders.Add(librarySection);
        addFolder(String.Format("{0}/", sectionUrl), _baseUrl, "", _progressMessage);
        saveToCache();
      }
    }

    public void saveToCache()
    {
      try
      {
        Type objectType = m_folders.GetType();
        XmlSerializer xmlSerializer = new XmlSerializer(objectType);
        using (StreamWriter sw = new StreamWriter(cacheFileName()))
        {
          xmlSerializer.Serialize(sw, m_folders);
        }
      }
      catch
      {
      }
    }

    public void loadFromCache(bool _forceReload, bool _loadFromServer, ImportManager.ProgressEventHandler _progressMessage)
    {
      if (_forceReload)
      {
        m_folders.Clear();
      }
      else if (File.Exists(cacheFileName()))
      {
        try
        {
          Type objectType = m_folders.GetType();
          XmlSerializer xmlSerializer = new XmlSerializer(objectType);
          using (StreamReader sr = new StreamReader(cacheFileName()))
          {
            m_folders = xmlSerializer.Deserialize(sr) as List<LibrarySection>;
          }
        }
        catch
        {
        }
      }
      if (m_folders.Count == 0 && _loadFromServer)
      {
        if (_progressMessage != null)
        {
          _progressMessage(String.Format("PlexMediaServer: loading folders from section {0}.....", Title), true);
        }
        loadFolders(Owner.baseUrl, _progressMessage);
      }
    }

    protected string cacheFileName()
    {
      return Path.Combine(PlaylistSettings.SettingsFolder, String.Format("Server_{0}_Section_{1}.cache", Owner.Name, Title));
    }


  }
}
