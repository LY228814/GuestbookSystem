<%@ Page Title="" Language="C#" MasterPageFile="~/NavMaster.Master" AutoEventWireup="true" CodeBehind="show.aspx.cs" Inherits="GuestbookSystem.show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function jiaodian() {
            $("#<%= PLText.ClientID%>").focus();
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            left: 0px;
            top: 0px;
            width: 647px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="box_right col-lg-9 ">
        <div class="segment left">
            <i class=" glyphicon glyphicon-book left"></i>
            <h3 class="left">信息内容<br />
            </h3>
        </div>
        <div class="clear"></div>
        <!--信息内容-->
        <div class="show">
            <h2>
                <asp:Label ID="biaoti" CssClass="h2" runat="server" Text="biaoti"></asp:Label></h2>
            <div class="show_time">作者：<asp:Label ID="name" runat="server" Text=""></asp:Label>
                发布时间：<asp:Label ID="time" runat="server" Text=""></asp:Label>
                阅读：<asp:Label ID="num" runat="server" Text=""></asp:Label></div>
            <p>
                <asp:Label ID="neirong" runat="server" Text=""></asp:Label></p>
        </div>
        <!--提交留言/回复-->
        <div id="rizhi_box" class="left" style="padding-left: 30px; padding-top: 10px;">

            <div class="auto-style1" id="group2">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <span class="left"><mark>*</mark><asp:Label ID="hfren" runat="server" Text="留言信息"></asp:Label>：</span>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:LinkButton ID="plwz" CssClass=" right" OnClientClick="jiaodian()" runat="server" OnClick="plwz_Click">点击留言信息</asp:LinkButton>
                <textarea id="PLText" class="form-control put3 put5" runat="server" placeholder="请输入内容"></textarea>
            </div>
            <div class="form-actions">
                <asp:Button ID="Button1" CssClass="btn-primary btn" runat="server" Text="确认提交 > " OnClick="Button1_Click" />
            </div>
        </div>
        <!--评论开始-->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound" OnItemCommand="Repeater1_ItemCommand">
                    <ItemTemplate>
                        <div class="pinglun">
                            <div class="pl_left left">
                                <asp:Image ID="Imagepl" ImageUrl='<%# Eval("ImageUrl") %>' runat="server" />
                            </div>
                            <div class="pl_right left">
                                <asp:HiddenField ID="HF1" Value='<%# Eval("Mid") %>' runat="server" />
                                <p class="p_a">
                                    <span><%# Eval("xh") %>楼</span>&nbsp;&nbsp;&nbsp;留言时间: <%# Eval("CreateTime") %>
                                    <asp:LinkButton ID="huifu" CommandArgument='<%# Eval("Mid") %>' OnClientClick="jiaodian()" CommandName="huifu" runat="server">回复</asp:LinkButton>
                                    <asp:LinkButton ID="delete" CommandArgument='<%# Eval("Mid") %>' CommandName="delete" runat="server"><span class="glyphicon glyphicon-trash"></span>删除</asp:LinkButton>
                                </p>
                                </p>
                        <p>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Uname") %>'></asp:Label>
                            ：<%# Eval("Contents") %></p>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="reply">
                                            <asp:Repeater ID="Repeater2" runat="server">
                                                <ItemTemplate>
                                                    <p>
                                                        回复时间: <%# Eval("CreateTime") %>
                                                        <asp:Image ID="Imagehf" ImageUrl='<%# Eval("ImageUrl") %>' Width="35" CssClass="left" runat="server" />
                                                    <p class="left"><span><%# Eval("Uname") %>：</span><%# Eval("Contents") %></p>
                                                    <br />
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>
</asp:Content>
