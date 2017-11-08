using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Person
{
    /// <summary>
    /// GetPersons 的摘要说明
    /// </summary>
    public class GetPersons : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            SM.YuQing.BLL.Person bll = new SM.YuQing.BLL.Person();
            //DataTable dt = bll.GetAllList().Tables[0];
            List<SM.YuQing.Model.Person> lst = bll.GetModelList("");
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