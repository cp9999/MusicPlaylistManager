namespace PlexMusicPlaylists
{
  partial class SettingsForm
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
      this.panelMain = new System.Windows.Forms.Panel();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panelTop = new System.Windows.Forms.Panel();
      this.lblCaption = new System.Windows.Forms.Label();
      this.panelBottom = new System.Windows.Forms.Panel();
      this.btnClose = new System.Windows.Forms.Button();
      this.panelData = new System.Windows.Forms.Panel();
      this.groupBoxOptions = new System.Windows.Forms.GroupBox();
      this.checkBoxAutoConnect = new System.Windows.Forms.CheckBox();
      this.playlistSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.groupBoxGUI = new System.Windows.Forms.GroupBox();
      this.rbPlexNative = new System.Windows.Forms.RadioButton();
      this.rbMusicPlaylistChannel = new System.Windows.Forms.RadioButton();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBoxChannel = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tbDataFolder = new System.Windows.Forms.TextBox();
      this.checkBoxPreferDataFolder = new System.Windows.Forms.CheckBox();
      this.btnSelectDataFolder = new System.Windows.Forms.Button();
      this.groupBoxDatabase = new System.Windows.Forms.GroupBox();
      this.checkBoxDatabaseDirectUpdate = new System.Windows.Forms.CheckBox();
      this.checkBoxDatabaseModifiedOnly = new System.Windows.Forms.CheckBox();
      this.checkBoxCreateSqlFiles = new System.Windows.Forms.CheckBox();
      this.tbPlexDatabase = new System.Windows.Forms.TextBox();
      this.btnSelectDatabase = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.ofdPlexDatabase = new System.Windows.Forms.OpenFileDialog();
      this.fbdDataFolder = new System.Windows.Forms.FolderBrowserDialog();
      this.checkBoxServerInSeparatedWindow = new System.Windows.Forms.CheckBox();
      this.checkBoxServerAllowMultipleWindows = new System.Windows.Forms.CheckBox();
      this.panelMain.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.panelTop.SuspendLayout();
      this.panelBottom.SuspendLayout();
      this.panelData.SuspendLayout();
      this.groupBoxOptions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.playlistSettingsBindingSource)).BeginInit();
      this.groupBoxGUI.SuspendLayout();
      this.groupBoxChannel.SuspendLayout();
      this.groupBoxDatabase.SuspendLayout();
      this.SuspendLayout();
      // 
      // panelMain
      // 
      this.panelMain.Controls.Add(this.tableLayoutPanel1);
      this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelMain.Location = new System.Drawing.Point(0, 0);
      this.panelMain.Name = "panelMain";
      this.panelMain.Size = new System.Drawing.Size(584, 554);
      this.panelMain.TabIndex = 0;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.panelTop, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.panelBottom, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.panelData, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.066667F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.93333F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 554);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // panelTop
      // 
      this.panelTop.Controls.Add(this.lblCaption);
      this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelTop.Location = new System.Drawing.Point(3, 3);
      this.panelTop.Name = "panelTop";
      this.panelTop.Size = new System.Drawing.Size(578, 39);
      this.panelTop.TabIndex = 0;
      // 
      // lblCaption
      // 
      this.lblCaption.BackColor = System.Drawing.Color.LightGreen;
      this.lblCaption.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCaption.Location = new System.Drawing.Point(0, 0);
      this.lblCaption.Name = "lblCaption";
      this.lblCaption.Size = new System.Drawing.Size(578, 39);
      this.lblCaption.TabIndex = 1;
      this.lblCaption.Text = "Settings";
      this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // panelBottom
      // 
      this.panelBottom.Controls.Add(this.btnClose);
      this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelBottom.Location = new System.Drawing.Point(3, 500);
      this.panelBottom.Name = "panelBottom";
      this.panelBottom.Size = new System.Drawing.Size(578, 51);
      this.panelBottom.TabIndex = 1;
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnClose.Location = new System.Drawing.Point(468, 12);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(101, 30);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // panelData
      // 
      this.panelData.Controls.Add(this.groupBoxOptions);
      this.panelData.Controls.Add(this.groupBoxGUI);
      this.panelData.Controls.Add(this.groupBoxChannel);
      this.panelData.Controls.Add(this.groupBoxDatabase);
      this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelData.Location = new System.Drawing.Point(3, 48);
      this.panelData.Name = "panelData";
      this.panelData.Size = new System.Drawing.Size(578, 446);
      this.panelData.TabIndex = 2;
      // 
      // groupBoxOptions
      // 
      this.groupBoxOptions.Controls.Add(this.checkBoxServerAllowMultipleWindows);
      this.groupBoxOptions.Controls.Add(this.checkBoxServerInSeparatedWindow);
      this.groupBoxOptions.Controls.Add(this.checkBoxAutoConnect);
      this.groupBoxOptions.Location = new System.Drawing.Point(9, 13);
      this.groupBoxOptions.Name = "groupBoxOptions";
      this.groupBoxOptions.Size = new System.Drawing.Size(555, 95);
      this.groupBoxOptions.TabIndex = 15;
      this.groupBoxOptions.TabStop = false;
      this.groupBoxOptions.Text = "Options";
      // 
      // checkBoxAutoConnect
      // 
      this.checkBoxAutoConnect.AutoSize = true;
      this.checkBoxAutoConnect.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.playlistSettingsBindingSource, "AutoConnect", true));
      this.checkBoxAutoConnect.Location = new System.Drawing.Point(96, 19);
      this.checkBoxAutoConnect.Name = "checkBoxAutoConnect";
      this.checkBoxAutoConnect.Size = new System.Drawing.Size(177, 17);
      this.checkBoxAutoConnect.TabIndex = 12;
      this.checkBoxAutoConnect.Text = "Automatically connect at startup";
      this.checkBoxAutoConnect.UseVisualStyleBackColor = true;
      // 
      // playlistSettingsBindingSource
      // 
      this.playlistSettingsBindingSource.DataSource = typeof(PlexMusicPlaylists.PlexMediaServer.PlaylistSettings);
      // 
      // groupBoxGUI
      // 
      this.groupBoxGUI.Controls.Add(this.rbPlexNative);
      this.groupBoxGUI.Controls.Add(this.rbMusicPlaylistChannel);
      this.groupBoxGUI.Controls.Add(this.label2);
      this.groupBoxGUI.Location = new System.Drawing.Point(10, 378);
      this.groupBoxGUI.Name = "groupBoxGUI";
      this.groupBoxGUI.Size = new System.Drawing.Size(554, 53);
      this.groupBoxGUI.TabIndex = 14;
      this.groupBoxGUI.TabStop = false;
      this.groupBoxGUI.Text = "Mode";
      // 
      // rbPlexNative
      // 
      this.rbPlexNative.AutoSize = true;
      this.rbPlexNative.Location = new System.Drawing.Point(95, 19);
      this.rbPlexNative.Name = "rbPlexNative";
      this.rbPlexNative.Size = new System.Drawing.Size(77, 17);
      this.rbPlexNative.TabIndex = 12;
      this.rbPlexNative.TabStop = true;
      this.rbPlexNative.Text = "Plex native";
      this.rbPlexNative.UseVisualStyleBackColor = true;
      this.rbPlexNative.CheckedChanged += new System.EventHandler(this.rbPlexNative_CheckedChanged);
      // 
      // rbMusicPlaylistChannel
      // 
      this.rbMusicPlaylistChannel.AutoSize = true;
      this.rbMusicPlaylistChannel.Location = new System.Drawing.Point(201, 19);
      this.rbMusicPlaylistChannel.Name = "rbMusicPlaylistChannel";
      this.rbMusicPlaylistChannel.Size = new System.Drawing.Size(128, 17);
      this.rbMusicPlaylistChannel.TabIndex = 11;
      this.rbMusicPlaylistChannel.TabStop = true;
      this.rbMusicPlaylistChannel.Text = "Music playlist channel";
      this.rbMusicPlaylistChannel.UseVisualStyleBackColor = true;
      this.rbMusicPlaylistChannel.CheckedChanged += new System.EventHandler(this.rbMusicPlaylistChannel_CheckedChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 23);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(68, 13);
      this.label2.TabIndex = 10;
      this.label2.Text = "Playlist mode";
      // 
      // groupBoxChannel
      // 
      this.groupBoxChannel.Controls.Add(this.label1);
      this.groupBoxChannel.Controls.Add(this.tbDataFolder);
      this.groupBoxChannel.Controls.Add(this.checkBoxPreferDataFolder);
      this.groupBoxChannel.Controls.Add(this.btnSelectDataFolder);
      this.groupBoxChannel.Location = new System.Drawing.Point(9, 273);
      this.groupBoxChannel.Name = "groupBoxChannel";
      this.groupBoxChannel.Size = new System.Drawing.Size(555, 95);
      this.groupBoxChannel.TabIndex = 13;
      this.groupBoxChannel.TabStop = false;
      this.groupBoxChannel.Text = "Music Playlist channel";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(59, 13);
      this.label1.TabIndex = 10;
      this.label1.Text = "Data folder";
      // 
      // tbDataFolder
      // 
      this.tbDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbDataFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.playlistSettingsBindingSource, "ChannelDataFolder", true));
      this.tbDataFolder.Location = new System.Drawing.Point(96, 27);
      this.tbDataFolder.Name = "tbDataFolder";
      this.tbDataFolder.ReadOnly = true;
      this.tbDataFolder.Size = new System.Drawing.Size(426, 20);
      this.tbDataFolder.TabIndex = 9;
      // 
      // checkBoxPreferDataFolder
      // 
      this.checkBoxPreferDataFolder.AutoSize = true;
      this.checkBoxPreferDataFolder.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.playlistSettingsBindingSource, "ChannelPreferDataFolder", true));
      this.checkBoxPreferDataFolder.Location = new System.Drawing.Point(96, 62);
      this.checkBoxPreferDataFolder.Name = "checkBoxPreferDataFolder";
      this.checkBoxPreferDataFolder.Size = new System.Drawing.Size(311, 17);
      this.checkBoxPreferDataFolder.TabIndex = 12;
      this.checkBoxPreferDataFolder.Text = "Prefer using data folder over music playlist channel on server";
      this.checkBoxPreferDataFolder.UseVisualStyleBackColor = true;
      // 
      // btnSelectDataFolder
      // 
      this.btnSelectDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSelectDataFolder.Image = global::PlexMusicPlaylists.Properties.Resources.Open_file_16x16;
      this.btnSelectDataFolder.Location = new System.Drawing.Point(528, 24);
      this.btnSelectDataFolder.Name = "btnSelectDataFolder";
      this.btnSelectDataFolder.Size = new System.Drawing.Size(23, 23);
      this.btnSelectDataFolder.TabIndex = 11;
      this.btnSelectDataFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnSelectDataFolder.UseVisualStyleBackColor = true;
      this.btnSelectDataFolder.Click += new System.EventHandler(this.btnSelectDataFolder_Click);
      // 
      // groupBoxDatabase
      // 
      this.groupBoxDatabase.Controls.Add(this.checkBoxDatabaseDirectUpdate);
      this.groupBoxDatabase.Controls.Add(this.checkBoxDatabaseModifiedOnly);
      this.groupBoxDatabase.Controls.Add(this.checkBoxCreateSqlFiles);
      this.groupBoxDatabase.Controls.Add(this.tbPlexDatabase);
      this.groupBoxDatabase.Controls.Add(this.btnSelectDatabase);
      this.groupBoxDatabase.Controls.Add(this.label3);
      this.groupBoxDatabase.Location = new System.Drawing.Point(9, 123);
      this.groupBoxDatabase.Name = "groupBoxDatabase";
      this.groupBoxDatabase.Size = new System.Drawing.Size(555, 131);
      this.groupBoxDatabase.TabIndex = 12;
      this.groupBoxDatabase.TabStop = false;
      this.groupBoxDatabase.Text = "Database";
      // 
      // checkBoxDatabaseDirectUpdate
      // 
      this.checkBoxDatabaseDirectUpdate.AutoSize = true;
      this.checkBoxDatabaseDirectUpdate.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.playlistSettingsBindingSource, "DatabaseDirectUpdate", true));
      this.checkBoxDatabaseDirectUpdate.Location = new System.Drawing.Point(96, 79);
      this.checkBoxDatabaseDirectUpdate.Name = "checkBoxDatabaseDirectUpdate";
      this.checkBoxDatabaseDirectUpdate.Size = new System.Drawing.Size(212, 17);
      this.checkBoxDatabaseDirectUpdate.TabIndex = 12;
      this.checkBoxDatabaseDirectUpdate.Text = "Direct update to .db file (Plex database)";
      this.checkBoxDatabaseDirectUpdate.UseVisualStyleBackColor = true;
      // 
      // checkBoxDatabaseModifiedOnly
      // 
      this.checkBoxDatabaseModifiedOnly.AutoSize = true;
      this.checkBoxDatabaseModifiedOnly.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.playlistSettingsBindingSource, "DatabaseModifiedTracksOnly", true));
      this.checkBoxDatabaseModifiedOnly.Location = new System.Drawing.Point(96, 102);
      this.checkBoxDatabaseModifiedOnly.Name = "checkBoxDatabaseModifiedOnly";
      this.checkBoxDatabaseModifiedOnly.Size = new System.Drawing.Size(204, 17);
      this.checkBoxDatabaseModifiedOnly.TabIndex = 12;
      this.checkBoxDatabaseModifiedOnly.Text = "Include modified tracks only in update";
      this.checkBoxDatabaseModifiedOnly.UseVisualStyleBackColor = true;
      // 
      // checkBoxCreateSqlFiles
      // 
      this.checkBoxCreateSqlFiles.AutoSize = true;
      this.checkBoxCreateSqlFiles.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.playlistSettingsBindingSource, "DatabaseCreateSqlFiles", true));
      this.checkBoxCreateSqlFiles.Location = new System.Drawing.Point(96, 56);
      this.checkBoxCreateSqlFiles.Name = "checkBoxCreateSqlFiles";
      this.checkBoxCreateSqlFiles.Size = new System.Drawing.Size(191, 17);
      this.checkBoxCreateSqlFiles.TabIndex = 12;
      this.checkBoxCreateSqlFiles.Text = "Create sql files when saving playlist";
      this.checkBoxCreateSqlFiles.UseVisualStyleBackColor = true;
      // 
      // tbPlexDatabase
      // 
      this.tbPlexDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbPlexDatabase.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.playlistSettingsBindingSource, "PlaylistDB", true));
      this.tbPlexDatabase.Location = new System.Drawing.Point(96, 19);
      this.tbPlexDatabase.Name = "tbPlexDatabase";
      this.tbPlexDatabase.ReadOnly = true;
      this.tbPlexDatabase.Size = new System.Drawing.Size(426, 20);
      this.tbPlexDatabase.TabIndex = 9;
      // 
      // btnSelectDatabase
      // 
      this.btnSelectDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSelectDatabase.Image = global::PlexMusicPlaylists.Properties.Resources.Open_file_16x16;
      this.btnSelectDatabase.Location = new System.Drawing.Point(528, 17);
      this.btnSelectDatabase.Name = "btnSelectDatabase";
      this.btnSelectDatabase.Size = new System.Drawing.Size(23, 23);
      this.btnSelectDatabase.TabIndex = 11;
      this.btnSelectDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnSelectDatabase.UseVisualStyleBackColor = true;
      this.btnSelectDatabase.Click += new System.EventHandler(this.btnSelectDatabase_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(7, 22);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(74, 13);
      this.label3.TabIndex = 10;
      this.label3.Text = "Plex database";
      // 
      // ofdPlexDatabase
      // 
      this.ofdPlexDatabase.DefaultExt = "db";
      this.ofdPlexDatabase.Filter = "sqlite files|*.db";
      this.ofdPlexDatabase.RestoreDirectory = true;
      this.ofdPlexDatabase.Title = "Select plex database";
      // 
      // fbdDataFolder
      // 
      this.fbdDataFolder.ShowNewFolderButton = false;
      // 
      // checkBoxServerInSeparatedWindow
      // 
      this.checkBoxServerInSeparatedWindow.AutoSize = true;
      this.checkBoxServerInSeparatedWindow.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.playlistSettingsBindingSource, "ServerInSeparatedWindow", true));
      this.checkBoxServerInSeparatedWindow.Location = new System.Drawing.Point(96, 42);
      this.checkBoxServerInSeparatedWindow.Name = "checkBoxServerInSeparatedWindow";
      this.checkBoxServerInSeparatedWindow.Size = new System.Drawing.Size(236, 17);
      this.checkBoxServerInSeparatedWindow.TabIndex = 12;
      this.checkBoxServerInSeparatedWindow.Text = "Show Plex Media Server in separate window";
      this.checkBoxServerInSeparatedWindow.UseVisualStyleBackColor = true;
      // 
      // checkBoxServerAllowMultipleWindows
      // 
      this.checkBoxServerAllowMultipleWindows.AutoSize = true;
      this.checkBoxServerAllowMultipleWindows.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.playlistSettingsBindingSource, "ServerAllowMultipleWindows", true));
      this.checkBoxServerAllowMultipleWindows.Location = new System.Drawing.Point(96, 65);
      this.checkBoxServerAllowMultipleWindows.Name = "checkBoxServerAllowMultipleWindows";
      this.checkBoxServerAllowMultipleWindows.Size = new System.Drawing.Size(225, 17);
      this.checkBoxServerAllowMultipleWindows.TabIndex = 12;
      this.checkBoxServerAllowMultipleWindows.Text = "Allow multiple Plex Media Server windows ";
      this.checkBoxServerAllowMultipleWindows.UseVisualStyleBackColor = true;
      // 
      // SettingsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(584, 554);
      this.Controls.Add(this.panelMain);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "SettingsForm";
      this.Text = "Settings";
      this.panelMain.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panelTop.ResumeLayout(false);
      this.panelBottom.ResumeLayout(false);
      this.panelData.ResumeLayout(false);
      this.groupBoxOptions.ResumeLayout(false);
      this.groupBoxOptions.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.playlistSettingsBindingSource)).EndInit();
      this.groupBoxGUI.ResumeLayout(false);
      this.groupBoxGUI.PerformLayout();
      this.groupBoxChannel.ResumeLayout(false);
      this.groupBoxChannel.PerformLayout();
      this.groupBoxDatabase.ResumeLayout(false);
      this.groupBoxDatabase.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panelMain;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel panelTop;
    private System.Windows.Forms.Panel panelBottom;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Panel panelData;
    private System.Windows.Forms.Button btnSelectDatabase;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox tbPlexDatabase;
    private System.Windows.Forms.OpenFileDialog ofdPlexDatabase;
    private System.Windows.Forms.GroupBox groupBoxDatabase;
    private System.Windows.Forms.CheckBox checkBoxCreateSqlFiles;
    private System.Windows.Forms.CheckBox checkBoxDatabaseDirectUpdate;
    private System.Windows.Forms.CheckBox checkBoxDatabaseModifiedOnly;
    private System.Windows.Forms.GroupBox groupBoxChannel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbDataFolder;
    private System.Windows.Forms.CheckBox checkBoxPreferDataFolder;
    private System.Windows.Forms.BindingSource playlistSettingsBindingSource;
    private System.Windows.Forms.Button btnSelectDataFolder;
    private System.Windows.Forms.FolderBrowserDialog fbdDataFolder;
    private System.Windows.Forms.GroupBox groupBoxGUI;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.RadioButton rbPlexNative;
    private System.Windows.Forms.RadioButton rbMusicPlaylistChannel;
    private System.Windows.Forms.GroupBox groupBoxOptions;
    private System.Windows.Forms.CheckBox checkBoxAutoConnect;
    private System.Windows.Forms.Label lblCaption;
    private System.Windows.Forms.CheckBox checkBoxServerAllowMultipleWindows;
    private System.Windows.Forms.CheckBox checkBoxServerInSeparatedWindow;
  }
}