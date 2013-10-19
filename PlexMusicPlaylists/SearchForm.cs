using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlexMusicPlaylists;

namespace PlexMusicPlaylists
{
  public partial class SearchForm : Form
  {

    public string SearchQuery
    {
      get { return tbSearchQuery.Text.Trim(); }
    }

    public SearchForm()
    {
      InitializeComponent();
    }

    public void SetSearchSection(PlexMediaServer.SearchSection _searchSection)
    {
      tbSearchQuery.Text = "";
      if (_searchSection != null)
      {
        lblPrompt.Text = _searchSection.Prompt;
        lblTitle.Text = _searchSection.Title;
      }
    }

    private void enableCommands()
    {
      btnOk.Enabled = !String.IsNullOrEmpty(SearchQuery);
    }

   
    private void btnOk_Click(object sender, EventArgs e)
    {

    }

    private void tbSearchQuery_TextChanged(object sender, EventArgs e)
    {
      enableCommands();
    }

    private void SearchForm_Shown(object sender, EventArgs e)
    {
      tbSearchQuery.Focus();   
    }
  }
}
