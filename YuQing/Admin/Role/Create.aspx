<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="YuQing.Admin.Role.Create" %>

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
        function save() {
            $('#ff').form('submit', {
                url: 'Save.ashx',
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
    </script>
</head>
<body>
    <div class="easyui-panel" title="创建角色" style="width:100%;max-width:400px;padding:30px 60px;">
        <form id="ff" method="post">
            <div style="margin-bottom:10px">
                <input class="easyui-textbox" name="Name" style="width:100%" data-options="label:'角色名：',required:true">
            </div>
            <div style="margin-bottom:10px">
                <input class="easyui-textbox" name="Description" style="width:100%;height:60px" data-options="label:'描述：',multiline:true">
            </div>
        </form>
        <div style="text-align:center;padding:5px 0">
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="save()" style="width:80px">创建</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="javascript:window.location.href = 'Index.aspx'" style="width:80px">关闭</a>
        </div>
    </div>
</body>
</html>
