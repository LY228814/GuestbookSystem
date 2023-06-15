<%@ Page Title="发布页" Language="C#" MasterPageFile="~/NavMaster.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Information.aspx.cs" Inherits="GuestbookSystem.Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--右半部分-->
    <div class="box_right col-lg-9 ">
        <div class="segment left">
            <i class=" glyphicon glyphicon-book left"></i>
            <h3 class="left">发布信息<br />
            </h3>
        </div>

        <div class="clear"></div>
        <!--发布日志-->
        <div id="rizhi_box" class="left" style="padding-left: 30px; padding-top: 10px;">
            <div class="my-group" id="gruop2">
                <span class="left"><mark>*</mark>信息标题</span>
                <asp:TextBox ID="RXbt" type="text" CssClass="form-control put3" placeholder="请输入标题" MaxLength="25" required="required" runat="server"></asp:TextBox>
            </div>
            <div class="my-group" id="group2">
                <span class="left"><mark>*</mark>信息内容</span>
                <textarea id="RZwb" class="form-control put3 put4" runat="server" required="required" placeholder="请输入内容"></textarea>
            </div>

            <div class="form-actions">
                <asp:Button ID="commit" CssClass="btn-primary btn" type="submit" CommandName="insert" runat="server" Text="确认发布 >" OnClientClick="fubu" OnClick="commit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
