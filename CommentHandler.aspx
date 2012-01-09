<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommentHandler.aspx.cs" Inherits="CommentHandler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:LoginView runat="server">
            <RoleGroups>
                <asp:RoleGroup Roles="admin">
                    <ContentTemplate>
                    <%
                        if (action == "accept")
                        {
                            comment.accept();
                        }
                        else if (action == "delete"){
                            comment.delete();
                        }

                        Response.Redirect("PhotoWall.aspx?id=" + photo.getId());
                    %>
                    </ContentTemplate>
                </asp:RoleGroup>
                <asp:RoleGroup Roles="user">
                    <ContentTemplate>
                    <%
                        if (album.getUserId() == userName) {
                            if (action == "accept")
                            {
                                comment.accept();
                            }
                            else if (action == "delete")
                            {
                                comment.delete();
                            }
                        }
                        Response.Redirect("PhotoWall.aspx?id=" + photo.getId());
                    %>
                    </ContentTemplate>
                </asp:RoleGroup>
            </RoleGroups>
        </asp:LoginView>
    </form>
</body>
</html>
