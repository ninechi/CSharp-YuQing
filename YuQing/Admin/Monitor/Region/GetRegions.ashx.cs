using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Data;

namespace YuQing.Admin.Monitor.Region
{
    /// <summary>
    /// GetRegions 的摘要说明
    /// </summary>
    public class GetRegions : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            SM.YuQing.Accounts.SiteIdentity identity = (SM.YuQing.Accounts.SiteIdentity)(new SM.YuQing.Accounts.AccountsPrincipal(context.User.Identity.Name)).Identity;
            string personid = identity.FID.ToString();
            SM.YuQing.BLL.Regions regbll = new SM.YuQing.BLL.Regions();
            string regionid = regbll.GetRegionIdByPersonID(personid);

            SM.YuQing.BLL.Regions bll = new SM.YuQing.BLL.Regions();
            //DataTable dt = bll.GetAllList().Tables[0];
            List<SM.YuQing.Model.Regions> lst = bll.GetModelList(" ID in (" + regionid + ")");
            context.Response.Write(JsonConvert.SerializeObject(lst));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}