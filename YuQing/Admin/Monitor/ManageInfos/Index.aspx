<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="YuQing.Admin.Monitor.ManageInfos.Index" %>

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

        var reg, bd, ed, prop;
        var lastIndex = -1;

        var types = [
            { "val": "正面", "txt": "正面" },
            { "val": "负面", "txt": "负面" },
            { "val": "中性", "txt": "中性" }
        ];

        var properties = [
            { "val": "全部", "txt": "全部" },
            { "val": "正面", "txt": "正面" },
            { "val": "负面", "txt": "负面" },
            { "val": "中性", "txt": "中性" },
            { "val": "空", "txt": "空" }
        ];

        $(function () {
            $('#Property').combobox({
                valueField: 'val',
                textField: 'txt',
                data: properties,
                panelHeight: 'auto',
                editable: false
            });

            $("#dg").datagrid({
                toolbar: '#toolbar',
                fit: true,
                border: false,
                striped: true,
                pagination: true,
                pageSize: 20,
                rownumbers: true,
                fitColumns: false,
                singleSelect: true,
                columns: [[
                    { field: 'ID', title: 'ID', hidden: true },
                    {
                        field: 'Title', title: '标题',
                        width: 750,
                        formatter: function (value, rowData, rowIndex) { return rowData.ID == "" ? value : "<a href='#' onclick='window.open(\"" + rowData.Url + "\");'>" + value + "</a>"; }
                    },
                    { field: 'PublishDate', title: '发布日期' },
                    { field: 'ViewsCounts', title: '阅读数' },
                    { field: 'Region', title: '监测区域' },
                    <%= GetFields() %>
                    //{
                    //    field: 'Property',
                    //    title: '性质',
                    //    width: 80,
                    //    editor: {
                    //        type: 'combobox',
                    //        options: {
                    //            valueField: 'val',
                    //            textField: 'txt',
                    //            data: types,
                    //            panelHeight: 'auto',
                    //            //onChange: function (newValue, oldValue) { alert(newValue); },
                    //            editable: false
                    //        }
                    //    }
                    //},
                    //{
                    //    field: 'opt', title: '操作', align: 'center',
                    //    formatter: function (value, rowData, rowIndex) {
                    //        return rowData.ID == "" ? "" : "<a href='#' onclick='saveProperty(" + rowData.ID + ", " + rowIndex + ");'><span style='color:blue'>保存</span></a>";
                    //    }
                    //}
                ]],
                onClickRow: function (rowIndex) {
                    if (lastIndex != rowIndex)
                    {
                        var row = $('#dg').datagrid('getSelected');
                        $('#dg').datagrid('endEdit', lastIndex);
                        $('#dg').datagrid('beginEdit', rowIndex);

                        //if(lastIndex != -1)
                        //    alert('lastIndex != rowIndex');
                    }
                    lastIndex = rowIndex;
                }
            });
        });

        function saveProperty(rowDataID, rowIndex) {
            //var row = $('#dg').datagrid('getSelected');
            var ed = $('#dg').datagrid('getEditor', { index: rowIndex, field: 'Property' });
            var property = $(ed.target).combobox("getValue");
            if (property == "")
                alert("请选择性质");
            else {
                $.post('SaveProperty.ashx?id=' + rowDataID, { property: property }, function (result) {
                    if (result.success) {
                        $('#dg').datagrid('acceptChanges');
                        $('#dg').datagrid('refreshRow', rowIndex);
                        alert('保存成功！');
                        $('#dg').datagrid('selectRow', rowIndex);
                    } else {
                        $.messager.show({	// show error message
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    }
                }, 'json');
            }

        }

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

            lastIndex = -1;

            var beginDate = $("#BeginDate").datebox("getText");
            var endDate = $("#EndDate").datebox("getText");
            var regionid = $("#Region").combobox("getValues");
            var property = $("#Property").combobox("getText");

            var b = new Date(beginDate);
            var e = new Date(endDate);

            if ((beginDate == '' && endDate != '') || (beginDate != '' && endDate == ''))
                alert('请选择发布日期范围');
            else if (b > e)
                alert('开始时间不能大于结束时间');
            else {
                reg = regionid;
                bd = beginDate;
                ed = endDate;
                prop = property;
                $("#label").show();
                $("#export").hide();
                $.post('DoSearch.ashx?regionid=' + reg + '&beginDate=' + bd + '&endDate=' + ed + '&property=' + escape(prop), {}, function (result) {
                    $('#dg').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', result);
                    $("#label").hide();
                    var row = $('#dg').datagrid('getData').rows[0];
                    var str = row.Title;
                    if (str.indexOf('未查询到数据') >= 0)
                        alert('未查询到数据');
                    else
                        $("#export").show();
                }, 'json');
            }
        }

        function exportToExcel()
        {
            window.open('DoSearch.ashx?regionid=' + reg + '&beginDate=' + bd + '&endDate=' + ed + '&property=' + escape(prop) + '&func=1');
        }
    </script>
    <style type="text/css">
        #fm {
            margin: 0;
            padding: 10px 30px;
        }

        .ftitle {
            font-size: 14px;
            font-weight: bold;
            padding: 5px 0;
            margin-bottom: 10px;
            border-bottom: 1px solid #ccc;
        }

        .fitem {
            margin-bottom: 5px;
        }

            .fitem label {
                display: inline-block;
                width: 80px;
            }

            .fitem input {
                width: 160px;
            }
    </style>
</head>
<body>
    <table id="dg" title="监测消息列表" class="easyui-datagrid"></table>
    <div id="toolbar">
        <div style="margin-left:4px; margin-top:4px; margin-bottom:4px">
            <span>发布日期范围：</span>
            <input id="BeginDate" name="BeginDate" type="text" class="easyui-datebox" />
            <span>至：</span>
            <input id="EndDate" name ="EndDate" type="text" class="easyui-datebox" />
            <span>监测区域：</span>
            <input class="easyui-combobox" id="Region" name="Region" style="width:180px"
                data-options="
                              method:'get',
                              valueField:'ID',
					          textField:'Mall',
					          panelHeight:'auto',
                              multiple:true,
                              editable:false" />
            <span>性质：</span>
            <select class="easyui-combobox" id="Property" name="Property" style="width:180px" data-options="panelHeight:'auto', editable:false, multiple:true">
                <%--<option value=""></option>
		        <option value="正面">正面</option>
		        <option value="负面">负面</option>
                <option value="中性">中性</option>
		        <option value="空">空</option>--%>
            </select>
            <a href="javascript:void(0)" class="easyui-linkbutton" style="width:80px" data-options="iconCls:'icon-search'" onclick="doSearch()">查询</a>
            <a href="javascript:void(0)" id="export" class="easyui-linkbutton" style="width:80px" data-options="iconCls:'icon-redo'"" onclick="exportToExcel()">导出</a>
            <label id="label" style="margin-top:4px">请稍候......</label>
        </div>
    </div>
</body>
</html>
