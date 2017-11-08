using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace YuQing.Admin.Monitor.Region
{
    /// <summary>
    /// GetRegion 的摘要说明
    /// </summary>
    public class GetRegion : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            int id = Convert.ToInt32(context.Request.QueryString["id"]);
            SM.YuQing.BLL.Regions bll = new SM.YuQing.BLL.Regions();
            SM.YuQing.Model.Regions region = bll.GetModel(id);
            context.Response.Write(JsonConvert.SerializeObject(region));
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