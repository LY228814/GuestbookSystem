<%@ Page Title="" Language="C#" MasterPageFile="~/NavMaster.Master" AutoEventWireup="true" CodeBehind="personaldata.aspx.cs" Inherits="GuestbookSystem.personaldata" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box_right col-lg-9 ">
        <div class="segment left">
            <i class=" glyphicon glyphicon-edit left"></i>
            <h3 class="left">个人资料修改<br />
            </h3>
        </div>
        <div class="clear"></div>

        <!--修改个人资料-->
        <div id="reg_box" class="left" style="padding-left: 30px; padding-top: 10px;">
            <div class="my-group">
                <span class="left">账 号</span>
                <asp:TextBox ID="Uname" CssClass="form-control put1" placeholder="请输入用户名" MaxLength="14" required="required" runat="server"></asp:TextBox>
            </div>

            <div class="my-group">
                <span class="left">密 码</span>
                <asp:TextBox ID="Upwd" CssClass="form-control put1" placeholder="请输入用密码" MaxLength="14" required="required" runat="server"></asp:TextBox>
            </div>

            <div class="my-group">
                <span class="left">性 别</span>
                <asp:RadioButtonList ID="Usex" CssClass="form-control put1 " RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Selected="True" Value="0" Text="先生&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                    <asp:ListItem Value="1" Text="&nbsp;女士&nbsp;"></asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <div class="my-group">
                <span class="left">修改头像</span>
                <asp:FileUpload ID="picture" CssClass="form-control put1 hidden" placeholder="上传头像" runat="server" />
                <asp:Button ID="scan" CssClass="form-control put1 " runat="server" Text="点击修改头像 jpg/png格式" />
            </div>

            <div class="my-group">
                <span class="left">手机号码</span>
                <asp:TextBox ID="Uphone" CssClass="form-control put1" placeholder="手机" MaxLength="11" required="required" runat="server"></asp:TextBox>

            </div>

            <div class="form-actions">
                <asp:Button ID="Button1" CssClass="btn-primary btn" name="commit" runat="server" Text="确认修改>" OnClick="Button1_Click" />
            </div>

        </div>
    </div>
    <script>
        //头像上传点击事件
        $(function () {
            $("input[id*='scan']").attr("type", "button");
            $("input[id *= 'scan']").click(function () {
                $("input[id*='picture']").click()
            });
            $("input[id *= 'picture']").bind('change', function () {
                var file = $(this).val();
                if (file.length > 0) {
                    var usename = ".jpg.png";
                    var exname = file.substring(file.lastIndexOf('.'));
                    if (usename.match(exname)) {
                        $("input[id *= 'scan']").val("已选择");
                    } else {
                        alert("请选择正确的文件格式 jpg/png");
                    }
                }
            });
            //用户名是否存在验证
            $("input[id*='Uname']").focus(function () {
                $("input[id*='Uname']").css("color", "black")
            })
            $("input[id*='Uname']").blur(function () {
                if ($("input[id*='Uname']").val() != "") {
                    var ajax = new XMLHttpRequest();
                    ajax.open("get", "EvHa.ashx?name=" + $("input[id*='Uname']").val() + "", true)
                    ajax.send()
                    ajax.onreadystatechange = function () {
                        if (ajax.readyState == 4 && ajax.status == 200) {
                            var hs = ajax.responseText;
                            if (hs == "True") {
                                alert("账号已存在,或与原账号相同,如需更改请输入新账号")
                            }
                        }
                    }
                }
            });
        })
    </script>
</asp:Content>
