using SM.YuQing.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YuQing.Admin
{
    public partial class Index : System.Web.UI.Page
    {
        public AccountsPrincipal user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new AccountsPrincipal(Context.User.Identity.Name);
        }

        public string GetUserDisplayName()
        {
            SM.YuQing.BLL.Person bll = new SM.YuQing.BLL.Person();
            SM.YuQing.Model.Person person = bll.GetModelFromCode(Context.User.Identity.Name);
            return person.Name;
        }

        public string GetMenu()
        {
            StringBuilder sb = new StringBuilder();

            SM.YuQing.BLL.Menu bll = new SM.YuQing.BLL.Menu();
            DataTable dtMenu1 = bll.GetList(0, "ParentID is null", "Sort").Tables[0];

            foreach (DataRow row in dtMenu1.Rows)
            {
                if (((SiteIdentity)user.Identity).CanViewMenu(row["ID"].ToString()))
                {
                    sb.Append("<div title=\"" + row["Name"] + "\" style=\"padding:10px;\">");

                    GetSubMenu(row["ID"].ToString(), sb);

                    sb.Append("</div>");
                }
            }

            return sb.ToString();
        }

        private void GetSubMenu(string ParentID, StringBuilder sb)
        {
            SM.YuQing.BLL.Menu bll = new SM.YuQing.BLL.Menu();
            DataTable dtMenu2 = bll.GetList(0, "ParentID=" + ParentID, "Sort").Tables[0];
            if (dtMenu2.Rows.Count > 0)
            {
                sb.Append("<ul class=\"easyui-tree\">");
                foreach (DataRow row2 in dtMenu2.Rows)
                {
                    if (((SiteIdentity)user.Identity).CanViewMenu(row2["ID"].ToString()))
                    {
                        sb.Append("<li data-options=\"iconCls:'" + row2["Icons"] + "'\">");
                        if (row2["Url"] == DBNull.Value)
                        {
                            sb.Append("<span>" + row2["Name"] + "</span>");
                        }
                        else
                        {
                            sb.Append("<a href=\"javascript:void(0)\" rel=\"" + row2["Url"] + "\">" + row2["Name"] + "</a>");
                        }
                        GetSubMenu(row2["ID"].ToString(), sb);
                        sb.Append("</li>");
                    }
                }
                sb.Append("</ul>");
            }
        }
    }
}