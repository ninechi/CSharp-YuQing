<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="YuQing.Admin.Monitor.Webs.Index" %>

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
        var regionid, status;

        $(document).ready(function () {
            $("#label").hide();

            $.ajax({
                url: '../Webs/GetRegions.ashx',
                dataType: 'json',
                success: function (jsonstr) {  // 增加空行
                    jsonstr.unshift({ 'ID': '', 'Region': '' });
                    $('#Region').combobox({ data: jsonstr, valueField: 'ID', textField: 'Region' });
                }
            });

            doSearch(0);
            //$(function () {
            //    $("#dg").datagrid({ url: 'GetWebs.ashx', loadFilter: pagerFilter });
            //});
        });

        function newWeb() {
            window.location.href = "Create.aspx";
            return false;
        }

        function editWeb() {
            var arr = $('#dg').datagrid('getSelections');

            if (arr.length == 1) {
                window.location.href = "Edit.aspx?id=" + arr[0].ID;

            } else {
                $.messager.alert('操作提示', '请选择一条数据!', 'warning');
            }
            return false;
        }

        function destroyWeb() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', 'Are you sure you want to destroy this Web?', function (r) {
                    if (r) {
                        $.post('DeleteWeb.ashx', { id: row.ID }, function (result) {
                            if (result.success) {
                                doSearch(0);
                                //$('#dg').datagrid('reload');	// reload the user data
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

        function doSearch(func)
        {
            var opts = $('#dg').datagrid('options');
            opts.pageNumber = 1;

            regionid = $("#Region").combobox("getValue");
            status = $("#Status").combobox("getText");
            if(func==1)
                $("#label").show();
            $.post('DoSearch.ashx?regionid=' + regionid + '&status=' + escape(status), {}, function (result) {
                $('#dg').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', result);
                if (func == 1)
                    $("#label").hide();
                var row = $('#dg').datagrid('getData').rows[0];
                var str = row.Url;
                if (str.indexOf('未查询到数据') >= 0)
                    alert('未查询到数据');
            }, 'json');
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
    <table id="dg" title="监测网站" class="easyui-datagrid" data-options="
        <%--url:'GetWebs.ashx',--%>
        loadFilter: pagerFilter,
        toolbar:'#toolbar', 
        fit: true,
        border: false,
        pagination:true,
        pageSize:20,
        rownumbers:true, 
        fitColumns:true, 
        singleSelect:true">
        <thead>
            <tr>
                <th data-options="field:'Url'">监测网站</th>
                <th data-options="field:'Name'">网站名称</th>
                <th data-options="field:'Region'">所属区域</th>
                <th data-options="field:'Status'">状态</th>
            </tr>
        </thead>
    </table>
    <div id="toolbar">
        <div style="margin-left:4px; margin-top:4px; margin-bottom:4px">
            <span>所属区域：</span>
                <input class="easyui-combobox" id="Region" name="Region" style="width:180px"
                    data-options="method:'get',
                                  valueField:'ID',
					              textField:'Region',
					              panelHeight:'auto',
                                  editable:false" />
            <span>状态：</span>
            <select class="easyui-combobox" id="Status" name="Status" style="width:180px" data-options="panelHeight:'auto', editable:false">
                    <option value=""></option>
		            <option value="启用">启用</option>
		            <option value="停用">停用</option>
            </select>
            <a href="javascript:void(0)" class="easyui-linkbutton" style="width:80px" data-options="iconCls:'icon-search'" onclick="doSearch(1)">查询</a>
            <label id="label" style="margin-top:4px">请稍候......</label>
            <br />
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add', plain:true" onclick="newWeb()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit', plain:true" onclick="editWeb()">修改</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove', plain:true" onclick="destroyWeb()">删除</a>
        </div>
    </div>
</body>
</html>
