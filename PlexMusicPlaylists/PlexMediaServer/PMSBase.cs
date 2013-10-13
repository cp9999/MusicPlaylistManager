using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class PMSBase
  {
    private string serverUrl = "";
    private string serverName = "";
    protected const string DIRECTORY = "Directory";
    protected const string TRACK = "Track";
    protected const string MESSAGE = "message";
    protected const string KEY = "key";
    protected const string TITLE = "title";
    protected const string FRIENDLYNAME = "friendlyName";

    public String IP { get; set; }

    public int Port { get; set; }

    public string baseUrl
    {
      get { return String.Format("http://{0}:{1}", IP, Port); }
    }

    protected PMSBase(string _ip, int _port)
    {
      IP = _ip;
      Port = _port;
      serverUrl = baseUrl;
    }

    public string Name
    {
      get { return getName(); }
    }

    private string getName()
    {
      if (!String.IsNullOrEmpty(IP) && (String.IsNullOrEmpty(serverName) || !serverUrl.Equals(baseUrl)))
      {
        XElement element = Utils.elementFromURL(baseUrl);
        if (element.Name.LocalName.Equals(Utils.DUMMY))
        {
          throw new Exception(String.Format("ERROR: No server found at: {0}", baseUrl));
        }
        serverName = attributeValue(element, FRIENDLYNAME);
      }
      serverUrl = baseUrl;
      return serverName;
    }

    public static string attributeValue(XElement _element, string _attrName, string _defaultValue)
    {
      try
      {
        return _element.Attribute(_attrName).Value;
      }
      catch
      {
      }
      return _defaultValue;
    }

    public static string attributeValue(XElement _element, string _attrName)
    {
      return attributeValue(_element, _attrName, "");
    }
  }
}
