using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace YuQing.Admin.Monitor.Webs
{
    /// <summary>
    /// GetWeb 的摘要说明
    /// </summary>
    public class GetWeb : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            int id = Convert.ToInt32(context.Request.QueryString["id"]);
            SM.YuQing.BLL.MonitorWebs bll = new SM.YuQing.BLL.MonitorWebs();
            SM.YuQing.Model.MonitorWebs web = bll.GetModel(id);
            context.Response.Write(JsonConvert.SerializeObject(web));
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