using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Config
{
    /// <summary>
    /// GetConfig 的摘要说明
    /// </summary>
    public class GetConfig : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            SM.YuQing.BLL.Config bll = new SM.YuQing.BLL.Config();
            SM.YuQing.Model.Config config = bll.GetModelByName("SearchLevels");

            context.Response.Write(JsonConvert.SerializeObject(config));
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