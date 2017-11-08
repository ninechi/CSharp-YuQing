using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Data;
using System.Collections;

namespace YuQing.Admin.Monitor.Webs
{
    /// <summary>
    /// DoSearch 的摘要说明
    /// </summary>
    public class DoSearch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            string regionid = context.Request.QueryString["regionid"];
            string status = context.Request.QueryString["status"];

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Url");
            dt.Columns.Add("Name");
            dt.Columns.Add("Region");
            dt.Columns.Add("Status");

            try
            {
                string strWhere = "";
                ArrayList strWhereList = new ArrayList();

                if (regionid != "" && regionid != null)
                {
                    strWhereList.Add("  RegionID=" + regionid);
                }
                else
                {
                    SM.YuQing.Accounts.SiteIdentity identity = (SM.YuQing.Accounts.SiteIdentity)(new SM.YuQing.Accounts.AccountsPrincipal(context.User.Identity.Name)).Identity;
                    string personid = identity.FID.ToString();
                    SM.YuQing.BLL.Regions regbll = new SM.YuQing.BLL.Regions();
                    regionid = regbll.GetRegionIdByPersonID(personid);
                    strWhereList.Add(" RegionID in (" + regionid + ")");
                }
                if (status != "" && status != null)
                {
                    strWhereList.Add(" Status=N\'" + status + "\'");
                }

                switch (strWhereList.Count)
                {
                    case 1:
                        strWhere = strWhereList[0].ToString();
                        break;
                    case 2:
                        strWhere = strWhereList[0].ToString() + " and " + strWhereList[1].ToString();
                        break;
                    default:
                        break;
                }

                SM.YuQing.BLL.MonitorWebs webBll = new SM.YuQing.BLL.MonitorWebs();
                List<SM.YuQing.Model.MonitorWebs> webList = webBll.GetModelList(strWhere);
                foreach (SM.YuQing.Model.MonitorWebs web in webList)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = web.ID;
                    dr["Url"] = web.Url;
                    dr["Name"] = web.Name;
                    SM.YuQing.BLL.Regions regionBll = new SM.YuQing.BLL.Regions();
                    SM.YuQing.Model.Regions region = regionBll.GetModel(web.RegionID);
                    dr["Region"] = region.Region;
                    dr["Status"] = web.Status;
                    dt.Rows.Add(dr);
                }

                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = "";
                    dr["Url"] = "未查询到数据";
                    dr["Name"] = "";
                    dr["Region"] = "";
                    dr["Status"] = "";
                    dt.Rows.Add(dr);
                }

                context.Response.Write(JsonConvert.SerializeObject(dt));
            }
            catch (Exception ex)
            {
                throw ex;
            }
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