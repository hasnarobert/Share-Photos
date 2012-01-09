using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharePhotos.Database.Entities;

public partial class Category : System.Web.UI.Page
{
    public Photo[] allPhotos = null;
    public SharePhotos.Database.Entities.Category category = null;
    public SharePhotos.Database.Entities.Category[] allCategories = null;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params.AllKeys.Contains<string>("category"))
        {

            string category_name = Request.Params.Get("category");
            try
            {
                category = new SharePhotos.Database.Entities.Category(category_name);
            }
            catch (Exception ex)
            {
                Response.Redirect("Default.aspx");
            }
            allPhotos = Photo.getAllPhotos(category.getId());
        }
        else {
            allCategories = SharePhotos.Database.Entities.Category.getAllCategories();
        }
    }

    protected void createCategory(object sender, EventArgs e)
    {
        string categoryName = NewCategoryName.Text;
        if (categoryName == "")
            return;

        NewCategoryName.Text = "";
        SharePhotos.Database.Entities.Category.createCategory(categoryName);
    }
}