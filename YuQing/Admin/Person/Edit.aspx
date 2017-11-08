<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="YuQing.Admin.Person.Edit" %>

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
        $(document).ready(function () {
            $('#ff').form('load', 'GetPerson.ashx?id=' + $("#personid").val());
        });
        function editPerson() {
            var datas = '';
            $("input[type='checkbox']").each(function () {
                if ($(this).is(":checked"))
                    datas += ',' + $(this).attr('id');
            });
            $("#ids").val(datas);
            $('#ff').form('submit', {
                url: 'SavePerson.ashx?id=' + $("#personid").val(),
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
    <div class="easyui-panel" title="修改人员" style="width:100%;max-width:400px;padding:30px 60px;">
        <form id="ff" method="post">
            <div style="margin-bottom:10px">
                <input class="easyui-textbox" name="Name" style="width:100%" data-options="label:'姓名：',required:true">
            </div>
            <div style="margin-bottom:10px">
                <input class="easyui-textbox" name="Code" style="width:100%" data-options="label:'用户名：',required:true,disabled:true">
            </div>
            <div style="margin-bottom:10px">
                <input class="easyui-textbox" name="Pwd" style="width:100%" data-options="label:'密码：',prompt:'如无需修改密码，则此处留空'">
            </div>
            <div style="margin-bottom: 10px">
                <label class="textbox-label textbox-label-before">角色：</label>
                <select id="RoleID" class="easyui-combobox" name="RoleID" style="width:180px;">
                    <%= GetRoles() %>
                </select>
            </div>
            <div style="margin-bottom:10px">
                <label class="textbox-label textbox-label-before">区域：</label>
                <div style="height:10px;"></div>
                <div>
                <%= GetRegions() %></div>
            </div>
            <input id="ids" name="ids" type="hidden" />
        </form>
        <div style="text-align:center;padding:5px 0">
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="editPerson()" style="width:80px">修改</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="javascript:window.location.href = 'Index.aspx'" style="width:80px">关闭</a>
        </div>
        <input id="personid" type="hidden" runat="server" />
    </div>
</body>
</html>
