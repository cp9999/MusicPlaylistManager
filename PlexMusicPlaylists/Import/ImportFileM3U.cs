using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace PlexMusicPlaylists.Import
{
  public class ImportFileM3U : ImportFile
  {
    public bool ExtendedFormat { get; set; }

    public static ImportFileM3U loadM3UFile(string _fileName, char _directorySeparator)
    {
      ImportFileM3U importFile = new ImportFileM3U() { FileName = _fileName, DirectorySeparator = _directorySeparator };
      importFile.DirectorySeparator = PlexMusicPlaylists.PlexMediaServer.PMSBase.BACKWARD_SLASH;
      try
      {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(_fileName));
        using (StreamReader sr = new StreamReader(_fileName))
        {
          string m3uContent = sr.ReadToEnd() + "\n";
          // Is this an extended playlist?
          string extendedIndicator = @"^\s*#EXTM3U";
          string whitespaceOnlyLine = @"^\s*?\n";
          string extendedInfo = @"^\s*#EXTINF:(?<duration>[0-9]*)\s*?,(?<artist>[^\n-]*)?-?(?<title>[^\n]*)?";
          importFile.ExtendedFormat = Regex.IsMatch(m3uContent, extendedIndicator);
          if (importFile.ExtendedFormat)
          {
            m3uContent = Regex.Replace(m3uContent, extendedIndicator, "");
          }
          string m3uContentClean = Regex.Replace(Regex.Replace(m3uContent, whitespaceOnlyLine, "", RegexOptions.Multiline), @"\r", "");
          ImportEntry importEntry = null;
          foreach (Match line in Regex.Matches(m3uContentClean, @"^(.*)$", RegexOptions.Multiline))
          {
            if (!String.IsNullOrEmpty(line.Value))
            {
              Match info = Regex.Match(line.Value, extendedInfo);
              if (info.Success)
              {
                importEntry = new ImportEntry() { Owner = importFile, Artist = info.Groups["artist"].Value.Trim(), Title = info.Groups["title"].Value.Trim() };
                try
                {
                  importEntry.Duration = Convert.ToInt32(info.Groups["duration"].Value);
                }
                catch { }
              }
              else
              {
                // This is the file name for the new entry
                if (importEntry == null)
                {
                  importEntry = new ImportEntry() { Owner = importFile };
                }
                importEntry.FileName = line.Value;
                importFile.Entries.Add(importEntry);
                importEntry = null;
              }
            }
          }
        }
      }
      catch { }
      return importFile;
    }
  }
}
