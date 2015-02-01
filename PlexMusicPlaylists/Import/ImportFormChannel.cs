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
  public partial class ImportFormChannel : Form
  {
    private PlaylistManager m_playlistManager = null;
    public List<Playlist> currentAllPlaylists { get; set; }
    public List<Track> currentTrackList { get; set; }
    public List<PLUser> currentUserlist { get; set; }
    private string m_lastCreatedKey = "";
    private string loadedListKey = "";
    private string currentUser = "";

    public ImportFormChannel()
    {
      InitializeComponent();
      enableCommands();
    }

    public string LastCreatedKey { get { return m_lastCreatedKey; } }
    public void setPlaylistManager(PlaylistManager _playlistManager)
    {
      m_playlistManager = _playlistManager;
      loadedListKey = "";
      enableCommands();
    }

    private void loadPlaylists()
    {
      if (m_playlistManager != null)
      {
        currentAllPlaylists = m_playlistManager.allPlaylistsFromChannel(currentUser);
        gvPlaylists.DataSource = currentAllPlaylists;
        if (currentAllPlaylists.Count() == 0)
        {
          gvSinglePlayList.DataSource = null;
        }
      }
    }

    private void ImportFormChannel_Shown(object sender, EventArgs e)
    {
      if (m_playlistManager != null)
      {
        fillUserList();
        loadPlaylists();
      }
    }


    private void enableCommands()
    {
      btnCreate.Enabled = canCreate();
    }

    private bool canCreate()
    {
      return !String.IsNullOrEmpty(tbPlaylistTitle.Text) && m_playlistManager != null && currentTrackList != null && currentTrackList.Count() > 0 && selectedPlaylist != null; 
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {
      if (canCreate())
      {
        Playlist playlist = selectedPlaylist;
        playlist.Title = tbPlaylistTitle.Text;
        playlist.Description = tbPlaylistTitle.Text;
        string newCreatedKey = m_playlistManager.PlexPlaylistCreator.addPlaylist(playlist, currentTrackList);
        if (!String.IsNullOrEmpty(newCreatedKey))
        {
          m_lastCreatedKey = newCreatedKey;
          gvPlaylists.DataSource = null;
          currentAllPlaylists.Remove(playlist);
          gvPlaylists.DataSource = currentAllPlaylists;
          if (currentAllPlaylists.Count() == 0)
          {
            gvSinglePlayList.DataSource = null;
          }
          else
          {
            /*DataGridViewRow newRow = gvPlaylists.Rows.OfType<DataGridViewRow>().FirstOrDefault();
            if (newRow != null)
            {
              newRow.Selected = true;
            }*/
          }
        }
        enableCommands();
      }
    }

    private void tbPlaylistTitle_TextChanged(object sender, EventArgs e)
    {
      enableCommands();
    }


    private void ImportFormChannel_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (m_playlistManager != null)
      {
        //e.Cancel = MessageBox.Show("No playlist created for loaded file.\nClose anyway?", "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No;
      }
    }

    private Playlist selectedPlaylist
    {
      get
      {
        if (gvPlaylists.SelectedRows.Count > 0)
        {
          return (Playlist)gvPlaylists.SelectedRows[0].DataBoundItem;
        }
        return null;
      }
    }

    private void gvPlaylists_SelectionChanged(object sender, EventArgs e)
    {
      Playlist playlist = selectedPlaylist;
      if (playlist != null)
      {
        //tbPlaylistTitle.Text = playlist.Key;
        tbPlaylistTitle.Text = playlist.Title;
        tbPlaylistDescription.Text = playlist.Description;
        loadSinglePlaylist(playlist);
      }
      enableCommands();

    }

    private void loadSinglePlaylist(Playlist _playlist)
    {
      loadSinglePlaylist(_playlist, "", false);
    }

    private void loadSinglePlaylist(Playlist _playlist, string _selectkey, bool _force)
    {
      if (m_playlistManager != null && _playlist != null && (_force || !loadedListKey.Equals(_playlist.Key)))
      {
        loadedListKey = _playlist.Key;
        currentTrackList = m_playlistManager.singlePlaylistFromChannel(_playlist, currentUser);
        gvSinglePlayList.DataSource = currentTrackList;
        if (gvSinglePlayList.Rows.Count > 0)
        {
          gvSinglePlayList.ClearSelection();
          // And select the track
          DataGridViewRow newRow = null;
          if (!String.IsNullOrEmpty(_selectkey))
          {
            newRow = gvSinglePlayList.Rows.OfType<DataGridViewRow>().FirstOrDefault(row => ((Track)row.DataBoundItem).Key == _selectkey);
          }
          (newRow ?? gvSinglePlayList.Rows[0]).Selected = true;
        }
      }
    }

    private void fillUserList()
    {
      comboUsers.Items.Clear();
      currentUserlist = m_playlistManager.userList(true);
      foreach (PLUser user in currentUserlist)
      {
        comboUsers.Items.Add(user.Name);
      }
      if (currentUserlist.Count() > 0)
      {
        comboUsers.SelectedIndex = 0;
      }
    }

    private void comboUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
      string newName = comboUsers.SelectedItem.ToString();

      changeUser(newName);
    }

    private void changeUser(string _newName)
    {
      if (!currentUser.Equals(_newName, StringComparison.OrdinalIgnoreCase))
      {
        currentUser = _newName;
        loadedListKey = "";
        loadPlaylists();
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {

    }
  }
}
