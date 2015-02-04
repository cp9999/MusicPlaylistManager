namespace PlexMusicPlaylists
{
  partial class PMSServerForm
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
      this.ucPMSServer = new PlexMusicPlaylists.PMSServerUserControl();
      this.SuspendLayout();
      // 
      // ucPMSServer
      // 
      this.ucPMSServer.AllowVideos = false;
      this.ucPMSServer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ucPMSServer.Location = new System.Drawing.Point(0, 0);
      this.ucPMSServer.Name = "ucPMSServer";
      this.ucPMSServer.Orientation = System.Windows.Forms.Orientation.Vertical;
      this.ucPMSServer.PMSServer = null;
      this.ucPMSServer.Size = new System.Drawing.Size(897, 585);
      this.ucPMSServer.TabIndex = 0;
      // 
      // PMSServerForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(897, 585);
      this.Controls.Add(this.ucPMSServer);
      this.Name = "PMSServerForm";
      this.ResumeLayout(false);

    }

    #endregion

    private PMSServerUserControl ucPMSServer;
  }
}