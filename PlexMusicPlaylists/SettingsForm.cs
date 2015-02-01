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
  public partial class SettingsForm : Form
  {
    PlexMediaServer.PlaylistSettings playlistSettings = PlexMediaServer.PlaylistSettings.theSettings();

    public SettingsForm()
    {
      InitializeComponent();
      playlistSettingsBindingSource.DataSource = playlistSettings;
      rbPlexNative.Checked = playlistSettings.GUIPlaylistMode == PlexMediaServer.PlaylistManager.PlaylistMode.PlexNative;
      rbMusicPlaylistChannel.Checked = !rbPlexNative.Checked;
    }
    
    private void btnSelectDatabase_Click(object sender, EventArgs e)
    {
      ofdPlexDatabase.FileName = String.IsNullOrEmpty(playlistSettings.PlaylistDB) ? PlexMediaServer.PlexPlaylistCreator.PLEX_DATABASE_NAME : playlistSettings.PlaylistDB;
      if (ofdPlexDatabase.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        tbPlexDatabase.Text = ofdPlexDatabase.FileName;
        playlistSettings.PlaylistDB = ofdPlexDatabase.FileName;
      }
    }

    private void btnSelectDataFolder_Click(object sender, EventArgs e)
    {
      fbdDataFolder.SelectedPath = playlistSettings.ChannelDataFolder;
      if (fbdDataFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        tbDataFolder.Text = fbdDataFolder.SelectedPath;
        playlistSettings.ChannelDataFolder = fbdDataFolder.SelectedPath;
      }

    }

    private void rbPlexNative_CheckedChanged(object sender, EventArgs e)
    {
      playlistSettings.GUIPlaylistMode = rbPlexNative.Checked ? PlexMediaServer.PlaylistManager.PlaylistMode.PlexNative : PlexMediaServer.PlaylistManager.PlaylistMode.ChannelMusicPlaylist;
    }

    private void rbMusicPlaylistChannel_CheckedChanged(object sender, EventArgs e)
    {
      playlistSettings.GUIPlaylistMode = rbPlexNative.Checked ? PlexMediaServer.PlaylistManager.PlaylistMode.PlexNative : PlexMediaServer.PlaylistManager.PlaylistMode.ChannelMusicPlaylist;
    }
  }
}
