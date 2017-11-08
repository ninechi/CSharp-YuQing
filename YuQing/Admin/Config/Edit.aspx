<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="YuQing.Admin.Config.Edit" %>

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
        $(document).ready(function () {
            $('#ff').form('load', 'GetConfig.ashx');
        });
        function saveConfig() {
            $('#ff').form('submit', {
                url: 'SaveConfig.ashx',
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    var result = eval('(' + result + ')');
                    if (result.errorMsg) {
                        $.messager.alert('错误', result.errorMsg, 'error');
                    } else {
                        $.messager.alert('通知', '保存成功！', 'info');
                    }
                }
            });
        }
    </script>
</head>
<body>
    <div class="easyui-panel" title="配置信息" style="width:100%;max-width:400px;padding:30px 60px;">
        <form id="ff" method="post">
            <div style="margin-bottom: 10px">
                <label class="textbox-label textbox-label-before">搜索层级：</label>
                <select class="easyui-combobox" name="Count" style="width: 100px;" data-options="editable:false, panelHeight:'auto'">
                    <option value="1">一级</option>
                    <option value="2">二级</option>
                </select>
            </div>
        </form>
        <div style="text-align:center;padding:5px 0">
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="saveConfig()" style="width:80px">修改</a>
        </div>
    </div>
</body>
</html>
