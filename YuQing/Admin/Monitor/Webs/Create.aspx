<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="YuQing.Admin.Monitor.Webs.Create" %>

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
        function save() {
            $('#ff').form('submit', {
                url: 'SaveWeb.ashx',
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
    <div class="easyui-panel" title="新增网站" style="width:100%;max-width:400px;padding:30px 60px;">
        <form id="ff" method="post">
            <div style="margin-bottom:10px">
                <input class="easyui-textbox" name="Url" style="width:100%" data-options="label:'监测网站：',required:true" />
            </div>
            <div style="margin-bottom:10px">
                <input class="easyui-textbox" name="Name" style="width:100%" data-options="label:'网站名称：',required:true" />
            </div>
            <div style="margin-bottom:10px">
                <input class="easyui-combobox" name="RegionID" style="width:100%"
                    data-options="  url:'GetRegions.ashx', 
                                    method:'get',
                                    valueField:'ID',
					                textField:'Region',
					                panelHeight:'auto',
                                    editable:false,
                                    label:'所属区域：',
                                    required:true" />
            </div>
            <div>
                <select class="easyui-combobox" name="Status" style="width:100%" data-options="label:'状态：', editable:false, panelHeight:'auto'">
		            <option value="启用">启用</option>
		            <option value="停用">停用</option>
                </select>
            </div>
        </form>
        <div style="text-align:center;padding:5px 0">
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="save()" style="width:80px">创建</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="javascript:window.location.href = 'Index.aspx'" style="width:80px">关闭</a>
        </div>
    </div>
</body>
</html>
