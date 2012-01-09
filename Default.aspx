<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>
        Latest photos
    </h1>
    <%
        for (int i = 0; i < allPhotos.Length; ++i) {
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
    <div style="clear: both"></div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="NavigationContent">
</asp:Content>