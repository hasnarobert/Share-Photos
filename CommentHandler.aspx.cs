using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SharePhotos.Database.Entities;

public partial class CommentHandler : System.Web.UI.Page
{
    public MembershipUser user = null;
    public string userName = null;
    int idComment = 0;
    int idPhoto = 0;
    public string action = null;
    public Photo photo = null;
    public Album album = null;
    public Comment comment = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        user = Membership.GetUser();
        if (user == null)
        {
            Response.Redirect("Default.aspx");
        }
        else {
            userName = user.UserName;
            idComment = int.Parse(Request.Params.Get("id-comment").ToString());
            action = Request.Params.Get("action").ToString();
            try
            {
                comment = new Comment(idComment);
                photo = new Photo(comment.getPhotoId());
                album = photo.getAlbum();
            }
            catch (Exception ex) {
                Response.Redirect("Default.aspx");
            }
        }
    }
}