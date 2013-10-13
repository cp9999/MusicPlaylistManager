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

    public MainForm()
    {
      InitializeComponent();
      playlistUC.OnLogMessage += new PlaylistUserControl.LogMessageEventHandler(addToLog);
      splitBottom.Panel2Collapsed = true;
      loadConfiguration();
    }

    private void loadConfiguration()
    {
      tbPlexIP.Text = playlistSettings.IP;
      udPlexPort.Value = playlistSettings.Port;
      enableCommands();
    }
    private void enableCommands()
    {
      btnConnect.Enabled = !String.IsNullOrEmpty(tbPlexIP.Text.Trim()) && udPlexPort.Value > 0;
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
  }
}
