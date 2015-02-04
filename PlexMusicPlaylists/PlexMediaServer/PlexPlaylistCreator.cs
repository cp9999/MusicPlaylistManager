using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class PlexPlaylistCreator
  {
    public delegate void LogMessageEventHandler(string _message);
    public event LogMessageEventHandler OnLogMessage;
    public const string PLEX_DATABASE_NAME = "com.plexapp.plugins.library.db";
    private const string METADATA_GUID = "com.plexapp.agents.none://";
    private const int METADATA_TYPE_PLAYLIST = 15;
    private const float MINIMUM_ORDER_STEPSIZE = 10.0F;
    private const string WORK_USERLIST_FILENAME = "Accounts.xml";
    private const string WORK_PLAYLIST_FILENAME = "AllPlaylists.xml";
    private const string WORK_PLAYLIST_SINGLEFILENAME = "{0}_Playlist.xml";
    private const string WORK_PLAYLIST_SINGLEFILENAMESQL = "{0}_{1}_Playlist.sql";
    private const string SQL_PREFIX_NEW = "NEW";
    private const string USER_ADMINISTRATOR_NAME = "Administrator";
    private const int USER_ADMINISTRATOR_ID = 1;

    public String PlexDatabaseName { get; set; }
    public float  OrderStepSize { get; set; }
    private bool m_validDatabase = false;
    private string m_validatedDatabaseName = "";
    private List<PLUser> m_userList = null;
    private List<Playlist> m_playlists = null;
    private PLUser m_currentUser = null;
    public PLUser currentUser { get { return m_currentUser; } }

    public PlexPlaylistCreator()
    {
      OrderStepSize = 1000;
      PlexDatabaseName = PlaylistSettings.theSettings().PlaylistDB;
    }

    public bool ValidDatabase
    {
      get
      {
        if (!String.IsNullOrEmpty(PlexDatabaseName))
        {
          if (!m_validatedDatabaseName.Equals(PlexDatabaseName, StringComparison.OrdinalIgnoreCase))
          {
            m_validatedDatabaseName = PlexDatabaseName;
            m_validDatabase = false;
            if (System.IO.File.Exists(PlexDatabaseName))
            {
              SQLiteConnection dbConnection = openDatabase();
              if (dbConnection != null)
              {
                m_validDatabase = true;
                closeDatabase(ref dbConnection);
              }
            }
          }
          return m_validDatabase;
        }
        return false;
      }
    }

    #region user handling
    public List<PLUser> userList()
    {
      if (m_userList == null)
      {
        this.loadUsers();
      }
      return m_userList;
    }
    public bool setCurrentUser(string _newUser)
    {
      _newUser = _newUser.Trim();
      if (!String.IsNullOrEmpty(_newUser) && !_newUser.Equals(PLUser.USER_UNKNOWN, StringComparison.OrdinalIgnoreCase) && userExist(_newUser))
      {
        if (m_currentUser == null || !_newUser.Equals(currentUser.Name, StringComparison.OrdinalIgnoreCase))
        {
          m_currentUser = m_userList.FirstOrDefault(u => u.Name.Equals(_newUser, StringComparison.OrdinalIgnoreCase));
        }
        return true;
      }
      m_currentUser = m_currentUser ?? new PLUser() { Name = PLUser.USER_UNKNOWN };
      return false;
    }
    #endregion

    #region playlist handling
    public List<Playlist> allPlaylists()
    {
      if (m_playlists == null)
      {
        this.loadPlaylist();
      }
      return m_playlists;
    }
    public List<Track> singlePlaylist(Playlist _playlist, out int _totalDuration)
    {
      return this.loadSinglePlaylist(_playlist, out _totalDuration);
    }
    public string createNewPlaylist(string _title, string _description, PlaylistManager.PLTypes _pltype)
    {
      if (m_playlists != null)
      {
        Guid newGuid = Guid.NewGuid();
        Playlist newPlaylist = new Playlist()
        {
          Id = 0,
          UniqueId = newGuid,
          Key = newGuid.ToString("D"),
          Title = _title,
          Description = _description,
          Duration = 0,
        };
        m_playlists.Add(newPlaylist);
        this.savePlaylists();
        return newPlaylist.Key;
      }
      return "";  
    }
    public string addPlaylist(Playlist _playlist, List<Track> _trackList)
    {
      if (m_playlists != null && _playlist != null && _trackList != null)
      {
        Guid newGuid = Guid.NewGuid();
        Playlist newPlaylist = new Playlist()
        {
          Id = 0,
          UniqueId = newGuid,
          Key = newGuid.ToString("D"),
          Title = _playlist.Title,
          Description = _playlist.Description,
          Duration = _playlist.Duration,
        };
        m_playlists.Add(newPlaylist);
        int totalDuration = 0;
        List<Track> newTrackList = new List<Track>();
        foreach (Track track in _trackList)
        {
          // Cleanup the key
          int metadataId = this.idFromKey(track.Key);
          if (metadataId != 0)
          {
            Track newTrack = new Track(newTrackList)
            {
              Id = 0,
              Key = metadataId.ToString(),
              Title = track.Title,
              Duration = track.Duration,
              Order = 0,
              TrackType = track.TrackType
            };
            totalDuration += newTrack.Duration;
            newTrackList.Add(newTrack);
          }
        }
        this.saveSinglePlaylist(newPlaylist, newTrackList, true, true);
        this.savePlaylists();
        return newPlaylist.Key;
      }
      return "";
    }
    public string deletePlaylist(string _key)
    {
      MessageBox.Show("Not supported yet");
      return "";  // TODO create new playlist
    }
    public string renamePlaylist(string _key, string _newName)
    {
      if (!String.IsNullOrEmpty(_newName) && !String.IsNullOrEmpty(_key))
      {
        Playlist playlist = getPlaylist(_key);
        if (playlist != null)
        {
          playlist.Title = _newName;
          this.savePlaylists();
        }
      }
      return "";  
    }
    public void generatePlaylistSql(Playlist _playlist = null)
    {
      if (m_playlists != null)
      {
        int totalDuration;        
        foreach (Playlist playlist in m_playlists.Where(p => (_playlist != null ) ? p.UniqueId.Equals(_playlist.UniqueId) : true) )
        {
          this.saveSinglePlaylistToDatabase(playlist, this.loadSinglePlaylist(playlist, out totalDuration));
        }
        this.savePlaylists();
      }
    }
    #endregion

    #region track handling
    public string addTrack(List<Track> _trackList, string _playlistKey, string _key, int _atPosition)
    {
      // _atPosition 0 = Append
      Playlist playlist = this.getPlaylist(_playlistKey);
      if (_trackList != null && playlist != null && !String.IsNullOrEmpty(_key))
      {
        int metadata_id = idFromKey(_key);

        Metadata_Media_Item metadataItem = loadMetadataItemFromDatabase(metadata_id);
        if (metadataItem != null)
        {
          Track track = (new Track(_trackList)
          {
            Id = 0,   // Play queue generator id
            Key = metadataItem.Id.ToString(),
            Title = metadataItem.Title,
            Duration = (metadataItem.Duration ?? 0) / 1000,
            TrackType = metadataItem.MetadataType(),
            Artist = metadataItem.Artist,
            Album = metadataItem.Album
          });
          bool normalizeOrder = false;
          if (_atPosition >= 0 && _atPosition <= _trackList.Count())
          {
            _atPosition--;
            float targetOrder = 0;
            if (_atPosition == -1)
            {
              targetOrder = (_trackList.Count() > 0) ? _trackList.Last().Order + OrderStepSize : OrderStepSize;
              _trackList.Add(track);
            }
            else
            {
              targetOrder = ((_atPosition > 0) ? _trackList.ElementAt(_atPosition - 1).Order : 0) + MINIMUM_ORDER_STEPSIZE;
              if (targetOrder < _trackList.ElementAt(_atPosition).Order)
                track.Order = targetOrder;
              else
                normalizeOrder = true;
              _trackList.Insert(_atPosition, track);
            }
          }
          else
          {
            track.Order = (_trackList.Count() > 0 ? _trackList.LastOrDefault().Order : 0) + OrderStepSize;
            _trackList.Add(track);
          }
          this.saveSinglePlaylist(playlist, _trackList, normalizeOrder);
          return String.Format("{0}", _key); 
        }
      }
      return "";
    }
    public string removeTrack(List<Track> _trackList, string _playlistKey, string _key)
    {
      Playlist playlist = this.getPlaylist(_playlistKey);
      if (_trackList != null && playlist != null && !String.IsNullOrEmpty(_key))
      {
        int metadata_id = idFromKey(_key);
        Track track = _trackList.FirstOrDefault(t => t.Key.Equals(_key, StringComparison.OrdinalIgnoreCase));
        if (track != null && _trackList.Remove(track))
        {
          // Track removed
          this.saveSinglePlaylist(playlist, _trackList);
          return String.Format("Track removed from playlist {0}", _key);
        }
      }
      return "";
    }
    public string moveTrack(List<Track> _trackList, string _playlistKey, string _key, int _toPosition)
    {
      Playlist playlist = this.getPlaylist(_playlistKey);
      if (_trackList != null && playlist != null && !String.IsNullOrEmpty(_key) && _toPosition > 0 && _toPosition <= _trackList.Count())
      {
        int metadata_id = idFromKey(_key);
        Track track = _trackList.FirstOrDefault(t => t.Key.Equals(_key, StringComparison.OrdinalIgnoreCase));
        if (track != null)
        {
          int atPosition = _trackList.IndexOf(track);
          if (atPosition != _toPosition - 1)
          {
            int newPosition = (atPosition < _toPosition) ? _toPosition - 2 : _toPosition - 1;
            if (_trackList.Remove(track))
            {
              _trackList.Insert(_toPosition - 1, track);
              this.saveSinglePlaylist(playlist, _trackList);
              return String.Format("Track moved to position {0}", _toPosition); 
            }
          }
        }
      }
      return "";
    }
    #endregion

    #region helpers
    private int idFromKey(string _key)
    {
      try
      {
        if (!String.IsNullOrEmpty(_key))
        {
          int startPos = _key.LastIndexOf("/") + 1;
          return Convert.ToInt32(startPos > 0 ? _key.Substring(startPos, _key.Length - startPos) : _key);
        }
      }
      catch
      {
      }
      return 0;
    }
    private void normalizeOrder(List<Track> _trackList)
    {
      if (_trackList != null)
      {        
        OrderStepSize = Math.Max(OrderStepSize, MINIMUM_ORDER_STEPSIZE);
        float order = 0;
        _trackList.ForEach(t => t.Order = order += OrderStepSize);
      }
    }
    private void resetModified(List<Track> _trackList)
    {
      if (_trackList != null)
      {
        _trackList.ForEach(t => t.Modified = false);
      }
    }
    private bool userExist(string _user)
    {
      return m_userList != null && m_userList.Find(user => user.Name.Equals(_user.Trim(), StringComparison.OrdinalIgnoreCase)) != null;
    }
    private void logMessage(string _message)
    {
      if (!String.IsNullOrEmpty(_message) && OnLogMessage != null)
      {
        OnLogMessage(_message);
      }
    }
    #endregion

    #region local storage
    private string userlistFileName()
    {
      return Path.Combine(PlaylistSettings.PlaylistFolder, WORK_USERLIST_FILENAME);
    }
    private string playlistAllFileName()
    {
      return Path.Combine(PlaylistSettings.PlaylistFolder, WORK_PLAYLIST_FILENAME);
    }
    private string playlistSingleFileName(Playlist _playlist)
    {
      return Path.Combine(PlaylistSettings.PlaylistFolder, String.Format(WORK_PLAYLIST_SINGLEFILENAME, _playlist.UniqueId));
    }
    private string playlistSingleFileNameSql(Playlist _playlist, DateTime _dateTimeStamp)
    {
      return Path.Combine(PlaylistSettings.SqlPlaylistFolder, String.Format(WORK_PLAYLIST_SINGLEFILENAMESQL, _playlist.Id == 0 ? SQL_PREFIX_NEW : _dateTimeStamp.ToString("yyyy_MM_dd__HH_mm_ss"), _playlist.UniqueId));
    }
    private Playlist getPlaylist(string _playlistKey)
    {
      if (m_playlists != null)
      {
        this.allPlaylists();
      }
      return (m_playlists != null) ? m_playlists.FirstOrDefault(p => p.Key.Equals(_playlistKey, StringComparison.OrdinalIgnoreCase)) : null;
    }
    private void loadUsers()
    {
      // Always take Users from database if it is valid
      if (ValidDatabase)
      {
        m_userList = this.loadUsersFromDatabase();
        this.saveUsers();
      }
      if (m_userList == null && File.Exists(userlistFileName()))
      {
        try
        {
          Type objectType = typeof(List<PLUser>);
          XmlSerializer xmlSerializer = new XmlSerializer(objectType);
          using (StreamReader sr = new StreamReader(userlistFileName()))
          {
            m_userList = xmlSerializer.Deserialize(sr) as List<PLUser>;
          }
        }
        catch
        {
        }
      }
      if (m_userList == null)
      {
        m_userList = new List<PLUser>();
        // Create a default user for the Administrator
        m_userList.Add(new PLUser() { Id = USER_ADMINISTRATOR_ID, Name = USER_ADMINISTRATOR_NAME, Title = USER_ADMINISTRATOR_NAME });
      }
      m_currentUser = m_userList.FirstOrDefault(u => u.Name.Equals(USER_ADMINISTRATOR_NAME)) ?? m_userList.FirstOrDefault();
    }
    private void saveUsers()
    {
      if (m_userList != null)
      {
        try
        {
          Type objectType = m_userList.GetType();
          XmlSerializer xmlSerializer = new XmlSerializer(objectType);
          using (StreamWriter sw = new StreamWriter(userlistFileName()))
          {
            xmlSerializer.Serialize(sw, m_userList);
          }
        }
        catch
        {
        }
      }
    }
    private void loadPlaylist()
    {
      bool savePlaylists = false;
      bool loadedFromDb = false;
      if (File.Exists(playlistAllFileName()))
      {
        try
        {
          Type objectType = typeof(List<Playlist>);
          XmlSerializer xmlSerializer = new XmlSerializer(objectType);
          using (StreamReader sr = new StreamReader(playlistAllFileName()))
          {
            m_playlists = xmlSerializer.Deserialize(sr) as List<Playlist>;
          }
        }
        catch
        {
        }
      }
      if (m_playlists == null && ValidDatabase)
      {
        m_playlists = this.loadPlaylistsFromDatabase();
        loadedFromDb = true;
        savePlaylists = true;
      }
      if (m_playlists != null)
      {
        foreach (Playlist pl in m_playlists)
        {
          if (pl.UniqueId.Equals(Guid.Empty))
          {
            savePlaylists = true;
            pl.UniqueId = Guid.NewGuid();
          }
        }
        if (!loadedFromDb)
        {
          this.updatePlaylists(false);
          savePlaylists = !this.ValidDatabase;
        }
        if (savePlaylists)
        {
          this.savePlaylists();
        }
      }
      else
      {
        m_playlists = new List<Playlist>();
      }
    }
    private void savePlaylists()
    {
      if (m_playlists != null)
      {
        try
        {
          Type objectType = m_playlists.GetType();
          XmlSerializer xmlSerializer = new XmlSerializer(objectType);
          using (StreamWriter sw = new StreamWriter(playlistAllFileName()))
          {
            xmlSerializer.Serialize(sw, m_playlists);
          }
        }
        catch
        {
        }
      }
    }
    private void updatePlaylists(bool _revertExisting)
    {
      // For all playlists that are in the database: revert to the database version
      if (m_playlists != null && this.ValidDatabase)
      {
        int dbTotalDuration;
        List<Playlist> dbPlaylists = this.loadPlaylistsFromDatabase();
        // For all playlist that do not exist in the database: Clear the Id         
        foreach (Playlist pl in m_playlists)
        {
          Playlist plDb = pl.Id != 0 ? dbPlaylists.FirstOrDefault(p => p.Id == pl.Id) : null;
          plDb = plDb ?? dbPlaylists.FirstOrDefault(p => p.UniqueId.ToString("D").Equals(pl.UniqueId.ToString("D"), StringComparison.OrdinalIgnoreCase));
          if (plDb == null)
          {
            // Playlist doesn't exist in the database
            pl.Id = 0;
            pl.Key = pl.UniqueId.ToString("D");
            List<Track> trackList = this.loadSinglePlaylist(pl, out dbTotalDuration, false);
            trackList.ForEach(t => t.Id = 0);
            this.loadMissingArtists(trackList);
            this.saveSinglePlaylist(pl, trackList, false, true);
          }
          else 
          {
            if (_revertExisting)
            {
              // Playlist does exist in the database: revert
              pl.Id = plDb.Id;
              pl.Key = plDb.Id.ToString();
              pl.Title = plDb.Title;
              pl.Description = plDb.Description;
              pl.Duration = plDb.Duration;
              // Load the tracks:
              this.saveSinglePlaylist(pl, this.loadSinglePlaylistFromDatabase(plDb, out dbTotalDuration), false, true);
            }
            else
            {
              if (pl.Id == 0)
              {
                pl.Id = plDb.Id;
                pl.Key = plDb.Id.ToString();
              }
              // Update our 'new' tracks (e.g. Tracks with Id == 0)
              this.updateSinglePlaylist(pl);
            }
            // Remove from the dbPlaylist
            dbPlaylists.Remove(plDb);
          }
        }
        // Add remaing playlists from the database
        foreach (Playlist plDb in dbPlaylists)
        {
          if (plDb.UniqueId.Equals(Guid.Empty))
          {
            plDb.UniqueId = Guid.NewGuid();
          }
          m_playlists.Add(plDb);
          this.saveSinglePlaylist(plDb, this.loadSinglePlaylistFromDatabase(plDb, out dbTotalDuration), false, true);
        }
        this.savePlaylists();
      }
    }
    private void updateSinglePlaylist(Playlist _playlist, List<Track> _trackList = null)
    {
      if (_playlist != null && _playlist.Id != 0)
      {
        bool resetProperty = _trackList != null;
        int totalDuration;
        _trackList = _trackList ?? this.loadSinglePlaylist(_playlist, out totalDuration, false);
        bool playlistUpdated = false;
        List<Track> dbTrackList = this.loadSinglePlaylistFromDatabase(_playlist, out totalDuration);
        // Check entries in the playlist wiht an Id that does not exist anymore in the db
        if (_trackList.Any(t => t.Id != 0) && dbTrackList.Count() > 0)
        {
          var excludedIDs = new HashSet<int>(dbTrackList.Select(track => track.Id));
          // Get a list of all tracks in our tracklist that are not in the database (anymore)
          foreach (Track t in _trackList.Where(t => t.Id != 0 && !excludedIDs.Contains(t.Id)))
          {
            t.Id = 0;
            playlistUpdated = true;
          }
          foreach (Track t in _trackList.Where(t => String.IsNullOrEmpty(t.Artist)))
          {
            Track newdbTrack = dbTrackList.FirstOrDefault(dbt => dbt.Id == t.Id);
            if (newdbTrack != null)
            {
              t.Artist = newdbTrack.Artist;
              t.Album = newdbTrack.Album;
              playlistUpdated = true;
            }
          }
        }
        if (_trackList.Any(t => t.Id == 0))
        {
          // There are 'new' entries in the tracklist
          var excludedIDs = new HashSet<int>(_trackList.Where(t => t.Id != 0).Select(track => track.Id));
          // Get a list of all tracks in the database that are not in our tracklist yet
          List<Track> dbNewTrackList = dbTrackList.Where(t => !excludedIDs.Contains(t.Id)).ToList();
          if (dbNewTrackList.Count() > 0)
          {
            // Now let's try to match our new entries to an entry in the database tracklist
            var newTracks = _trackList.Where(t => t.Id == 0);
            foreach (Track track in newTracks)
            {
              var newdbTracks = dbNewTrackList.Where(dt => dt.Key.Equals(track.Key));
              if (newdbTracks.Count() > 0)
              {
                // get single hit, or Find an exact match based on order
                Track newdbTrack = (newdbTracks.Count() == 1) ? newdbTracks.FirstOrDefault() : newdbTracks.FirstOrDefault(t => t.Order == track.Order);
                if (newdbTrack == null)
                {
                  // Find closest match based on order
                  foreach (Track tDiff in newdbTracks)
                  {
                    if (newdbTrack == null || Math.Abs(tDiff.Order - track.Order) < Math.Abs(newdbTrack.Order - track.Order))
                    {
                      newdbTrack = tDiff;
                    }
                  }
                }
                if (newdbTrack != null)
                {
                  // single hit, use it
                  track.Id = newdbTrack != null ? newdbTrack.Id : 0;
                  track.Artist = newdbTrack.Artist;
                  track.Album = newdbTrack.Album;
                  if (resetProperty)
                  {
                    track.resetPropertyModified();
                  }
                  dbNewTrackList.Remove(newdbTrack);
                  playlistUpdated = true;
                }
              }
            }
          }
        }
        if (this.loadMissingArtists(_trackList))
        {
          playlistUpdated = true;
        }
        if (playlistUpdated)
        {
          this.saveSinglePlaylist(_playlist, _trackList, false, true);
        }
      }
    }
    private bool loadMissingArtists(List<Track> _trackList)
    {
      bool updated = false;
      if (_trackList != null)
      {
        foreach (Track track in _trackList.Where(t => String.IsNullOrEmpty(t.Artist)))
        {
          int metadata_id = this.idFromKey(track.Key);
          if (metadata_id != 0)
          {
            Metadata_Media_Item metadataItem = this.loadMetadataItemFromDatabase(metadata_id);
            if (metadataItem != null)
            {
              track.Artist = metadataItem.Artist;
              track.Album = metadataItem.Album;
              updated = true;
            }
          }
        }
      }
      return updated;
    }
    private void revertSinglePlaylist(Playlist _playlist)
    {
      // For all playlists that are in the database: revert to the database version
      if (_playlist != null && this.ValidDatabase)
      {
        List<Playlist> dbPlaylists = this.loadPlaylistsFromDatabase();
        Playlist plDb = dbPlaylists.FirstOrDefault(p => p.UniqueId.ToString("D").Equals(_playlist.UniqueId.ToString("D"), StringComparison.OrdinalIgnoreCase));
        if (plDb == null)
        {
          // Playlist doesn't exist in the database
          _playlist.Id = 0;
          _playlist.Key = _playlist.UniqueId.ToString("D");
        }
        else
        {
          _playlist.Id = plDb.Id;
          _playlist.Key = plDb.Id.ToString();
          _playlist.Title = plDb.Title;
          _playlist.Description = plDb.Description;
          _playlist.Duration = plDb.Duration;
          // Load the tracks:
          int dbTotalDuration;
          this.saveSinglePlaylist(_playlist, this.loadSinglePlaylistFromDatabase(plDb, out dbTotalDuration), false, true);
        }
        this.savePlaylists();
      }
    }
    private List<Track> loadSinglePlaylist(Playlist _playlist, out int _totalDuration, bool _fromDBIfMissing = true)
    {
      List<Track> playListTracks = null;
      _totalDuration = 0;
      if (File.Exists(playlistSingleFileName(_playlist)))
      {
        try
        {
          Type objectType = typeof(List<Track>); 
          XmlSerializer xmlSerializer = new XmlSerializer(objectType);
          using (StreamReader sr = new StreamReader(playlistSingleFileName(_playlist)))
          {
            playListTracks = xmlSerializer.Deserialize(sr) as List<Track>;
          }
          _totalDuration = 0;
          foreach (Track track in playListTracks)
          {
            track.resetPropertyModified();
            track.setOwnerList(playListTracks);
            _totalDuration += track.Duration;
          }
        }
        catch
        {
        }
      }
      if (playListTracks == null && _fromDBIfMissing && ValidDatabase)
      {
        playListTracks = this.loadSinglePlaylistFromDatabase(_playlist, out _totalDuration);
        this.saveSinglePlaylist(_playlist, playListTracks);
      }
      return playListTracks ?? new List<Track>();
    }
    private void saveSinglePlaylist(Playlist _playlist, List<Track> _playListTracks, bool _normalizeOrder = false, bool _skipSaveAll = false)
    {
      if (_playlist != null && _playListTracks != null && _playListTracks.Count() > 0 )
      {
        try
        {
          if (_normalizeOrder)
          {
            this.normalizeOrder(_playListTracks);
          }
          Type objectType = _playListTracks.GetType();
          XmlSerializer xmlSerializer = new XmlSerializer(objectType);
          using (StreamWriter sw = new StreamWriter(playlistSingleFileName(_playlist)))
          {
            xmlSerializer.Serialize(sw, _playListTracks);
          }
          int totalDuration = _playListTracks.Sum<Track>(t => t.Duration);
          if (!_playlist.Duration.Equals(totalDuration))
          {
            _playlist.Duration = totalDuration;
            if (!_skipSaveAll)
            {
              this.savePlaylists();
            }
          }
        }
        catch
        {
        }
      }
    }
    #endregion

    #region plex database
    private SQLiteConnection openDatabase()
    {
      SQLiteConnection dbConnection = null;
      try
      {
        dbConnection = new SQLiteConnection(String.Format("Data Source={0};Version=3;", PlexDatabaseName));
        dbConnection.Open();
      }
      catch (Exception ex)
      {
        dbConnection = null;
      }
      return dbConnection;
    }
    private void closeDatabase(ref SQLiteConnection _dbConnection)
    {
      if (_dbConnection != null)
      {
        try
        {
          _dbConnection.Close();
        }
        catch (Exception)
        {
        }
        _dbConnection = null;
      }
    }
    private List<PLUser> loadUsersFromDatabase()
    {
      List<PLUser> userlist = new List<PLUser>();
      SQLiteConnection dbConnection = openDatabase();
      if (dbConnection != null)
      {
        try
        {
          var context = new DataContext(dbConnection);

          var items = context.GetTable<Account>();
          foreach (var item in items)
          {
            userlist.Add(new PLUser()
            {
              Id = item.Id,
              Name = item.Name,
              Title = item.Name
            });
          }
        }
        catch (Exception)
        {

        }
        closeDatabase(ref dbConnection);
      }
      return userlist;
    }
    private List<Playlist> loadPlaylistsFromDatabase()
    {
      List<Playlist> playlists = new List<Playlist>();
      SQLiteConnection dbConnection = openDatabase();
      if (dbConnection != null)
      {
        try
        {
          var context = new DataContext(dbConnection);

          var items = context.GetTable<Metadata_Item>().Where(mdi => mdi.Metadata_type == METADATA_TYPE_PLAYLIST);
          foreach (var item in items)
          {
            Guid guid = Guid.NewGuid();
            if (!String.IsNullOrEmpty(item.Guid) && item.Guid.StartsWith(METADATA_GUID))
            {
              // Extract the Guid from the database item 
              if (!Guid.TryParse(item.Guid.Remove(0, METADATA_GUID.Length), out guid))
              {
                guid = Guid.NewGuid();
              }
            }
            playlists.Add(new Playlist()
            {
              Id = item.Id ?? 0,
              Key = item.Id.ToString(),
              Title = item.Title,
              Description = item.Title_sort,
              Duration = item.Duration ?? 0,
              UniqueId = guid
            });
          }
        }
        catch (Exception)
        {

        }
        closeDatabase(ref dbConnection);
      }
      return playlists;
    }
    private List<Track> loadSinglePlaylistFromDatabase(Playlist _playlist, out int _totalDuration)
    {
      List<Track> playlist = new List<Track>();
      _totalDuration = 0;
      if (_playlist != null)
      {
        if (_playlist.Id == 0)
        {

        }

        if (_playlist.Id != 0)
        {
          SQLiteConnection dbConnection = openDatabase();
          if (dbConnection != null)
          {
            try
            {
              int metadata_id = _playlist.Id; // Convert.ToInt32(_playlist.Key);
              var context = new DataContext(dbConnection);
              var items = context.ExecuteQuery<Play_Queue_Item>(Play_Queue_Item.sqlSelect(), new object[] { }).Where(pqi => pqi.Playlist_id == metadata_id).OrderBy(p => p.Order);
              foreach (var item in items)
              {
                Track track = new Track(playlist)
                {
                  Id = item.Id,
                  Key = item.Metadata_item_id.ToString(),
                  Title = item.Title,
                  Duration = item.Duration / 1000,
                  Order = item.Order,
                  TrackType = item.MetadataType(),
                  Artist = item.Artist,
                  Album = item.Album
                };
                playlist.Add(track);
                _totalDuration += track.Duration;
                this.resetModified(playlist);
              }
            }
            catch (Exception)
            {

            }
            closeDatabase(ref dbConnection);
          }
        }
      }
      return playlist;
    }
    private void saveSinglePlaylistToDatabase(Playlist _playlist, List<Track> _playListTracks)
    {
      if (_playlist != null && _playListTracks != null)
      {
        DateTime timeStamp = DateTime.Now;

        string sql = this.sqlSavePlaylist(_playlist, _playListTracks, timeStamp);
        if (!String.IsNullOrEmpty(sql))
        {
          // Save it to the xml to save updated Modified fields
          this.saveSinglePlaylist(_playlist, _playListTracks, false, true);
          if (PlaylistSettings.theSettings().DatabaseCreateSqlFiles)
          {
            // save the file
            string fileName = playlistSingleFileNameSql(_playlist, timeStamp);
            using (StreamWriter sw = new StreamWriter(fileName))
            {
              sw.Write(sql);
            }
            logMessage(String.Format("Playlist '{0}': SQL file created [{1}]", _playlist.Title, fileName));
          }
          if (PlaylistSettings.theSettings().DatabaseDirectUpdate && this.ValidDatabase)
          {
            // Update to the database file
            SQLiteConnection dbConnection = openDatabase();
            if (dbConnection != null)
            {
              bool dbUpdated = false;
              try
              {
                // Save the playlist
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
                dbUpdated = true;
                logMessage(String.Format("Playlist '{0}': Plex database updated [{1}]", _playlist.Title, PlexDatabaseName));
              }
              catch
              {
              }
              closeDatabase(ref dbConnection);
              if (dbUpdated && this.updatePlaylistId(_playlist))
              {
                // Playlist is updated in the database  && we have a valid Id on the playlist: Reload playlist from the database 
                this.updateSinglePlaylist(_playlist, _playListTracks);
              }
            }
          }
        }
      }
    }
    private bool updatePlaylistId(Playlist _playlist)
    {
      // For a new playlist, check if it is already in the database and retrieve the id if so.
      // Return true if the Playlist has an Id.
      if (_playlist != null && _playlist.Id == 0)
      {
        List<Playlist> dbPlaylists = this.loadPlaylistsFromDatabase();
        Playlist plDb = dbPlaylists.FirstOrDefault(p => p.UniqueId.ToString("D").Equals(_playlist.UniqueId.ToString("D"), StringComparison.OrdinalIgnoreCase));
        if (plDb != null)
        {
          _playlist.Id = plDb.Id;
          _playlist.Key = plDb.Id.ToString();
        }
      }
      return _playlist != null && _playlist.Id != 0;
    }

    private Metadata_Media_Item loadMetadataItemFromDatabase(int _metadata_id)
    {
      Metadata_Media_Item metadataItem = null;
      SQLiteConnection dbConnection = openDatabase();
      if (dbConnection != null)
      {
        try
        {
          var context = new DataContext(dbConnection);

          metadataItem = context.ExecuteQuery<Metadata_Media_Item>(Metadata_Media_Item.sqlSelect(), new object[] { }).FirstOrDefault(mmi => mmi.Id.Equals(_metadata_id));
        }
        catch (Exception)
        {

        }
        closeDatabase(ref dbConnection);
      }
      return metadataItem;
    }
    private void loadMetadataItemObjectsFromDatabase(int _metadata_id, int _accountId, out Metadata_Item _metadataItem, out Metadata_Item_Account _metadataItemAccount)
    {
      _metadataItem = null;
      _metadataItemAccount = null;
      SQLiteConnection dbConnection = openDatabase();
      if (dbConnection != null)
      {
        try
        {
          var context = new DataContext(dbConnection);
          var items = context.GetTable<Metadata_Item>().Where(mdi => mdi.Metadata_type == METADATA_TYPE_PLAYLIST && mdi.Id == _metadata_id);
          foreach (Metadata_Item item in items)
          {
            _metadataItem = item;
            break;
          }
          var accountItems = context.GetTable<Metadata_Item_Account>().Where(mia => mia.Account_id == _accountId && mia.Metadata_item_id == _metadata_id);
          foreach (Metadata_Item_Account accountItem in accountItems)
          {
            _metadataItemAccount = accountItem;
            break;
          }
        }
        catch (Exception)
        {

        }
        closeDatabase(ref dbConnection);
      }
    }
    private string sqlSavePlaylist(Playlist _playlist, List<Track> _playListTracks, DateTime _timeStamp, PLUser _account = null)
    {
      StringBuilder sb = new StringBuilder();
      _account = _account ?? currentUser;
      if (_playlist != null && _account != null && !_account.isUnknownUser())
      {
        Metadata_Item dbPlaylist = null;
        Metadata_Item_Account dbItemAccount = null;
        if (_playlist.Id != 0)
        {
          loadMetadataItemObjectsFromDatabase(_playlist.Id, _account.Id, out dbPlaylist, out dbItemAccount);
        }
        bool isNewPlaylist = dbPlaylist == null;
        string dbTitle = _playlist.Title.Replace("'", "''");
        string dbGuid = dbPlaylist == null ? String.Format("{0}{1}", METADATA_GUID, _playlist.UniqueId.ToString("D")) : dbPlaylist.Guid ?? "";

        string timeStamp = _timeStamp.ToString("yyyy-MM-dd HH:mm:ss");
        if (isNewPlaylist)
        {
          // Create a new playlist record in the metadata_items table
          sb.Append("INSERT INTO metadata_items (metadata_type, guid, title, title_sort, [index], absolute_index, added_at, updated_at) ");
          sb.AppendFormat("VALUES ({0}, '{1}', '{2}', '{3}', {4}, {5}, '{6}', '{7}');", METADATA_TYPE_PLAYLIST, dbGuid, dbTitle, dbTitle, 0, 10, timeStamp, timeStamp);
          sb.AppendLine();

        }
        else if (!dbPlaylist.Title.Equals(_playlist.Title))
        {
          // Update a  playlist record in the metadata_items table
          sb.AppendLine(String.Format("UPDATE metadata_items SET title = '{1}', title_sort = '{2}', updated_at = '{3}' WHERE id = {0} ", dbPlaylist.Id, dbTitle, dbTitle, timeStamp));
        }
        string selectWhere = isNewPlaylist
          ? String.Format("metadata_items.metadata_type = {0} and guid = '{1}' ", METADATA_TYPE_PLAYLIST, dbGuid)
          : String.Format("metadata_items.id = {0}", dbPlaylist.Id);
        if (dbItemAccount == null)
        {
          // Create a new record in the metadata_item_accounts table
          sb.Append("INSERT INTO metadata_item_accounts (account_id , metadata_item_id) ");
          if (dbPlaylist != null)
          {
            // Use the Playlist ID if we already have it
            sb.AppendFormat("VALUES ({0}, {1});", _account.Id, dbPlaylist.Id);
          }
          else
          {
            sb.AppendFormat(" SELECT {0}, id FROM metadata_items where {1};", _account.Id, selectWhere);
            sb.AppendLine();
          }
        }
        if (_playListTracks != null)
        {
          bool includeAll = isNewPlaylist || !PlaylistSettings.theSettings().DatabaseModifiedTracksOnly;
          string updatePlaylistId = isNewPlaylist
            ? String.Format("(SELECT id FROM metadata_items WHERE {0})", selectWhere)
            : dbPlaylist.Id.ToString();
          foreach (Track track in _playListTracks.Where(t => t.Modified || includeAll))
          {
            int metadataId = this.idFromKey(track.Key);
            if (metadataId != 0)
            {
              if (track.Id == 0)
              {
                // This is a new track in the playlist
                sb.Append("INSERT INTO play_queue_generators (playlist_id, metadata_item_id, [order], created_at, updated_at) ");
                if (dbPlaylist != null)
                {
                  // Use the Playlist ID if we already have it
                  sb.AppendFormat("VALUES ({0}, {1}, {2}, '{3}', '{4}');", dbPlaylist.Id, metadataId, track.Order, timeStamp, timeStamp);
                }
                else
                {
                  // Use select statement because we don't know the Id of the newly created playlust
                  sb.AppendFormat(" SELECT id, {1}, {2}, '{3}', '{4}' FROM metadata_items where {0};", selectWhere, metadataId, track.Order, timeStamp, timeStamp);
                }
              }
              else 
              {
                // This is an existing track in the playlist
                sb.AppendFormat("UPDATE play_queue_generators SET playlist_id = {1}, metadata_item_id = {2}, [order] = {3}, updated_at = '{4}' WHERE id = {0};", track.Id, updatePlaylistId, metadataId, track.Order, timeStamp);
              }
              sb.AppendLine();
              track.Modified = false;
            }
          }
        }
      }
      return sb.ToString();
    }
    #endregion

    public static string MetadataType(int _metadataType)
    {
      switch (_metadataType)
      {
        case 10: return PMSBase.TYPE_TRACK;
        case 2: return PMSBase.TYPE_SHOW;
        case 1: return PMSBase.TYPE_MOVIE;
        case 4: return PMSBase.TYPE_EPISODE;
      }
      return PMSBase.TYPE_TRACK;
    }

  }

  #region Database classes

  [Table(Name = "accounts")]
  class Account
  {
    [Column(Name = "id")]
    public int Id { get; set; }

    [Column(Name = "name")]
    public string Name { get; set; }
  }

  [Table(Name = "metadata_items")]
  class Metadata_Item
  {
    [Column(IsPrimaryKey=true, IsDbGenerated=true, Name = "id")]
    public int? Id { get; set; }

    [Column(Name = "metadata_type")]
    public int? Metadata_type { get; set; }

    [Column(Name = "title")]
    public string Title { get; set; }

    [Column(Name = "title_sort")]
    public string Title_sort { get; set; }

    [Column(Name = "guid")]
    public string Guid { get; set; }

    [Column(Name = "index")]
    public int Index { get; set; }

    [Column(Name = "absolute_index")]
    public int Absolute_index { get; set; }

    [Column(Name = "media_item_count")]
    public int? Media_item_count { get; set; }

    [Column(Name = "duration")]
    public int? Duration { get; set; }

    //[Column(Name = "added_at")]
    //public DateTime Added_at { get; set; }

    //[Column(Name = "updated_at")]
    //public DateTime Updated_at { get; set; }

    public string MetadataType()
    {
      return PlexPlaylistCreator.MetadataType(Metadata_type ?? 0);
    }
  }

  [Table(Name = "metadata_item_accounts")]
  class Metadata_Item_Account
  {
    [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "id")]
    public int Id { get; set; }

    [Column(Name = "account_id")]
    public int Account_id { get; set; }

    [Column(Name = "metadata_item_id")]
    public int Metadata_item_id { get; set; }
  }  

  [Table(Name = "play_queue_generators")]
  class Play_Queue_Generator
  {
    [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "id")]
    public int Id { get; set; }

    [Column(Name = "playlist_id")]
    public int Playlist_id { get; set; }

    [Column(Name = "metadata_item_id")]
    public int Metadata_item_id { get; set; }

    [Column(Name = "order")]
    public float Order { get; set; }

    //[Column(Name = "created_at")]
    //public DateTime Created_at { get; set; }

    //[Column(Name = "updated_at")]
    //public DateTime Updated_at { get; set; }
  }

  class Metadata_Media_Item
  {
    [Column(Name = "id")]
    public int? Id { get; set; }

    [Column(Name = "metadata_type")]
    public int? Metadata_type { get; set; }

    [Column(Name = "title")]
    public string Title { get; set; }

    [Column(Name = "title_sort")]
    public string Title_sort { get; set; }

    [Column(Name = "duration")]
    public int? Duration { get; set; }

    [Column(Name = "artist")]
    public string Artist { get; set; }

    [Column(Name = "album")]
    public string Album { get; set; }

    public string MetadataType()
    {
      return PlexPlaylistCreator.MetadataType(Metadata_type ?? 0);
    }

    static internal string sqlSelect()
    {
      //string sql = "SELECT mdi.id, mdi.title, mdi.title_sort, mdi.metadata_type, mi.duration FROM metadata_items as mdi ";
      //sql += "join media_items mi ";
      //sql += "where mi.metadata_item_id = mdi.id ";
      //return sql;
      StringBuilder sb = new StringBuilder();
      sb.Append("SELECT tr.id, tr.title, tr.title_sort, tr.metadata_type, mi.duration, al.title as album, ar.title as artist ");
      sb.Append("FROM metadata_items as tr ");
      sb.Append("join media_items mi ");
      sb.Append("join metadata_items al ");
      sb.Append("join metadata_items ar ");
      sb.Append("where mi.metadata_item_id = tr.id and tr.parent_id = al.id and al.parent_id = ar.id and al.metadata_type = 9 and ar.metadata_type = 8");
      return sb.ToString();
    }
  }

  class Play_Queue_Item
  {
    [Column(Name = "id")]
    public int Id { get; set; }

    [Column(Name = "playlist_id")]
    public int Playlist_id { get; set; }

    [Column(Name = "metadata_item_id")]
    public int Metadata_item_id { get; set; }

    [Column(Name = "order")]
    public float Order { get; set; }

    //[Column(Name = "created_at")]
    //public DateTime Created_at { get; set; }

    //[Column(Name = "updated_at")]
    //public DateTime Updated_at { get; set; }

    [Column(Name = "title")]
    public string Title { get; set; }

    [Column(Name = "title_sort")]
    public string Title_sort { get; set; }

    [Column(Name = "metadata_type")]
    public int Metadata_type { get; set; }

    [Column(Name = "duration")]
    public int Duration { get; set; }

    [Column(Name = "artist")]
    public string Artist { get; set; }

    [Column(Name = "album")]
    public string Album { get; set; }

    public string MetadataType()
    {
      return PlexPlaylistCreator.MetadataType(Metadata_type);
    }

    static internal string sqlSelect()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("SELECT pqg.id, pqg.playlist_id, pqg.metadata_item_id, pqg.[order] , pqg.created_at, pqg.updated_at, tr.title, tr.title_sort, tr.metadata_type, mi.duration, al.title as album, ar.title as artist "); 
      sb.Append("FROM play_queue_generators as pqg ");
      sb.Append("join metadata_items as tr ");
      sb.Append("join media_items mi ");
      sb.Append("join metadata_items al ");
      sb.Append("join metadata_items ar ");
      sb.Append("where pqg.metadata_item_id = tr.id and mi.metadata_item_id = tr.id and tr.parent_id = al.id and al.parent_id = ar.id and al.metadata_type = 9 and ar.metadata_type = 8");
      return sb.ToString();
    }
  }  
  #endregion

}

