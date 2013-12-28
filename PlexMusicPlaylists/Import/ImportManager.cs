using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PlexMusicPlaylists.PlexMediaServer;
using System.Diagnostics;
using System.Xml.Linq;
using System.Windows.Forms;

namespace PlexMusicPlaylists.Import
{
  public class ImportManager
  {
    public enum ImportFormatType
    {
      M3U
      ,WPL
      //,XSPF
    }
    public delegate void ProgressEventHandler(string _message, bool _mainMessage);
    public event ProgressEventHandler OnProgress;
    public PlaylistManager PlaylistManager { get; set; }
    public PMSServer PMSServer { get; set; }
    private List<MainSection> m_mainSections = new List<MainSection>();
    private List<SectionLocation> m_sectionLocations = new List<SectionLocation>();

    public List<SectionLocation> SectionLocations { get { return m_sectionLocations; } }

    private ImportFile m_importFile = null;
    public ImportFile ImportFile { get { return m_importFile; } }
    private string m_newKey = "";
    private StreamWriter m_logWriter = null;
    private bool LoggingActive { get { return m_logWriter != null; } }
    public bool AutoShownLocationMappingOnEmpty { get; set; }
    public ImportFormatType ImportFormat { get; set; }

    public ImportManager()
    {
      ImportFormat = ImportFormatType.M3U;
    }

    public string ImportFileFilter
    {
      get
      {
        switch (ImportFormat)
        {
          case ImportFormatType.M3U:
            return "m3u files|*.m3u";
          case ImportFormatType.WPL:
            return "Windows Media Player Playlist|*.wpl";
          //case ImportFormatType.XSPF:
          //  return "XML Shareable Playlist Format|*.xspf";
        }
        return "All files|*.*";
      }
    }
    public string ImportFileTitle
    {
      get
      {
        if (m_importFile != null && !String.IsNullOrEmpty(m_importFile.Title))
        {
          return m_importFile.Title;
        }
        return null;
      }
    }

    public void reset()
    {
      m_mainSections.Clear();
      m_sectionLocations.Clear();
    }

    public void initImportFileFormat(ComboBox _comboBox)
    {
      _comboBox.Items.Clear();
      _comboBox.Items.Add("m3u");
      _comboBox.Items.Add("Windows Media Player Playlist");
      _comboBox.SelectedIndex = ImportFormat == ImportFormatType.M3U ? 0 : 1;
      _comboBox.SelectedIndexChanged += new EventHandler(importFileFormat_SelectedIndexChanged);
    }

    void importFileFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
      ComboBox comboBox = sender as ComboBox;

      if (comboBox != null)
      {
        switch (comboBox.SelectedIndex)
        {
          case 0: 
            ImportFormat = ImportFormatType.M3U;
            break;
          case 1:
            ImportFormat = ImportFormatType.WPL;
            break;
        }
      }
    }

    public void addMainSection(MainSection _mainSection)
    {
      if (_mainSection != null && m_mainSections.FirstOrDefault(mainSection => mainSection.Key.Equals(_mainSection.Key, StringComparison.OrdinalIgnoreCase)) == null)
      {
        _mainSection.Owner = PMSServer;
        m_mainSections.Add(_mainSection);
        _mainSection.loadFromCache(false, false, null);

        foreach (string location in _mainSection.Locations)
        {
          if (m_sectionLocations.FirstOrDefault(section => section.PlexLocation.Equals(location, StringComparison.OrdinalIgnoreCase)) == null)
          {
            m_sectionLocations.Add(new SectionLocation(_mainSection) { PlexLocation = location });
          }
        }
      }
    }

    protected void progressMessage(string _message, bool _mainMessage)
    {
      _message = _message ?? "";
      if (OnProgress != null)
      {
        OnProgress(_message, _mainMessage);
      }
    }

    protected string locationMappingFileName()
    {
      return String.Format("Server_{0} location mapping.xml", PMSServer.Name);
    }

    protected void copyMappedLocations(List<SectionLocation> _fromLocations, List<SectionLocation> _toLocations, bool _addIfMissing)
    {
      if (_fromLocations != null && _toLocations != null)
      {
        foreach (SectionLocation sectionLocation in _toLocations)
        {
          SectionLocation mappedLocation = _fromLocations.FirstOrDefault(location => location.PlexLocation.Equals(sectionLocation.PlexLocation, StringComparison.OrdinalIgnoreCase));
          if (mappedLocation != null )
          {
            if (PMSServer.DirectorySeparator == PMSBase.FORWARD_SLASH && !String.IsNullOrEmpty(mappedLocation.MappedLocation))
            {
              mappedLocation.MappedLocation = ImportEntry.normalizePath(mappedLocation.MappedLocation, PMSBase.BACKWARD_SLASH, PMSServer.DirectorySeparator);
            }
            sectionLocation.MappedLocation = mappedLocation.MappedLocation ?? "";
          }
        }
        if (_addIfMissing)
        {
          foreach (SectionLocation sectionLocation in _fromLocations)
          {
            if (_toLocations.FirstOrDefault(location => location.PlexLocation.Equals(sectionLocation.PlexLocation, StringComparison.OrdinalIgnoreCase)) == null)
            {
              _toLocations.Add(new SectionLocation(null) { PlexLocation = sectionLocation.PlexLocation });
            }
          }
        }
      }
    }

    protected void loadMappedLocations()
    {
      PlexLocationMapping plexLocationMapping = PlexLocationMapping.ReadFromFile(locationMappingFileName());

      PMSServer.DirectorySeparator = plexLocationMapping.DirectorySeparator;
      AutoShownLocationMappingOnEmpty = !plexLocationMapping.ShownOnEmptyMappedlocation;
      copyMappedLocations(plexLocationMapping.Locations, m_sectionLocations, false);
    }

    protected void saveMappedLocations()
    {
      PlexLocationMapping plexLocationMapping = PlexLocationMapping.ReadFromFile(locationMappingFileName());

      plexLocationMapping.DirectorySeparator = PMSServer.DirectorySeparator;
      plexLocationMapping.ShownOnEmptyMappedlocation = !AutoShownLocationMappingOnEmpty;
      copyMappedLocations(m_sectionLocations, plexLocationMapping.Locations, true);

      plexLocationMapping.Save(locationMappingFileName());
    }

    SectionLocation findSectionLocation(ImportEntry _importEntry, out bool _usePlexLocation)
    {
      _usePlexLocation = true;
      if (_importEntry != null)
      {
        // 1. find section locations with corresponding PlexLocation
        var locations =
          from location in m_sectionLocations.OrderByDescending(loc => loc.PlexLocation.Length)
          where !String.IsNullOrEmpty(location.PlexLocation) && _importEntry.FullFileName.StartsWith(location.PlexLocation, StringComparison.OrdinalIgnoreCase)
          select location;
        SectionLocation sectionLocation = locations.FirstOrDefault();

        if (sectionLocation == null)
        {
          _usePlexLocation = false;
          // 2. find section locations with corresponding MappedLocation
          locations =
            from location in m_sectionLocations.OrderByDescending(loc => loc.MappedLocation.Length)
            where !String.IsNullOrEmpty(location.MappedLocation) && _importEntry.FullFileName.StartsWith(location.MappedLocation, StringComparison.OrdinalIgnoreCase)
            select location;
          
          sectionLocation = locations.FirstOrDefault();
        }

        return sectionLocation;
      }
      return null;
    }

    public bool matchOnTitle(bool _checkAll)
    {
      return matchOnTitle(null, _checkAll);
    }
    public bool matchOnTitle(ImportEntry _singleEntry)
    {
      return matchOnTitle(_singleEntry, true);
    }

    public bool matchOnTitle(ImportEntry _singleEntry, bool _checkAll)
    {
      bool anyMatched = false;

      var entries =
        from entry in m_importFile.Entries
        where ((_checkAll || !entry.Matched) && !String.IsNullOrEmpty(entry.Title)) && (_singleEntry == null || entry == _singleEntry)
        select entry;

      string baseMessage = "Find matches based on title.... ";
      progressMessage(baseMessage, true);
      foreach (ImportEntry entry in entries)
      {
        progressMessage(entry.Title, false);
        entry.resetMatches(false);
        foreach (MainSection mainSection in m_mainSections)
        {
          SearchSection searchSection = mainSection.TitleSearchSection();
          LibrarySection librarySection = (searchSection != null) ? searchSection.createFromSearch(entry.Title) : null;
          if (librarySection != null)
          {
            librarySection.IsMusic = mainSection.IsMusic;
            var tracks =
              from track in PMSServer.getSectionElements(librarySection, false)
              select track;

            foreach (XElement trackElement in tracks)
            {
              if (entry.AddMatch(new MatchEntry(mainSection, trackElement)))
              {
                anyMatched = true;
              }
            }
          }
        }
        entry.CheckBestMatch();
      }
      progressMessage("", true);
      return anyMatched;
    }


    protected void startLogging(string _fileName)
    {      
      m_logWriter = new StreamWriter(Path.Combine(PlaylistSettings.LogFolder, _fileName));
    }

    protected void stopLogging()
    {
      if (m_logWriter != null)
      {
        m_logWriter.Dispose();
        m_logWriter = null;
      }
    }

    protected void add2Log(string _message, string _label)
    {
      if (LoggingActive)
      {
        _message = _message ?? "";
        m_logWriter.WriteLine(String.Format("{0}: [{1}] '{2}'", DateTime.Now, _label, _message));
      }
    }

    public bool matchOnFileInFolder(bool _checkAll)
    {
      bool anyMatched = false;

      var entries =
        from entry in m_importFile.Entries
        where (_checkAll || !entry.Matched) && entry.FolderSection != null
        select entry;

      try
      {
        string logFileName = String.Format("MatchOnFileInFolder_{0}.log", m_importFile != null ? Path.GetFileNameWithoutExtension(m_importFile.FileName) : "");
        startLogging(logFileName);
        if (m_importFile != null)
        {
          add2Log(m_importFile.FileName, "FILENAME");
        }
        string baseMessage = "Find matches based on (relative) folder.... ";
        add2Log(baseMessage, "START");
        progressMessage(baseMessage, true);
        foreach (ImportEntry entry in entries)
        {
          entry.resetMatches(true);
          progressMessage(entry.Title, false);
          add2Log(entry.Title, "Title");
          add2Log(entry.FullPlexFileName, "FullPlexFileName");
          add2Log(entry.FolderSection.SectionUrl, "FolderURL");
          var tracks =
            from track in entry.FolderSection.tracks(PMSServer)
            where track.Elements().FirstOrDefault(
              media => media.Elements().FirstOrDefault(
                 part => part.Attribute("file").Value.Equals(entry.FullPlexFileName, StringComparison.OrdinalIgnoreCase)) != null) != null
            select track;

          XElement trackElement = tracks.FirstOrDefault();
          if (trackElement != null)
          {
            if (entry.AddMatch(new MatchEntry(entry.MainSection, trackElement) { MatchOnFolder = true }))
            {
              anyMatched = true;
            }
            entry.TrackType = entry.FolderSection.TrackType;
            anyMatched = true;
          }
        }
        progressMessage("", true);
      }
      catch (Exception ex)
      {
        add2Log(string.Format("{0}. Stacktrace={1}", ex.Message, ex.StackTrace), "ERROR");
      }
      stopLogging();
      return anyMatched;
    }

    public void updateSectionLocations()
    {
      if (m_importFile != null)
      {
        foreach (ImportEntry entry in m_importFile.Entries)
        {
          bool usePlexLocation;
          SectionLocation sectionLocation = findSectionLocation(entry, out usePlexLocation);
          if (sectionLocation != null && sectionLocation.Owner() != null)
          {
            entry.setSectionLocation(sectionLocation, usePlexLocation, progressMessage);
            if (entry.FolderSection != null)
            {
              Debug.WriteLine(String.Format("==> FOLDER FOUND[{0}]: {1} - {2}", entry.FullFileName, entry.FolderSection.Key, entry.FolderSection.Title));
            }
            else
            {
              Debug.WriteLine(String.Format("==> folderSection NOT FOUND[{0}]", entry.FullFileName));
            }
          }
          else
          {
            entry.FolderSection = null;
            Debug.WriteLine(String.Format("==> Mapped SectionLocation NOT FOUND[{0}]", entry.FullFileName));
          }
        }
      }
      progressMessage("", true);
    }
    
    public bool LoadImportFile(string _fileName)
    {
      m_importFile = null;
      if (File.Exists(_fileName))
      {
        progressMessage("Loading file....", true);
        switch (ImportFormat)
        {
          case ImportFormatType.M3U:
            m_importFile = ImportFileM3U.loadM3UFile(_fileName);
            break;
          case ImportFormatType.WPL:
            m_importFile = ImportFileWPL.loadWPLFile(_fileName);
            break;
          //case ImportFormatType.XSPF:
            // TODO
          //  break;
        }
        if (m_importFile != null)
        {
          updateSectionLocations();
          return true;
        }
      }
      return false;
    }

    public bool showLocationMapping()
    {
      LocationMappingForm locationMappingForm = new LocationMappingForm();
      locationMappingForm.setImportManager(this);
      locationMappingForm.ShowDialog();
      if (locationMappingForm.MappingChanged)
      {
        this.updateSectionLocations();
        return true;
      }
      return false;
    }

    public string showImport()
    {
      m_newKey = "";
      m_importFile = null;
      loadMappedLocations();
      OnProgress = null;
      ImportForm importForm = new ImportForm();
      importForm.setImportManager(this);
      if (importForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
      }
      saveMappedLocations();
      return m_newKey;
    }

    public bool createPlaylist(string _title, string _description)
    {
      if (!String.IsNullOrEmpty(_title))
      {
        m_newKey = PlaylistManager.createNewPlaylist(_title.Trim(), _description.Trim(), PlaylistManager.PLTypes.Simple);
        if (!String.IsNullOrEmpty(m_newKey))
        {
          List<Track> currentList = PlaylistManager.currentTrackList;
          PlaylistManager.currentTrackList = new List<Track>();
          var entries =
            from entry in m_importFile.Entries
            where entry.Matched
            select entry;

          foreach (ImportEntry importEntry in entries)
          {
            PlaylistManager.addTrack(m_newKey, importEntry.Key, importEntry.TrackType);
          }
          PlaylistManager.currentTrackList = currentList;
          m_importFile = null;
          return true;
        }
      }
      return false;
    }
  }
}
