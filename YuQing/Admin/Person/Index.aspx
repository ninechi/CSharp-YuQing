<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="YuQing.Admin.Person.Index" %>

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
        function newUser() {
            window.location.href = "Create.aspx";
            return false;
        }
        function editUser() {
            var arr = $('#dg').datagrid('getSelections');

            if (arr.length == 1) {
                window.location.href = "Edit.aspx?id=" + arr[0].ID;

            } else {
                $.messager.alert('操作提示', '请选择一条数据!', 'warning');
            }
            return false;
        }
        function destroyUser() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('警告', '确定要删除？', function (r) {
                    if (r) {
                        $.post('DeletePerson.ashx', { id: row.ID }, function (result) {
                            if (result.success) {
                                $('#dg').datagrid('reload');	// reload the user data
                            } else {
                                $.messager.show({	// show error message
                                    title: 'Error',
                                    msg: result.errorMsg
                                });
                            }
                        }, 'json');
                    }
                });
            }
        }
        function lockUser() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.post('LockPerson.ashx', { id: row.ID }, function (result) {
                    if (result.success) {
                        $('#dg').datagrid('reload');	// reload the user data
                    } else {
                        $.messager.show({	// show error message
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    }
                }, 'json');
            }
        }
    </script>
    <style type="text/css">
		#fm{
			margin:0;
			padding:10px 30px;
		}
		.ftitle{
			font-size:14px;
			font-weight:bold;
			padding:5px 0;
			margin-bottom:10px;
			border-bottom:1px solid #ccc;
		}
		.fitem{
			margin-bottom:5px;
		}
		.fitem label{
			display:inline-block;
			width:80px;
		}
		.fitem input{
			width:160px;
		}
	</style>
</head>
<body>
    <table id="dg" title="人员" class="easyui-datagrid"
        url="GetPersons.ashx"
        toolbar="#toolbar" pagination="true"
        rownumbers="true" fitcolumns="true" singleselect="true">
        <thead>
            <tr>
                <th field="Code">用户名</th>
                <th field="Name">姓名</th>
                <th field="Role">角色</th>
                <th field="Status">状态</th>
                <th field="Regions">所属区域</th>
                <th field="CreateTime">创建日期</th>
                <th field="LastLoginTime">上次登录日期</th>
                <th field="LoginTimes">登录次数</th>
            </tr>
        </thead>
    </table>
    <div id="toolbar">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newUser()">创建</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editUser()">修改</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyUser()">删除</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-lock" plain="true" onclick="lockUser()">锁定或解锁</a>
    </div>
</body>
</html>
