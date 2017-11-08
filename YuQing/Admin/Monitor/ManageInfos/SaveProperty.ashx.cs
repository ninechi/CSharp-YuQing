using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Collections;

namespace YuQing.Admin.Monitor.ManageInfos
{
    /// <summary>
    /// SaveProperty 的摘要说明
    /// </summary>
    public class SaveProperty : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            string id = context.Request.QueryString["id"];
            string property = context.Request.Form["property"];
            bool success;

            SM.YuQing.BLL.MonitorInfos bll = new SM.YuQing.BLL.MonitorInfos();
            SM.YuQing.Model.MonitorInfos monitorInfo = bll.GetModel(Convert.ToInt32(id));
            string oldProperty = monitorInfo.Property;
            monitorInfo.Property = property;
            monitorInfo.UpdatePerson = context.User.Identity.Name;
            monitorInfo.UpdateTime = DateTime.Now;

            success = bll.Update(monitorInfo);

            Hashtable ht = new Hashtable();
            if (success)
            {
                SM.YuQing.Accounts.SiteIdentity identity = (SM.YuQing.Accounts.SiteIdentity)(new SM.YuQing.Accounts.AccountsPrincipal(context.User.Identity.Name)).Identity;
                int personid = identity.FID;

                SM.YuQing.Model.Log log = new SM.YuQing.Model.Log();
                log.LogType = "操作";
                log.Message = context.User.Identity.Name + " 保存了ID=" + id + "的监测记录的性质为 " + property + " （原性质为 " + (oldProperty == "" ? "空" : oldProperty) + " ）";
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