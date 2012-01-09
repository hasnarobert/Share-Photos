<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PhotoWall.aspx.cs" Inherits="PhotoWall" %>

<asp:Content ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="wall-photo">
        <img src="<%=photoUrl %>" alt="<%=photo.getDescription() %>" />
        <asp:LoginView runat="server">
            <RoleGroups>
                <asp:RoleGroup Roles="user">
                    <ContentTemplate>
                        <%
                            if (album.getUserId() == userName)
                            { 
                            %> 
                                <a href="PhotoDeleter.aspx?id-photo=<%=photo.getId() %>" class="delete">[delete]</a>
                            <%
                            }
                        %>
                     </ContentTemplate>
                </asp:RoleGroup>
                <asp:RoleGroup Roles="admin">
                    <ContentTemplate>
                        <a href="PhotoDeleter.aspx?id-photo=<%=photo.getId() %>" class="delete">[delete]</a>
                    </ContentTemplate>
                </asp:RoleGroup>
            </RoleGroups>
        </asp:LoginView>
    </div>

    <asp:LoginView runat="server">
        <RoleGroups>
            <asp:RoleGroup Roles="admin">
                <ContentTemplate>
                <% 
                    for (int i = 0; i < comments.Length; ++i)
                    {
                  
                        string nume = comments[i].getUserId();
                        string text = comments[i].getText();
                        string deleteComment = "<a href=\"CommentHandler.aspx?id-comment=" + comments[i].getId() + "&action=delete\" class=\"delete\">[delete]</a>";
                        %>
                        <div class="comment">
                            <div>
                                <%=nume%> 
                                <a href="CommentHandler.aspx?id-comment= <%=comments[i].getId()%> &action=delete" class="delete">[delete]</a>
                                <%
                                    if (comments[i].isAccepted() == false && album.getUserId() == userName)
                                    { 
                                    %> 
                                        <a href="CommentHandler.aspx?id-comment= <%=comments[i].getId()%> &action=accept" class="accept">[accept]</a>
                                    <%
                                    }
                                %>
                            </div>
                            <div><%=text%></div>
                        </div>

                        <%
                        
                    } %>
                </ContentTemplate>
            </asp:RoleGroup>
            <asp:RoleGroup Roles="user">
                <ContentTemplate>
                    <% 
                    for (int i = 0; i < comments.Length; ++i)
                    {
                        if (comments[i].isAccepted() || album.getUserId() == userName)
                        {
                            string nume = comments[i].getUserId();
                            string text = comments[i].getText();
                            string deleteComment = "<a href=\"CommentHandler.aspx?id-comment=" + comments[i].getId() + "&action=delete\" class=\"delete\">[delete]</a>";
                            %>
                            <div class="comment">
                                <div>
                                    <%=nume%>
                                    <%
                                        if (album.getUserId() == userName)
                                        { 
                                        %> 
                                            <a href="CommentHandler.aspx?id-comment= <%=comments[i].getId()%> &action=delete" class="delete">[delete]</a>
                                        <%
                                        }
                                        if (comments[i].isAccepted() == false && album.getUserId() == userName)
                                        { 
                                        %> 
                                            <a href="CommentHandler.aspx?id-comment= <%=comments[i].getId()%> &action=accept" class="accept">[accept]</a>
                                        <%
                                        }
                                    %>
                                </div>
                                <div><%=text%></div>
                            </div>

                            <%
                        }
                    } %>
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
    </asp:LoginView>


    <asp:LoginView runat="server">
        <LoggedInTemplate>
            <asp:TextBox ID="CommentText" runat="server" CssClass="comment" OnTextChanged="postComment">Add comment...</asp:TextBox>
        </LoggedInTemplate>
        <AnonymousTemplate>
            <div class="comment">Log in to be able to add comments.</div>
        </AnonymousTemplate>
    </asp:LoginView>
</asp:Content>

