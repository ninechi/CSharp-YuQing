using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Role
{
    /// <summary>
    /// GetRoles 的摘要说明
    /// </summary>
    public class GetRoles : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            SM.YuQing.BLL.Role bll = new SM.YuQing.BLL.Role();
            DataTable dt = bll.GetAllList().Tables[0];
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