using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Role
{
    /// <summary>
    /// GetMenuTree 的摘要说明
    /// </summary>
    public class GetMenuTree : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            SM.YuQing.BLL.Menu bll = new SM.YuQing.BLL.Menu();
            List<SM.YuQing.Model.Menu> lst = bll.GetMenuTree();
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