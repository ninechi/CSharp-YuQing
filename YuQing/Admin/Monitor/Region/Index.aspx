<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="YuQing.Admin.Monitor.Region.Index" %>

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
        function newRegion() {
            window.location.href = "Create.aspx";
            return false;
        }

        function editRegion() {
            var arr = $('#dg').datagrid('getSelections');

            if (arr.length == 1) {
                window.location.href = "Edit.aspx?id=" + arr[0].ID;

            } else {
                $.messager.alert('操作提示', '请选择一条数据!', 'warning');
            }
            return false;
        }

        function destroyRegion() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', 'Are you sure you want to destroy this Region?', function (r) {
                    if (r) {
                        $.post('DeleteRegion.ashx', { id: row.ID }, function (result) {
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

        function pagerFilter(data) {
            if (typeof data.length == 'number' && typeof data.splice == 'function') {
                data = {
                    total: data.length,
                    rows: data
                }
            }
            var dg = $(this);
            var opts = dg.datagrid('options');
            var pager = dg.datagrid('getPager');
            pager.pagination({
                onSelectPage: function (pageNum, pageSize) {
                    opts.pageNumber = pageNum;
                    opts.pageSize = pageSize;
                    pager.pagination('refresh', {
                        pageNumber: pageNum,
                        pageSize: pageSize
                    });
                    dg.datagrid('loadData', data);
                }
            });
            if (!data.originalRows) {
                data.originalRows = (data.rows);
            }
            var start = (opts.pageNumber - 1) * parseInt(opts.pageSize);
            var end = start + parseInt(opts.pageSize);
            data.rows = (data.originalRows.slice(start, end));
            return data;
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
    <table id="dg" title="监测区域" class="easyui-datagrid" data-options="
        url: 'GetRegions.ashx',
        loadFilter: pagerFilter,
        toolbar: '#toolbar', 
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true, 
        fitColumns: false, 
        singleSelect: true">
        <thead>
            <tr>
                <th data-options="field:'Region'">所属区域</th>
                <th data-options="field:'Mall'">监测商场</th>
                <th data-options="field:'Keyword'">关键字</th>
            </tr>
        </thead>
    </table>
    <div id="toolbar">
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add', plain:true" onclick="newRegion()">新增</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit', plain:true" onclick="editRegion()">修改</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove', plain:true" onclick="destroyRegion()">删除</a>
    </div>
</body>
</html>
