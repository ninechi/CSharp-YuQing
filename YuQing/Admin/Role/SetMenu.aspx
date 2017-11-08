<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetMenu.aspx.cs" Inherits="YuQing.Admin.Role.SetMenu" %>

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
        $(function () {
            $('#flexigridData').treegrid({
                title: '当前角色是：<%= GetCurrentRole() %>',
                iconCls: 'icon-site',
                url: 'GetMenuTree.ashx',
                idField: 'ID',
                treeField: 'Name',
                rownumbers: true,

                toolbar: [
                    {
                        text: '保存',
                        iconCls: 'icon-save',
                        handler: function () {
                            return Save();
                        }
                    }, {
                        text: '全选',
                        iconCls: 'icon-ok',
                        handler: function () {
                            return SelectAll();
                        }
                    }, {
                        text: '全不选',
                        iconCls: 'icon-remove',
                        handler: function () {
                            return SelectNothing();
                        }
                    }, {
                        text: '返回',
                        iconCls: 'icon-undo',
                        handler: function () {
                            return Back();
                        }
                    }],

                columns: [[

                    	{
                    	    field: 'Name', title: '菜单', width: 205
                                        , formatter: function (value, rec) {
                                            if (value) {
                                                return '<input id="' + rec.ID + '" type="checkbox">' + (value);
                                            }
                                        }
                    	}
					, {
					    field: 'Operation', title: '操作', width: 599, formatter: function (value, rec) {
					        if (value) {
					            var index = value.split(","); //分割符 , 的位置
					            if (index[0] == null || index[0] == "undefined" || index[0].length < 1) {
					                return;
					            }
					            var content = ""; //需要添加到check中的内容 
					            for (var i = 0; i < index.length; i++) {
					                var view = index[i].split('^'); //显示值
					                if (view != null) {
					                    content += '<input id="' + rec.ID + '^' + view[0] + '" type="checkbox">' + view[1];
					                }
					            }
					            return content;
					        }
					    }
					}
                ]],
                onLoadSuccess: function (row, data) {
                    if (data) {
                        $.ajaxSetup({
                            cache: false //关闭AJAX相应的缓存
                        });
                        $.getJSON('LoadOperation.ashx?id=' + $("#roleid").val(), function (checks) {
                            $.each(checks, function (i, item) {
                                var c = document.getElementById(item);
                                c.checked = true;
                            });
                        });
                    }
                }
            });
        });

        function Back() {
            window.location.href = "Index.aspx";
            return false;
        }

        function SelectAll() {
            $("input[type='checkbox']").each(function () {
                $(this).attr("checked", true);
            });
            return false;
        }

        function SelectNothing() {
            $("input[type='checkbox']").each(function () {
                $(this).attr("checked", false);
            });
            return false;
        }

        function Save() {
            var datas = '';
            $("input[type='checkbox']").each(function () {
                if ($(this).is(":checked"))
                    datas += ',' + $(this).attr('id');
            });
            $.post("SaveOperation.ashx", { id: $("#roleid").val(), ids: datas }, function (res) {
                if (res == "OK") {
                    $.messager.alert('操作提示', '保存成功!', 'info');
                }
                else {
                    $.messager.alert('操作提示', '保存失败!', 'info');
                }
            });
            return false;
        }
    </script>
</head>
<body>
    <table id="flexigridData">
    </table>
    <input id="roleid" type="hidden" value="<%= Request.QueryString["id"] %>" />
</body>
</html>
