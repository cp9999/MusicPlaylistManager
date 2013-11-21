using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using PlexMusicPlaylists.PlexMediaServer;

namespace PlexMusicPlaylists.Import
{
  public class MatchEntry
  {
    private MainSection m_mainSection = null;
    private XElement m_trackElement = null;

    public MatchEntry(MainSection _mainSection, XElement _trackElement)
    {
      m_mainSection = _mainSection;
      m_trackElement = _trackElement;
    }

    public string MainSectionName
    {
      get
      {
        return m_mainSection != null ? m_mainSection.Title : "";
      }
    }
    public string Key
    {
      get { return PMSBase.attributeValue(m_trackElement, PMSBase.KEY); }
    }
    public string Title
    {
      get { return PMSBase.attributeValue(m_trackElement, PMSBase.TITLE); }
    }
    public string Artist
    {
      get { return PMSBase.attributeValue(m_trackElement, "grandparentTitle"); }
    }
    public string FileName
    {
      get
      {
        if (m_trackElement != null)
        {
          try
          {
            XElement media = m_trackElement.Element("Media");
            XElement part = media != null ? media.Element("Part") : null;
            if (part != null)
            {
              string fileName = PMSBase.attributeValue(part, "file");
              return !String.IsNullOrEmpty(fileName) ? Path.GetFileName(fileName) : "";
            }
          }
          catch { }
        }
        return "";
      }
    }
    public string TrackType
    {
      get { return m_mainSection == null || m_mainSection.IsMusic ? "track" : "movie"; }
    }
    public string Info
    {
      get
      {
        return String.Format("[{2}] {3} : {0} - {1}", Artist, Title, Key, MatchDetail);
      }
    }
    public bool MatchOnFolder { get; set; }
    public bool MatchOnTitle { get; set; }
    public bool MatchOnArtist { get; set; }
    public bool MatchOnFileName { get; set; }
    public int MatchFactor
    {
      get
      {
        return factor(MatchOnFolder, 16) + factor(MatchOnTitle, 8) + factor(!(MatchOnFolder && MatchOnTitle), 4) +
          factor(MatchOnArtist, 2) + factor(MatchOnFileName, 1);
      }
    }
    public string MatchDetail
    {
      get
      {
        return String.Format("Match=[{0}{1}{2}{3}]"
          , detail(MatchOnFolder, "F")
          , detail(MatchOnTitle, "T")
          , detail(MatchOnArtist, "A")
          , detail(MatchOnFileName, "N")
          );
      }
    }

    private string detail(bool _isMatch, string _detail)
    {
      return _isMatch ? _detail : "-";
    }

    private int factor(bool _isMatch, int _matchFactor)
    {
      return _isMatch ? _matchFactor : 0;
    }
    public bool IsArtistMatch(string _artist)
    {
      string artist = Artist;
      if (String.IsNullOrEmpty(artist))
      {
        return String.IsNullOrEmpty(_artist);
      }
      return artist.Equals(_artist, StringComparison.OrdinalIgnoreCase);
    }
  }
}
