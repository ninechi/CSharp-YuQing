using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Logs
{
    /// <summary>
    /// GetLogs 的摘要说明
    /// </summary>
    public class GetLogs : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            SM.YuQing.BLL.Log bll = new SM.YuQing.BLL.Log();

            DataTable dt = bll.GetList(0, "", "ID DESC").Tables[0];
            context.Response.Write(JsonConvert.SerializeObject(dt));
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