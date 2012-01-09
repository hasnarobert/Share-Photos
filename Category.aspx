<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Category" %>

<asp:Content ContentPlaceHolderID="MainContent" Runat="Server">
    <% 
        if (category == null)
        {
            %><h1>Select one category</h1>
            <%
            for (int i = 0; i < allCategories.Length; ++i)
            {
                SharePhotos.Database.Entities.Photo[] photos = allCategories[i].getPhotos();
                if (photos.Length > 0)
                {
                    string theUrl = "Photos/" + photos[0].getId() + ".jpg";
                    string desc = allCategories[i].getName(); ;
                    %>
                        <div class="thumbnail" style="height: 300px;">
                            <a href="Category.aspx?category=<%=allCategories[i].getName() %>">
                                <img src="<%=theUrl %>" alt="<%=desc %>"/>
                            </a>
                            <span><%=desc%></span>
                        </div>
                    <%
                }   
            }
        }
        else
        {
    %>
    
    
    <h1>
        Latest photos from category : <%= category.getName()%>
    </h1>
    <%
        for (int i = 0; i < allPhotos.Length; ++i)
        {
            string theUrl = "Photos/" + allPhotos[i].getId() + ".jpg";
            string desc = allPhotos[i].getDescription();
            string categ = allPhotos[i].getCategory().getName();
    %>
            <div class="thumbnail">
                <a href="PhotoWall.aspx?id=<%=allPhotos[i].getId() %>">
                    <img src="<%=theUrl %>" alt="<%=desc %>"/>
                </a>
                <div>
                    <div><%=categ %></div>
                    <div><%=desc %></div>
                </div>
            </div>
    <%
            
        }
    %>
    <% } %>
    <div style="clear: both"></div>

    <h1>Create a new category : </h1>
    <label style="margin-left: 10px;">Category name : </label>
    <asp:TextBox runat="server" ID="NewCategoryName"></asp:TextBox>
    <asp:Button runat="server" ID="NewCategoryButton" Text="Create" OnClick="createCategory"/>
</asp:Content>

