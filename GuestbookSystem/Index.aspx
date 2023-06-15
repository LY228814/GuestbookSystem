<%@ Page Title="首页" Language="C#" MasterPageFile="~/NavMaster.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="GuestbookSystem.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!--右半部分-->     
    <div class="box_right col-lg-9 ">
        <div class="segment left">
            <i class=" glyphicon glyphicon-book left"></i>
            <h3 class="left">信息列表<br />
                <span></span>
            </h3>
        </div>
        <div class="clear">
             <div class="col-sm-5">
                                <div class="input-group">
                                    <asp:TextBox ID="keyword" placeholder="请输入信息标题关键词" CssClass="input-sm form-control" runat="server" Width="548px"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button ID="search" CssClass="btn btn-sm btn-primary" runat="server" Text="搜索" OnClick="search_Click"  />
                                        <a href="Information.aspx" class="btn btn-sm btn-primary">添加</a>
                                    </span>
                                </div>
                            </div>
        </div>
       <!--文章列表-->
       
    <div class="segment3">
        <div class="table-responsive" style="min-height: 400px">
            <table class="table" >
                <thead>
                    <tr>
                        <th>选择</th>
                        <th>作者</th>
                        <th>信息标题</th>
                        <th>查看次数</th>
                        <th>创建时间</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <tr class="mylist">
                                <td>
                                    <asp:CheckBox ID="CheckBox" runat="server" /></td>
                                <asp:HiddenField ID="HF1" runat="server" Value='<%#Eval("Uid")%>' />
                                <td><%#Eval("Uname") %></td>
                                <td><%#Eval("Title") %></td>
                                <td><%#Eval("SeeCount") %></td>
                                <td><%#Eval("CreateTime") %></td>
                                <td>
                                    <asp:LinkButton ID="update" CommandName="update" CommandArgument='<%# Eval("Iid") %>' runat="server">修改</asp:LinkButton>
                                    <asp:LinkButton ID="select" CommandName="create" CommandArgument='<%# Eval("Iid") %>' runat="server">查看</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            
            </div>
        <div class="case_page" id="xuanze" runat="server">
                <div class="page_left">
                    <input id="quanxuan" type="button" value="全选" />
                    <input id="buxuan" type="button" value="不选" />
                    <input id="fanxuan" type="button" value="反选" />
                    <span class="glyphicon glyphicon-trash"></span>
                    <asp:LinkButton ID="delete" runat="server" OnClick="delete_Click">&nbsp;删除</asp:LinkButton>
                </div>
        </div>
    </div>  
</div>

     <script type="text/javascript">

         //页面就绪事件,提供信息列表的全选反选和全不选
         $(function () {
             $("#quanxuan").click(function () {
                 $(":checkbox").prop("checked", true);
             });
             $("#buxuan").click(function () {
                 $(":checkbox").prop("checked", false);
             });
             $("#fanxuan").click(function () {
                 $(":checkbox").each(function () { //遍历每一个复选框
                     $(this).is(":checked") ? $(this).prop("checked", false) : $(this).prop("checked", true);
                 });
             });
         })
     </script>  
</asp:Content>
 