using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharePhotos.Database.Entities;
using System.Web.Security;

public partial class PhotoWall : System.Web.UI.Page
{
    public Photo photo = null;
    public Album album = null;
    public Comment[] comments = null;
    public string photoUrl = null;
    public int photoId;
    public string userName = null;
    public MembershipUser user = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        user = Membership.GetUser();
        if (user != null) 
            userName = user.UserName;
        
        if (Request.Params.AllKeys.Contains<string>("id"))
        {

            photoId = int.Parse(Request.Params.Get("id"));
            try
            {
                photo = new Photo(photoId);
                album = photo.getAlbum();
                photoUrl = "Photos/" + photo.getId() + ".jpg";
                comments = photo.getComments();
            }
            catch (Exception ex)
            {
                Response.Redirect("Default.aspx");
            }
                
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void postComment(object sender, EventArgs e)
    {
        TextBox commentText = (TextBox)sender;
        Comment.createComment(commentText.Text, userName, photoId);
        Response.Redirect(Request.Url.AbsoluteUri);
    }

}