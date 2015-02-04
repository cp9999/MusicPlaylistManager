using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlexMusicPlaylists.PlexMediaServer;

namespace PlexMusicPlaylists
{
  public partial class PMSServerUserControl : UserControl
  {
    public delegate bool SearchInputHandler(SearchSection _searchSection, out string _query);
    public event SearchInputHandler OnSearchInput;
    public delegate void AddTracksEventHandler(IEnumerable<DataGridViewRow> _tracks, bool _append, string _confirmMessage);
    public event AddTracksEventHandler OnAddTracksEventHandler;
    private bool m_playlistSelected = false;
    private bool m_singleTrackSelected = false;
    public PMSServer PMSServer { get;  set; }
    public bool AllowVideos { get; set; }
    bool plexConnected = false;
    protected string caption = "PMS Server";

    public PMSServerUserControl()
    {
      InitializeComponent();
    }

    public Orientation Orientation
    {
      get { return splitContainerServerMain.Orientation; }
      set { splitContainerServerMain.Orientation = value; }
    }

    #region helper methods
    public bool isConnectedToPlaylist
    {
      get
      {
        return OnAddTracksEventHandler != null;
      }
    }
    public void connectedToPlaylist(PlaylistUserControl _playlistUserControl)
    {
      if (_playlistUserControl != null)
      {
        _playlistUserControl.OnEnableServerTrackCommandsHandler += new PlaylistUserControl.EnableServerTrackCommandsHandler(enableServerTrackCommandsHandler);
        _playlistUserControl.OnServerConnectionChanged += new PlaylistUserControl.ServerConnectionChanged(serverConnectionChanged);
        this.checkPMSConnection();
      }
    }
    public void disconnectedFromPlaylist(PlaylistUserControl _playlistUserControl)
    {
      if (_playlistUserControl != null)
      {
        _playlistUserControl.OnEnableServerTrackCommandsHandler -= new PlaylistUserControl.EnableServerTrackCommandsHandler(enableServerTrackCommandsHandler);
        _playlistUserControl.OnServerConnectionChanged -= new PlaylistUserControl.ServerConnectionChanged(serverConnectionChanged);
        PMSServer = null;
        this.checkPMSConnection();
      }
    }
    public void linkToSplitter(SplitContainer _splitContainer, int _toolstripHeight)
    {
      if (_splitContainer != null && splitContainerServerMain.Orientation == _splitContainer.Orientation)
      {
        panelCaption.Visible = false;
        panelServerSectionToolstrip.Height = _toolstripHeight;
        panelServerTrackToolstrip.Height = _toolstripHeight;
        splitContainerServerMain.SplitterDistance = _splitContainer.SplitterDistance;
      }
    }

    private void serverConnectionChanged(bool _connected)
    {
      checkPMSConnection();
    }

    public void checkPMSConnection()
    {
      plexConnected = false;
      if (PMSServer != null)
      try
      {
        updateCaption();
        plexConnected = true;
        loadMainSections();
        enableServerSectionCommands();
      }
      catch (Exception ex)
      {
      }
      toolStripServerSections.Enabled = plexConnected;
      toolStripServerTracks.Enabled = plexConnected;
    }

    private void updateCaption()
    {
      lblCaption.Text = String.Format("{0}: {1} [{2}]", caption, PMSServer.Name, PMSServer.baseUrl);
    }


    private void enableServerSectionCommands()
    {
      MainSection section = selectedServerSection as MainSection;
      btnServerSectionSearch.Enabled = OnSearchInput != null && section != null && section.canBeSearched;
      if (btnServerSectionSearch.Enabled && btnServerSectionSearch.Tag != section)
      {
        btnServerSectionSearch.DropDownItems.Clear();
        foreach (SearchSection search in section.searchSections)
        {
          ToolStripMenuItem tsi = (ToolStripMenuItem)btnServerSectionSearch.DropDownItems.Add(search.Title);
          tsi.Tag = search;
          tsi.Click += new EventHandler(serverSearch_Click);
        }
      }
    }

    private LibrarySection selectedServerSection
    {
      get
      {
        return tvServerSection.SelectedNode != null ? (LibrarySection)tvServerSection.SelectedNode.Tag : null;
      }
    }

    private void addAllTracks(bool _append)
    {
      if (OnAddTracksEventHandler != null)
      {
        OnAddTracksEventHandler(gvServerTrack.Rows.Cast<DataGridViewRow>(), _append, "All tracks");
      }
    }

    private void addSelectedTracks(bool _append)
    {
      if (OnAddTracksEventHandler != null)
      {
        List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();
        foreach (DataGridViewRow selectedRow in gvServerTrack.SelectedRows)
        {
          selectedRows.Add(selectedRow);
        }
        OnAddTracksEventHandler(selectedRows, _append, "");
      }
    }

    private void enableServerTrackCommandsHandler(bool _playlistSelected, bool _singleTrackSelected)
    {
      m_playlistSelected = _playlistSelected;
      m_singleTrackSelected = _singleTrackSelected;
      enableServerTrackCommands();
    }

    private void enableServerTrackCommands()
    {
      int tracksSelectedServer = gvServerTrack.SelectedRows.Count;
      btnServerTracksAppend.Enabled = tracksSelectedServer > 0 && m_playlistSelected;
      btnServerTracksInsert.Enabled = tracksSelectedServer > 0 && m_playlistSelected && m_singleTrackSelected;
      btnServerTrackAppendAll.Enabled = gvServerTrack.Rows.Count > 0;
    }

    private void loadMainSections()
    {
      gvServerTrack.DataSource = null;
      tvServerSection.Nodes.Clear();
      // Add the root node
      TreeNode rootNode = tvServerSection.Nodes.Add(String.Format("{0} [{1}]", PMSServer.Name, PMSServer.baseUrl));

      foreach (LibrarySection section in PMSServer.musicSections())
      {
        TreeNode tn = rootNode.Nodes.Add(section.Key, section.Title);
        tn.Tag = section;
        loadSubSection(tn);
      }

      if (AllowVideos)
      {
        foreach (LibrarySection section in PMSServer.movieSections())
        {
          TreeNode tn = rootNode.Nodes.Add(section.Key, section.Title);
          tn.Tag = section;
          loadSubSection(tn);
        }
      }
      rootNode.ExpandAll();
    }

    private void loadSubSection(TreeNode _parentNode)
    {
      LibrarySection librarySection = (LibrarySection)_parentNode.Tag;
      if (librarySection != null)
      {
        List<LibrarySection> sections = PMSServer.librarySections(librarySection);
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

    #endregion

    private void btnServerTracksAppend_Click(object sender, EventArgs e)
    {
      addSelectedTracks(true);
    }

    private void btnServerTracksInsert_Click(object sender, EventArgs e)
    {
      addSelectedTracks(false);
    }

    private void btnServerTrackAppendAll_Click(object sender, EventArgs e)
    {
      addAllTracks(true);
    }

    private void gvServerTrack_SelectionChanged(object sender, EventArgs e)
    {
      enableServerTrackCommands();
    }

    void serverSearch_Click(object sender, EventArgs e)
    {
      MainSection section = selectedServerSection as MainSection;
      ToolStripMenuItem tsi = sender as ToolStripMenuItem;
      SearchSection searchSection = tsi != null ? tsi.Tag as SearchSection : null;
      if (OnSearchInput != null && section != null && searchSection != null)
      {
        // Show search dialog
        string query = "";
        if (OnSearchInput(searchSection, out query))
        {
          LibrarySection librarySection = searchSection.createFromSearch(query);
          if (librarySection != null)
          {
            librarySection.IsMusic = section.IsMusic;
            // Add a new child node to the musicsection node
            TreeNode tn = tvServerSection.SelectedNode.Nodes.Add(librarySection.Key, librarySection.Title);
            tn.Tag = librarySection;
            // Select the newly added node in the tree
            tvServerSection.SelectedNode = tn;
          }
        }
      }
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
            gvServerTrack.DataSource = PMSServer.librarySections(librarySection);
          }
        }
      }
      enableServerSectionCommands();
    }

  }
}
