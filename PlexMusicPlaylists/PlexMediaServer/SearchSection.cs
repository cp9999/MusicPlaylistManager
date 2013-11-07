using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PlexMusicPlaylists.PlexMediaServer
{
  public class SearchSection : PlaylistBase
  {
    protected MainSection m_owner = null;

    public SearchSection(MainSection _owner)
      : base()
    {
      m_owner = _owner;
    }

    public string Prompt { get; set; }

    public string Search
    {
      get { return this.Key; }
      set { this.Key = value; }
    }


    public LibrarySection createFromSearch(string _query)
    {
      LibrarySection librarySection = null;
      if (m_owner != null && !String.IsNullOrEmpty(_query))
      {
        string searchUrl = String.Format("{0}{1}&query={2}", m_owner.SectionUrl, Search, _query);
        librarySection = new LibrarySection() { Key= Search, SectionUrl = searchUrl, Title = String.Format("{0} '{1}'", Prompt, _query) };
      }
      return librarySection;
    }

  }
}
