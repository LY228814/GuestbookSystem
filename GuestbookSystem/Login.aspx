<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GuestbookSystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登录</title>
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="//cdn.bootcss.com/jquery-cookie/1.4.1/jquery.cookie.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="log_banner">
                <div class="inset">
                    <h1>登录页面</h1>
                    <p>Wish everyone happy and always with you!</p>
                </div>
            </div>
            <!--登录开始-->
            <div id="body">
                <div id="reg_box" class="left">

                    <div class="my-group">
                        <span class="left">账 号</span>
                        <asp:TextBox ID="Textname" CssClass="form-control put1" placeholder="请输入用户名" MaxLength="14" required="server" runat="server"></asp:TextBox>
                    </div>
                    <div class="my-group">
                        <span class="left">密 码</span>
                        <asp:TextBox ID="Textpwd" type="password"  CssClass="form-control put1" placeholder="请输入用密码"  MaxLength="14" required="required" runat="server"></asp:TextBox>
                    </div>

                    <div class="my-group">
                        <span class="left">验证码</span>
                        <div class="pull-right">
                            <asp:TextBox ID="TextYZ" CssClass="form-control put3 " Width="179px" placeholder="请输入验证码" required="required" runat="server"></asp:TextBox>
                            <asp:Image runat="server" ID="getcode" ImageUrl="ValidateCode.aspx"></asp:Image>&nbsp;
                <a id="yzmsx" href="#">点击刷新</a>
                        </div>
                    </div>
                    <div class="form-actions">
                        <asp:Button ID="Button1" CssClass="btn-primary btn " OnClientClick="return houtai()" runat="server" Text="登  录 留 言 簿 >" OnClick="Button1_Click"  />
                    </div>
                </div>
                <div class="side_box right">
                    <h2>欢迎登录</h2>
                    <p>
                        感谢您的关注并准备注册为留言簿用户。
                    </p>
                    <p>快速注册？ <a href="register.aspx"><i class="icon-weibo"></i>点击快速注册</a> </p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
   //判断验证码是否正确
   function houtai() {
                if ($("#TextYZ").val() == $.cookie('code')) {                    
                        return true;                  
                    }
                 else {
                    alert("请核对信息输入是否正确!");
                    return false;
                }
    }
    //验证码图片获取事件
    $(function () {
        $("#TextYZ").css("display", "inline")
        $("#yzmsx").click(function () {
            $("#getcode").attr("src", "ValidateCode.aspx?" + Math.random());
            })
    });
</script>
