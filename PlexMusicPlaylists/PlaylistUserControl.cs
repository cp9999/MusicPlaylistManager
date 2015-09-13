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
using PlexMusicPlaylists.Import;

namespace PlexMusicPlaylists
{
  public partial class PlaylistUserControl : UserControl
  {
    public event PMSServerUserControl.SearchInputHandler OnSearchInput;
    public delegate void LogMessageEventHandler(string _message);
    public event LogMessageEventHandler OnLogMessage;
    public delegate void EnableServerTrackCommandsHandler(bool _playlistSelected, bool _singleTrackSelected);
    public event EnableServerTrackCommandsHandler OnEnableServerTrackCommandsHandler;
    public delegate void ServerConnectionChanged(bool _connected);
    public event ServerConnectionChanged OnServerConnectionChanged;

    #region data members
    PlaylistManager playlistManager = new PlaylistManager("", 32400);
    PMSServer server = new PMSServer("", 32400);
    ImportManager importManager = new ImportManager();
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
    //public bool Hori
    #endregion

    #region public methods

    public PlaylistUserControl()
    {
      InitializeComponent();
      ucPMSServerUserControl.OnSearchInput += new PMSServerUserControl.SearchInputHandler(doSearchInput);
      importManager.PlaylistManager = playlistManager;
      importManager.PMSServer = server;
      playlistManager.PlexPlaylistCreator.OnLogMessage += logMessage;
      initPlaylistControls();
      if (playlistManager.playlistMode == PlaylistManager.PlaylistMode.PlexNative)
      {
        loadedListKey = "";
        fillUserList();
        loadPlaylists();
        enableEditCommands();
      }
    }

    public bool setPlexMediaServerIP(string _ip, int _port)
    {
      plexConnected = false;
      playlistManager.IP = _ip;
      playlistManager.Port = _port;
      server.IP = _ip;
      server.Port = _port;
      btnUserAdd.Visible = playlistManager.allowUserMaintenance;
      btnUserDelete.Visible = playlistManager.allowUserMaintenance;
      server.validateConnection();
      try
      {
        updateCaption();
        plexConnected = true;
        loadedListKey = "";
        playlistManager.loadPreferences();
        fillUserList();
        loadPlaylists();
        //loadMainSections();
        enableEditCommands();
        enableTrackCommands();
        //enableServerSectionCommands();
        this.triggerEnableServerTrackCommands();
      }
      catch (Exception ex)
      {
        plexConnected = false;
        logMessage(ex.Message);
        MessageBox.Show(ex.Message);
      }
      if (OnServerConnectionChanged != null)
        OnServerConnectionChanged(plexConnected);
      return plexConnected;
    }
    public void settingsChanged()
    {
      playlistManager.PlexDatabaseName = PlaylistSettings.theSettings().PlaylistDB;
      if (playlistManager.playlistMode != PlaylistSettings.theSettings().GUIPlaylistMode)
      {
        playlistManager.playlistMode = PlaylistSettings.theSettings().GUIPlaylistMode;
        if (!plexConnected && playlistManager.playlistMode == PlaylistManager.PlaylistMode.ChannelMusicPlaylist)
        {
          // Clear the lists
          loadedListKey = "";
          comboUsers.Items.Clear();
          gvPlaylists.DataSource = null;
          gvSinglePlayList.DataSource = null;
        }
        if (plexConnected || playlistManager.playlistMode == PlaylistManager.PlaylistMode.PlexNative)
        {
          loadedListKey = "";
          fillUserList();
          loadPlaylists();
        }
      }
      this.updateServerPanel(false);
      btnUserAdd.Visible = playlistManager.allowUserMaintenance;
      btnUserDelete.Visible = playlistManager.allowUserMaintenance;
      enableEditCommands();
    }
    public void connectPMSServer(PMSServerUserControl _PMSServerUserControl, bool _linkSplitter = false)
    {
      if (_PMSServerUserControl != null && !_PMSServerUserControl.isConnectedToPlaylist)
      {
        _PMSServerUserControl.OnAddTracksEventHandler += new PMSServerUserControl.AddTracksEventHandler(addTracksFromList);
        _PMSServerUserControl.AllowVideos = playlistManager.Preferences.AllowVideos;
        _PMSServerUserControl.PMSServer = server;
        if (_linkSplitter)
        {
          _PMSServerUserControl.linkToSplitter(splitContainerMain, panelPlaylistToolStrip.Height);
        }
        _PMSServerUserControl.connectedToPlaylist(this);
        this.triggerEnableServerTrackCommands();
      }
    }
    public void disconnectPMSServer(PMSServerUserControl _PMSServerUserControl)
    {
      if (_PMSServerUserControl != null && _PMSServerUserControl.isConnectedToPlaylist)
      {
        _PMSServerUserControl.OnAddTracksEventHandler -= new PMSServerUserControl.AddTracksEventHandler(addTracksFromList);
        _PMSServerUserControl.disconnectedFromPlaylist(this);
      }
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
    private void updateServerPanel(bool _force = false)
    {
      if (_force || splitContainerGlobal.Panel2Collapsed != PlaylistSettings.theSettings().ServerInSeparatedWindow)
      {
        if (PlaylistSettings.theSettings().ServerInSeparatedWindow)
        {
          this.disconnectPMSServer(ucPMSServerUserControl);
        }
        else
        {
          this.connectPMSServer(ucPMSServerUserControl, true);
        }
        splitContainerGlobal.Panel2Collapsed = PlaylistSettings.theSettings().ServerInSeparatedWindow;
      }
    }
    private void initPlaylistControls()
    {
      this.updateServerPanel(true);
      btnPlaylistEditRename.Top = btnPlaylistEditCreate.Top;
      btnPlaylistEditRename.Left = btnPlaylistEditCreate.Left;
      tbPlaylistEditRenameTitle.Top = tbPlaylistEditAddTitle.Top;
      tbPlaylistEditRenameTitle.Left = tbPlaylistEditAddTitle.Left;
      tbPlaylistEditRenameDescription.Top = tbPlaylistEditAddDescription.Top;
      tbPlaylistEditRenameDescription.Left = tbPlaylistEditAddDescription.Left;
      hidePlaylistEditPanel();
      panelTrackToolstrip.Height = panelPlaylistToolStrip.Height;
      enableEditCommands();
      triggerEnableServerTrackCommands();
    }
    private void loadPlaylists()
    {
      gvPlaylists.DataSource = null;
      playlistManager.currentAllPlaylists = playlistManager.allPlaylists(false);
      gvPlaylists.DataSource = new BindingList<Playlist>(
        playlistManager.currentAllPlaylists.Where(pl => pl.AccountId == playlistManager.currentUser.Id).ToList());
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
    private void loadSinglePlaylist(Playlist _playlist)
    {
      loadSinglePlaylist(_playlist, "", false);
    }
    private void loadSinglePlaylist(Playlist _playlist, string _selectkey, bool _force)
    {
      if (_playlist != null && (_force || !loadedListKey.Equals(_playlist.Key)))
      {
        loadedListKey = _playlist.Key;
        playlistManager.currentTrackList = playlistManager.singlePlaylist(_playlist, false);
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
      btnImport.Enabled = plexConnected;
      btnPlaylistAdd.Enabled = plexConnected;
      btnPlaylistRename.Enabled = plexConnected;
      btnUserAdd.Enabled = plexConnected;
      toolStripSinglePlaylist.Enabled = plexConnected;
      if (editIsUser)
      {
        btnPlaylistEditCreate.Enabled = !String.IsNullOrEmpty(tbPlaylistEditAddTitle.Text) && !playlistManager.userExist(tbPlaylistEditAddTitle.Text);
      }
      else
      {
        btnPlaylistEditCreate.Enabled = !String.IsNullOrEmpty(tbPlaylistEditAddTitle.Text) && tbPlaylistEditAddTitle.Text.Trim() != String.Empty;
      }
      Playlist playlist = selectedPlaylist;
      btnPlaylistEditRename.Enabled = playlist != null && playlist.canRenameTo(tbPlaylistEditRenameTitle.Text, tbPlaylistEditRenameDescription.Text);
      btnPlaylistDelete.Enabled = plexConnected && playlistBindingSource != null;
      btnUserDelete.Enabled = plexConnected && playlistManager.isNormalUser(playlistManager.currentUser.Name);
      saveSelectedToolStripMenuItem.Enabled = playlist != null;
      btnImportChannelPlaylist.Enabled = plexConnected && playlistManager.MusicPlaylistsAvailable && playlistManager.playlistMode == PlaylistManager.PlaylistMode.PlexNative;
      btnPlaylistSaveToSql.Enabled = playlistManager.playlistMode == PlaylistManager.PlaylistMode.PlexNative &&
        (PlaylistSettings.theSettings().DatabaseCreateSqlFiles || PlaylistSettings.theSettings().DatabaseDirectUpdate);
    }
    private void enableTrackCommands()
    {
      int tracksSelected = gvSinglePlayList.SelectedRows.Count;
      btnTrackUp.Enabled = tracksSelected == 1 && gvSinglePlayList.Rows.IndexOf(gvSinglePlayList.SelectedRows[0]) > 0;
      btnTrackDown.Enabled = tracksSelected == 1 && gvSinglePlayList.Rows.IndexOf(gvSinglePlayList.SelectedRows[0]) < gvSinglePlayList.Rows.Count - 1;
      btnTrackRemove.Enabled = tracksSelected > 0;
      triggerEnableServerTrackCommands();
    }
    private void triggerEnableServerTrackCommands()
    {
      if (OnEnableServerTrackCommandsHandler != null)
      {
        Playlist playlist = selectedPlaylist;
        int tracksSelected = gvSinglePlayList.SelectedRows.Count;
        OnEnableServerTrackCommandsHandler(playlist != null, tracksSelected == 1);
      }
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
            loadSinglePlaylist(playlist, track.Key, true);
          }
        }
      }
    }
    private void addTracksFromList(IEnumerable<DataGridViewRow> _tracks, bool _append, string _confirmMessage = "")
    {
      Playlist playlist = selectedPlaylist;

      if (playlist != null && _tracks != null && _tracks.Count() > 0 &&
        (String.IsNullOrEmpty(_confirmMessage) || MessageBox.Show(String.Format("{0} will be added to the selected playlist!", _confirmMessage), "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes))
      {
        int atPosition = 0;
        string firstKey = null;
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
        foreach (DataGridViewRow row in _tracks.OrderBy(r => r.Index))
        {
          LibrarySection section = row.DataBoundItem as LibrarySection;
          if (section != null)
          {
            logMessage(playlistManager.addTrack(playlist.Key, section.Key, section.TrackType, atPosition, out trackAdded));
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
          loadSinglePlaylist(playlist, firstKey, true);
          gvPlaylists.Refresh();
        }
      }
    }
    private bool doSearchInput(PlexMediaServer.SearchSection _searchSection, out string _query)
    {
      _query = "";
      return (OnSearchInput != null && OnSearchInput(_searchSection, out _query));
    }

    #endregion

    #region user helper methods
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
      playlistManager.currentUserlist = playlistManager.userList(false);
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
        loadSinglePlaylist(playlist);
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

    private void tbPlaylistEditRenameDescription_TextChanged(object sender, EventArgs e)
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
        playlistManager.renamePlaylist(playlist.Key, tbPlaylistEditRenameTitle.Text.Trim(), tbPlaylistEditRenameDescription.Text.Trim());
        playlist.Title = tbPlaylistEditRenameTitle.Text.Trim();
        playlist.Description = tbPlaylistEditRenameDescription.Text.Trim();
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
              logMessage(playlistManager.removeTrack(playlist.Key, track.Key));
            }
          }
          loadSinglePlaylist(playlist, "", true);
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
      if (playlistManager.allowUserMaintenance)
      {
        showPlaylistEditPanel(true, true);
      }
    }

    private void btnUserDelete_Click(object sender, EventArgs e)
    {
      if (playlistManager.allowUserMaintenance)
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

    private void btnImport_Click(object sender, EventArgs e)
    {
      if (importManager != null)
      {
        string newKey = importManager.showImport();
        if (!String.IsNullOrEmpty(newKey))
        {
          tbPlaylistEditAddTitle.Text = "";
          tbPlaylistEditAddDescription.Text = "";
          // Reload the playlists and select the new one!
          loadPlaylists();
          if (gvPlaylists.Rows.Count > 0)
          {
            DataGridViewRow newRow = gvPlaylists.Rows.OfType<DataGridViewRow>().FirstOrDefault(row => ((Playlist)row.DataBoundItem).Key == newKey);
            if (newRow != null)
            {
              newRow.Selected = true;
            }
          }
        }
      }
    }

    private void btnImportChannelPlaylist_Click(object sender, EventArgs e)
    {
      ImportFormChannel importFormChannel = new ImportFormChannel();
      importFormChannel.setPlaylistManager(playlistManager);
      if (importFormChannel.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        string newKey = importFormChannel.LastCreatedKey;
        if (!String.IsNullOrEmpty(newKey))
        {
          // Reload the playlists and select the new one!
          loadPlaylists();
          if (gvPlaylists.Rows.Count > 0)
          {
            DataGridViewRow newRow = gvPlaylists.Rows.OfType<DataGridViewRow>().FirstOrDefault(row => ((Playlist)row.DataBoundItem).Key == newKey);
            if (newRow != null)
            {
              newRow.Selected = true;
            }
          }
        }
      }
    }

    private void saveSelectedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (selectedPlaylist != null)
      {
        playlistManager.PlexPlaylistCreator.generatePlaylistSql(selectedPlaylist);
      }
    }

    private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      playlistManager.PlexPlaylistCreator.generatePlaylistSql();
    }

    #endregion
  }
}
