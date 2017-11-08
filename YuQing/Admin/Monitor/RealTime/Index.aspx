<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="YuQing.Admin.Monitor.RealTime.Index" %>

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
            $("#label").hide();
            $("#export").hide();

            $.ajax({
                url: '../Webs/GetRegions.ashx',
                dataType: 'json',
                success: function (jsonstr) {  // 增加空行
                    jsonstr.unshift({ 'ID': '-1', 'Mall': 'All' });
                    $('#Region').combobox({ data: jsonstr, valueField: 'ID', textField: 'Mall' });
                }
            });
        });

        var reg, kw, ws;

        $(function () {
            $("#dg").datagrid({
                toolbar: '#toolbar',
                fit: true,
                border: false,
                striped: true,
                pagination: true,
                pageSize: 20,
                rownumbers: true,
                fitColumns: true,
                singleSelect: true,
                columns: [[
                    {
                        field: 'Title', title: '标题',
                        width: 650,
                        formatter: function (value, rowData, rowIndex) { return value == ("未匹配到关键字“" + $("#Keyword").textbox("getText") + "”") ? value : "<a href='#' onclick='window.open(\"" + rowData.Url + "\");'>" + value + "</a>"; }
                    },
                    { field: 'PublishDate', title: '发布日期' },
                    { field: 'ViewsCounts', title: '阅读数' },
                    { field: 'Region', title: '所属区域' },
                    { field: 'Url', title: 'Url', hidden: true }
                ]]
            });
        });

        function pagerFilter(data) {
            if (typeof data.length == 'number' && typeof data.splice == 'function') {    // 判断数据是否是数组
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

        function doSearch()
        {
            var opts = $('#dg').datagrid('options');
            opts.pageNumber = 1;

            var keyword = $("#Keyword").textbox("getText");
            var region = $("#Region").combobox("getValues");
            var website = $("#Website").textbox("getText");
            var sl = $("#SearchLevel").combobox("getValue");
            if (keyword == "") {
                alert("请输入关键字");
            }
            else if (website == "" && region == "") {
                alert("请选择监测区域，或输入指定网站");
            }
            else {
                $("#label").show();
                $("#export").hide();
                reg = region;
                kw = keyword;
                ws = website;
                $.post('DoSearch.ashx?regionid=' + reg + '&keyword=' + escape(kw) + '&website=' + ws + '&sl=' + sl, {}, function (result) {
                    $('#dg').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', result);
                    $("#label").hide();
                    var row = $('#dg').datagrid('getData').rows[0];
                    var str = row.Title;
                    if (str.indexOf('未匹配到关键字') >= 0)
                        alert('未匹配到关键字“' + keyword + '”');
                    else
                        $("#export").show();
                }, 'json');
            }
        }

        function exportToExcel() {
            window.open('DoSearch.ashx?regionid=' + reg + '&keyword=' + escape(kw) + '&website=' + ws + '&func=1');
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
    <table id="dg" title="监测消息列表" class="easyui-datagrid"></table>
    <div id="toolbar">
        <div style="margin-left:4px; margin-top:4px; margin-bottom:4px">
            <span>关键字：</span>
            <input id="Keyword" name="Keyword" type="text" class="easyui-textbox" style="width:150px" required="required" />
            <span>监测所属区域：</span>
            <input class="easyui-combobox" id="Region" name="Region" style="width:150px"
               data-options="
                             method:'get',
                             valueField:'ID',
					         textField:'Mall',
					         panelHeight:'auto',
                             multiple:true,
                             editable:false" />
            
            <span>指定网站：</span>
            <input id="Website" name ="Website" type="text" class="easyui-textbox" style="width:250px" />
            <span>搜索级别：</span>
            <select id="SearchLevel" name ="SearchLevel" class="easyui-combobox" style="width:100px" data-options="panelHeight:'auto',editable:false">
                <option value="1">一级页面</option>
                <option value="2">二级页面</option>
            </select>

            <a href="javascript:void(0)" class="easyui-linkbutton" style="width:80px" data-options="iconCls:'icon-search'" onclick="doSearch()">查询</a>
            <a href="javascript:void(0)" id="export" class="easyui-linkbutton" style="width:80px" data-options="iconCls:'icon-redo'"" onclick="exportToExcel()">导出</a>
            <label id="label" style="margin-top:2px">请稍候......</label>
        </div>
    </div>
</body>
</html>
