<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="YuQing.Admin.Person.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/themes/default/easyui.css" rel="stylesheet" />
    <link href="/themes/icon.css" rel="stylesheet" />
    <link href="/themes/demo.css" rel="stylesheet" />
    <link href="/themes/Default.css" rel="stylesheet" />
    <script src="/js/jquery.min.js"></script>
    <script src="/js/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        function checkform() {
            return $('#form1').form('validate');
        }
        $(document).ready(function () {
            // extend the 'equals' rule
            $.extend($.fn.validatebox.defaults.rules, {
                equals: {
                    validator: function (value, param) {
                        return value == $(param[0]).val();
                    },
                    message: '两个新密码不一致！'
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="easyui-panel" title="修改密码" style="width:100%;max-width:400px;padding:30px 60px;">
            
            <div style="margin-bottom:10px">
                <input class="easyui-textbox" id="Name" runat="server" 
                    style="width:100%" data-options="label:'用户名：',required:true,disabled:true">
            </div>
            <div style="margin-bottom:10px">
                <input class="easyui-passwordbox" id="OldPwd" runat="server" 
                    style="width:100%" data-options="label:'旧密码：',required:true">
            </div>
        <div style="margin-bottom:10px">
                <input class="easyui-passwordbox" id="NewPwd" name="NewPwd" runat="server" 
                    style="width:100%" data-options="label:'新密码：',required:true">
            </div>
        <div style="margin-bottom:10px">
                <input class="easyui-passwordbox" id="CheckPwd" runat="server" 
                    style="width:100%" data-options="label:'确认新密码：',required:true" validType="equals['#NewPwd']">
            </div>
        <div style="text-align: center;">
                        <asp:Button ID="btnSubmit" runat="server" Text="提交" Width="80px" Height="30px" CssClass="easyui-linkbutton"
                            OnClientClick="javascript:return checkform();" OnClick="btnSubmit_Click" />
                    </div>
                    <div style="margin-top: 15px;text-align: center;color:red;">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    </div>
        <input id="personid" type="hidden" runat="server" />
    </div>
        </form>
</body>
</html>
