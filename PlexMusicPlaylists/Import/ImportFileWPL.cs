using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Xml.Linq;
using System.Windows.Forms;

namespace PlexMusicPlaylists.Import
{
  class ImportFileWPL : ImportFile
  {

    public static ImportFileWPL loadWPLFile(string _fileName)
    {
      ImportFileWPL importFile = new ImportFileWPL() { FileName = _fileName };
      try
      {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(_fileName));
        XDocument document = XDocument.Load(_fileName, LoadOptions.None);
        if (document != null && document.Root != null)
        {
          if (document.Root.Name.LocalName.Equals("smil", StringComparison.OrdinalIgnoreCase))
          {
            XElement mainElement = document.Root.Element("head");
            if (mainElement != null && mainElement.Element("title") != null)
            {
              importFile.Title = mainElement.Element("title").Value;
            }
            mainElement = document.Root.Element("body");
            if (mainElement != null && mainElement.Element("seq") != null)
            {
              var tracks =
                from media in mainElement.Element("seq").Elements("media")
                where media.Attribute("src") != null && !String.IsNullOrEmpty(media.Attribute("src").Value)
                select media;
              foreach (XElement mediaElement in tracks)
              {
                ImportEntry importEntry = new ImportEntry() { Owner = importFile };
                importEntry.FileName = mediaElement.Attribute("src").Value;
                importFile.Entries.Add(importEntry);

              }
            }
          }
          else
          {
            throw new Exception("Root element must be <smil>");
          }
        }
        else
        {
          // Invalid file format
          throw new Exception("Invalid file.");
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format("Error={0}", ex.Message), "Error reading file");
      }
      return importFile;
    }
  }
}
