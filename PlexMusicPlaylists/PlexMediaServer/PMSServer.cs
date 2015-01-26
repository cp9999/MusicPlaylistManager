using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class PMSServer : PMSBase
  {
    private const string LIBRARY_SECTIONS = "/library/sections";

    static PMSServer()
    {
      DirectorySeparator = PMSBase.FORWARD_SLASH;
    }

    public PMSServer(string _ip, int _port) : base(_ip, _port)
    {
    }

    public static char DirectorySeparator { get; set; }

    public IEnumerable<XElement> getMusicSections()
    {
      string sectionUrl = String.Format("{0}{1}", this.baseUrl, LIBRARY_SECTIONS);
      XElement sections = Utils.elementFromURL(sectionUrl);

      var musicsections =
        from section in sections.Elements(PMSBase.DIRECTORY)
        where ((string)section.Attribute("scanner")).Equals("Plex Music Scanner")
        select section;

      return musicsections;
    }

    public IEnumerable<XElement> getMovieSections()
    {
      string sectionUrl = String.Format("{0}{1}", this.baseUrl, LIBRARY_SECTIONS);
      XElement sections = Utils.elementFromURL(sectionUrl);

      var moviesections =
        from section in sections.Elements(PMSBase.DIRECTORY)
        where ((string)section.Attribute(PMSBase.ATTR_TYPE)).Equals(PMSBase.TYPE_MOVIE) || ((string)section.Attribute(PMSBase.ATTR_TYPE)).Equals(PMSBase.TYPE_SHOW)
        select section;

      return moviesections;
    }

    private List<LibrarySection> listFromElements(IEnumerable<XElement> _elements, string _parentUrl, bool _hasTracks, bool _isMainSection, bool _isMusicSection)
    {
      List<LibrarySection> sections = new List<LibrarySection>();
      foreach (XElement element in _elements)
      {
        string key = attributeValue(element, KEY);
        LibrarySection section = _isMainSection
          ? new MainSection() { Key = key, Title = attributeValue(element, TITLE), SectionUrl = getSectionUrl(key, _parentUrl), IsMusic = _isMusicSection }
          : new LibrarySection() { Key = key, Title = attributeValue(element, TITLE), SectionUrl = getSectionUrl(key, _parentUrl), HasTracks = _hasTracks, IsMusic = _isMusicSection };
        sections.Add(section);
        if (_isMainSection)
        {
          ((MainSection)section).Owner = this;
          ((MainSection)section).addLocations(element);
          ((MainSection)section).addSearchSections();
          if (!_isMusicSection)
          {
            section.IsEpisode = attributeValue(element, PMSBase.ATTR_TYPE).Equals(PMSBase.TYPE_SHOW, StringComparison.OrdinalIgnoreCase);
          }
        }
        else 
        {
          section.IsEpisode = attributeValue(element, PMSBase.ATTR_TYPE).Equals(PMSBase.TYPE_EPISODE, StringComparison.OrdinalIgnoreCase);
        }
      }
      // CP 2015-01-26: Proposed changesfrom marc_al
      sections.Sort(new CustomLibrarySectiontringComparer());
      // CP 2015-01-26
      return sections;
    }

    private string getSectionUrl(string _key, string _sectionUrl)
    {
      return Utils.getSectionUrl(_key, _sectionUrl, this.baseUrl, LIBRARY_SECTIONS);
    }

    public IEnumerable<XElement> getSectionElements(LibrarySection _section, bool _allowDirectory)
    {
      XElement sectionElements = Utils.elementFromURL(_section.SectionUrl);

      string xpath = _section.IsMusic ? TRACK : VIDEO;
      var elements =
        from element in sectionElements.Elements(xpath)
        select element;

      _section.HasTracks = elements.Count() > 0;
      if (!_section.HasTracks && _allowDirectory)
      {
        return 
          from element in sectionElements.Elements(DIRECTORY)
          where element.Attribute("search") == null
          select element;
      }
      return elements;
    }

    public IEnumerable<XElement> getSectionTracks(LibrarySection _section)
    {
      return getSectionElements(_section, false);
    }


    public List<LibrarySection> musicSections()
    {
      return listFromElements(getMusicSections(), "", false, true, true);
    }

    public List<LibrarySection> movieSections()
    {
      return listFromElements(getMovieSections(), "", false, true, false);
    }

    public List<LibrarySection> librarySections(LibrarySection _section)
    {
      return listFromElements(getSectionElements(_section, true), _section.SectionUrl, _section.HasTracks, false, _section.IsMusic);
    }

  }

  // CP 2015-01-26: Proposed changesfrom marc_al
  class CustomLibrarySectiontringComparer : IComparer<LibrarySection>
  {
    int IComparer<LibrarySection>.Compare(LibrarySection x, LibrarySection y)
    {
      return string.Compare(x.Title, y.Title);
    }
  }
}
