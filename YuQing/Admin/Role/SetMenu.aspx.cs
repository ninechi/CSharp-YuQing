using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YuQing.Admin.Role
{
    public partial class SetMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public string GetCurrentRole()
        {
            string roleid = Request.QueryString["id"];
            SM.YuQing.BLL.Role bll = new SM.YuQing.BLL.Role();
            return bll.GetModel(Convert.ToInt32(roleid)).Name;
        }
    }
}