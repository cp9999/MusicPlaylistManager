using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class MusicSection : LibrarySection
  {
    protected const string SEARCH = "search";
    protected const string PROMPT = "prompt";
    private List<SearchSection> m_searchSections = new List<SearchSection>();

    public List<SearchSection> searchSections { get { return m_searchSections; } }
    public bool canBeSearched
    {
      get { return m_searchSections != null ? m_searchSections.Count() > 0 : false; }
    }

    public void addSearchSections()
    {
      m_searchSections.Clear();

      XElement sectionElements = Utils.elementFromURL(SectionUrl);

      var elements =
        from element in sectionElements.Elements(PMSBase.DIRECTORY)
        where element.Attribute(SEARCH) != null
        select element;
      foreach (XElement searchSection in elements)
      {
        m_searchSections.Add(new SearchSection(this)
        {
          Search = PMSBase.attributeValue(searchSection, PMSBase.KEY),
          Prompt = PMSBase.attributeValue(searchSection, PROMPT),
          Title = PMSBase.attributeValue(searchSection, PMSBase.TITLE)
        });
      }
    }

  }
}
