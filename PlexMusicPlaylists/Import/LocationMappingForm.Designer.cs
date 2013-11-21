namespace PlexMusicPlaylists.Import
{
  partial class LocationMappingForm
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnClose = new System.Windows.Forms.Button();
      this.panel2 = new System.Windows.Forms.Panel();
      this.gvSectionLocation = new System.Windows.Forms.DataGridView();
      this.MainSectionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.plexLocationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.mappedLocationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.sectionLocationBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.panel3 = new System.Windows.Forms.Panel();
      this.gbPlexMediaServer = new System.Windows.Forms.GroupBox();
      this.label5 = new System.Windows.Forms.Label();
      this.tbDirectorySeparator = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.ep = new System.Windows.Forms.ErrorProvider(this.components);
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvSectionLocation)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.sectionLocationBindingSource)).BeginInit();
      this.panel3.SuspendLayout();
      this.gbPlexMediaServer.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ep)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(846, 529);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnClose);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 482);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(840, 44);
      this.panel1.TabIndex = 0;
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnClose.Location = new System.Drawing.Point(756, 12);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.gvSectionLocation);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(3, 66);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(840, 410);
      this.panel2.TabIndex = 1;
      // 
      // gvSectionLocation
      // 
      this.gvSectionLocation.AllowUserToAddRows = false;
      this.gvSectionLocation.AllowUserToDeleteRows = false;
      dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.gvSectionLocation.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
      this.gvSectionLocation.AutoGenerateColumns = false;
      this.gvSectionLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvSectionLocation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MainSectionName,
            this.plexLocationDataGridViewTextBoxColumn,
            this.mappedLocationDataGridViewTextBoxColumn});
      this.gvSectionLocation.DataSource = this.sectionLocationBindingSource;
      this.gvSectionLocation.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvSectionLocation.Location = new System.Drawing.Point(0, 0);
      this.gvSectionLocation.MultiSelect = false;
      this.gvSectionLocation.Name = "gvSectionLocation";
      dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.gvSectionLocation.RowsDefaultCellStyle = dataGridViewCellStyle4;
      this.gvSectionLocation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvSectionLocation.Size = new System.Drawing.Size(840, 410);
      this.gvSectionLocation.TabIndex = 0;
      // 
      // MainSectionName
      // 
      this.MainSectionName.DataPropertyName = "MainSectionName";
      this.MainSectionName.HeaderText = "Main section";
      this.MainSectionName.Name = "MainSectionName";
      this.MainSectionName.ReadOnly = true;
      this.MainSectionName.Width = 140;
      // 
      // plexLocationDataGridViewTextBoxColumn
      // 
      this.plexLocationDataGridViewTextBoxColumn.DataPropertyName = "PlexLocation";
      this.plexLocationDataGridViewTextBoxColumn.HeaderText = "Plex Location";
      this.plexLocationDataGridViewTextBoxColumn.Name = "plexLocationDataGridViewTextBoxColumn";
      this.plexLocationDataGridViewTextBoxColumn.ReadOnly = true;
      this.plexLocationDataGridViewTextBoxColumn.Width = 300;
      // 
      // mappedLocationDataGridViewTextBoxColumn
      // 
      this.mappedLocationDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.mappedLocationDataGridViewTextBoxColumn.DataPropertyName = "MappedLocation";
      this.mappedLocationDataGridViewTextBoxColumn.HeaderText = "Mapped Location";
      this.mappedLocationDataGridViewTextBoxColumn.Name = "mappedLocationDataGridViewTextBoxColumn";
      // 
      // sectionLocationBindingSource
      // 
      this.sectionLocationBindingSource.DataSource = typeof(PlexMusicPlaylists.PlexMediaServer.SectionLocation);
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.gbPlexMediaServer);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(3, 3);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(840, 57);
      this.panel3.TabIndex = 2;
      // 
      // gbPlexMediaServer
      // 
      this.gbPlexMediaServer.Controls.Add(this.label5);
      this.gbPlexMediaServer.Controls.Add(this.tbDirectorySeparator);
      this.gbPlexMediaServer.Controls.Add(this.label4);
      this.gbPlexMediaServer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbPlexMediaServer.Location = new System.Drawing.Point(0, 0);
      this.gbPlexMediaServer.Name = "gbPlexMediaServer";
      this.gbPlexMediaServer.Size = new System.Drawing.Size(840, 57);
      this.gbPlexMediaServer.TabIndex = 0;
      this.gbPlexMediaServer.TabStop = false;
      this.gbPlexMediaServer.Text = "PlexMediaServer";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(164, 23);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(319, 13);
      this.label5.TabIndex = 8;
      this.label5.Text = "Character used for separating directories on the Plex Media Server";
      // 
      // tbDirectorySeparator
      // 
      this.tbDirectorySeparator.Location = new System.Drawing.Point(109, 19);
      this.tbDirectorySeparator.MaxLength = 1;
      this.tbDirectorySeparator.Name = "tbDirectorySeparator";
      this.tbDirectorySeparator.Size = new System.Drawing.Size(26, 20);
      this.tbDirectorySeparator.TabIndex = 7;
      this.tbDirectorySeparator.Text = "\\";
      this.tbDirectorySeparator.Validating += new System.ComponentModel.CancelEventHandler(this.tbDirectorySeparator_Validating);
      this.tbDirectorySeparator.Validated += new System.EventHandler(this.tbDirectorySeparator_Validated);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(7, 23);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(96, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "Directory separator";
      // 
      // ep
      // 
      this.ep.ContainerControl = this;
      // 
      // LocationMappingForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(846, 529);
      this.ControlBox = false;
      this.Controls.Add(this.tableLayoutPanel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "LocationMappingForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Plex Media Server - Location mapping";
      this.Shown += new System.EventHandler(this.LocationMappingForm_Shown);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvSectionLocation)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.sectionLocationBindingSource)).EndInit();
      this.panel3.ResumeLayout(false);
      this.gbPlexMediaServer.ResumeLayout(false);
      this.gbPlexMediaServer.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ep)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.DataGridView gvSectionLocation;
    private System.Windows.Forms.BindingSource sectionLocationBindingSource;
    private System.Windows.Forms.DataGridViewTextBoxColumn MainSectionName;
    private System.Windows.Forms.DataGridViewTextBoxColumn plexLocationDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn mappedLocationDataGridViewTextBoxColumn;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.GroupBox gbPlexMediaServer;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox tbDirectorySeparator;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ErrorProvider ep;
  }
}