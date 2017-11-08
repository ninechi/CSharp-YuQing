using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Person
{
    /// <summary>
    /// GetPerson 的摘要说明
    /// </summary>
    public class GetPerson : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            int id = Convert.ToInt32(context.Request.QueryString["id"]);
            SM.YuQing.BLL.Person bll = new SM.YuQing.BLL.Person();
            SM.YuQing.Model.Person person = bll.GetModel(id);
            person.Pwd = "";
            if (person.IsLock == 1)
            {
                person.Status = "on";
            }
            else
            {
                person.Status = "off";
            }
            context.Response.Write(JsonConvert.SerializeObject(person));
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