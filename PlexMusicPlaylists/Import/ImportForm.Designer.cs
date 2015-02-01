namespace PlexMusicPlaylists.Import
{
  partial class ImportForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
      this.panelTop = new System.Windows.Forms.Panel();
      this.panelTopRight = new System.Windows.Forms.Panel();
      this.panelProgress = new System.Windows.Forms.Panel();
      this.labelProgressSub = new System.Windows.Forms.Label();
      this.labelProgress = new System.Windows.Forms.Label();
      this.gbMatchDetails = new System.Windows.Forms.GroupBox();
      this.tbNumberMatched = new System.Windows.Forms.TextBox();
      this.tbNumberOfTracks = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.panelTopLeft = new System.Windows.Forms.Panel();
      this.gbNewPlaylist = new System.Windows.Forms.GroupBox();
      this.lblPlaylistEditDescription = new System.Windows.Forms.Label();
      this.lblPlaylistEditTitle = new System.Windows.Forms.Label();
      this.tbPlaylistTitle = new System.Windows.Forms.TextBox();
      this.tbPlaylistDescription = new System.Windows.Forms.TextBox();
      this.gbFileSelect = new System.Windows.Forms.GroupBox();
      this.comboImportFormat = new System.Windows.Forms.ComboBox();
      this.btnOpenFile = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.tbImportFile = new System.Windows.Forms.TextBox();
      this.panelImport = new System.Windows.Forms.Panel();
      this.panelImportDetail = new System.Windows.Forms.Panel();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panelImportBottom = new System.Windows.Forms.Panel();
      this.btnClose = new System.Windows.Forms.Button();
      this.panelImportDetailGrid = new System.Windows.Forms.Panel();
      this.gvImportList = new System.Windows.Forms.DataGridView();
      this.MatchIcon = new System.Windows.Forms.DataGridViewImageColumn();
      this.Matched = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.MatchedOnTitleCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.artistDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.fileNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.MainSectionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.PMSFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.FullPlexFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.importEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.panelImportToolstrip = new System.Windows.Forms.Panel();
      this.toolStripImport = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel = new System.Windows.Forms.ToolStripLabel();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.btnMatchFolder = new System.Windows.Forms.ToolStripButton();
      this.btnMatchTitle = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.btnSelectMatch = new System.Windows.Forms.ToolStripButton();
      this.btnSearchSelected = new System.Windows.Forms.ToolStripButton();
      this.btnSwitchTitleArtist = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.btnCreate = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
      this.btnSectionLocation = new System.Windows.Forms.ToolStripButton();
      this.ofdImportFile = new System.Windows.Forms.OpenFileDialog();
      this.ep = new System.Windows.Forms.ErrorProvider(this.components);
      this.panelTop.SuspendLayout();
      this.panelTopRight.SuspendLayout();
      this.panelProgress.SuspendLayout();
      this.gbMatchDetails.SuspendLayout();
      this.panelTopLeft.SuspendLayout();
      this.gbNewPlaylist.SuspendLayout();
      this.gbFileSelect.SuspendLayout();
      this.panelImport.SuspendLayout();
      this.panelImportDetail.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.panelImportBottom.SuspendLayout();
      this.panelImportDetailGrid.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvImportList)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.importEntryBindingSource)).BeginInit();
      this.panelImportToolstrip.SuspendLayout();
      this.toolStripImport.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ep)).BeginInit();
      this.SuspendLayout();
      // 
      // panelTop
      // 
      this.panelTop.Controls.Add(this.panelTopRight);
      this.panelTop.Controls.Add(this.panelTopLeft);
      this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelTop.Location = new System.Drawing.Point(0, 0);
      this.panelTop.Name = "panelTop";
      this.panelTop.Size = new System.Drawing.Size(1233, 162);
      this.panelTop.TabIndex = 0;
      // 
      // panelTopRight
      // 
      this.panelTopRight.Controls.Add(this.panelProgress);
      this.panelTopRight.Controls.Add(this.gbMatchDetails);
      this.panelTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelTopRight.Location = new System.Drawing.Point(668, 0);
      this.panelTopRight.Name = "panelTopRight";
      this.panelTopRight.Size = new System.Drawing.Size(565, 162);
      this.panelTopRight.TabIndex = 5;
      // 
      // panelProgress
      // 
      this.panelProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.panelProgress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panelProgress.Controls.Add(this.labelProgressSub);
      this.panelProgress.Controls.Add(this.labelProgress);
      this.panelProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelProgress.Location = new System.Drawing.Point(0, 101);
      this.panelProgress.Name = "panelProgress";
      this.panelProgress.Size = new System.Drawing.Size(565, 61);
      this.panelProgress.TabIndex = 5;
      // 
      // labelProgressSub
      // 
      this.labelProgressSub.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.labelProgressSub.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelProgressSub.Location = new System.Drawing.Point(0, 34);
      this.labelProgressSub.Name = "labelProgressSub";
      this.labelProgressSub.Size = new System.Drawing.Size(561, 23);
      this.labelProgressSub.TabIndex = 1;
      this.labelProgressSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelProgress
      // 
      this.labelProgress.Dock = System.Windows.Forms.DockStyle.Top;
      this.labelProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelProgress.Location = new System.Drawing.Point(0, 0);
      this.labelProgress.Name = "labelProgress";
      this.labelProgress.Size = new System.Drawing.Size(561, 30);
      this.labelProgress.TabIndex = 0;
      this.labelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // gbMatchDetails
      // 
      this.gbMatchDetails.Controls.Add(this.tbNumberMatched);
      this.gbMatchDetails.Controls.Add(this.tbNumberOfTracks);
      this.gbMatchDetails.Controls.Add(this.label3);
      this.gbMatchDetails.Controls.Add(this.label2);
      this.gbMatchDetails.Dock = System.Windows.Forms.DockStyle.Top;
      this.gbMatchDetails.Location = new System.Drawing.Point(0, 0);
      this.gbMatchDetails.Name = "gbMatchDetails";
      this.gbMatchDetails.Size = new System.Drawing.Size(565, 66);
      this.gbMatchDetails.TabIndex = 3;
      this.gbMatchDetails.TabStop = false;
      this.gbMatchDetails.Text = "Match details";
      // 
      // tbNumberMatched
      // 
      this.tbNumberMatched.Location = new System.Drawing.Point(263, 22);
      this.tbNumberMatched.Name = "tbNumberMatched";
      this.tbNumberMatched.ReadOnly = true;
      this.tbNumberMatched.Size = new System.Drawing.Size(76, 20);
      this.tbNumberMatched.TabIndex = 4;
      // 
      // tbNumberOfTracks
      // 
      this.tbNumberOfTracks.Location = new System.Drawing.Point(115, 22);
      this.tbNumberOfTracks.Name = "tbNumberOfTracks";
      this.tbNumberOfTracks.ReadOnly = true;
      this.tbNumberOfTracks.Size = new System.Drawing.Size(76, 20);
      this.tbNumberOfTracks.TabIndex = 4;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(208, 26);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(49, 13);
      this.label3.TabIndex = 0;
      this.label3.Text = "Matched";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(21, 25);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(88, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "Number of tracks";
      // 
      // panelTopLeft
      // 
      this.panelTopLeft.Controls.Add(this.gbNewPlaylist);
      this.panelTopLeft.Controls.Add(this.gbFileSelect);
      this.panelTopLeft.Dock = System.Windows.Forms.DockStyle.Left;
      this.panelTopLeft.Location = new System.Drawing.Point(0, 0);
      this.panelTopLeft.Name = "panelTopLeft";
      this.panelTopLeft.Size = new System.Drawing.Size(668, 162);
      this.panelTopLeft.TabIndex = 4;
      // 
      // gbNewPlaylist
      // 
      this.gbNewPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.gbNewPlaylist.Controls.Add(this.lblPlaylistEditDescription);
      this.gbNewPlaylist.Controls.Add(this.lblPlaylistEditTitle);
      this.gbNewPlaylist.Controls.Add(this.tbPlaylistTitle);
      this.gbNewPlaylist.Controls.Add(this.tbPlaylistDescription);
      this.gbNewPlaylist.Location = new System.Drawing.Point(0, 81);
      this.gbNewPlaylist.Name = "gbNewPlaylist";
      this.gbNewPlaylist.Size = new System.Drawing.Size(668, 81);
      this.gbNewPlaylist.TabIndex = 3;
      this.gbNewPlaylist.TabStop = false;
      this.gbNewPlaylist.Text = "New playlist";
      // 
      // lblPlaylistEditDescription
      // 
      this.lblPlaylistEditDescription.AutoSize = true;
      this.lblPlaylistEditDescription.Location = new System.Drawing.Point(15, 48);
      this.lblPlaylistEditDescription.Name = "lblPlaylistEditDescription";
      this.lblPlaylistEditDescription.Size = new System.Drawing.Size(60, 13);
      this.lblPlaylistEditDescription.TabIndex = 5;
      this.lblPlaylistEditDescription.Text = "Description";
      // 
      // lblPlaylistEditTitle
      // 
      this.lblPlaylistEditTitle.AutoSize = true;
      this.lblPlaylistEditTitle.Location = new System.Drawing.Point(15, 22);
      this.lblPlaylistEditTitle.Name = "lblPlaylistEditTitle";
      this.lblPlaylistEditTitle.Size = new System.Drawing.Size(27, 13);
      this.lblPlaylistEditTitle.TabIndex = 6;
      this.lblPlaylistEditTitle.Text = "Title";
      // 
      // tbPlaylistTitle
      // 
      this.tbPlaylistTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbPlaylistTitle.Location = new System.Drawing.Point(118, 19);
      this.tbPlaylistTitle.Name = "tbPlaylistTitle";
      this.tbPlaylistTitle.Size = new System.Drawing.Size(409, 20);
      this.tbPlaylistTitle.TabIndex = 4;
      this.tbPlaylistTitle.TextChanged += new System.EventHandler(this.tbPlaylistTitle_TextChanged);
      // 
      // tbPlaylistDescription
      // 
      this.tbPlaylistDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbPlaylistDescription.Location = new System.Drawing.Point(118, 45);
      this.tbPlaylistDescription.Name = "tbPlaylistDescription";
      this.tbPlaylistDescription.Size = new System.Drawing.Size(409, 20);
      this.tbPlaylistDescription.TabIndex = 3;
      // 
      // gbFileSelect
      // 
      this.gbFileSelect.Controls.Add(this.comboImportFormat);
      this.gbFileSelect.Controls.Add(this.btnOpenFile);
      this.gbFileSelect.Controls.Add(this.label4);
      this.gbFileSelect.Controls.Add(this.label1);
      this.gbFileSelect.Controls.Add(this.tbImportFile);
      this.gbFileSelect.Dock = System.Windows.Forms.DockStyle.Top;
      this.gbFileSelect.Location = new System.Drawing.Point(0, 0);
      this.gbFileSelect.Name = "gbFileSelect";
      this.gbFileSelect.Size = new System.Drawing.Size(668, 81);
      this.gbFileSelect.TabIndex = 2;
      this.gbFileSelect.TabStop = false;
      this.gbFileSelect.Text = "Select file";
      // 
      // comboImportFormat
      // 
      this.comboImportFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboImportFormat.FormattingEnabled = true;
      this.comboImportFormat.Location = new System.Drawing.Point(118, 19);
      this.comboImportFormat.Name = "comboImportFormat";
      this.comboImportFormat.Size = new System.Drawing.Size(222, 21);
      this.comboImportFormat.TabIndex = 3;
      // 
      // btnOpenFile
      // 
      this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOpenFile.Image = global::PlexMusicPlaylists.Properties.Resources.Open_file_16x16;
      this.btnOpenFile.Location = new System.Drawing.Point(639, 46);
      this.btnOpenFile.Name = "btnOpenFile";
      this.btnOpenFile.Size = new System.Drawing.Size(23, 23);
      this.btnOpenFile.TabIndex = 2;
      this.btnOpenFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnOpenFile.UseVisualStyleBackColor = true;
      this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(15, 22);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(84, 13);
      this.label4.TabIndex = 1;
      this.label4.Text = "Import file format";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(15, 48);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(84, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Select import file";
      // 
      // tbImportFile
      // 
      this.tbImportFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbImportFile.Location = new System.Drawing.Point(118, 45);
      this.tbImportFile.Name = "tbImportFile";
      this.tbImportFile.ReadOnly = true;
      this.tbImportFile.Size = new System.Drawing.Size(515, 20);
      this.tbImportFile.TabIndex = 0;
      // 
      // panelImport
      // 
      this.panelImport.Controls.Add(this.panelImportDetail);
      this.panelImport.Controls.Add(this.panelImportToolstrip);
      this.panelImport.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelImport.Location = new System.Drawing.Point(0, 162);
      this.panelImport.Name = "panelImport";
      this.panelImport.Size = new System.Drawing.Size(1233, 669);
      this.panelImport.TabIndex = 1;
      // 
      // panelImportDetail
      // 
      this.panelImportDetail.Controls.Add(this.tableLayoutPanel1);
      this.panelImportDetail.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelImportDetail.Location = new System.Drawing.Point(0, 31);
      this.panelImportDetail.Name = "panelImportDetail";
      this.panelImportDetail.Size = new System.Drawing.Size(1233, 638);
      this.panelImportDetail.TabIndex = 1;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.panelImportBottom, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.panelImportDetailGrid, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1233, 638);
      this.tableLayoutPanel1.TabIndex = 3;
      // 
      // panelImportBottom
      // 
      this.panelImportBottom.Controls.Add(this.btnClose);
      this.panelImportBottom.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelImportBottom.Location = new System.Drawing.Point(3, 591);
      this.panelImportBottom.Name = "panelImportBottom";
      this.panelImportBottom.Size = new System.Drawing.Size(1227, 44);
      this.panelImportBottom.TabIndex = 2;
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnClose.Location = new System.Drawing.Point(1143, 12);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // panelImportDetailGrid
      // 
      this.panelImportDetailGrid.Controls.Add(this.gvImportList);
      this.panelImportDetailGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelImportDetailGrid.Location = new System.Drawing.Point(3, 3);
      this.panelImportDetailGrid.Name = "panelImportDetailGrid";
      this.panelImportDetailGrid.Size = new System.Drawing.Size(1227, 582);
      this.panelImportDetailGrid.TabIndex = 1;
      // 
      // gvImportList
      // 
      this.gvImportList.AllowUserToAddRows = false;
      this.gvImportList.AllowUserToDeleteRows = false;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.gvImportList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.gvImportList.AutoGenerateColumns = false;
      this.gvImportList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvImportList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MatchIcon,
            this.Matched,
            this.MatchedOnTitleCount,
            this.artistDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.fileNameDataGridViewTextBoxColumn,
            this.MainSectionName,
            this.PMSFolder,
            this.Key,
            this.FullPlexFileName});
      this.gvImportList.DataSource = this.importEntryBindingSource;
      this.gvImportList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvImportList.Location = new System.Drawing.Point(0, 0);
      this.gvImportList.Name = "gvImportList";
      dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.gvImportList.RowsDefaultCellStyle = dataGridViewCellStyle3;
      this.gvImportList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvImportList.Size = new System.Drawing.Size(1227, 582);
      this.gvImportList.TabIndex = 0;
      this.gvImportList.VirtualMode = true;
      this.gvImportList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvImportList_CellMouseClick);
      this.gvImportList.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gvImportList_CellValueNeeded);
      this.gvImportList.SelectionChanged += new System.EventHandler(this.gvImportList_SelectionChanged);
      // 
      // MatchIcon
      // 
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
      dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle2.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle2.NullValue")));
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
      this.MatchIcon.DefaultCellStyle = dataGridViewCellStyle2;
      this.MatchIcon.HeaderText = "";
      this.MatchIcon.Name = "MatchIcon";
      this.MatchIcon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.MatchIcon.Width = 32;
      // 
      // Matched
      // 
      this.Matched.DataPropertyName = "Matched";
      this.Matched.HeaderText = "Matched";
      this.Matched.Name = "Matched";
      this.Matched.ReadOnly = true;
      this.Matched.Width = 50;
      // 
      // MatchedOnTitleCount
      // 
      this.MatchedOnTitleCount.DataPropertyName = "MatchedOnTitleCount";
      this.MatchedOnTitleCount.HeaderText = "Title matches";
      this.MatchedOnTitleCount.Name = "MatchedOnTitleCount";
      this.MatchedOnTitleCount.ReadOnly = true;
      // 
      // artistDataGridViewTextBoxColumn
      // 
      this.artistDataGridViewTextBoxColumn.DataPropertyName = "Artist";
      this.artistDataGridViewTextBoxColumn.HeaderText = "Artist";
      this.artistDataGridViewTextBoxColumn.Name = "artistDataGridViewTextBoxColumn";
      // 
      // titleDataGridViewTextBoxColumn
      // 
      this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
      this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
      this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
      this.titleDataGridViewTextBoxColumn.Width = 150;
      // 
      // fileNameDataGridViewTextBoxColumn
      // 
      this.fileNameDataGridViewTextBoxColumn.DataPropertyName = "FileName";
      this.fileNameDataGridViewTextBoxColumn.HeaderText = "FileName";
      this.fileNameDataGridViewTextBoxColumn.Name = "fileNameDataGridViewTextBoxColumn";
      this.fileNameDataGridViewTextBoxColumn.ReadOnly = true;
      this.fileNameDataGridViewTextBoxColumn.Width = 150;
      // 
      // MainSectionName
      // 
      this.MainSectionName.DataPropertyName = "MainSectionName";
      this.MainSectionName.HeaderText = "Main section";
      this.MainSectionName.Name = "MainSectionName";
      this.MainSectionName.ReadOnly = true;
      // 
      // PMSFolder
      // 
      this.PMSFolder.DataPropertyName = "PMSFolder";
      this.PMSFolder.HeaderText = "Folder (Plex Media Server)";
      this.PMSFolder.Name = "PMSFolder";
      this.PMSFolder.ReadOnly = true;
      this.PMSFolder.Width = 240;
      // 
      // Key
      // 
      this.Key.DataPropertyName = "Key";
      this.Key.HeaderText = "Key";
      this.Key.Name = "Key";
      this.Key.ReadOnly = true;
      this.Key.Width = 120;
      // 
      // FullPlexFileName
      // 
      this.FullPlexFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.FullPlexFileName.DataPropertyName = "FullPlexFileName";
      this.FullPlexFileName.HeaderText = "Full filename (Plex)";
      this.FullPlexFileName.Name = "FullPlexFileName";
      this.FullPlexFileName.ReadOnly = true;
      // 
      // importEntryBindingSource
      // 
      this.importEntryBindingSource.DataSource = typeof(PlexMusicPlaylists.Import.ImportEntry);
      // 
      // panelImportToolstrip
      // 
      this.panelImportToolstrip.Controls.Add(this.toolStripImport);
      this.panelImportToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelImportToolstrip.Location = new System.Drawing.Point(0, 0);
      this.panelImportToolstrip.Name = "panelImportToolstrip";
      this.panelImportToolstrip.Size = new System.Drawing.Size(1233, 31);
      this.panelImportToolstrip.TabIndex = 0;
      // 
      // toolStripImport
      // 
      this.toolStripImport.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripImport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel,
            this.toolStripSeparator3,
            this.btnMatchFolder,
            this.btnMatchTitle,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.btnSelectMatch,
            this.btnSearchSelected,
            this.btnSwitchTitleArtist,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.btnCreate,
            this.toolStripSeparator4,
            this.toolStripLabel3,
            this.btnSectionLocation});
      this.toolStripImport.Location = new System.Drawing.Point(0, 0);
      this.toolStripImport.Name = "toolStripImport";
      this.toolStripImport.Size = new System.Drawing.Size(1233, 31);
      this.toolStripImport.TabIndex = 0;
      this.toolStripImport.Text = "toolStrip1";
      // 
      // toolStripLabel
      // 
      this.toolStripLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStripLabel.Name = "toolStripLabel";
      this.toolStripLabel.Size = new System.Drawing.Size(63, 28);
      this.toolStripLabel.Text = "All entries";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
      // 
      // btnMatchFolder
      // 
      this.btnMatchFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnMatchFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnMatchFolder.Image")));
      this.btnMatchFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMatchFolder.Name = "btnMatchFolder";
      this.btnMatchFolder.Size = new System.Drawing.Size(111, 28);
      this.btnMatchFolder.Text = "Match on filename";
      this.btnMatchFolder.Click += new System.EventHandler(this.btnMatchFolder_Click);
      // 
      // btnMatchTitle
      // 
      this.btnMatchTitle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnMatchTitle.Image = ((System.Drawing.Image)(resources.GetObject("btnMatchTitle.Image")));
      this.btnMatchTitle.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnMatchTitle.Name = "btnMatchTitle";
      this.btnMatchTitle.Size = new System.Drawing.Size(85, 28);
      this.btnMatchTitle.Text = "Match on title";
      this.btnMatchTitle.Click += new System.EventHandler(this.btnMatchTitle_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(89, 28);
      this.toolStripLabel1.Text = "Selected entry";
      // 
      // btnSelectMatch
      // 
      this.btnSelectMatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnSelectMatch.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectMatch.Image")));
      this.btnSelectMatch.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSelectMatch.Name = "btnSelectMatch";
      this.btnSelectMatch.Size = new System.Drawing.Size(88, 28);
      this.btnSelectMatch.Text = "Select a match";
      this.btnSelectMatch.Click += new System.EventHandler(this.btnSelectMatch_Click);
      // 
      // btnSearchSelected
      // 
      this.btnSearchSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnSearchSelected.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchSelected.Image")));
      this.btnSearchSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSearchSelected.Name = "btnSearchSelected";
      this.btnSearchSelected.Size = new System.Drawing.Size(106, 28);
      this.btnSearchSelected.Text = "(Re)search on title";
      this.btnSearchSelected.Click += new System.EventHandler(this.btnSearchSelected_Click);
      // 
      // btnSwitchTitleArtist
      // 
      this.btnSwitchTitleArtist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnSwitchTitleArtist.Image = ((System.Drawing.Image)(resources.GetObject("btnSwitchTitleArtist.Image")));
      this.btnSwitchTitleArtist.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSwitchTitleArtist.Name = "btnSwitchTitleArtist";
      this.btnSwitchTitleArtist.Size = new System.Drawing.Size(111, 28);
      this.btnSwitchTitleArtist.Text = "Switch Title / Artist";
      this.btnSwitchTitleArtist.Click += new System.EventHandler(this.btnSwitchTitleArtist_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(43, 28);
      this.toolStripLabel2.Text = "Action";
      // 
      // btnCreate
      // 
      this.btnCreate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnCreate.Name = "btnCreate";
      this.btnCreate.Size = new System.Drawing.Size(85, 28);
      this.btnCreate.Text = "Create playlist";
      this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
      // 
      // toolStripLabel3
      // 
      this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStripLabel3.Name = "toolStripLabel3";
      this.toolStripLabel3.Size = new System.Drawing.Size(40, 28);
      this.toolStripLabel3.Text = "Setup";
      // 
      // btnSectionLocation
      // 
      this.btnSectionLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnSectionLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnSectionLocation.Image")));
      this.btnSectionLocation.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSectionLocation.Name = "btnSectionLocation";
      this.btnSectionLocation.Size = new System.Drawing.Size(100, 28);
      this.btnSectionLocation.Text = "Folder mappings";
      this.btnSectionLocation.Click += new System.EventHandler(this.btnSectionLocation_Click);
      // 
      // ofdImportFile
      // 
      this.ofdImportFile.DefaultExt = "m3u";
      this.ofdImportFile.Filter = "m3u files|*.m3u";
      this.ofdImportFile.RestoreDirectory = true;
      this.ofdImportFile.Title = "Select import file";
      // 
      // ep
      // 
      this.ep.ContainerControl = this;
      // 
      // ImportForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1233, 831);
      this.Controls.Add(this.panelImport);
      this.Controls.Add(this.panelTop);
      this.Name = "ImportForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Import";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImportForm_FormClosing);
      this.Shown += new System.EventHandler(this.ImportForm_Shown);
      this.panelTop.ResumeLayout(false);
      this.panelTopRight.ResumeLayout(false);
      this.panelProgress.ResumeLayout(false);
      this.gbMatchDetails.ResumeLayout(false);
      this.gbMatchDetails.PerformLayout();
      this.panelTopLeft.ResumeLayout(false);
      this.gbNewPlaylist.ResumeLayout(false);
      this.gbNewPlaylist.PerformLayout();
      this.gbFileSelect.ResumeLayout(false);
      this.gbFileSelect.PerformLayout();
      this.panelImport.ResumeLayout(false);
      this.panelImportDetail.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panelImportBottom.ResumeLayout(false);
      this.panelImportDetailGrid.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvImportList)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.importEntryBindingSource)).EndInit();
      this.panelImportToolstrip.ResumeLayout(false);
      this.panelImportToolstrip.PerformLayout();
      this.toolStripImport.ResumeLayout(false);
      this.toolStripImport.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ep)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panelTop;
    private System.Windows.Forms.Panel panelImport;
    private System.Windows.Forms.Panel panelImportToolstrip;
    private System.Windows.Forms.Panel panelImportDetail;
    private System.Windows.Forms.DataGridView gvImportList;
    private System.Windows.Forms.ToolStrip toolStripImport;
    private System.Windows.Forms.BindingSource importEntryBindingSource;
    private System.Windows.Forms.ToolStripButton btnMatchFolder;
    private System.Windows.Forms.ToolStripButton btnMatchTitle;
    private System.Windows.Forms.GroupBox gbFileSelect;
    private System.Windows.Forms.Button btnOpenFile;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbImportFile;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton btnCreate;
    private System.Windows.Forms.OpenFileDialog ofdImportFile;
    private System.Windows.Forms.Label lblPlaylistEditDescription;
    private System.Windows.Forms.Label lblPlaylistEditTitle;
    private System.Windows.Forms.TextBox tbPlaylistDescription;
    private System.Windows.Forms.TextBox tbPlaylistTitle;
    private System.Windows.Forms.GroupBox gbMatchDetails;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox tbNumberOfTracks;
    private System.Windows.Forms.TextBox tbNumberMatched;
    private System.Windows.Forms.Panel panelProgress;
    private System.Windows.Forms.Label labelProgress;
    private System.Windows.Forms.ToolStripButton btnSelectMatch;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripButton btnSearchSelected;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.Panel panelTopRight;
    private System.Windows.Forms.Panel panelTopLeft;
    private System.Windows.Forms.GroupBox gbNewPlaylist;
    private System.Windows.Forms.ErrorProvider ep;
    private System.Windows.Forms.ToolStripButton btnSectionLocation;
    private System.Windows.Forms.Panel panelImportBottom;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Panel panelImportDetailGrid;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label labelProgressSub;
    private System.Windows.Forms.DataGridViewImageColumn MatchIcon;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Matched;
    private System.Windows.Forms.DataGridViewTextBoxColumn MatchedOnTitleCount;
    private System.Windows.Forms.DataGridViewTextBoxColumn artistDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn fileNameDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn MainSectionName;
    private System.Windows.Forms.DataGridViewTextBoxColumn PMSFolder;
    private System.Windows.Forms.DataGridViewTextBoxColumn Key;
    private System.Windows.Forms.DataGridViewTextBoxColumn FullPlexFileName;
    private System.Windows.Forms.ToolStripButton btnSwitchTitleArtist;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    private System.Windows.Forms.ComboBox comboImportFormat;
    private System.Windows.Forms.Label label4;
  }
}