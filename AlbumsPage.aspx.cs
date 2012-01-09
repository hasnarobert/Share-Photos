using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharePhotos.Database.Entities;
using System.Web.Security;

public partial class AlbumsPage : System.Web.UI.Page
{
    public Photo[] allPhotos = null;
    public Album album = null;
    public Album[] allAlbums = null;
    public MembershipUser user = Membership.GetUser();
    public string userName = null;
    public SharePhotos.Database.Entities.Category[] allCategories = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        allCategories = SharePhotos.Database.Entities.Category.getAllCategories();
        if (user == null)
        {
            Response.Redirect("Default.aspx");
        }
        else {
            userName = user.UserName;
        }
        if (Request.Params.AllKeys.Contains<string>("album"))
        {

            int album_id = int.Parse(Request.Params.Get("album").ToString());
            try
            {
                album = new Album(album_id);
            }
            catch (Exception ex)
            {
                Response.Redirect("Default.aspx");
            }
            allPhotos = album.getPhotos();
        }
        else
        {
            allAlbums = Album.getAllAlbums(userName);
        }
    }

    protected void createAlbum(object sender, EventArgs e)
    {
        string AlbumName = NewAlbumName.Text;
        if (AlbumName == "")
            return;

        NewAlbumName.Text = "";
        Album.createAlbum(userName, AlbumName);
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void uploadPhoto(object sender, EventArgs e)
    {
        string description = PhotoDescription.Text;
        //string selValue = DropDownListCategory.SelectedValue;
        if (/*selValue == null || */description == null || selectedPhoto.HasFile == false) {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        SharePhotos.Database.Entities.Category category = null;
        try
        {
            category = new SharePhotos.Database.Entities.Category(categoryName.Text);
        }
        catch (Exception ex) {
            categoryName.Text = "Does not exists";
            return;
        }

        int categoryId = category.getId();
        int newId = Photo.createPhoto(album.getId(), categoryId, description);
        selectedPhoto.SaveAs("C:\\Users\\rhasna\\Documents\\Visual Studio 2010\\WebSites\\SharePhotos\\Photos\\" + newId + ".jpg");
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}