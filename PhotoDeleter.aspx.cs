using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SharePhotos.Database.Entities;

public partial class PhotoDeleter : System.Web.UI.Page
{
    public MembershipUser user = null;
    public string userName = null;
    int idPhoto = 0;
    public Photo photo = null;
    public Album album = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        user = Membership.GetUser();
        if (user == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            userName = user.UserName;
            idPhoto = int.Parse(Request.Params.Get("id-photo").ToString());
            try
            {
                photo = new Photo(idPhoto);
                album = photo.getAlbum();
            }
            catch (Exception ex)
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}