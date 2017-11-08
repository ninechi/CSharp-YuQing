using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Role
{
    /// <summary>
    /// LoadOperation 的摘要说明
    /// </summary>
    public class LoadOperation : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            int id = Convert.ToInt32(context.Request.QueryString["id"]);
            SM.YuQing.BLL.Role bll = new SM.YuQing.BLL.Role();
            context.Response.Write(JsonConvert.SerializeObject(bll.GetMenuOperation(id)));
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