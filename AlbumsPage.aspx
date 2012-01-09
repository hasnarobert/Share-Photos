<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AlbumsPage.aspx.cs" Inherits="AlbumsPage" %>

<asp:Content  ContentPlaceHolderID="MainContent" Runat="Server">
    <% 
        if (album == null)
        {
            %><h1>Select one Album</h1>
            <%
            for (int i = 0; i < allAlbums.Length; ++i)
            {
                SharePhotos.Database.Entities.Photo[] photos = allAlbums[i].getPhotos();
                if (photos.Length > 0)
                {
                    string theUrl = "Photos/" + photos[0].getId() + ".jpg";
                    string desc = allAlbums[i].getName();
                    %>
                        <div class="thumbnail" style="height: 300px;">
                            <a href="AlbumsPage.aspx?album=<%=allAlbums[i].getId() %>">
                                <img src="<%=theUrl %>" alt="<%=desc %>"/>
                            </a>
                            <span><%=desc%></span>
                        </div>
                    <%
                }
                else { 
                    string theUrl = "Photos/empty.jpg";
                    string desc = allAlbums[i].getName() + " is empty";
                    %>
                        <div class="thumbnail" style="height: 300px;">
                            <a href="AlbumsPage.aspx?album=<%=allAlbums[i].getId() %>">
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
    
    
    <%
        if (allPhotos.Length > 0) { 
            %>
            <h1>Photos from album : <%= album.getName()%></h1>
            <%
        }
        else  { 
            %>
            <h1><%= album.getName()%> is empty</h1>
            <div style="height: 100px;"></div>
            <%
        }
        
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

    <% if (album != null)
       {%>
    <h1>Upload a photo to this album : </h1>
    <asp:FileUpload runat="server" ID="selectedPhoto" CssClass="upload-file"/>
    <div>
        <asp:TextBox runat="server" ID="PhotoDescription" CssClass="comment">Enter photo description...</asp:TextBox>
    </div>
    <div style="margin-left: 20px;">
        <asp:TextBox ID="categoryName" runat="server">Category Name ...</asp:TextBox>
        <!--<asp:DropDownList
            ID="DropDownListCategory" runat="server" >
        </asp:DropDownList>
        <%
            for (int i = 0; i < allCategories.Length; ++i) {
                DropDownListCategory.Items.Add(new ListItem(allCategories[i].getName(), allCategories[i].getName(), true));
            }
        %>-->
        <asp:Button runat="server" Text="Upload" OnClick="uploadPhoto"/>
    </div>
    <div style="height: 100px;"></div>
    <%} %>

    <h1>Create a new album : </h1>
    <label style="margin-left: 10px;">Album name : </label>
    <asp:TextBox runat="server" ID="NewAlbumName"></asp:TextBox>
    <asp:Button runat="server" ID="NewAlbumButton" Text="Create" OnClick="createAlbum"/>
</asp:Content>

