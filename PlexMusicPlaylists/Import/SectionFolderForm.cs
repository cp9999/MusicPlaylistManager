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
  public partial class SectionFolderForm : Form
  {
    private MainSection m_mainSection = null;

    public void setMainSection(MainSection _mainSection)
    {
      m_mainSection = _mainSection;

      this.Text = String.Format("Folders in section {0}", m_mainSection != null ? m_mainSection.Title : "");
    }

    public SectionFolderForm()
    {
      InitializeComponent();
    }

    private void SectionFolderForm_Shown(object sender, EventArgs e)
    {
      if (m_mainSection != null)
      {
        gvFolders.DataSource = m_mainSection.folders;
      }
    }

    private void enableCommands()
    {
      btnReloadFolders.Enabled = m_mainSection != null;
    }

    private void btnReloadFolders_Click(object sender, EventArgs e)
    {
      if (m_mainSection != null)
      {
        if (MessageBox.Show("Reloading all folders for a section can take a long time. Continue?", "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
        {
          gvFolders.DataSource = null;
          gvFolders.Refresh();
          m_mainSection.loadFromCache(true, true, this.showProgress);
          this.showProgress("", true);
          gvFolders.DataSource = m_mainSection.folders;
        }
      }
    }

    void showProgress(string _message, bool _mainMessage)
    {
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
  }
}
