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
    private static string authToken = "";

    public static bool authenticate(string _userName, string _password)
    {
      authToken = "";
      try
      {
        using (var client = new WebClient())
        {
          string url = "https://plex.tv/users/sign_in.xml";
          // CP 2015-01-26: Proposed changesfrom marc_al
          client.Encoding = Encoding.UTF8;
          // CP 2015-01-26
          client.Headers.Add(HEADER_ENTRY_PLEX_CLIENT, PLEX_CLIENT_IDENTIFIER);
          client.Headers.Add("Authorization", authHeaderVal(_userName, _password));
          XElement response = XElement.Parse(client.UploadString(url, ""), LoadOptions.None);

          authToken = PMSBase.attributeValue(response, "authenticationToken", "");
        }
      }
      catch (Exception ex)
      {
      }
      return !String.IsNullOrEmpty(authToken);
    }

    private static string authHeaderVal(string _username, string _password)
    {
      var authString = _username + ":" + _password;
      var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(authString);
      return "Basic " + System.Convert.ToBase64String(plainTextBytes);
    }

    public static string textFromURL(string _url, bool _throw)
    {
      var client = new WebClient();
      try
      {
        // CP 2015-01-26: Proposed changesfrom marc_al
        client.Encoding = Encoding.UTF8;
        // CP 2015-01-26
        client.Headers.Add(HEADER_ENTRY_PLEX_CLIENT, PLEX_CLIENT_IDENTIFIER);
        if (!String.IsNullOrEmpty(authToken))
        {
          string separator = _url.Contains("?") ? "&" : "?";
          _url += string.Format("{0}{1}={2}", separator, "X-Plex-Token", authToken);
        }
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
