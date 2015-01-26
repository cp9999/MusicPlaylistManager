using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;


namespace PlexMusicPlaylists.PlexMediaServer
{
  public class Utils
  {
    private const string HEADER_ENTRY_PLEX_CLIENT = "X-Plex-Client-Identifier";
    private const string PLEX_CLIENT_IDENTIFIER = "MusicPlaylist-Configurator";
    public const string DUMMY = "dummy";

    public static string textFromURL(string _url, bool _throw)
    {
      var client = new WebClient();
      try
      {
        // CP 2015-01-26: Proposed changesfrom marc_al
        client.Encoding = Encoding.UTF8;
        // CP 2015-01-26
        client.Headers.Add(HEADER_ENTRY_PLEX_CLIENT, PLEX_CLIENT_IDENTIFIER);
        return client.DownloadString(_url);
      }
      catch (Exception ex)
      {
        if (_throw)
          throw ex;
        return "";
      }
    }
    public static string textFromURL(string _url)
    {
      return textFromURL(_url, false);
    }

    public static XElement elementFromURL(string _url)
    {
      try
      {
        return XElement.Parse(Utils.textFromURL(_url, true), LoadOptions.None);
      }
      catch (Exception ex)
      {
        string innerException = ex.Message;
        if (!String.IsNullOrEmpty(innerException))
          innerException = String.Format(" [Exception={0}]", innerException);
        return new XElement(DUMMY, new XAttribute("url", _url), new XAttribute("exception", innerException));
      }
    }

    public static string getSectionUrl(string _key, string _sectionUrl, string _baseUrl, string _defaultUrl)
    {
      if (_key.StartsWith("/"))
      {
        _sectionUrl = String.Format("{0}{1}", _baseUrl, _key);
      }
      else
      {
        if (String.IsNullOrEmpty(_sectionUrl) && ! String.IsNullOrEmpty(_defaultUrl))
        {
          _sectionUrl = String.Format("{0}{1}/", _baseUrl, _defaultUrl);
        }
        _sectionUrl += _key + "/";
      }
      return _sectionUrl;
    }


  }
}
