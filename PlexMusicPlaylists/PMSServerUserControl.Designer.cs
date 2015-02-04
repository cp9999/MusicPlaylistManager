namespace PlexMusicPlaylists
{
  partial class PMSServerUserControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PMSServerUserControl));
      this.panelCaption = new System.Windows.Forms.Panel();
      this.lblCaption = new System.Windows.Forms.Label();
      this.panelSever = new System.Windows.Forms.Panel();
      this.splitContainerServerMain = new System.Windows.Forms.SplitContainer();
      this.panelServerSection = new System.Windows.Forms.Panel();
      this.tvServerSection = new System.Windows.Forms.TreeView();
      this.panelServerSectionToolstrip = new System.Windows.Forms.Panel();
      this.toolStripServerSections = new System.Windows.Forms.ToolStrip();
      this.toolStripLabelServerSection = new System.Windows.Forms.ToolStripLabel();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.btnServerSectionSearch = new System.Windows.Forms.ToolStripDropDownButton();
      this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.panelServerTrack = new System.Windows.Forms.Panel();
      this.gvServerTrack = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panelServerTrackToolstrip = new System.Windows.Forms.Panel();
      this.toolStripServerTracks = new System.Windows.Forms.ToolStrip();
      this.toolStripLabelServerTracks = new System.Windows.Forms.ToolStripLabel();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
      this.btnServerTracksAppend = new System.Windows.Forms.ToolStripButton();
      this.btnServerTracksInsert = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.btnServerTrackAppendAll = new System.Windows.Forms.ToolStripButton();
      this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.librarySectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.panelCaption.SuspendLayout();
      this.panelSever.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerServerMain)).BeginInit();
      this.splitContainerServerMain.Panel1.SuspendLayout();
      this.splitContainerServerMain.Panel2.SuspendLayout();
      this.splitContainerServerMain.SuspendLayout();
      this.panelServerSection.SuspendLayout();
      this.panelServerSectionToolstrip.SuspendLayout();
      this.toolStripServerSections.SuspendLayout();
      this.panelServerTrack.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvServerTrack)).BeginInit();
      this.panelServerTrackToolstrip.SuspendLayout();
      this.toolStripServerTracks.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.librarySectionBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // panelCaption
      // 
      this.panelCaption.Controls.Add(this.lblCaption);
      this.panelCaption.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelCaption.Location = new System.Drawing.Point(0, 0);
      this.panelCaption.Name = "panelCaption";
      this.panelCaption.Size = new System.Drawing.Size(1207, 31);
      this.panelCaption.TabIndex = 0;
      // 
      // lblCaption
      // 
      this.lblCaption.BackColor = System.Drawing.Color.Moccasin;
      this.lblCaption.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCaption.Location = new System.Drawing.Point(0, 0);
      this.lblCaption.Name = "lblCaption";
      this.lblCaption.Size = new System.Drawing.Size(1207, 31);
      this.lblCaption.TabIndex = 1;
      this.lblCaption.Text = "PMS Server";
      this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // panelSever
      // 
      this.panelSever.Controls.Add(this.splitContainerServerMain);
      this.panelSever.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelSever.Location = new System.Drawing.Point(0, 31);
      this.panelSever.Name = "panelSever";
      this.panelSever.Size = new System.Drawing.Size(1207, 544);
      this.panelSever.TabIndex = 1;
      // 
      // splitContainerServerMain
      // 
      this.splitContainerServerMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainerServerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainerServerMain.Location = new System.Drawing.Point(0, 0);
      this.splitContainerServerMain.Name = "splitContainerServerMain";
      // 
      // splitContainerServerMain.Panel1
      // 
      this.splitContainerServerMain.Panel1.Controls.Add(this.panelServerSection);
      this.splitContainerServerMain.Panel1.Controls.Add(this.panelServerSectionToolstrip);
      // 
      // splitContainerServerMain.Panel2
      // 
      this.splitContainerServerMain.Panel2.Controls.Add(this.panelServerTrack);
      this.splitContainerServerMain.Panel2.Controls.Add(this.panelServerTrackToolstrip);
      this.splitContainerServerMain.Size = new System.Drawing.Size(1207, 544);
      this.splitContainerServerMain.SplitterDistance = 351;
      this.splitContainerServerMain.TabIndex = 2;
      // 
      // panelServerSection
      // 
      this.panelServerSection.Controls.Add(this.tvServerSection);
      this.panelServerSection.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelServerSection.Location = new System.Drawing.Point(0, 52);
      this.panelServerSection.Name = "panelServerSection";
      this.panelServerSection.Size = new System.Drawing.Size(351, 492);
      this.panelServerSection.TabIndex = 1;
      // 
      // tvServerSection
      // 
      this.tvServerSection.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvServerSection.Location = new System.Drawing.Point(0, 0);
      this.tvServerSection.Name = "tvServerSection";
      this.tvServerSection.Size = new System.Drawing.Size(351, 492);
      this.tvServerSection.TabIndex = 0;
      this.tvServerSection.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvServerSection_AfterSelect);
      // 
      // panelServerSectionToolstrip
      // 
      this.panelServerSectionToolstrip.Controls.Add(this.toolStripServerSections);
      this.panelServerSectionToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelServerSectionToolstrip.Location = new System.Drawing.Point(0, 0);
      this.panelServerSectionToolstrip.Name = "panelServerSectionToolstrip";
      this.panelServerSectionToolstrip.Size = new System.Drawing.Size(351, 52);
      this.panelServerSectionToolstrip.TabIndex = 0;
      // 
      // toolStripServerSections
      // 
      this.toolStripServerSections.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelServerSection,
            this.toolStripSeparator8,
            this.btnServerSectionSearch});
      this.toolStripServerSections.Location = new System.Drawing.Point(0, 0);
      this.toolStripServerSections.Name = "toolStripServerSections";
      this.toolStripServerSections.Size = new System.Drawing.Size(351, 25);
      this.toolStripServerSections.TabIndex = 0;
      this.toolStripServerSections.Text = "toolStrip2";
      // 
      // toolStripLabelServerSection
      // 
      this.toolStripLabelServerSection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
      this.toolStripLabelServerSection.Name = "toolStripLabelServerSection";
      this.toolStripLabelServerSection.Size = new System.Drawing.Size(128, 22);
      this.toolStripLabelServerSection.Text = "Server music sections";
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
      // 
      // btnServerSectionSearch
      // 
      this.btnServerSectionSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem});
      this.btnServerSectionSearch.Image = global::PlexMusicPlaylists.Properties.Resources._22;
      this.btnServerSectionSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnServerSectionSearch.Name = "btnServerSectionSearch";
      this.btnServerSectionSearch.Size = new System.Drawing.Size(71, 22);
      this.btnServerSectionSearch.Text = "Search";
      // 
      // searchToolStripMenuItem
      // 
      this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
      this.searchToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
      this.searchToolStripMenuItem.Text = "Search";
      this.searchToolStripMenuItem.Click += new System.EventHandler(this.serverSearch_Click);
      // 
      // panelServerTrack
      // 
      this.panelServerTrack.Controls.Add(this.gvServerTrack);
      this.panelServerTrack.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelServerTrack.Location = new System.Drawing.Point(0, 50);
      this.panelServerTrack.Name = "panelServerTrack";
      this.panelServerTrack.Size = new System.Drawing.Size(852, 494);
      this.panelServerTrack.TabIndex = 1;
      // 
      // gvServerTrack
      // 
      this.gvServerTrack.AllowUserToAddRows = false;
      this.gvServerTrack.AllowUserToDeleteRows = false;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.gvServerTrack.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.gvServerTrack.AutoGenerateColumns = false;
      this.gvServerTrack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvServerTrack.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
      this.gvServerTrack.DataSource = this.librarySectionBindingSource;
      this.gvServerTrack.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvServerTrack.Location = new System.Drawing.Point(0, 0);
      this.gvServerTrack.Name = "gvServerTrack";
      this.gvServerTrack.ReadOnly = true;
      dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.gvServerTrack.RowsDefaultCellStyle = dataGridViewCellStyle2;
      this.gvServerTrack.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvServerTrack.Size = new System.Drawing.Size(852, 494);
      this.gvServerTrack.TabIndex = 3;
      this.gvServerTrack.SelectionChanged += new System.EventHandler(this.gvServerTrack_SelectionChanged);
      // 
      // dataGridViewTextBoxColumn5
      // 
      this.dataGridViewTextBoxColumn5.DataPropertyName = "TrackType";
      this.dataGridViewTextBoxColumn5.HeaderText = "Type";
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn5.ReadOnly = true;
      this.dataGridViewTextBoxColumn5.Width = 60;
      // 
      // panelServerTrackToolstrip
      // 
      this.panelServerTrackToolstrip.Controls.Add(this.toolStripServerTracks);
      this.panelServerTrackToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelServerTrackToolstrip.Location = new System.Drawing.Point(0, 0);
      this.panelServerTrackToolstrip.Name = "panelServerTrackToolstrip";
      this.panelServerTrackToolstrip.Size = new System.Drawing.Size(852, 50);
      this.panelServerTrackToolstrip.TabIndex = 0;
      // 
      // toolStripServerTracks
      // 
      this.toolStripServerTracks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelServerTracks,
            this.toolStripSeparator6,
            this.toolStripLabel3,
            this.btnServerTracksAppend,
            this.btnServerTracksInsert,
            this.toolStripSeparator10,
            this.toolStripLabel2,
            this.btnServerTrackAppendAll});
      this.toolStripServerTracks.Location = new System.Drawing.Point(0, 0);
      this.toolStripServerTracks.Name = "toolStripServerTracks";
      this.toolStripServerTracks.Size = new System.Drawing.Size(852, 25);
      this.toolStripServerTracks.TabIndex = 0;
      this.toolStripServerTracks.Text = "toolStrip1";
      // 
      // toolStripLabelServerTracks
      // 
      this.toolStripLabelServerTracks.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
      this.toolStripLabelServerTracks.Name = "toolStripLabelServerTracks";
      this.toolStripLabelServerTracks.Size = new System.Drawing.Size(98, 22);
      this.toolStripLabelServerTracks.Text = "Tracks on server";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel3
      // 
      this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStripLabel3.Name = "toolStripLabel3";
      this.toolStripLabel3.Size = new System.Drawing.Size(53, 22);
      this.toolStripLabel3.Text = "Selected:";
      // 
      // btnServerTracksAppend
      // 
      this.btnServerTracksAppend.Image = global::PlexMusicPlaylists.Properties.Resources.down;
      this.btnServerTracksAppend.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnServerTracksAppend.Name = "btnServerTracksAppend";
      this.btnServerTracksAppend.Size = new System.Drawing.Size(123, 22);
      this.btnServerTracksAppend.Text = "Append to playlist";
      this.btnServerTracksAppend.Click += new System.EventHandler(this.btnServerTracksAppend_Click);
      // 
      // btnServerTracksInsert
      // 
      this.btnServerTracksInsert.Image = global::PlexMusicPlaylists.Properties.Resources.left;
      this.btnServerTracksInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnServerTracksInsert.Name = "btnServerTracksInsert";
      this.btnServerTracksInsert.Size = new System.Drawing.Size(109, 22);
      this.btnServerTracksInsert.Text = "Insert in playlist";
      this.btnServerTracksInsert.Click += new System.EventHandler(this.btnServerTracksInsert_Click);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(24, 22);
      this.toolStripLabel2.Text = "All:";
      // 
      // btnServerTrackAppendAll
      // 
      this.btnServerTrackAppendAll.Image = ((System.Drawing.Image)(resources.GetObject("btnServerTrackAppendAll.Image")));
      this.btnServerTrackAppendAll.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnServerTrackAppendAll.Name = "btnServerTrackAppendAll";
      this.btnServerTrackAppendAll.Size = new System.Drawing.Size(84, 22);
      this.btnServerTrackAppendAll.Text = "Append all";
      this.btnServerTrackAppendAll.Click += new System.EventHandler(this.btnServerTrackAppendAll_Click);
      // 
      // dataGridViewTextBoxColumn4
      // 
      this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn4.DataPropertyName = "Title";
      this.dataGridViewTextBoxColumn4.HeaderText = "Title";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn6
      // 
      this.dataGridViewTextBoxColumn6.DataPropertyName = "Key";
      this.dataGridViewTextBoxColumn6.HeaderText = "Key";
      this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      this.dataGridViewTextBoxColumn6.ReadOnly = true;
      // 
      // librarySectionBindingSource
      // 
      this.librarySectionBindingSource.DataSource = typeof(PlexMusicPlaylists.PlexMediaServer.LibrarySection);
      // 
      // PMSServerUserControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panelSever);
      this.Controls.Add(this.panelCaption);
      this.Name = "PMSServerUserControl";
      this.Size = new System.Drawing.Size(1207, 575);
      this.panelCaption.ResumeLayout(false);
      this.panelSever.ResumeLayout(false);
      this.splitContainerServerMain.Panel1.ResumeLayout(false);
      this.splitContainerServerMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerServerMain)).EndInit();
      this.splitContainerServerMain.ResumeLayout(false);
      this.panelServerSection.ResumeLayout(false);
      this.panelServerSectionToolstrip.ResumeLayout(false);
      this.panelServerSectionToolstrip.PerformLayout();
      this.toolStripServerSections.ResumeLayout(false);
      this.toolStripServerSections.PerformLayout();
      this.panelServerTrack.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvServerTrack)).EndInit();
      this.panelServerTrackToolstrip.ResumeLayout(false);
      this.panelServerTrackToolstrip.PerformLayout();
      this.toolStripServerTracks.ResumeLayout(false);
      this.toolStripServerTracks.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.librarySectionBindingSource)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panelCaption;
    private System.Windows.Forms.Panel panelSever;
    private System.Windows.Forms.SplitContainer splitContainerServerMain;
    private System.Windows.Forms.Panel panelServerSection;
    private System.Windows.Forms.TreeView tvServerSection;
    private System.Windows.Forms.Panel panelServerSectionToolstrip;
    private System.Windows.Forms.ToolStrip toolStripServerSections;
    private System.Windows.Forms.ToolStripLabel toolStripLabelServerSection;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripDropDownButton btnServerSectionSearch;
    private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
    private System.Windows.Forms.Panel panelServerTrack;
    private System.Windows.Forms.DataGridView gvServerTrack;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private System.Windows.Forms.BindingSource librarySectionBindingSource;
    private System.Windows.Forms.Panel panelServerTrackToolstrip;
    private System.Windows.Forms.ToolStrip toolStripServerTracks;
    private System.Windows.Forms.ToolStripLabel toolStripLabelServerTracks;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    private System.Windows.Forms.ToolStripButton btnServerTracksAppend;
    private System.Windows.Forms.ToolStripButton btnServerTracksInsert;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripButton btnServerTrackAppendAll;
    private System.Windows.Forms.Label lblCaption;
  }
}
