<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="YuQing.Admin.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>舆情监测系统</title>
    <link href="/themes/default/easyui.css" rel="stylesheet" />
    <link href="/themes/icon.css" rel="stylesheet" />
    <link href="/themes/demo.css" rel="stylesheet" />
    <link href="/themes/Default.css" rel="stylesheet" />
    <script src="/js/jquery.min.js"></script>
    <script src="/js/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('ul li a').click(function () {
                var tabTitle = $(this).text();
                var url = $(this).attr("rel"); //获取地址
                var icon = $(this).attr("icon"); //获取图标
                if (icon == "") {
                    icon = "icon-save";
                }
                addTab(tabTitle, url, icon, true);
            });
        });

        function addTab(subtitle, url, icon, closable) {
            if (!$('#tabs').tabs('exists', subtitle)) {
                $('#tabs').tabs('add', {
                    title: subtitle,
                    content: createFrame(url),
                    closable: closable,
                    icon: icon
                });
            } else {
                $('#tabs').tabs('select', subtitle);
            }
        }

        function createFrame(url) {
            var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:99%;overflow-y: auto;"></iframe>';
            return s;
        }
    </script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <div data-options="region:'north'" style="height: 50px; padding: 10px 15px; text-align:right;background:url('../themes/bar1.jpg');">
            <a href="#" class="easyui-menubutton" data-options="menu:'#mm2'" style="text-align:right;color:white;"><%= GetUserDisplayName() %></a>
            <div id="mm2" style="width: 100px;">
                <div><a href="/SignOut.aspx">退出系统</a></div>
            </div>
        </div>
		<div data-options="region:'south',split:true" style="height:50px;text-align:center; vertical-align:middle; padding: 10px 15px">
            © 2016 <a href="http://www.smsupermalls.cn/" target="_blank">SM-China</a> 版权所有
		</div>
		<div data-options="region:'west',split:true" title="导航菜单" style="width:15%;">
			<div class="easyui-accordion" data-options="fit:true,border:false">
                <%= GetMenu() %>
			</div>
		</div>
		<div data-options="region:'center',iconCls:'icon-ok'" style="overflow: hidden">
            <div id="tabs" class="easyui-tabs" data-options="fit:true,border:false,plain:true,animate:true">
                <div title="我的工作台" style="padding: 10px">
                </div>
            </div>
		</div>
    </form>
</body>
</html>
