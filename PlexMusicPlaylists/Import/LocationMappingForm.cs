using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlexMusicPlaylists.PlexMediaServer;

namespace PlexMusicPlaylists.Import
{
  public partial class LocationMappingForm : Form
  {
    public LocationMappingForm()
    {
      InitializeComponent();
    }

    private ImportManager m_importManager = null;

    public void setImportManager(ImportManager _importManager)
    {
      m_importManager = _importManager;
    }

    private void LocationMappingForm_Shown(object sender, EventArgs e)
    {
      tbDirectorySeparator.Text = PMSServer.DirectorySeparator.ToString();
      if (m_importManager != null)
      {
        gvSectionLocation.DataSource = m_importManager.SectionLocations;
      }
    }

    private void enableCommands()
    {
      btnClose.Enabled = validDirectorySeparator();
    }

    private char directorySeparator()
    {
      return String.IsNullOrEmpty(tbDirectorySeparator.Text) ? ' ' : tbDirectorySeparator.Text[0];
    }

    private bool validDirectorySeparator()
    {
      char sep = directorySeparator();
      return sep.Equals(PMSBase.BACKWARD_SLASH) || sep.Equals(PMSBase.FORWARD_SLASH);
    }

    private void tbDirectorySeparator_Validating(object sender, CancelEventArgs e)
    {
      e.Cancel = !validDirectorySeparator();
      if (e.Cancel)
      {
        ep.SetError(tbDirectorySeparator, String.Format("Must either be {0} or {1}", PMSBase.BACKWARD_SLASH, PMSBase.FORWARD_SLASH));
        enableCommands();
      }
      else
      {
        ep.SetError(tbDirectorySeparator, String.Empty);
      }
    }

    private void tbDirectorySeparator_Validated(object sender, EventArgs e)
    {
      enableCommands();
      PMSServer.DirectorySeparator = directorySeparator();
    }
  }
}
