using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharePhotos.Database.Entities;

public partial class Search : System.Web.UI.Page
{
    public Photo[] results = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        string query = Request.Params.Get("query");
        if (query != null && query != "")
        {
            results = Photo.search(query);
        }
        else {
            results = null;
        }
    }

    protected void doSearch(object sender, EventArgs e)
    {
        if (SearchText.Text != "" && SearchText.Text != "Search ...") {
            Response.Redirect("Search.aspx?query=" + SearchText.Text);
        }
    }
}