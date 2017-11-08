<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="YuQing.Admin.Person.Create" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/themes/default/easyui.css" rel="stylesheet" />
    <link href="/themes/icon.css" rel="stylesheet" />
    <link href="/themes/demo.css" rel="stylesheet" />
    <link href="/themes/Default.css" rel="stylesheet" />
    <script src="/js/jquery.min.js"></script>
    <script src="/js/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        function savePerson() {
            $('#ff').form('submit', {
                url: 'SavePerson.ashx',
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    var result = eval('(' + result + ')');
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {
                        window.location.href = 'Index.aspx';
                    }
                }
            });
        }
        $(document).ready(function () {
            // extend the 'equals' rule
            $.extend($.fn.validatebox.defaults.rules, {
                equals: {
                    validator: function (value, param) {
                        return value == $(param[0]).val();
                    },
                    message: '两个密码不一致！'
                }
            });
        });
    </script>
</head>
<body>
    <div class="easyui-panel" title="创建人员" style="width:100%;max-width:400px;padding:30px 60px;">
        <form id="ff" method="post">
            <div style="margin-bottom:10px">
                <input class="easyui-textbox" name="Name" style="width:100%" data-options="label:'姓名：',required:true">
            </div>
            <div style="margin-bottom:10px">
                <input class="easyui-textbox" name="Code" style="width:100%" data-options="label:'用户名：',required:true">
            </div>
            <div style="margin-bottom:10px">
                <input class="easyui-passwordbox" id="Pwd" name="Pwd" style="width:100%" data-options="label:'密码：',required:true">
            </div>
            <div style="margin-bottom:10px">
                <input class="easyui-passwordbox" id="CheckPwd" name="CheckPwd" style="width:100%" data-options="label:'确认密码：',required:true" validType="equals['#Pwd']">
            </div>
        </form>
        <div style="text-align:center;padding:5px 0">
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="savePerson()" style="width:80px">创建</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="javascript:window.location.href = 'Index.aspx'" style="width:80px">关闭</a>
        </div>
    </div>
</body>
</html>
