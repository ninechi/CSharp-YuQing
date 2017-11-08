using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Data;

namespace YuQing.Admin.Monitor.Webs
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
            //string func = context.Request.QueryString["func"];

            GetRegionIdHelper grih = new GetRegionIdHelper();
            string regionid = grih.GetRegionID();
            
            SM.YuQing.BLL.Regions bll = new SM.YuQing.BLL.Regions();
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