using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlexMusicPlaylists
{
  public partial class PMSServerForm : Form
  {
    public PMSServerUserControl PMSServerUserControl { get { return ucPMSServer; } }

    public PMSServerForm()
    {
      InitializeComponent();
    }
  }
}
