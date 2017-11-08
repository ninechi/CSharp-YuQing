using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YuQing.Admin.Person
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.personid.Value = Request.QueryString["id"];
        }

        public string GetRegions()
        {
            string id = Request.QueryString["id"];
            SM.YuQing.BLL.Person bll = new SM.YuQing.BLL.Person();
            List<SM.YuQing.Model.Regions> regions = bll.GetRegions(Convert.ToInt32(id));
            List<SM.YuQing.Model.Regions> allRegions = (new SM.YuQing.BLL.Regions()).GetModelList("");
            string content = "";
            foreach (SM.YuQing.Model.Regions item in allRegions)
            {
                if (regions.Contains(item))
                {
                    content += "<input id='" + item.ID + "' type='checkbox' checked='true'>" + item.Region;
                }
                else
                {
                    content += "<input id='" + item.ID + "' type='checkbox'>" + item.Region;
                }
                
            }
            return content;
        }

        public string GetRoles()
        {
            string id = Request.QueryString["id"];
            //SM.YuQing.BLL.Person bll = new SM.YuQing.BLL.Person();
            List<SM.YuQing.Model.Role> roles = (new SM.YuQing.BLL.Role()).GetModelList("");
            string content = "";
            foreach (SM.YuQing.Model.Role item in roles)
            {
                content += "<option value='" + item.ID + "'>" + item.Name + "</option>";
            }
            return content;
        }
    }
}