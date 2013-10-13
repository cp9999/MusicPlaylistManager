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

    private const string PLAYLIST_BASE_URL = "/music/playlists";
    private const string PLAYLIST_PLAYLISTS_URL = PLAYLIST_BASE_URL + "/playlists";
    private const string PLAYLIST_TRACKS_URL = PLAYLIST_BASE_URL + "/tracks";
    protected enum PLCommands { PLAll, PLSingle, PLCreate, PLDelete, PLRename, TRAdd, TRRemove, TRMove };
    public enum PLTypes { Simple, Smart};
    private SortedDictionary<PLCommands, string> PLCommandBaseUrl = new SortedDictionary<PLCommands,string>
      {{PLCommands.PLAll , PLAYLIST_PLAYLISTS_URL}, {PLCommands.PLSingle, PLAYLIST_PLAYLISTS_URL + "/list"}, 
        {PLCommands.PLCreate, PLAYLIST_PLAYLISTS_URL+"/create"}, {PLCommands.PLDelete, PLAYLIST_PLAYLISTS_URL +"/delete"}, {PLCommands.PLRename, PLAYLIST_PLAYLISTS_URL+"/rename"}, 
        {PLCommands.TRAdd, PLAYLIST_TRACKS_URL + "/add"}, {PLCommands.TRRemove, PLAYLIST_TRACKS_URL + "/remove"}, {PLCommands.TRMove, PLAYLIST_TRACKS_URL + "/move"}, 
      };
    private SortedDictionary<PLTypes, string> PLTypeNames = new SortedDictionary<PLTypes, string> { { PLTypes.Simple, "SIMPLE" }, { PLTypes.Smart, "SMART" } };

    public PlaylistManager(string _ip, int _port) : base(_ip, _port)
    {
    }

    public List<Track> currentTrackList { get; set; }

    public IEnumerable<XElement> getAllPlaylists()
    {
      string commandUrl = this.makeUrl(PLCommands.PLAll);
      XElement elementRoot = Utils.elementFromURL(commandUrl);

      var elements =
        from el in elementRoot.Elements(DIRECTORY)
        select el;

      return elements;
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

    public List<Playlist> allPlaylists()
    {
      var elements = getAllPlaylists();
      List<Playlist> playlists = new List<Playlist>();
      foreach (XElement element in elements)
      {
        playlists.Add(new Playlist() { Key = attributeValue(element, KEY), Title = attributeValue(element, TITLE), Description = attributeValue(element, SUMMARY) });
      }
      return playlists;
    }

    public List<Track> singlePlaylist(string _key)
    {
      var elements = getSinglePlaylist(_key);
      List<Track> playlist = new List<Track>();
      foreach (XElement element in elements)
      {
        playlist.Add(new Track(playlist) { Key = attributeValue(element, KEY), Title = attributeValue(element, TITLE) });
      }
      return playlist;
    }

    public string createNewPlaylist(string _title, string _description, PLTypes _pltype)
    {
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
