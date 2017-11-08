<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="YuQing.Admin.Monitor.Statistics.Index" %>

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
        var reg, bd, ed;

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
                    { field: 'Region', title: '监测区域' },
                    { field: 'Positive', title: '正面数' },
                    { field: 'Negative', title: '负面数' },
                    { field: 'Neutral', title: '中性数' },
                    { field: 'Empty', title: '空数' },
                    { field: 'Total', title: '累计' }
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

            var region = $("#Region").combobox("getValues");
            var beginDate = $("#BeginDate").datebox("getText");
            var endDate = $("#EndDate").datebox("getText");

            var b = new Date(beginDate);
            var e = new Date(endDate);

            if ((beginDate == '' && endDate != '') || (beginDate != '' && endDate == ''))
                alert('请选择统计日期范围');
            else if (b > e)
                alert('开始时间不能大于结束时间');
            else {
                reg = region;
                bd = beginDate;
                ed = endDate;
                $("#label").show();
                $("#export").hide();
                $.post('DoSearch.ashx?RegionId=' + reg + '&beginDate=' + bd + '&endDate=' + ed, {}, function (result) {
                    $('#dg').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', result);
                    $("#label").hide();
                    $("#export").show();
                }, 'json');
            }
        }

        function exportToExcel()
        {
            //window.open('Export.aspx?RegionId=' + reg + '&beginDate=' + bd + '&endDate=' + ed);
            window.open('DoSearch.ashx?RegionId=' + reg + '&beginDate=' + bd + '&endDate=' + ed + '&func=1');
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
    <table id="dg" title="统计信息列表" class="easyui-datagrid"></table>
    <div id="toolbar">
        <div style="margin-left:4px; margin-top:4px; margin-bottom:4px">
            <span>监测区域：</span>
            <input class="easyui-combobox" id="Region" name="Region" style="width:150px"
               data-options="
                             method:'get',
                             valueField:'ID',
					         textField:'Mall',
					         panelHeight:'auto',
                             multiple:true,
                             editable:false" />
            
            <span>统计日期范围：</span>
            <input id="BeginDate" name="BeginDate" type="text" class="easyui-datebox" />
            <span>至：</span>
            <input id="EndDate" name ="EndDate" type="text" class="easyui-datebox" />

            <a href="javascript:void(0)" class="easyui-linkbutton" style="width:80px" data-options="iconCls:'icon-search'" onclick="doSearch()">查询</a>
            <a href="javascript:void(0)" id="export" class="easyui-linkbutton" style="width:80px" data-options="iconCls:'icon-redo'"" onclick="exportToExcel()">导出</a>
            <label id="label" style="margin-top:2px">请稍候......</label>
        </div>
    </div>
</body>
</html>
