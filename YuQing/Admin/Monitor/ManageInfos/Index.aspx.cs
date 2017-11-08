using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YuQing.Admin.Monitor.ManageInfos
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string GetFields()
        {
            SM.YuQing.BLL.Person person = new SM.YuQing.BLL.Person();
            bool permission = person.HavePermission(HttpContext.Current.User.Identity.Name, "监测数据管理", "修改");

            string fields = "";
            if(permission)
                fields = "{field: \'Property\',title: \'性质\',width: 80,editor: {type: \'combobox\',options: {valueField: \'val\',textField: \'txt\',data: types,panelHeight: \'auto\',editable: false}}},{field: \'opt\', title: \'操作\', align: \'center\',formatter: function (value, rowData, rowIndex) {return rowData.ID == \"\" ? \"\" : \"<a href=\'#\'onclick=\'saveProperty(\" + rowData.ID + \", \" + rowIndex + \");\'><span style=\'color:blue\'>保存</span></a>\";}}";
            else
                fields = "{field: \'Property\',title: \'性质\',width: 80}";
            return fields;
        }
    }
}