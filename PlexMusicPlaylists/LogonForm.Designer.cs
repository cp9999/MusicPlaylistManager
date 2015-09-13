namespace PlexMusicPlaylists
{
  partial class LogonForm
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
      this.tbUserName = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.tbPassword = new System.Windows.Forms.MaskedTextBox();
      this.panelBottom = new System.Windows.Forms.Panel();
      this.btnLogon = new System.Windows.Forms.Button();
      this.playlistSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.panelBottom.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.playlistSettingsBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // tbUserName
      // 
      this.tbUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbUserName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.playlistSettingsBindingSource, "UserName", true));
      this.tbUserName.Location = new System.Drawing.Point(98, 35);
      this.tbUserName.Name = "tbUserName";
      this.tbUserName.Size = new System.Drawing.Size(237, 20);
      this.tbUserName.TabIndex = 11;
      this.tbUserName.TextChanged += new System.EventHandler(this.tbUserName_TextChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(9, 38);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(58, 13);
      this.label3.TabIndex = 12;
      this.label3.Text = "User name";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 79);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(53, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "Password";
      // 
      // tbPassword
      // 
      this.tbPassword.Location = new System.Drawing.Point(98, 78);
      this.tbPassword.Name = "tbPassword";
      this.tbPassword.PasswordChar = '*';
      this.tbPassword.Size = new System.Drawing.Size(237, 20);
      this.tbPassword.TabIndex = 13;
      this.tbPassword.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
      // 
      // panelBottom
      // 
      this.panelBottom.Controls.Add(this.btnLogon);
      this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelBottom.Location = new System.Drawing.Point(0, 155);
      this.panelBottom.Name = "panelBottom";
      this.panelBottom.Size = new System.Drawing.Size(356, 51);
      this.panelBottom.TabIndex = 14;
      // 
      // btnLogon
      // 
      this.btnLogon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLogon.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnLogon.Enabled = false;
      this.btnLogon.Location = new System.Drawing.Point(246, 12);
      this.btnLogon.Name = "btnLogon";
      this.btnLogon.Size = new System.Drawing.Size(101, 30);
      this.btnLogon.TabIndex = 0;
      this.btnLogon.Text = "Logon";
      this.btnLogon.UseVisualStyleBackColor = true;
      this.btnLogon.Click += new System.EventHandler(this.btnLogon_Click);
      // 
      // playlistSettingsBindingSource
      // 
      this.playlistSettingsBindingSource.DataSource = typeof(PlexMusicPlaylists.PlexMediaServer.PlaylistSettings);
      // 
      // LogonForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(356, 206);
      this.Controls.Add(this.panelBottom);
      this.Controls.Add(this.tbPassword);
      this.Controls.Add(this.tbUserName);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label3);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "LogonForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Logon to myPlex";
      this.panelBottom.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.playlistSettingsBindingSource)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbUserName;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.MaskedTextBox tbPassword;
    private System.Windows.Forms.Panel panelBottom;
    private System.Windows.Forms.Button btnLogon;
    private System.Windows.Forms.BindingSource playlistSettingsBindingSource;
  }
}