<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="GuestbookSystem.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>注册</title>
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="log_banner">
                <div class="inset">
                    <h1>用户注册</h1>
                    <p>Wish everyone happy and always with you!</p>
                </div>

            </div>
            <!--注册开始-->
            <div id="body">
                <div id="reg_box" class="left">
                    <div class="my-group">
                        <span class="left">账 号</span>
                        <asp:TextBox ID="Uname" CssClass="form-control put1" placeholder="请输入用户名" MaxLength="14" required="required" runat="server"></asp:TextBox>
                    </div>

                    <div class="my-group">
                        <span class="left">密 码</span>
                        <asp:TextBox ID="Upwd" type="password" CssClass="form-control put1" placeholder="请输入用密码" MaxLength="14" required="required" runat="server"></asp:TextBox>
                    </div>

                    <div class="my-group">
                        <span class="left">确认密码</span>
                        <asp:TextBox ID="Upwd2" type="password" CssClass="form-control put1" placeholder="请重复密码" MaxLength="14" required="required" runat="server"></asp:TextBox>
                    </div>

                    <div class="my-group">
                        <span class="left">性 别</span>
                        <asp:RadioButtonList ID="Usex" CssClass="form-control put1 " RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Selected="True" Value="0" Text="先生&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                            <asp:ListItem Value="1" Text="&nbsp;女士&nbsp;"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="my-group">
                        <span class="left">手机号码</span>
                        <asp:TextBox ID="Uphone" CssClass="form-control put1" placeholder="手机" MaxLength="11" required="required" runat="server"></asp:TextBox>

                    </div>

                    <div class="my-group">
                        <span class="left">上传头像</span>
                        <asp:FileUpload ID="picture" CssClass="form-control put1 hidden" placeholder="上传头像" runat="server" />
                        <asp:Button ID="scan" CssClass="form-control put1 " runat="server" Text="点击选择头像 jpg/png格式" />
                    </div>

                    <div class="form-actions">
                        <p>
                            <asp:Button ID="zhuce" CssClass="btn-primary btn" name="commit" runat="server" Text="注册留言簿用户" OnClientClick=" return houtai()" OnClick="zhuce_Click" />
                        </p>
                    </div>

                </div>
                <div class="side_box right">
                    <h2>欢迎注册</h2>
                    <p>
                        感谢您的关注并准备注册为留言簿用户。
                    </p>
                    <p>已有账号快速登录？ <a href="Login.aspx"><i class="icon-weibo"></i>点击快速登录</a> </p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
<script>

    function houtai() {
    /* 判断信息是否输入*/
        var str = $(Uname).val();
        var patrn = /[`~!@#$%^&*()_\-+=<>?:"{}|,.\/;'\\[\]·~！@#￥%……&*（）——\-+={}|《》？：“”【】、；‘'，。、]/im;
        if (!patrn.test(str)) {// 如果包含特殊字符返回false
            if ($("#scan").val() == "已选择") {
                var str = $(Uphone).val();
                var patrn = /^((13[0-9])|(14[5,7])|(15[0-3,5-9])|(17[0,3,5-8])|(18[0-9])|166|198|199|(147))\d{8}$/;
                if (!patrn.test(str)) {
                    alert("请输入有效的手机号");
                    return false;
                } else {
                    return true;
                }
                
            } else {
                alert("请上传头像");
                return false;
            }
        } else {
            alert("账号包含特殊字符");
            return false;
        }

    };

    $(function () {

        // $('#Ubirthday').attr("type", "date");
        $('#scan').attr("type", "button");
        $("#scan").click(function () {
            $("#picture").click()
        });
        $("#picture").bind('change', function () {
            var file = $(this).val();
            if (file.length > 0) {
                var usename = ".jpg.png";
                var exname = file.substring(file.lastIndexOf('.'));
                if (usename.match(exname)) {
                    $("#scan").val("已选择");
                } else {
                    alert("请选择正确的文件格式 jpg/png");
                }
            }
        });

        $("#Uname").focus(function () {        
            $("#Uname").css("color", "black");
        })
        $("#Uname").blur(function () {
            if ($("#Uname").val() != "") {

                var ajax = new XMLHttpRequest();
                ajax.open("get", "EvHa.ashx?name=" + $("#Uname").val() + "", true)
                ajax.send()
                ajax.onreadystatechange = function () {
                    if (ajax.readyState == 4 && ajax.status == 200) {
                        var hs = ajax.responseText;
                        if (hs == "True") {
                            alert("账号已存在,请更换账号")
                            $("#Uname").css("color", "red")
                        }
                    }
                }
            }
        });

        $("#Upwd").blur(function () { mmyz(); })
        $("#Upwd2").blur(function () { mmyz(); })

        function mmyz() {
            if ($("#Upwd").val() != "" && $("#Upwd2").val() != "") {
                if ($("#Upwd").val() != $("#Upwd2").val()) {
                    alert("两次输入密码不同,请重新输入")
                    $("#Upwd").css("color", "red")
                    $("#Upwd2").css("color", "red")
                } else {
                    $("#Upwd").css("color", "black")
                    $("#Upwd2").css("color", "black")
                }
                $("#Uphone").focus(); 
            }
        }
    })
</script>
