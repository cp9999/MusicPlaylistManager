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
    public bool MappingChanged { get; set; }
    private string m_startText = "";

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
      btnShowFolders.Enabled = getSelectedMainSection() != null;
    }

    private SectionLocation getSingleSelectedEntry()
    {
      if (gvSectionLocation.SelectedRows.Count == 1)
      {
        return gvSectionLocation.SelectedRows[0].DataBoundItem as SectionLocation;
      }
      return null;
    }
    private MainSection getSelectedMainSection()
    {
      if (gvSectionLocation.SelectedRows.Count == 1)
      {
        SectionLocation sectionLocation = gvSectionLocation.SelectedRows[0].DataBoundItem as SectionLocation;
        return sectionLocation != null ? sectionLocation.Owner() : null;
      }
      return null;
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

    private void btnShowFolders_Click(object sender, EventArgs e)
    {
      MainSection mainSection = getSelectedMainSection();
      if (mainSection != null)
      {
        SectionFolderForm folderForm = new SectionFolderForm();
        folderForm.setMainSection(mainSection);
        folderForm.ShowDialog();
      }
    }

    private void gvSectionLocation_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      if (!MappingChanged && gvSectionLocation.CurrentCell != null && gvSectionLocation.CurrentCell.Value != null)
      {
        MappingChanged = !m_startText.Equals(gvSectionLocation.CurrentCell.Value.ToString(), StringComparison.OrdinalIgnoreCase);
      }
    }

    private void gvSectionLocation_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
      if (gvSectionLocation.CurrentCell != null && gvSectionLocation.CurrentCell.Value != null)
      {
        m_startText = gvSectionLocation.CurrentCell.Value.ToString();
      }
    }
  }
}
