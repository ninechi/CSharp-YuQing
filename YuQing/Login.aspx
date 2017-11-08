<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="YuQing.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登录</title>
    <link href="/themes/default/easyui.css" rel="stylesheet" />
    <link href="/themes/icon.css" rel="stylesheet" />
    <link href="/themes/demo.css" rel="stylesheet" />
    <link href="/themes/Default.css" rel="stylesheet" />
    <script src="/js/jquery.min.js"></script>
    <script src="/js/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Code').textbox('textbox').focus();
            //$("#Code").textbox('setValue', 'jeff');
            //$("#Pwd").textbox('setValue', '123');
        });
        
        function checkform() {
            return $('#form1').form('validate');
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .header {
            text-align:center;
        }
        body {
            background:url("themes/background1.jpg") 0 0 no-repeat;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
        <tr style="height:150px;">
            <td style="width: 33%">&nbsp;</td>
            <td style="font-family: 微软雅黑, Verdana;font-size:18px;"></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <div class="easyui-panel" title="SM China 舆情监测系统" headerCls="header" style="width: 100%; max-width: 400px; padding: 30px 60px;">

                    <div style="margin-bottom: 15px">
                        <input class="easyui-textbox" id="Code" name="Code" style="width: 100%" data-options="label:'用户名：',required:true" />
                    </div>
                    <div style="margin-bottom: 15px">
                        <input class="easyui-passwordbox" id="Pwd" name="Pwd" style="width: 100%" data-options="label:'密码：',required:true" />
                    </div>

                    <div style="text-align: center;">
                        <asp:Button ID="btnLogin" runat="server" Text="登录" Width="80px" Height="30px" CssClass="easyui-linkbutton"
                            OnClick="btnLogin_Click"
                            OnClientClick="javascript:return checkform();" />
                    </div>
                    <div style="margin-top: 15px;text-align: center;color:red;">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </form>
</body>
</html>
