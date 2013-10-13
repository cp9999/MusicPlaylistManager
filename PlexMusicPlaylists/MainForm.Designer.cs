namespace PlexMusicPlaylists
{
  partial class MainForm
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
      this.splitMain = new System.Windows.Forms.SplitContainer();
      this.gbServer = new System.Windows.Forms.GroupBox();
      this.udPlexPort = new System.Windows.Forms.NumericUpDown();
      this.btnConnect = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.tbPlexIP = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.splitBottom = new System.Windows.Forms.SplitContainer();
      this.splitDetail = new System.Windows.Forms.SplitContainer();
      this.rtbLog = new System.Windows.Forms.RichTextBox();
      this.playlistUC = new PlexMusicPlaylists.PlaylistUserControl();
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
      this.splitMain.Panel1.SuspendLayout();
      this.splitMain.Panel2.SuspendLayout();
      this.splitMain.SuspendLayout();
      this.gbServer.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udPlexPort)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.splitBottom)).BeginInit();
      this.splitBottom.Panel1.SuspendLayout();
      this.splitBottom.Panel2.SuspendLayout();
      this.splitBottom.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitDetail)).BeginInit();
      this.splitDetail.Panel1.SuspendLayout();
      this.splitDetail.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitMain
      // 
      this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitMain.Location = new System.Drawing.Point(0, 0);
      this.splitMain.Name = "splitMain";
      this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitMain.Panel1
      // 
      this.splitMain.Panel1.Controls.Add(this.gbServer);
      // 
      // splitMain.Panel2
      // 
      this.splitMain.Panel2.Controls.Add(this.splitBottom);
      this.splitMain.Size = new System.Drawing.Size(1255, 795);
      this.splitMain.SplitterDistance = 68;
      this.splitMain.TabIndex = 0;
      // 
      // gbServer
      // 
      this.gbServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.gbServer.Controls.Add(this.udPlexPort);
      this.gbServer.Controls.Add(this.btnConnect);
      this.gbServer.Controls.Add(this.label2);
      this.gbServer.Controls.Add(this.tbPlexIP);
      this.gbServer.Controls.Add(this.label1);
      this.gbServer.Location = new System.Drawing.Point(12, 12);
      this.gbServer.Name = "gbServer";
      this.gbServer.Size = new System.Drawing.Size(1231, 46);
      this.gbServer.TabIndex = 1;
      this.gbServer.TabStop = false;
      this.gbServer.Text = "Plex Media Server";
      // 
      // udPlexPort
      // 
      this.udPlexPort.Location = new System.Drawing.Point(276, 20);
      this.udPlexPort.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
      this.udPlexPort.Name = "udPlexPort";
      this.udPlexPort.Size = new System.Drawing.Size(76, 20);
      this.udPlexPort.TabIndex = 5;
      this.udPlexPort.ValueChanged += new System.EventHandler(this.udPlexPort_ValueChanged);
      // 
      // btnConnect
      // 
      this.btnConnect.Location = new System.Drawing.Point(376, 17);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(75, 23);
      this.btnConnect.TabIndex = 4;
      this.btnConnect.Text = "Connect";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(244, 23);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(26, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Port";
      // 
      // tbPlexIP
      // 
      this.tbPlexIP.Location = new System.Drawing.Point(65, 20);
      this.tbPlexIP.Name = "tbPlexIP";
      this.tbPlexIP.Size = new System.Drawing.Size(144, 20);
      this.tbPlexIP.TabIndex = 1;
      this.tbPlexIP.TextChanged += new System.EventHandler(this.tbPlexIP_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 23);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(53, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "IP Addres";
      // 
      // splitBottom
      // 
      this.splitBottom.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitBottom.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitBottom.Location = new System.Drawing.Point(0, 0);
      this.splitBottom.Name = "splitBottom";
      this.splitBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitBottom.Panel1
      // 
      this.splitBottom.Panel1.Controls.Add(this.splitDetail);
      // 
      // splitBottom.Panel2
      // 
      this.splitBottom.Panel2.Controls.Add(this.rtbLog);
      this.splitBottom.Size = new System.Drawing.Size(1255, 723);
      this.splitBottom.SplitterDistance = 610;
      this.splitBottom.TabIndex = 1;
      // 
      // splitDetail
      // 
      this.splitDetail.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitDetail.Location = new System.Drawing.Point(0, 0);
      this.splitDetail.Name = "splitDetail";
      // 
      // splitDetail.Panel1
      // 
      this.splitDetail.Panel1.Controls.Add(this.playlistUC);
      this.splitDetail.Panel2Collapsed = true;
      this.splitDetail.Size = new System.Drawing.Size(1255, 610);
      this.splitDetail.SplitterDistance = 664;
      this.splitDetail.TabIndex = 1;
      // 
      // rtbLog
      // 
      this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbLog.Location = new System.Drawing.Point(0, 0);
      this.rtbLog.Name = "rtbLog";
      this.rtbLog.ReadOnly = true;
      this.rtbLog.Size = new System.Drawing.Size(1255, 109);
      this.rtbLog.TabIndex = 0;
      this.rtbLog.Text = "";
      // 
      // playlistUC
      // 
      this.playlistUC.Caption = "Playlist configurator";
      this.playlistUC.Dock = System.Windows.Forms.DockStyle.Fill;
      this.playlistUC.Location = new System.Drawing.Point(0, 0);
      this.playlistUC.Name = "playlistUC";
      this.playlistUC.Size = new System.Drawing.Size(1255, 610);
      this.playlistUC.TabIndex = 0;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1255, 795);
      this.Controls.Add(this.splitMain);
      this.Name = "MainForm";
      this.Text = "Plex - Music playlist configurator";
      this.splitMain.Panel1.ResumeLayout(false);
      this.splitMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
      this.splitMain.ResumeLayout(false);
      this.gbServer.ResumeLayout(false);
      this.gbServer.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udPlexPort)).EndInit();
      this.splitBottom.Panel1.ResumeLayout(false);
      this.splitBottom.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitBottom)).EndInit();
      this.splitBottom.ResumeLayout(false);
      this.splitDetail.Panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitDetail)).EndInit();
      this.splitDetail.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitMain;
    private System.Windows.Forms.SplitContainer splitBottom;
    private System.Windows.Forms.SplitContainer splitDetail;
    private PlaylistUserControl playlistUC;
    private System.Windows.Forms.RichTextBox rtbLog;
    private System.Windows.Forms.GroupBox gbServer;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnConnect;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbPlexIP;
    private System.Windows.Forms.NumericUpDown udPlexPort;
  }
}

