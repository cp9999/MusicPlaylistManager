using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class PlaylistManager : PMSBase
  {
    protected const string PLTYPE = "pltype";
    protected const string DESCRIPTION = "description";
    protected const string NEWNAME = "newname";
    protected const string PLAYLISTKEY = "playlistkey";
    protected const string TRACK_TYPE = "track_type";
    protected const string TOPOSITION = "to";
    protected const string TITLE1 = "title1";
    protected const string TITLE2 = "title2";
    protected const string SUMMARY = "summary";
    protected const string USER = "user";

    private const string PLAYLIST_BASE_URL = "/music/playlists";
    private const string PLAYLIST_USERS_URL = PLAYLIST_BASE_URL + "/users";
    private const string PLAYLIST_PLAYLISTS_URL = PLAYLIST_BASE_URL + "/playlists";
    private const string PLAYLIST_TRACKS_URL = PLAYLIST_BASE_URL + "/tracks";
    public enum PlaylistMode { PlexNative, ChannelMusicPlaylist };
    protected enum PLCommands { PLAll, PLSingle, PLCreate, PLDelete, PLRename, TRAdd, TRRemove, TRMove, USERList, USERGetCurrent, USERSetCurrent, USERDelete, PREFSGet, PLEXChannelsGet };
    public enum PLTypes { Simple, Smart};
    private SortedDictionary<PLCommands, string> PLCommandBaseUrl = new SortedDictionary<PLCommands,string>
      {{PLCommands.PLAll , PLAYLIST_PLAYLISTS_URL}, {PLCommands.PLSingle, PLAYLIST_PLAYLISTS_URL + "/list"}, 
        {PLCommands.PLCreate, PLAYLIST_PLAYLISTS_URL+"/create"}, {PLCommands.PLDelete, PLAYLIST_PLAYLISTS_URL +"/delete"}, {PLCommands.PLRename, PLAYLIST_PLAYLISTS_URL+"/rename"}, 
        {PLCommands.TRAdd, PLAYLIST_TRACKS_URL + "/add"}, {PLCommands.TRRemove, PLAYLIST_TRACKS_URL + "/remove"}, {PLCommands.TRMove, PLAYLIST_TRACKS_URL + "/move"}, 
        {PLCommands.USERList, PLAYLIST_USERS_URL}, {PLCommands.USERGetCurrent, PLAYLIST_USERS_URL + "/current"}, 
        {PLCommands.USERSetCurrent, PLAYLIST_USERS_URL + "/set"}, {PLCommands.USERDelete, PLAYLIST_USERS_URL + "/delete"},
        {PLCommands.PREFSGet, PLAYLIST_BASE_URL + "/preferences"},
        {PLCommands.PLEXChannelsGet, "/channels/all"}
      };
    private SortedDictionary<PLTypes, string> PLTypeNames = new SortedDictionary<PLTypes, string> { { PLTypes.Simple, "SIMPLE" }, { PLTypes.Smart, "SMART" } };
    private PLUser m_currentUser = null;
    private PlexPlaylistCreator m_PlexPlaylistCreator = new PlexPlaylistCreator();
    private Preferences m_Preferences = new Preferences();
    private bool m_musicPlaylistInstalled = false;

    public PlaylistManager(string _ip, int _port) : base(_ip, _port)
    {
      playlistMode = PlaylistSettings.theSettings().GUIPlaylistMode;
    }

    public Preferences Preferences { get { return m_Preferences; } }
    public PlaylistMode playlistMode { get; set; }
    public PlexPlaylistCreator PlexPlaylistCreator { get { return m_PlexPlaylistCreator; } }
    
    public List<PLUser> currentUserlist { get; set; }
    public List<Playlist> currentAllPlaylists { get; set; }
    public List<Track> currentTrackList { get; set; }
    public PLUser currentUser
    {
      get { return this.getCurrentUser(); }
    }
    public bool MusicPlaylistInstalled
    {
      get { return m_musicPlaylistInstalled; }
    }
    public bool MusicPlaylistsAvailable
    {
      get { return m_musicPlaylistInstalled || PlaylistSettings.ValidChannelDataFolder; }
    }

    public String PlexDatabaseName
    {
      get { return m_PlexPlaylistCreator.PlexDatabaseName; }
      set 
      {
        m_PlexPlaylistCreator.PlexDatabaseName = value;
      }
    }

    public bool allowUserMaintenance
    {
      get { return playlistMode == PlaylistMode.ChannelMusicPlaylist && this.MusicPlaylistInstalled; }
    }

    public void loadPreferences()
    {
      checkMusicPlaylistsChannelInstalled(); 
      m_Preferences.loadFromList(getElementList(PLCommands.PREFSGet, DIRECTORY));
    }

    public bool isNormalUser(string _userName)
    {
      return !String.IsNullOrEmpty(_userName) && !_userName.Equals(PLUser.USER_UNKNOWN);
    }

    protected IEnumerable<XElement> getElementList(PLCommands _command, string _elementsKey)
    {
      string commandUrl = this.makeUrl(_command);
      XElement elementRoot = Utils.elementFromURL(commandUrl);

      var elements =
        from el in elementRoot.Elements(_elementsKey)
        select el;

      return elements;
    }

    private void checkMusicPlaylistsChannelInstalled()
    {
      IEnumerable<XElement> channels = getElementList(PLCommands.PLEXChannelsGet, DIRECTORY);
      var musicChannel = channels.FirstOrDefault(e => e.Attribute(PMSBase.KEY).Value.Equals(PLAYLIST_BASE_URL));
      m_musicPlaylistInstalled = musicChannel != null;
    }

    public IEnumerable<XElement> getUserList()
    {
      return getElementList(PLCommands.USERList, DIRECTORY);
    }

    public IEnumerable<XElement> getAllPlaylists()
    {
      return getElementList(PLCommands.PLAll, DIRECTORY);
    }

    public IEnumerable<XElement> getSinglePlaylist(string _key)
    {
      string commandUrl = this.makeUrl(PLCommands.PLSingle, new Dictionary<string, string> { { KEY, _key } });
      XElement elementRoot = Utils.elementFromURL(commandUrl);

      var elements =
        from el in elementRoot.Elements(TRACK)
        select el;

      return elements;
    }

    #region user handling
    public PLUser getCurrentUser()
    {
      if (m_currentUser == null || m_currentUser.Name.Equals(PLUser.USER_UNKNOWN))
      {
        if (playlistMode == PlaylistMode.PlexNative)
        {
          m_currentUser = m_PlexPlaylistCreator.currentUser;
        }
        else
        {
          var elements = getElementList(PLCommands.USERGetCurrent, DIRECTORY);
          if (elements.Count() > 0)
          {
            m_currentUser = new PLUser() { Name = attributeValue(elements.First(), KEY) };
          }
          else
          {
            m_currentUser = null;
          }
        }
        m_currentUser = m_currentUser ?? new PLUser() { Name = PLUser.USER_UNKNOWN };
      }
      return m_currentUser;
    }

    public List<PLUser> userList(bool _forceFromChannel)
    {
      if (!_forceFromChannel && playlistMode == PlaylistMode.PlexNative)
      {
        return m_PlexPlaylistCreator.userList();
      }
      List<PLUser> userlist = null;
      if (this.MusicPlaylistsAvailable)
      {
        if (PlaylistSettings.theSettings().ChannelPreferDataFolder && PlaylistSettings.ValidChannelDataFolder)
        {
          userlist = this.readChannelUsersFromFolder();
        }
        if ((userlist == null || userlist.Count() == 0) && this.MusicPlaylistInstalled)
        {
          var elements = getUserList();
          foreach (XElement element in elements)
          {
            userlist.Add(new PLUser()
            {
              Name = attributeValue(element, KEY),
              Title = attributeValue(element, TITLE),
            });
          }
        }
        // Music playlist channel not installed: read direclty from data folder
        if (!PlaylistSettings.theSettings().ChannelPreferDataFolder && PlaylistSettings.ValidChannelDataFolder)
        {
          userlist = this.readChannelUsersFromFolder();
        }
      }
      return userlist ?? new List<PLUser>();
    }
    
    public bool setCurrentUser(string _newUser)
    {
      if (!String.IsNullOrEmpty(_newUser) && !_newUser.Equals(PLUser.USER_UNKNOWN) && !_newUser.Equals(currentUser.Name))
      {
        if (playlistMode == PlaylistMode.PlexNative)
        {
          if (!m_PlexPlaylistCreator.setCurrentUser(_newUser))
          {
            return false;
          }
        }
        else
        {
          String commandUrl = this.makeUrl(PLCommands.USERSetCurrent, new Dictionary<string, string> { { USER, _newUser } });
          XElement elementRoot = Utils.elementFromURL(commandUrl);
        }
        m_currentUser = null;
        this.getCurrentUser();
        return true;
      }
      return false;
    }

    public bool deleteCurrentUser(string _user)
    {
      if (!String.IsNullOrEmpty(_user) && !_user.Equals(PLUser.USER_UNKNOWN) && playlistMode == PlaylistMode.ChannelMusicPlaylist)
      {
        String commandUrl = this.makeUrl(PLCommands.USERDelete, new Dictionary<string, string> { { USER, _user } });
        XElement elementRoot = Utils.elementFromURL(commandUrl);
        m_currentUser = null;
        this.getCurrentUser();
        return true;
      }
      return false;
    }

    public bool userExist(string _user)
    {
      return currentUserlist != null && currentUserlist.Find(user => user.Name.Equals(_user.Trim())) != null;
    }
    #endregion

    public List<Playlist> allPlaylists(bool _forceFromChannel)
    {
      if (!_forceFromChannel && playlistMode == PlaylistMode.PlexNative)
      {
        return m_PlexPlaylistCreator.allPlaylists();
      }
      return this.allPlaylistsFromChannel();
    }

    public List<Playlist> allPlaylistsFromChannel(string _user = "")
    {
      List<Playlist> playlists = new List<Playlist>();
      if (this.MusicPlaylistsAvailable)
      {
        if (PlaylistSettings.theSettings().ChannelPreferDataFolder && PlaylistSettings.ValidChannelDataFolder && !String.IsNullOrEmpty(_user))
        {
          playlists = this.readChannelPlaylistsFromFolder(_user);
        }        
        if (this.MusicPlaylistInstalled && playlists.Count() == 0)
        {
          var elements = getAllPlaylists();
          foreach (XElement element in elements)
          {
            playlists.Add(new Playlist()
            {
              Key = attributeValue(element, KEY),
              Title = attributeValue(element, TITLE),
              Description = attributeValue(element, SUMMARY),
              Duration = attributeValueAsInt(element, DURATION)
            });
          }
        }
        if (playlists.Count() == 0 && !PlaylistSettings.theSettings().ChannelPreferDataFolder && PlaylistSettings.ValidChannelDataFolder && !String.IsNullOrEmpty(_user))
        {
          // Music playlist channel not installed: read direclty from data folder
          playlists = this.readChannelPlaylistsFromFolder(_user);
        }
      }
      return playlists;
    }

    public List<Track> singlePlaylist(Playlist _playlist, bool _forceFromChannel)
    {
      List<Track> playlist;
      int totalDuration = 0;
      if (_playlist != null)
      {
        if (!_forceFromChannel && playlistMode == PlaylistMode.PlexNative)
        {
          playlist = m_PlexPlaylistCreator.singlePlaylist(_playlist, out totalDuration);
        }
        else
        {
          playlist = this.singlePlaylistFromChannel(_playlist);
          totalDuration = playlist.Sum(p => p.Duration);
        }
        if (currentAllPlaylists != null)
        {
          Playlist pl = currentAllPlaylists.Find(p => p.Key.Equals(_playlist.Key));
          if (pl != null)
          {
            pl.Duration = totalDuration;
          }
        }
      }
      else
      {
        playlist = new List<Track>();
      }
      return playlist;
    }

    public List<Track> singlePlaylistFromChannel(Playlist _playlist, string _user = "")
    {
      List<Track> playlist = null;
      if (_playlist != null && this.MusicPlaylistsAvailable)
      {
        if (PlaylistSettings.theSettings().ChannelPreferDataFolder && PlaylistSettings.ValidChannelDataFolder && !String.IsNullOrEmpty(_user))
        {
          playlist = this.readChannelSinglePlaylistFromFolder(_playlist, _user);
        }
        if ((playlist == null || playlist.Count() == 0) && this.MusicPlaylistInstalled)
        {
          var elements = getSinglePlaylist(_playlist.Key);
          playlist = new List<Track>();
          foreach (XElement element in elements)
          {
            Track track = new Track(playlist)
            {
              Key = attributeValue(element, KEY),
              Title = attributeValue(element, TITLE),
              Duration = attributeValueAsInt(element, DURATION) / 1000,
              TrackType = attributeValue(element, ALBUM, PMSBase.TYPE_TRACK)
            };
            playlist.Add(track);
          }
        }
        if (playlist.Count() == 0 && !PlaylistSettings.theSettings().ChannelPreferDataFolder && PlaylistSettings.ValidChannelDataFolder && !String.IsNullOrEmpty(_user))
        {
          // Music playlist channel not installed: read direclty from data folder
          playlist = this.readChannelSinglePlaylistFromFolder(_playlist, _user);
        }
      }
      return playlist ?? new List<Track>();
    }

    public string createNewPlaylist(string _title, string _description, PLTypes _pltype)
    {
      if (playlistMode == PlaylistMode.PlexNative)
      {
        return m_PlexPlaylistCreator.createNewPlaylist(_title, _description, _pltype);
      }
      else
      {
        _description = String.IsNullOrEmpty(_description) ? "-" : _description;
        string commandUrl = this.makeUrl(PLCommands.PLCreate, new Dictionary<string, string> { { TITLE, _title }, { PLTYPE, PLTypeNames[_pltype] }, { DESCRIPTION, _description } });
        XElement elementRoot = Utils.elementFromURL(commandUrl);

        return attributeValue(elementRoot, MESSAGE);
      }
    }

    public string deletePlaylist(string _key)
    {
      if (playlistMode == PlaylistMode.PlexNative)
      {
        return m_PlexPlaylistCreator.deletePlaylist(_key);
      }
      else
      {
        string commandUrl = this.makeUrl(PLCommands.PLDelete, new Dictionary<string, string> { { KEY, _key } });
        XElement elementRoot = Utils.elementFromURL(commandUrl);

        return attributeValue(elementRoot, MESSAGE);
      }
    }

    public string renamePlaylist(string _key, string _newName)
    {
      if (!String.IsNullOrEmpty(_newName))
      {
        if (playlistMode == PlaylistMode.PlexNative)
        {
          return m_PlexPlaylistCreator.renamePlaylist(_key, _newName);
        }
        else
        {
          string commandUrl = this.makeUrl(PLCommands.PLRename, new Dictionary<string, string> { { KEY, _key }, { NEWNAME, _newName } });
          XElement elementRoot = Utils.elementFromURL(commandUrl);

          return attributeValue(elementRoot, MESSAGE);
        }
      }
      return "";
    }

    public string addTrack(string _playlistKey, string _key, string _trackType, int _atPosition, out bool _trackAdded)
    {
      string message = "";
      _trackAdded = false;
      if (!String.IsNullOrEmpty(_playlistKey) && !string.IsNullOrEmpty(_key))
      {
        if (playlistMode == PlaylistMode.PlexNative)
        {
          message = m_PlexPlaylistCreator.addTrack(currentTrackList, _playlistKey, _key, _atPosition);
          _trackAdded = !String.IsNullOrEmpty(message);
        }
        else
        {
          message = addTrack(_playlistKey, _key, _trackType);

          _trackAdded = !String.IsNullOrEmpty(message) && message.StartsWith("/");
          if (_trackAdded && _atPosition > 0)
          {
            message += "\n" + moveTrack(_playlistKey, _key, _atPosition);
          }
        }
      }
      return message;
    }

    public string addTrack(string _playlistKey, string _key, string _trackType)
    {
      if (!String.IsNullOrEmpty(_playlistKey) && !string.IsNullOrEmpty(_key))
      {
        if (playlistMode == PlaylistMode.PlexNative)
        {
          return m_PlexPlaylistCreator.addTrack(currentTrackList, _playlistKey, _key, -1);
        }
        else if (!trackInCurrentTrackList(_key))
        {
          string commandUrl = this.makeUrl(PLCommands.TRAdd, new Dictionary<string, string> { { PLAYLISTKEY, _playlistKey }, { KEY, _key }, { TRACK_TYPE, _trackType } });
          XElement elementRoot = Utils.elementFromURL(commandUrl);

          return attributeValue(elementRoot, MESSAGE);
        }
      }
      return "";
    }

    public string removeTrack(string _playlistKey, string _key)
    {
      if (!String.IsNullOrEmpty(_playlistKey) && !string.IsNullOrEmpty(_key))
      {
        if (playlistMode == PlaylistMode.PlexNative)
        {
          return m_PlexPlaylistCreator.removeTrack(currentTrackList, _playlistKey, _key);
        }
        else
        {
          string commandUrl = this.makeUrl(PLCommands.TRRemove, new Dictionary<string, string> { { PLAYLISTKEY, _playlistKey }, { KEY, _key } });
          XElement elementRoot = Utils.elementFromURL(commandUrl);

          return attributeValue(elementRoot, MESSAGE);
        }
      }
      return "";
    }

    public string moveTrack(string _playlistKey, string _key, int _toPosition)
    {
      if (!String.IsNullOrEmpty(_playlistKey) && !string.IsNullOrEmpty(_key))
      {
        if (playlistMode == PlaylistMode.PlexNative)
        {
          return m_PlexPlaylistCreator.moveTrack(currentTrackList, _playlistKey, _key, _toPosition);
        }
        else
        {
          string commandUrl = this.makeUrl(PLCommands.TRMove, new Dictionary<string, string> { { PLAYLISTKEY, _playlistKey }, { KEY, _key }, { TOPOSITION, _toPosition.ToString() } });
          XElement elementRoot = Utils.elementFromURL(commandUrl);

          return attributeValue(elementRoot, MESSAGE);
        }
      }
      return "";
    }

    public bool trackInCurrentTrackList(string _key)
    {
      if (currentTrackList != null)
      {
        return currentTrackList.FirstOrDefault(track => track.Key.Equals(_key)) != null;
      }
      return false;
    }

    protected string makeUrl(PLCommands _command, Dictionary<string, string> _params)
    {
      string paramSeparator = "?";
      string commandUrl = String.Format("{0}{1}", this.baseUrl, PLCommandBaseUrl[_command]);
      if (_params != null)
      {
        foreach (KeyValuePair<string, string> param in _params)
        {
          commandUrl += String.Format("{0}{1}={2}", paramSeparator, param.Key, param.Value);
          paramSeparator = "&";
        }
      }
      return commandUrl;
    }

    protected string makeUrl(PLCommands _command)
    {
      return this.makeUrl(_command, null);
    }

    #region Music channel data files
    private List<PLUser> readChannelUsersFromFolder()
    {
      List<PLUser> users = new List<PLUser>();
      if (PlaylistSettings.ValidChannelDataFolder)
      {
        // Find all files named: "<Username>_allPlaylists.xml"
        var playlistFiles = Directory.EnumerateFiles(PlaylistSettings.theSettings().ChannelDataFolder).Where(f => f.EndsWith("_allPlaylists.xml"));
        foreach (string file in playlistFiles)
        {
          // Extract the username
          string user = Path.GetFileName(file);
          user = user.Remove(user.Length - "_allPlaylists.xml".Length);
          users.Add(new PLUser() { Name = user });
        }
      }
      return users;
    }

    private List<Playlist> readChannelPlaylistsFromFolder(string _user)
    {
      _user = _user.Trim();
      List<Playlist> playlists = new List<Playlist>();
      if (PlaylistSettings.ValidChannelDataFolder && !String.IsNullOrEmpty(_user))
      {
        // Find all files named: "<Username>_allPlaylists.xml"
        string playlistFile = Path.Combine(PlaylistSettings.theSettings().ChannelDataFolder, String.Format("{0}_allPlaylists.xml", _user));
        if (File.Exists(playlistFile))
        {
          // Read the playlist
          try
          {
            //Load xml
            XDocument xdoc = XDocument.Load(playlistFile);

            //Run query
            var plist = from pl in xdoc.Descendants("playlist")
                       select new
                       {
                         Key = pl.Attribute("key").Value,
                         Title = pl.Attribute("title").Value,
                         Description = pl.Attribute("description").Value,
                         Duration = pl.Attribute("duration").Value,
                         //urationTracks = pl.Attribute("durationTracks").Value
                       };
            foreach (var pl in plist)
            {
              playlists.Add(new Playlist()
              {
                Key = pl.Key,
                Title = pl.Title,
                Description = pl.Description,
                Duration = String.IsNullOrEmpty(pl.Duration) ? 0 : Convert.ToInt32(pl.Duration),
              });
            }
          }
          catch { }
        }
      }
      return playlists;
    }

    public List<Track> readChannelSinglePlaylistFromFolder(Playlist _playlist, string _user)
    {
      List<Track> trackList = new List<Track>();
      if (_playlist != null && !String.IsNullOrEmpty(_user) && PlaylistSettings.ValidChannelDataFolder)
      {
        string playlistFile = Path.Combine(PlaylistSettings.theSettings().ChannelDataFolder, String.Format("{0} - {1}.xml", _user, _playlist.Key));
        if (File.Exists(playlistFile))
        {
          // Read the playlist
          try
          {
            //Load xml
            XDocument xdoc = XDocument.Load(playlistFile);

            //Run query
            var tlist = from tr in xdoc.Descendants("Track")
                       select new
                       {
                         Key = tr.Attribute("key").Value,
                         Title = tr.Attribute("title").Value,
                         Duration = tr.Attribute("duration").Value,
                         TrackType = tr.Attribute("type") != null ? tr.Attribute("type").Value : "track"
                       };
            foreach (var tr in tlist)
            {
              trackList.Add(new Track(trackList)
              {
                Key = tr.Key,
                Title = tr.Title,
                Duration = String.IsNullOrEmpty(tr.Duration) ? 0 : Convert.ToInt32(tr.Duration),
                TrackType = tr.TrackType
              });
            }
          }
          catch { }
        }
      }      
      return trackList;
    }

    #endregion
  }
}
