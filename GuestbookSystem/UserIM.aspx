<%@ Page Title="" Language="C#" MasterPageFile="~/NavMaster.Master"  AutoEventWireup="true" CodeBehind="UserIM.aspx.cs" Inherits="GuestbookSystem.UserIM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 { width: 150px; }
        .auto-style4 { width: 120px; }
        .auto-style5 {width: 80px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- 右半部分-->
    <div class="box_right col-lg-9 ">
        <div class="segment left">

            <i class=" glyphicon glyphicon-cog left"></i>
            <h3 class="left">用户管理系统<br />
                <span>管理这个设备及其传感器</span>
            </h3>
        </div>
        <div class="clear"></div>
        <!--文章列表-->
        <div class="segment3">
            <div class="table-responsive" style="min-height: 400px">
                <table class="table" style="width: auto">
                    <thead>
                        <tr>
                            <th>选择</th>
                            <th class="auto-style5">头像</th>
                            <th class="auto-style4">用户名</th>
                            <th class="auto-style5">性别</th>
                            <th class="auto-style4">电话</th>
                            <th class="auto-style3">注册时间</th>
                            <th class="auto-style5">权限</th>
                            <th class="auto-style4">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                            <ItemTemplate>
                                <tr class="mylist">
                                    <td><asp:CheckBox ID="CheckBox" name="subBox" CssClass="checkbox" runat="server" /></td>
                                    <asp:HiddenField ID="HF1" Value='<%# Eval("Uid") %>' runat="server" />
                                    <td><asp:Image ID="Image1" runat="server" Width="45" Height="36" ImageUrl='<%#Eval("ImageUrl")%>' /></td> 
                                    <td><%#Eval("Uname")%></td>                                   
                                    <td><%#Convert.ToInt32( Eval("Gendar"))==0 ?"先生":"女士" %></td>
                                    <td><%# Eval("Phone")%></td>
                                    <td><%# Eval("CreateTime")%></td>
                                    <td><%#Convert.ToInt32( Eval("Type"))==1?"管理员":"用户" %></td>
                                    <td><asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("Uid")%>' runat="server"><span class="glyphicon glyphicon-edit"></span>查看修改</asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>          
            </div>
            <div class="case_page" id="xuanze" runat="server">
                    <div class="page_left">
                        <input type='button' id="quanxuan" value="全选" />
                        <input type='button' id="fanxuan" value="反选" />
                        <input type='button' id="buxuan" value="全不选" />
                        <asp:LinkButton ID="delete" runat="server" OnClick="delete_Click"> <span class="glyphicon glyphicon-trash"></span> &nbsp;删除</asp:LinkButton>
                    </div>
                </div>
        </div>
    </div>
     <script type="text/javascript">
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
