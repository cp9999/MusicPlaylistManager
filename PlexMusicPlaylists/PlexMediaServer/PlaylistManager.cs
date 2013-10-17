using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class PlaylistManager : PMSBase
  {
    protected const string PLTYPE = "pltype";
    protected const string DESCRIPTION = "description";
    protected const string NEWNAME = "newname";
    protected const string PLAYLISTKEY = "playlistkey";
    protected const string TOPOSITION = "to";
    protected const string TITLE1 = "title1";
    protected const string TITLE2 = "title2";
    protected const string SUMMARY = "summary";
    protected const string USER_UNKNOWN = "Unknown";
    protected const string USER = "user";

    private const string PLAYLIST_BASE_URL = "/music/playlists";
    private const string PLAYLIST_USERS_URL = PLAYLIST_BASE_URL + "/users";
    private const string PLAYLIST_PLAYLISTS_URL = PLAYLIST_BASE_URL + "/playlists";
    private const string PLAYLIST_TRACKS_URL = PLAYLIST_BASE_URL + "/tracks";
    protected enum PLCommands { PLAll, PLSingle, PLCreate, PLDelete, PLRename, TRAdd, TRRemove, TRMove, USERList, USERGetCurrent, USERSetCurrent, USERDelete };
    public enum PLTypes { Simple, Smart};
    private SortedDictionary<PLCommands, string> PLCommandBaseUrl = new SortedDictionary<PLCommands,string>
      {{PLCommands.PLAll , PLAYLIST_PLAYLISTS_URL}, {PLCommands.PLSingle, PLAYLIST_PLAYLISTS_URL + "/list"}, 
        {PLCommands.PLCreate, PLAYLIST_PLAYLISTS_URL+"/create"}, {PLCommands.PLDelete, PLAYLIST_PLAYLISTS_URL +"/delete"}, {PLCommands.PLRename, PLAYLIST_PLAYLISTS_URL+"/rename"}, 
        {PLCommands.TRAdd, PLAYLIST_TRACKS_URL + "/add"}, {PLCommands.TRRemove, PLAYLIST_TRACKS_URL + "/remove"}, {PLCommands.TRMove, PLAYLIST_TRACKS_URL + "/move"}, 
        {PLCommands.USERList, PLAYLIST_USERS_URL}, {PLCommands.USERGetCurrent, PLAYLIST_USERS_URL + "/current"}, 
        {PLCommands.USERSetCurrent, PLAYLIST_USERS_URL + "/set"}, {PLCommands.USERDelete, PLAYLIST_USERS_URL + "/delete"}
      };
    private SortedDictionary<PLTypes, string> PLTypeNames = new SortedDictionary<PLTypes, string> { { PLTypes.Simple, "SIMPLE" }, { PLTypes.Smart, "SMART" } };
    private PLUser m_currentUser = null;

    public PlaylistManager(string _ip, int _port) : base(_ip, _port)
    {
    }

    public List<PLUser> currentUserlist { get; set; }
    public List<Playlist> currentAllPlaylists { get; set; }
    public List<Track> currentTrackList { get; set; }
    public PLUser currentUser
    {
      get { return this.getCurrentUser(); }
    }

    public bool isNormalUser(string _userName)
    {
      return !String.IsNullOrEmpty(_userName) && !_userName.Equals(USER_UNKNOWN);
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

    public PLUser getCurrentUser()
    {
      if (m_currentUser == null || m_currentUser.Name.Equals(USER_UNKNOWN))
      {
        var elements = getElementList(PLCommands.USERGetCurrent, DIRECTORY);
        if (elements.Count() > 0)
        {
          m_currentUser = new PLUser() { Name = attributeValue(elements.First(), KEY) };
        }
        else
        {
          m_currentUser = new PLUser() { Name = USER_UNKNOWN };
        }
      }
      return m_currentUser;
    }

    public List<PLUser> userList()
    {
      var elements = getUserList();
      List<PLUser> userlist = new List<PLUser>();
      foreach (XElement element in elements)
      {
        userlist.Add(new PLUser()
        {
          Name = attributeValue(element, KEY),
          Title = attributeValue(element, TITLE),
        });
      }
      return userlist;
    }
    
    public bool setCurrentUser(string _newUser)
    {
      if (!String.IsNullOrEmpty(_newUser) && !_newUser.Equals(USER_UNKNOWN) && !_newUser.Equals(currentUser.Name))
      {
        String commandUrl = this.makeUrl(PLCommands.USERSetCurrent, new Dictionary<string, string> { { USER, _newUser } });
        XElement elementRoot = Utils.elementFromURL(commandUrl);
        m_currentUser = null;
        this.getCurrentUser();
        return true;
      }
      return false;
    }

    public bool deleteCurrentUser(string _user)
    {
      if (!String.IsNullOrEmpty(_user) && !_user.Equals(USER_UNKNOWN))
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

    public List<Playlist> allPlaylists()
    {
      var elements = getAllPlaylists();
      List<Playlist> playlists = new List<Playlist>();
      foreach (XElement element in elements)
      {
        playlists.Add(new Playlist() { Key = attributeValue(element, KEY), Title = attributeValue(element, TITLE), 
          Description = attributeValue(element, SUMMARY), Duration =  attributeValueAsInt(element, DURATION)});
      }
      return playlists;
    }

    public List<Track> singlePlaylist(string _key)
    {
      var elements = getSinglePlaylist(_key);
      List<Track> playlist = new List<Track>();
      int totalDuration = 0;
      foreach (XElement element in elements)
      {
        Track track = new Track(playlist) { Key = attributeValue(element, KEY), Title = attributeValue(element, TITLE), 
          Duration = attributeValueAsInt(element, DURATION) / 1000 };
        playlist.Add(track);
        totalDuration += track.Duration;
      }
      if (currentAllPlaylists != null)
      {
        Playlist pl = currentAllPlaylists.Find(p => p.Key.Equals(_key));
        if (pl != null)
        {
          pl.Duration = totalDuration;
        }
      }
      return playlist;
    }

    public string createNewPlaylist(string _title, string _description, PLTypes _pltype)
    {
      _description = String.IsNullOrEmpty(_description) ? "-" : _description;
      string commandUrl = this.makeUrl(PLCommands.PLCreate, new Dictionary<string, string> { { TITLE, _title }, { PLTYPE, PLTypeNames[_pltype] }, { DESCRIPTION, _description } });
      XElement elementRoot = Utils.elementFromURL(commandUrl);

      return attributeValue(elementRoot, MESSAGE);
    }

    public string deletePlaylist(string _key)
    {
      string commandUrl = this.makeUrl(PLCommands.PLDelete, new Dictionary<string, string> { { KEY, _key } });
      XElement elementRoot = Utils.elementFromURL(commandUrl);

      return attributeValue(elementRoot, MESSAGE);
    }

    public string renamePlaylist(string _key, string _newName)
    {
      if (!String.IsNullOrEmpty(_newName))
      {
        string commandUrl = this.makeUrl(PLCommands.PLRename, new Dictionary<string, string> { { KEY, _key }, { NEWNAME, _newName } });
        XElement elementRoot = Utils.elementFromURL(commandUrl);

        return attributeValue(elementRoot, MESSAGE);
      }
      return "";
    }


    public string addTrack(string _playlistKey, string _key, int _atPosition, out bool _trackAdded)
    {
      string message = addTrack(_playlistKey, _key);

      _trackAdded = !String.IsNullOrEmpty(message) && message.StartsWith("/");
      if (_trackAdded && _atPosition > 0)
      {
        message += "\n" + moveTrack(_playlistKey, _key, _atPosition);
      }
      return message;
    }

    public string addTrack(string _playlistKey, string _key)
    {
      if (!String.IsNullOrEmpty(_playlistKey) && !string.IsNullOrEmpty(_key) && !trackInCurrentTrackList(_key))
      {
        string commandUrl = this.makeUrl(PLCommands.TRAdd, new Dictionary<string, string> {  { PLAYLISTKEY, _playlistKey } , { KEY, _key }});
        XElement elementRoot = Utils.elementFromURL(commandUrl);

        return attributeValue(elementRoot, MESSAGE);
      }
      return "";
    }

    public string removeTrack(string _playlistKey, string _key)
    {
      if (!String.IsNullOrEmpty(_playlistKey) && !string.IsNullOrEmpty(_key))
      {
        string commandUrl = this.makeUrl(PLCommands.TRRemove, new Dictionary<string, string> {  { PLAYLISTKEY, _playlistKey } , { KEY, _key }});
        XElement elementRoot = Utils.elementFromURL(commandUrl);

        return attributeValue(elementRoot, MESSAGE);
      }
      return "";
    }

    public string moveTrack(string _playlistKey, string _key, int _toPosition)
    {
      if (!String.IsNullOrEmpty(_playlistKey) && !string.IsNullOrEmpty(_key))
      {
        string commandUrl = this.makeUrl(PLCommands.TRMove, new Dictionary<string, string> {  { PLAYLISTKEY, _playlistKey } , { KEY, _key }, {TOPOSITION, _toPosition.ToString()}});
        XElement elementRoot = Utils.elementFromURL(commandUrl);

        return attributeValue(elementRoot, MESSAGE);
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
  }
}
