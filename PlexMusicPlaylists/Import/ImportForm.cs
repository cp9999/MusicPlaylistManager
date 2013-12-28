using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using PlexMusicPlaylists.PlexMediaServer;

namespace PlexMusicPlaylists.Import
{
  public partial class ImportForm : Form
  {
    private ImportManager m_importManager = null;
    private ContextMenuStrip m_contextMenuStrip = new ContextMenuStrip();
    private ImportEntry m_importEntryMenu = null;

    public ImportForm()
    {
      InitializeComponent();
      m_contextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(m_contextMenuStrip_ItemClicked);
      enableCommands();
    }

    void m_contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      if (e.ClickedItem != null && m_importEntryMenu != null && m_importEntryMenu.selectMatch(e.ClickedItem.Tag as MatchEntry))
      {
        gvImportList.Refresh();
        updateMatchDetails();
        enableCommands();
      }
      m_importEntryMenu = null;
    }


    public void setImportManager(ImportManager _importManager)
    {
      m_importManager = _importManager;
      m_importManager_OnProgress("", false);
      if (m_importManager != null)
      {
        m_importManager.OnProgress += new ImportManager.ProgressEventHandler(m_importManager_OnProgress);
      }
      btnSectionLocation.Enabled = (m_importManager != null && m_importManager.SectionLocations.Count > 0);
      m_importManager.initImportFileFormat(comboImportFormat);
      updateMatchDetails();
      enableCommands();
    }

    void m_importManager_OnProgress(string _message, bool _mainMessage)
    {
      _message = _message ?? "";
      if (_mainMessage)
      {
        labelProgress.Text = _message.Trim();
        labelProgressSub.Text = "";
      }
      else
      {
        labelProgressSub.Text = _message.Trim();
      }
      panelProgress.Visible = !String.IsNullOrEmpty(labelProgress.Text);
      labelProgress.Refresh();
      labelProgressSub.Refresh();
    }

    private void ImportForm_Shown(object sender, EventArgs e)
    {
      if (m_importManager != null && m_importManager.ImportFile != null)
      {
        gvImportList.DataSource = m_importManager.ImportFile.Entries;
      }
      btnSectionLocation.Enabled = (m_importManager != null && m_importManager.SectionLocations.Count > 0);
      if (btnSectionLocation.Enabled && m_importManager.AutoShownLocationMappingOnEmpty &&
        m_importManager.SectionLocations.FirstOrDefault(location => String.IsNullOrEmpty(location.MappedLocation)) != null)
      {
        m_importManager.AutoShownLocationMappingOnEmpty = false;
        m_importManager.showLocationMapping();
      }
    }

    private void updateMatchDetails()
    {
      tbNumberOfTracks.Text = Convert.ToString(m_importManager.ImportFile != null ? m_importManager.ImportFile.NumberOfEntries : 0);
      tbNumberMatched.Text = Convert.ToString(m_importManager.ImportFile != null ? m_importManager.ImportFile.NumberMatched : 0);
    }

    private void enableCommands()
    {
      btnMatchFolder.Enabled = m_importManager != null && m_importManager.ImportFile != null;
      btnMatchTitle.Enabled = m_importManager != null && m_importManager.ImportFile != null;
      btnCreate.Enabled = !String.IsNullOrEmpty(tbPlaylistTitle.Text) && m_importManager != null && m_importManager.ImportFile != null && 
        m_importManager.ImportFile.NumberMatched > 0;
      ImportEntry importEntry = getSingleSelectedImportEntry();
      btnSelectMatch.Enabled = importEntry != null && importEntry.MatchedOnTitleCount > 0;
      btnSearchSelected.Enabled = importEntry != null && !String.IsNullOrEmpty(importEntry.Title);
      btnSwitchTitleArtist.Enabled = gvImportList.SelectedRows.Count > 0;
    }

    private void btnMatchFolder_Click(object sender, EventArgs e)
    {
      if (m_importManager.matchOnFileInFolder(true))
      {
        gvImportList.Refresh();
        updateMatchDetails();
        enableCommands();
      }
    }

    private void btnMatchTitle_Click(object sender, EventArgs e)
    {
      if (m_importManager.matchOnTitle(true))
      {
        gvImportList.Refresh();
        updateMatchDetails();
        enableCommands();
      }
    }

    private void btnOpenFile_Click(object sender, EventArgs e)
    {
      ofdImportFile.Filter = m_importManager.ImportFileFilter;
      if (ofdImportFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && m_importManager != null)
      {        
        if (m_importManager.LoadImportFile(ofdImportFile.FileName))
        {
          m_importManager_OnProgress("", true);
          tbImportFile.Text = ofdImportFile.FileName;
          tbPlaylistTitle.Text = m_importManager.ImportFileTitle ?? Path.GetFileNameWithoutExtension(ofdImportFile.FileName).Trim();
          gvImportList.DataSource = m_importManager.ImportFile.Entries;
          if (m_importManager.matchOnFileInFolder(true))
          {
            gvImportList.Refresh();
          }
          updateMatchDetails();
          enableCommands();
        }
      }
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {
      if (m_importManager != null)
      {
        if (m_importManager.ImportFile.NumberMatched == m_importManager.ImportFile.NumberOfEntries
          || MessageBox.Show("Not all entries in the list are matched with a track from Plex Media Server.\nContinue?", "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
        {
          if (m_importManager.createPlaylist(tbPlaylistTitle.Text, tbPlaylistDescription.Text))
          {
            MessageBox.Show("New playlist created.");
            gvImportList.DataSource = m_importManager.ImportFile != null ? m_importManager.ImportFile.Entries : null;
            updateMatchDetails();
            tbImportFile.Text = "";
            tbPlaylistTitle.Text = "";
            tbPlaylistDescription.Text = "";
          }
        }
        enableCommands();
      }
    }

    private void tbPlaylistTitle_TextChanged(object sender, EventArgs e)
    {
      enableCommands();
    }

    private void gvImportList_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex == MatchIcon.Index)
      {
        ImportEntry importEntry = gvImportList.Rows[e.RowIndex].DataBoundItem as ImportEntry;
        if (importEntry != null)
        {
          if (importEntry.Matched)
          {
            e.Value = Properties.Resources.OKmark;
          }
          else if (importEntry.MatchedOnTitleCount > 0)
          {
            e.Value = Properties.Resources.Blue_question_mark_16x16;
          }
          else
          {
            e.Value = Properties.Resources.Red_stop_16x16;
          }
        }
      }
    }

    private ImportEntry getSingleSelectedImportEntry()
    {
      if ( gvImportList.SelectedRows.Count == 1)
      {
        return gvImportList.SelectedRows[0].DataBoundItem as ImportEntry;
      }
      return null;
    }

    private void showMatchMenu(DataGridViewRow _row, Point _offset)
    {
      if (_row != null)
      {
        ImportEntry importEntry = _row.DataBoundItem as ImportEntry;
        if (importEntry != null && importEntry.FillContextMenu(m_contextMenuStrip))
        {
          m_importEntryMenu = importEntry;
          Rectangle cell = gvImportList.GetCellDisplayRectangle(MatchIcon.Index, _row.Index, true);
          m_contextMenuStrip.Show(gvImportList, cell.X + _offset.X, cell.Y + _offset.Y);
        }
      }
    }

    private void gvImportList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex == MatchIcon.Index)
      {
        showMatchMenu(gvImportList.Rows[e.RowIndex], e.Location);
      }
    }

    private void btnSelectMatch_Click(object sender, EventArgs e)
    {
      if (gvImportList.SelectedRows.Count == 1)
      {
        showMatchMenu(gvImportList.SelectedRows[0], new Point(24, 8));
      }
    }

    private void gvImportList_SelectionChanged(object sender, EventArgs e)
    {
      enableCommands();
    }

    private void btnSearchSelected_Click(object sender, EventArgs e)
    {
      ImportEntry importEntry = getSingleSelectedImportEntry();
      if (importEntry != null && m_importManager.matchOnTitle(importEntry))
      {
        gvImportList.Refresh();
        updateMatchDetails();
        enableCommands();
      }
    }


    private void btnSectionLocation_Click(object sender, EventArgs e)
    {
      if (m_importManager != null && m_importManager.SectionLocations.Count > 0)
      {
        if (m_importManager.showLocationMapping())
        {
          gvImportList.Refresh();
          updateMatchDetails();
          enableCommands();
        }
      }
    }

    private void ImportForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (m_importManager != null && m_importManager.ImportFile != null && m_importManager.ImportFile.NumberOfEntries > 0)
      {
        e.Cancel = MessageBox.Show("No playlist created for loaded file.\nClose anyway?", "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No;
      }
    }

    private void btnSwitchTitleArtist_Click(object sender, EventArgs e)
    {
      foreach (DataGridViewRow row in gvImportList.SelectedRows)
      {
        ImportEntry importEntry = row.DataBoundItem as ImportEntry;
        if (importEntry != null)
        {
          string temp = importEntry.Artist;
          importEntry.Artist = importEntry.Title;
          importEntry.Title = temp;
        }
      }
      gvImportList.Refresh();
      updateMatchDetails();
      enableCommands();
    }

  }
}
