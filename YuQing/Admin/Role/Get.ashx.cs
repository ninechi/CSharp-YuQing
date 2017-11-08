using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Role
{
    /// <summary>
    /// Get 的摘要说明
    /// </summary>
    public class Get : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            int id = Convert.ToInt32(context.Request.QueryString["id"]);
            SM.YuQing.BLL.Role bll = new SM.YuQing.BLL.Role();
            SM.YuQing.Model.Role model = bll.GetModel(id);
            context.Response.Write(JsonConvert.SerializeObject(model));
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