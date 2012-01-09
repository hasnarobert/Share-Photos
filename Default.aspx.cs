using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharePhotos.Database.Entities;

public partial class _Default : System.Web.UI.Page
{
    public Photo[] allPhotos = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        allPhotos = Photo.getAllPhotos();
    }
}
