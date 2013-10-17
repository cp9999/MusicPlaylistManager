using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using PlexMusicPlaylists.PlexMediaServer;

namespace PlexMusicPlaylists
{
  public partial class PlaylistUserControl : UserControl
  {
    public delegate void LogMessageEventHandler(string _message);
    public event LogMessageEventHandler OnLogMessage;

    #region data members
    PlaylistManager playlistManager = new PlaylistManager("", 32400);
    PlexMediaServer.PMSServer server = new PlexMediaServer.PMSServer("", 32400);
    protected string caption = "Playlist configurator";
    string loadedListKey = "";
    bool editAddMode = false;
    bool editIsUser = false;
    bool plexConnected = false;
    #endregion

    #region public properties
    public string Caption
    {
      get { return caption; }
      set
      {
        caption = value;
        updateCaption();
      }
    }
    #endregion

    #region public methods

    public PlaylistUserControl()
    {
      InitializeComponent();
      initPlaylistControls();
    }

    public bool setPlexMediaServerIP(string _ip, int _port)
    {
      plexConnected = false;
      playlistManager.IP = _ip;
      playlistManager.Port = _port;
      server.IP = _ip;
      server.Port = _port;
      try
      {
        updateCaption();
        plexConnected = true;
        loadedListKey = "";
        fillUserList();
        loadPlaylists();
        loadMusicSections();
        enableEditCommands();
        enableTrackCommands();
        return true;
      }
      catch (Exception ex)
      {
        logMessage(ex.Message);
        MessageBox.Show(ex.Message);
      }
      return false;
    }

    #endregion


    #region Playlist Helper methods

    private void updateCaption()
    {
      lblCaption.Text = String.Format("{0}: {1} [{2}]", caption, playlistManager.Name, playlistManager.baseUrl);
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

    private void showPlaylistEditPanel(bool _addnew, bool _isUser)
    {
      panelPlaylistEdit.Visible = true;
      editAddMode = _addnew;
      editIsUser = _isUser;

      tbPlaylistEditAddTitle.Visible = _addnew;
      tbPlaylistEditAddDescription.Visible = _addnew && !_isUser;
      btnPlaylistEditCreate.Visible = _addnew;
      lblPlaylistEditDescription.Visible = !_isUser;

      tbPlaylistEditKey.Visible = !_addnew;
      tbPlaylistEditRenameTitle.Visible = !_addnew;
      tbPlaylistEditRenameDescription.Visible = !_addnew;
      btnPlaylistEditRename.Visible = !_addnew;
      if (_addnew)
      {
        gbPlaylistEdit.Text = _isUser ? "Add new user" : "Add new playlist";
        lblPlaylistEditTitle.Text = _isUser ? "User name" : "Title";
      }
      else
      {
        gbPlaylistEdit.Text = "Rename playlist";
        lblPlaylistEditTitle.Text = "New title";
      }
    }

    private void hidePlaylistEditPanel()
    {
      panelPlaylistEdit.Visible = false;
    }

    private void initPlaylistControls()
    {
      //splitContainerGlobal.Panel2Collapsed = true;
      btnPlaylistEditRename.Top = btnPlaylistEditCreate.Top;
      btnPlaylistEditRename.Left = btnPlaylistEditCreate.Left;
      tbPlaylistEditRenameTitle.Top = tbPlaylistEditAddTitle.Top;
      tbPlaylistEditRenameTitle.Left = tbPlaylistEditAddTitle.Left;
      tbPlaylistEditRenameDescription.Top = tbPlaylistEditAddDescription.Top;
      tbPlaylistEditRenameDescription.Left = tbPlaylistEditAddDescription.Left;
      hidePlaylistEditPanel();
      panelTrackToolstrip.Height = panelPlaylistToolStrip.Height;
      panelServerSectionToolstrip.Height = panelPlaylistToolStrip.Height;
      panelServerTrackToolstrip.Height = panelPlaylistToolStrip.Height;
      splitContainerServerMain.SplitterDistance = splitContainerMain.SplitterDistance;

      enableEditCommands();
      enableServerTrackCommands();
    }

    private void loadPlaylists()
    {
      playlistManager.currentAllPlaylists = playlistManager.allPlaylists();
      gvPlaylists.DataSource = playlistManager.currentAllPlaylists;
      if (playlistManager.currentAllPlaylists.Count() == 0)
      {
        gvSinglePlayList.DataSource = null;
      }
    }

    private void logMessage(string _message)
    {
      if (!String.IsNullOrEmpty(_message) && OnLogMessage != null)
      {
        OnLogMessage(_message);
      }
    }

    private void loadSinglePlaylist(string _key)
    {
      loadSinglePlaylist(_key, "", false);
    }

    private void loadSinglePlaylist(string _key, string _selectkey, bool _force)
    {
      if (_force || !loadedListKey.Equals(_key))
      {
        loadedListKey = _key;
        playlistManager.currentTrackList = playlistManager.singlePlaylist(_key);
        gvSinglePlayList.DataSource = playlistManager.currentTrackList;
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

    private void enableEditCommands()
    {
      toolStripPlaylists.Enabled = plexConnected;
      toolStripSinglePlaylist.Enabled = plexConnected;
      toolStripServerSections.Enabled = plexConnected;
      toolStripServerTracks.Enabled = plexConnected;
      if (editIsUser)
      {
        btnPlaylistEditCreate.Enabled = !String.IsNullOrEmpty(tbPlaylistEditAddTitle.Text) && !playlistManager.userExist(tbPlaylistEditAddTitle.Text);
      }
      else
      {
        btnPlaylistEditCreate.Enabled = !String.IsNullOrEmpty(tbPlaylistEditAddTitle.Text) && tbPlaylistEditAddTitle.Text.Trim() != String.Empty;
      }
      Playlist playlist = selectedPlaylist;
      btnPlaylistEditRename.Enabled = playlist != null && playlist.canRenameTo(tbPlaylistEditRenameTitle.Text);
      btnPlaylistDelete.Enabled = playlistBindingSource != null;
      btnUserDelete.Enabled = playlistManager.isNormalUser(playlistManager.currentUser.Name);
    }

    private void enableTrackCommands()
    {
      int tracksSelected = gvSinglePlayList.SelectedRows.Count;
      btnTrackUp.Enabled = tracksSelected == 1 && gvSinglePlayList.Rows.IndexOf(gvSinglePlayList.SelectedRows[0]) > 0;
      btnTrackDown.Enabled = tracksSelected == 1 && gvSinglePlayList.Rows.IndexOf(gvSinglePlayList.SelectedRows[0]) < gvSinglePlayList.Rows.Count - 1;
      btnTrackRemove.Enabled = tracksSelected > 0;
      enableServerTrackCommands();
    }

    private void moveTrack(int _offset)
    {
      Playlist playlist = selectedPlaylist;
      if (playlist != null && gvSinglePlayList.SelectedRows.Count == 1)
      {
        Track track = (Track)(gvSinglePlayList.SelectedRows[0].DataBoundItem);
        if (track != null && track.Index > 0)
        {
          string message = playlistManager.moveTrack(selectedPlaylist.Key, track.Key, track.Index + _offset);
          if (!String.IsNullOrEmpty(message))
          {
            logMessage(String.Format("{0}: {1}", message, track.Title));
            // Reload the track list
            loadSinglePlaylist(playlist.Key, track.Key, true);
          }
        }
      }
    }

    #endregion


    #region PMS Server Helper methods

    private void loadMusicSections()
    {
      gvServerTrack.DataSource = null;
      tvServerSection.Nodes.Clear();
      // Add the root node
      TreeNode rootNode = tvServerSection.Nodes.Add(String.Format("{0} [{1}]", server.Name, server.baseUrl));

      foreach (LibrarySection section in server.musicSections())
      {
        TreeNode tn = rootNode.Nodes.Add(section.Key, section.Title);
        tn.Tag = section;
        loadSubSection(tn);
      }
      rootNode.ExpandAll();
    }

    private void loadSubSection(TreeNode _parentNode)
    {
      LibrarySection librarySection = (LibrarySection)_parentNode.Tag;
      if (librarySection != null)
      {
        List<LibrarySection> sections = server.librarySections(librarySection);
        if (!librarySection.HasTracks)
        {
          foreach (LibrarySection section in sections)
          {
            TreeNode tn = _parentNode.Nodes.Add(section.Key, section.Title);
            tn.Tag = section;
          }
        }
      }
    }

    private void enableServerTrackCommands()
    {
      Playlist playlist = selectedPlaylist;
      int tracksSelected = gvSinglePlayList.SelectedRows.Count;
      int tracksSelectedServer = gvServerTrack.SelectedRows.Count;
      btnServerTracksAppend.Enabled = tracksSelectedServer > 0 && playlist != null;
      btnServerTracksInsert.Enabled = tracksSelectedServer > 0 && playlist != null && tracksSelected == 1;
    }

    private void addSelectedTracks(bool _append)
    {
      Playlist playlist = selectedPlaylist;
      int atPosition = 0;
      string firstKey = null;

      if (playlist != null)
      {
        if (!_append && gvSinglePlayList.SelectedRows.Count == 1)
        {
          Track track = gvSinglePlayList.SelectedRows[0].DataBoundItem as Track;
          if (track != null)
          {
            atPosition = track.Index;
          }
        }
        bool trackAdded;
        int added = 0;
        foreach (DataGridViewRow row in gvServerTrack.SelectedRows)
        {
          LibrarySection section = row.DataBoundItem as LibrarySection;
          if (section != null)
          {
            logMessage(playlistManager.addTrack(playlist.Key, section.Key, atPosition, out trackAdded));
            if (trackAdded)
            {
              firstKey = firstKey ?? section.Key;
              added++;
              if (atPosition > 0)
              {
                atPosition++;
              }
            }
          }
        }
        logMessage(String.Format("{0} tracks added to the playlist", added));
        if (added > 0)
        {
          // Reload the playlist
          loadSinglePlaylist(playlist.Key, firstKey, true);
          gvPlaylists.Refresh();
        }
      }
    }

    private void changeUser(string _newName)
    {
      if (playlistManager.setCurrentUser(_newName))
      {
        loadedListKey = "";
        loadPlaylists();
      }
    }

    private void fillUserList()
    {
      comboUsers.Items.Clear();
      playlistManager.currentUserlist = playlistManager.userList();
      foreach (PLUser user in playlistManager.currentUserlist)
      {
        comboUsers.Items.Add(user.Name);
      }
      comboUsers.SelectedIndex = comboUsers.Items.IndexOf(playlistManager.currentUser.Name);
    }

    #endregion


    #region control event handlers

    private void gvPlaylists_SelectionChanged(object sender, EventArgs e)
    {
      Playlist playlist = selectedPlaylist;
      if (playlist != null)
      {
        tbPlaylistEditKey.Text = playlist.Key;
        tbPlaylistEditRenameTitle.Text = playlist.Title;
        tbPlaylistEditRenameDescription.Text = playlist.Description;
        loadSinglePlaylist(playlist.Key);
      } 
      enableEditCommands();
    }

    private void btnPlaylistAdd_Click(object sender, EventArgs e)
    {
      showPlaylistEditPanel(true, false);
    }

    private void btnPlaylistRename_Click(object sender, EventArgs e)
    {
      showPlaylistEditPanel(false, false);
    }

    private void btnPlaylistEditHide_Click(object sender, EventArgs e)
    {
      hidePlaylistEditPanel();
    }

    private void btnPlaylistEditCreate_Click(object sender, EventArgs e)
    {
      if (editIsUser)
      {
        if (playlistManager.setCurrentUser(tbPlaylistEditAddTitle.Text.Trim()))
        {
          gvPlaylists.DataSource = null;
          gvSinglePlayList.DataSource = null;
          loadedListKey = "";
          fillUserList();
        }
      }
      else
      {
        string newkey = playlistManager.createNewPlaylist(tbPlaylistEditAddTitle.Text.Trim(), tbPlaylistEditAddDescription.Text.Trim(), PlaylistManager.PLTypes.Simple);
        if (!String.IsNullOrEmpty(newkey))
        {
          tbPlaylistEditAddTitle.Text = "";
          tbPlaylistEditAddDescription.Text = "";
          // Reload the playlists and select the new one!
          loadPlaylists();
          if (gvPlaylists.Rows.Count > 0)
          {
            DataGridViewRow newRow = gvPlaylists.Rows.OfType<DataGridViewRow>().FirstOrDefault(row => ((Playlist)row.DataBoundItem).Key == newkey);
            if (newRow != null)
            {
              newRow.Selected = true;
            }
          }
        }
      }
    }

    private void btnPlaylistEditRename_Click(object sender, EventArgs e)
    {
      Playlist playlist = selectedPlaylist;
      if (playlist != null)
      {
        playlistManager.renamePlaylist(playlist.Key, tbPlaylistEditRenameTitle.Text.Trim());
      }
    }

    private void tbPlaylistEditAddTitle_TextChanged(object sender, EventArgs e)
    {
      enableEditCommands();
    }

    private void tbPlaylistEditRenameTitle_TextChanged(object sender, EventArgs e)
    {
      enableEditCommands();
    }

    private void btnPlaylistDelete_Click(object sender, EventArgs e)
    {
      Playlist playlist = selectedPlaylist;
      if (playlist != null)
      {
        if (MessageBox.Show("Delete the selected playlist", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          playlistManager.deletePlaylist(playlist.Key);
          // Reload the playlists
          loadPlaylists();
        }
      }
    }

    private void btnPlaylistEditRename_Click_1(object sender, EventArgs e)
    {
      Playlist playlist = selectedPlaylist;
      if (playlist != null)
      {
        playlistManager.renamePlaylist(playlist.Key, tbPlaylistEditRenameTitle.Text.Trim());
        playlist.Title = tbPlaylistEditRenameTitle.Text.Trim();
      }
    }

    private void gvSinglePlayList_SelectionChanged(object sender, EventArgs e)
    {
      enableTrackCommands();
    }

    private void btnTrackUp_Click(object sender, EventArgs e)
    {
      moveTrack(-1);
    }

    private void btnTrackDown_Click(object sender, EventArgs e)
    {
      moveTrack(1);
    }

    private void btTrackBrowseMusice_Click(object sender, EventArgs e)
    {
      splitContainerGlobal.Panel2Collapsed = !splitContainerGlobal.Panel2Collapsed;
    }

    private void tvServerSection_AfterSelect(object sender, TreeViewEventArgs e)
    {
      gvServerTrack.DataSource = null;
      if (e.Node != null && e.Node.Tag != null)
      {
        LibrarySection librarySection = e.Node.Tag as LibrarySection;
        if (librarySection != null)
        {
          if (!librarySection.HasTracks && e.Node.Nodes.Count == 0)
          {
            loadSubSection(e.Node);
            e.Node.Expand();
          }
          if (librarySection.HasTracks)
          {
            gvServerTrack.DataSource = server.librarySections(librarySection);
          }
        }
      }
    }

    private void gvServerTrack_SelectionChanged(object sender, EventArgs e)
    {
      enableServerTrackCommands();
    }

    private void btnServerTracksAppend_Click(object sender, EventArgs e)
    {
      addSelectedTracks(true);
    }

    private void btnServerTracksInsert_Click(object sender, EventArgs e)
    {
      addSelectedTracks(false);
    }

    private void btnTrackRemove_Click(object sender, EventArgs e)
    {
      Playlist playlist = selectedPlaylist;
      if (playlist != null && gvSinglePlayList.SelectedRows.Count > 0)
      {
        if (MessageBox.Show("Remove selected tracks from the playlist", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          foreach (DataGridViewRow row in gvSinglePlayList.SelectedRows)
          {
            Track track = row.DataBoundItem as Track;
            if (track != null)
            {
              playlistManager.removeTrack(playlist.Key, track.Key);
            }
          }
          loadSinglePlaylist(playlist.Key, "", true);
          gvPlaylists.Refresh();
        }
      }
    }

    private void comboUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
      string newName = comboUsers.SelectedItem.ToString();

      changeUser(newName);
    }

    private void btnUserAdd_Click(object sender, EventArgs e)
    {
      showPlaylistEditPanel(true, true);
    }

    #endregion

    private void btnUserDelete_Click(object sender, EventArgs e)
    {
      string userName = comboUsers.SelectedItem.ToString();

      if (playlistManager.isNormalUser(userName))
      {
        if (MessageBox.Show(String.Format("Remove user {0} and all associated playlists?", userName),
          "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          if (playlistManager.deleteCurrentUser(userName))
          {
            gvPlaylists.DataSource = null;
            gvSinglePlayList.DataSource = null;
            loadedListKey = "";
            fillUserList();
            loadPlaylists();
          }
        }
      }
    }
  }
}
