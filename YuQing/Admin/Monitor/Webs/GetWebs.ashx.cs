using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Data;

namespace YuQing.Admin.Monitor.Webs
{
    /// <summary>
    /// GetWebs 的摘要说明
    /// </summary>
    public class GetWebs : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            
            //List<SM.YuQing.Model.MonitorWebs> lst = bll.GetModelList("");
            //context.Response.Write(JsonConvert.SerializeObject(lst));

            SM.YuQing.Accounts.SiteIdentity identity = (SM.YuQing.Accounts.SiteIdentity)(new SM.YuQing.Accounts.AccountsPrincipal(context.User.Identity.Name)).Identity;
            string personid = identity.FID.ToString();
            SM.YuQing.BLL.Regions regbll = new SM.YuQing.BLL.Regions();
            string regionid = regbll.GetRegionIdByPersonID(personid);

            SM.YuQing.BLL.MonitorWebs bll = new SM.YuQing.BLL.MonitorWebs();
            DataTable dt = bll.GetList(" RegionID in (" + regionid + ")").Tables[0], retDt = new DataTable();
            retDt.Columns.Add("ID");
            retDt.Columns.Add("Region");
            retDt.Columns.Add("Name");
            retDt.Columns.Add("Url");
            retDt.Columns.Add("Status");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SM.YuQing.BLL.Regions regionBll = new SM.YuQing.BLL.Regions();
                SM.YuQing.Model.Regions region = regionBll.GetModel(Convert.ToInt32(dt.Rows[i]["RegionID"]));
                DataRow dr = retDt.NewRow();
                dr["ID"] = dt.Rows[i]["ID"];
                dr["Region"] = region.Region;
                dr["Name"] = dt.Rows[i]["Name"];
                dr["Url"] = dt.Rows[i]["Url"];
                dr["Status"] = dt.Rows[i]["Status"];
                retDt.Rows.Add(dr);
            }
            context.Response.Write(JsonConvert.SerializeObject(retDt));
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