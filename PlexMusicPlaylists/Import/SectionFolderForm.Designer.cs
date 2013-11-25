namespace PlexMusicPlaylists.Import
{
  partial class SectionFolderForm
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SectionFolderForm));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panelBottom = new System.Windows.Forms.Panel();
      this.panelGrid = new System.Windows.Forms.Panel();
      this.btnClose = new System.Windows.Forms.Button();
      this.panelTop = new System.Windows.Forms.Panel();
      this.panelData = new System.Windows.Forms.Panel();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.gvFolders = new System.Windows.Forms.DataGridView();
      this.btnReloadFolders = new System.Windows.Forms.ToolStripButton();
      this.panelProgress = new System.Windows.Forms.Panel();
      this.panelToolstrip = new System.Windows.Forms.Panel();
      this.labelProgress = new System.Windows.Forms.Label();
      this.labelProgressSub = new System.Windows.Forms.Label();
      this.isMusicDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.sectionUrlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.librarySectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.tableLayoutPanel1.SuspendLayout();
      this.panelBottom.SuspendLayout();
      this.panelGrid.SuspendLayout();
      this.panelTop.SuspendLayout();
      this.panelData.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvFolders)).BeginInit();
      this.panelProgress.SuspendLayout();
      this.panelToolstrip.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.librarySectionBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.panelBottom, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.panelGrid, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(917, 703);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // panelBottom
      // 
      this.panelBottom.Controls.Add(this.btnClose);
      this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelBottom.Location = new System.Drawing.Point(3, 656);
      this.panelBottom.Name = "panelBottom";
      this.panelBottom.Size = new System.Drawing.Size(911, 44);
      this.panelBottom.TabIndex = 0;
      // 
      // panelGrid
      // 
      this.panelGrid.Controls.Add(this.panelData);
      this.panelGrid.Controls.Add(this.panelTop);
      this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelGrid.Location = new System.Drawing.Point(3, 3);
      this.panelGrid.Name = "panelGrid";
      this.panelGrid.Size = new System.Drawing.Size(911, 647);
      this.panelGrid.TabIndex = 1;
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnClose.Location = new System.Drawing.Point(823, 12);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // panelTop
      // 
      this.panelTop.Controls.Add(this.panelToolstrip);
      this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelTop.Location = new System.Drawing.Point(0, 0);
      this.panelTop.Name = "panelTop";
      this.panelTop.Size = new System.Drawing.Size(911, 29);
      this.panelTop.TabIndex = 0;
      // 
      // panelData
      // 
      this.panelData.Controls.Add(this.gvFolders);
      this.panelData.Controls.Add(this.panelProgress);
      this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelData.Location = new System.Drawing.Point(0, 29);
      this.panelData.Name = "panelData";
      this.panelData.Size = new System.Drawing.Size(911, 618);
      this.panelData.TabIndex = 1;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnReloadFolders});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(911, 25);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // gvFolders
      // 
      this.gvFolders.AllowUserToAddRows = false;
      this.gvFolders.AllowUserToDeleteRows = false;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.gvFolders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.gvFolders.AutoGenerateColumns = false;
      this.gvFolders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvFolders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isMusicDataGridViewCheckBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.sectionUrlDataGridViewTextBoxColumn});
      this.gvFolders.DataSource = this.librarySectionBindingSource;
      this.gvFolders.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvFolders.Location = new System.Drawing.Point(0, 75);
      this.gvFolders.MultiSelect = false;
      this.gvFolders.Name = "gvFolders";
      this.gvFolders.ReadOnly = true;
      dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.gvFolders.RowsDefaultCellStyle = dataGridViewCellStyle2;
      this.gvFolders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvFolders.Size = new System.Drawing.Size(911, 543);
      this.gvFolders.TabIndex = 0;
      // 
      // btnReloadFolders
      // 
      this.btnReloadFolders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnReloadFolders.Image = ((System.Drawing.Image)(resources.GetObject("btnReloadFolders.Image")));
      this.btnReloadFolders.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnReloadFolders.Name = "btnReloadFolders";
      this.btnReloadFolders.Size = new System.Drawing.Size(86, 22);
      this.btnReloadFolders.Text = "Reload folders";
      this.btnReloadFolders.Click += new System.EventHandler(this.btnReloadFolders_Click);
      // 
      // panelProgress
      // 
      this.panelProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.panelProgress.Controls.Add(this.labelProgressSub);
      this.panelProgress.Controls.Add(this.labelProgress);
      this.panelProgress.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelProgress.Location = new System.Drawing.Point(0, 0);
      this.panelProgress.Name = "panelProgress";
      this.panelProgress.Size = new System.Drawing.Size(911, 75);
      this.panelProgress.TabIndex = 1;
      this.panelProgress.Visible = false;
      // 
      // panelToolstrip
      // 
      this.panelToolstrip.Controls.Add(this.toolStrip1);
      this.panelToolstrip.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelToolstrip.Location = new System.Drawing.Point(0, 0);
      this.panelToolstrip.Name = "panelToolstrip";
      this.panelToolstrip.Size = new System.Drawing.Size(911, 29);
      this.panelToolstrip.TabIndex = 2;
      // 
      // labelProgress
      // 
      this.labelProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.labelProgress.Dock = System.Windows.Forms.DockStyle.Top;
      this.labelProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelProgress.Location = new System.Drawing.Point(0, 0);
      this.labelProgress.Name = "labelProgress";
      this.labelProgress.Size = new System.Drawing.Size(911, 32);
      this.labelProgress.TabIndex = 0;
      this.labelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelProgressSub
      // 
      this.labelProgressSub.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.labelProgressSub.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelProgressSub.Location = new System.Drawing.Point(0, 50);
      this.labelProgressSub.Name = "labelProgressSub";
      this.labelProgressSub.Size = new System.Drawing.Size(911, 25);
      this.labelProgressSub.TabIndex = 1;
      this.labelProgressSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // isMusicDataGridViewCheckBoxColumn
      // 
      this.isMusicDataGridViewCheckBoxColumn.DataPropertyName = "IsMusic";
      this.isMusicDataGridViewCheckBoxColumn.HeaderText = "Music";
      this.isMusicDataGridViewCheckBoxColumn.Name = "isMusicDataGridViewCheckBoxColumn";
      this.isMusicDataGridViewCheckBoxColumn.ReadOnly = true;
      this.isMusicDataGridViewCheckBoxColumn.Width = 40;
      // 
      // titleDataGridViewTextBoxColumn
      // 
      this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
      this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
      this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
      this.titleDataGridViewTextBoxColumn.ReadOnly = true;
      this.titleDataGridViewTextBoxColumn.Width = 280;
      // 
      // sectionUrlDataGridViewTextBoxColumn
      // 
      this.sectionUrlDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.sectionUrlDataGridViewTextBoxColumn.DataPropertyName = "SectionUrl";
      this.sectionUrlDataGridViewTextBoxColumn.HeaderText = "SectionUrl";
      this.sectionUrlDataGridViewTextBoxColumn.Name = "sectionUrlDataGridViewTextBoxColumn";
      this.sectionUrlDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // librarySectionBindingSource
      // 
      this.librarySectionBindingSource.DataSource = typeof(PlexMusicPlaylists.PlexMediaServer.LibrarySection);
      // 
      // SectionFolderForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(917, 703);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "SectionFolderForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Folders in section";
      this.Shown += new System.EventHandler(this.SectionFolderForm_Shown);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panelBottom.ResumeLayout(false);
      this.panelGrid.ResumeLayout(false);
      this.panelTop.ResumeLayout(false);
      this.panelData.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvFolders)).EndInit();
      this.panelProgress.ResumeLayout(false);
      this.panelToolstrip.ResumeLayout(false);
      this.panelToolstrip.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.librarySectionBindingSource)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel panelBottom;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Panel panelGrid;
    private System.Windows.Forms.Panel panelData;
    private System.Windows.Forms.DataGridView gvFolders;
    private System.Windows.Forms.BindingSource librarySectionBindingSource;
    private System.Windows.Forms.Panel panelTop;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnReloadFolders;
    private System.Windows.Forms.Panel panelToolstrip;
    private System.Windows.Forms.Panel panelProgress;
    private System.Windows.Forms.Label labelProgress;
    private System.Windows.Forms.Label labelProgressSub;
    private System.Windows.Forms.DataGridViewCheckBoxColumn isMusicDataGridViewCheckBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn sectionUrlDataGridViewTextBoxColumn;
  }
}