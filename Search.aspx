<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Search photos</h1>
    <div style="margin-left: 20px; margin-top: 50px;" >
        <asp:TextBox runat="server" ID="SearchText">Search ...</asp:TextBox>
        <asp:Button runat="server" OnClick="doSearch" Text="Search"/>
    </div>

     <%
         if (results != null)
        for (int i = 0; i < results.Length; ++i)
        {
            string theUrl = "Photos/" + results[i].getId() + ".jpg";
            string desc = results[i].getDescription();
            string categ = results[i].getCategory().getName();
    %>
            <div class="thumbnail">
                <a href="PhotoWall.aspx?id=<%=results[i].getId() %>">
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
    
</asp:Content>

