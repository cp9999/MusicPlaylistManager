using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlexMusicPlaylists
{
  public partial class LogonForm : Form
  {
    PlexMediaServer.PlaylistSettings playlistSettings = PlexMediaServer.PlaylistSettings.theSettings();

    public LogonForm()
    {
      InitializeComponent();
      playlistSettingsBindingSource.DataSource = playlistSettings;
      tbPassword.Text = playlistSettings.parmPassword();
      this.enableCommands();
    }

    private void btnLogon_Click(object sender, EventArgs e)
    {
      if (!String.IsNullOrEmpty(playlistSettings.UserName) && !String.IsNullOrEmpty(tbPassword.Text))
      {
        if (PlexMusicPlaylists.PlexMediaServer.Utils.authenticate(playlistSettings.UserName, tbPassword.Text))
        {
          playlistSettings.parmPassword(tbPassword.Text, true);
        }
        this.Close();
      }
    }

    private void enableCommands()
    {
      btnLogon.Enabled = !String.IsNullOrEmpty(playlistSettings.UserName) && !String.IsNullOrEmpty(tbPassword.Text);
    }

    private void tbUserName_TextChanged(object sender, EventArgs e)
    {
      this.enableCommands();
    }

    private void tbPassword_TextChanged(object sender, EventArgs e)
    {
      this.enableCommands();
    }
  }
}
