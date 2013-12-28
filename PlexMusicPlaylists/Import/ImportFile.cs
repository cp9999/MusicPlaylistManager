using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PlexMusicPlaylists.Import
{
  public class ImportFile
  {
    private string m_Path = null;
    protected List<ImportEntry> m_entries = new List<ImportEntry>();

    public string FileName { get; set; }
    public string Title { get; set; }

    public string FullPath
    {
      get
      {
        if (String.IsNullOrEmpty(m_Path) && !String.IsNullOrEmpty(FileName))
        {
          m_Path = Path.GetFullPath(Path.GetDirectoryName(FileName));
        }
        return m_Path ?? "";
      }
    }

    public List<ImportEntry> Entries { get { return m_entries; } }

    public int NumberOfEntries
    {
      get
      {
        return m_entries.Count;
      }
    }

    public int NumberMatched
    {
      get
      {
        var matches =
          from match in m_entries
          where match.Matched
          select match;

        return matches.Count();
      }
    }

    public ImportFile()
    {
      Title = "";
    }
  }
}
