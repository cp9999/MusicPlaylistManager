using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using PlexMusicPlaylists;
using System.Configuration;


namespace PlexMusicPlaylists
{
  public partial class MainForm : Form
  {
    PlexMediaServer.PlaylistSettings playlistSettings = PlexMediaServer.PlaylistSettings.theSettings();
    SearchForm searchForm = new SearchForm();
    List<PMSServerForm> serverForms = new List<PMSServerForm>();

    public MainForm()
    {
      InitializeComponent();
      playlistUC.OnLogMessage += new PlaylistUserControl.LogMessageEventHandler(addToLog);
      playlistUC.OnSearchInput += new PMSServerUserControl.SearchInputHandler(onSearchInput);
      splitBottom.Panel2Collapsed = false;
      loadConfiguration();
    }

    bool onSearchInput(PlexMediaServer.SearchSection _searchSection, out string _query)
    {
      _query = "";
      searchForm.SetSearchSection(_searchSection);
      if (searchForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        _query = searchForm.SearchQuery;
        return true;
      }
      return false;
    }

    private void loadConfiguration()
    {
      tbPlexIP.Text = playlistSettings.IP;
      udPlexPort.Value = playlistSettings.Port;
      tbPlexDatabase.Text = playlistSettings.PlaylistDB;
      enableCommands();
      if (btnConnect.Enabled && playlistSettings.AutoConnect)
      {
        btnConnect_Click(btnConnect, null);
      }
    }
    private void enableCommands()
    {
      btnConnect.Enabled = !String.IsNullOrEmpty(tbPlexIP.Text.Trim()) && udPlexPort.Value > 0;
      btnOpenServer.Visible = playlistSettings.ServerInSeparatedWindow;
    }

    private void addToLog(string _message)
    {      
      rtbLog.AppendText(_message + "\n");
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
      int port = (int)(new DecimalConverter().ConvertTo(udPlexPort.Value, typeof(int)));
      if (playlistUC.setPlexMediaServerIP(tbPlexIP.Text, port))
      {
        playlistSettings.IP = tbPlexIP.Text;
        playlistSettings.Port = port;
        playlistSettings.Save();
        openPMSServer(true);
      }
    }

    private void tbPlexIP_TextChanged(object sender, EventArgs e)
    {
      enableCommands();
    }

    private void udPlexPort_ValueChanged(object sender, EventArgs e)
    {
      enableCommands();
    }


    private void btnSettings_Click(object sender, EventArgs e)
    {
      SettingsForm settingsForm = new SettingsForm();
      settingsForm.ShowDialog();
      playlistSettings.PlaylistDB = playlistSettings.PlaylistDB.Replace("\\", "/");
      playlistSettings.ChannelDataFolder = playlistSettings.ChannelDataFolder.Replace("\\", "/");
      tbPlexDatabase.Text = playlistSettings.PlaylistDB;
      playlistSettings.Save();
      btnOpenServer.Visible = playlistSettings.ServerInSeparatedWindow;
      if (!playlistSettings.ServerInSeparatedWindow)
      {
        this.closePMSServers();
      }
      else if (serverForms.Count() == 0)
      {
        this.openPMSServer(true);
      }
      playlistUC.settingsChanged();
    }

    private void openPMSServer(bool _onlyIfFirst = true)
    {
      if (playlistSettings.ServerInSeparatedWindow)
      {
        if (_onlyIfFirst && serverForms.Count() > 0)
        {
          PMSServerForm openForm = serverForms.First();
          openForm.Activate();
        }
        else
        {
          PMSServerForm serverForm = new PMSServerForm();
          serverForm.FormClosing += new FormClosingEventHandler(serverForm_FormClosing);
          serverForm.PMSServerUserControl.OnSearchInput += new PMSServerUserControl.SearchInputHandler(onSearchInput);
          serverForms.Add(serverForm);
          serverForm.Show();
          playlistUC.connectPMSServer(serverForm.PMSServerUserControl);
        }
      }
    }

    void serverForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      playlistUC.disconnectPMSServer(((PMSServerForm)sender).PMSServerUserControl);
      serverForms.Remove((PMSServerForm)sender);
      btnOpenServer.Enabled = playlistSettings.ServerAllowMultipleWindows || serverForms.Count() == 0;
    }

    private void closePMSServers()
    {
      foreach (var form in serverForms)
      {
        form.FormClosing -= new FormClosingEventHandler(serverForm_FormClosing);
        form.Close();
      }
      serverForms.Clear();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      closePMSServers();
    }

    private void btnOpenServer_Click(object sender, EventArgs e)
    {
      this.openPMSServer(!playlistSettings.ServerAllowMultipleWindows);
    }
  }
}
