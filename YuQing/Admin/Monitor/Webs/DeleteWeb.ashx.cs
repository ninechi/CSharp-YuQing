using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections;
using Newtonsoft.Json;

namespace YuQing.Admin.Monitor.Webs
{
    /// <summary>
    /// DeleteWeb 的摘要说明
    /// </summary>
    public class DeleteWeb : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.Form["id"];

            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            SM.YuQing.Accounts.SiteIdentity identity = (SM.YuQing.Accounts.SiteIdentity)(new SM.YuQing.Accounts.AccountsPrincipal(context.User.Identity.Name)).Identity;
            int personid = identity.FID;

            SM.YuQing.BLL.MonitorWebs bll = new SM.YuQing.BLL.MonitorWebs();
            SM.YuQing.Model.MonitorWebs web = bll.GetModel(Convert.ToInt32(id));
            string url = web.Url, name = web.Name;

            bool success = bll.Delete(Convert.ToInt32(id));
            Hashtable ht = new Hashtable();
            if (success)
            {
                SM.YuQing.Model.Log log = new SM.YuQing.Model.Log();
                log.LogType = "操作";
                log.Message = context.User.Identity.Name + " 删除了网站 " + url + "(" + name + ")"; ;
                log.IP = context.Request.UserHostAddress;
                log.MenuID = 0;
                log.PersonID = personid;
                log.CreateTime = DateTime.Now;
                SM.YuQing.BLL.Log logBll = new SM.YuQing.BLL.Log();
                logBll.Add(log);

                ht.Add("success", true);
            }
            else
            {
                ht.Add("errorMsg", "Some errors occured.");
            }
            context.Response.Write(JsonConvert.SerializeObject(ht));
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