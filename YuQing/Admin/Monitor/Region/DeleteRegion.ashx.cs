using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections;
using Newtonsoft.Json;

namespace YuQing.Admin.Monitor.Region
{
    /// <summary>
    /// DeleteRegion 的摘要说明
    /// </summary>
    public class DeleteRegion : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.Form["id"];

            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            SM.YuQing.Accounts.SiteIdentity identity = (SM.YuQing.Accounts.SiteIdentity)(new SM.YuQing.Accounts.AccountsPrincipal(context.User.Identity.Name)).Identity;
            int personid = identity.FID;
            SM.YuQing.BLL.Regions bll = new SM.YuQing.BLL.Regions();
            SM.YuQing.Model.Regions region = bll.GetModel(Convert.ToInt32(id));
            string mall = region.Mall;

            bool success;
            SM.YuQing.BLL.MonitorWebs webBll = new SM.YuQing.BLL.MonitorWebs();
            SM.YuQing.BLL.MonitorInfos infoBll = new SM.YuQing.BLL.MonitorInfos();
            if (!bll.ExistRegion(Convert.ToInt32(id), personid) && !webBll.ExistRegion(Convert.ToInt32(id)) && !infoBll.ExistRegion(Convert.ToInt32(id)))
            {
                bll.Delete(Convert.ToInt32(id));
                bll.DeletePersonRegion(personid, Convert.ToInt32(id));
                success = true;
            }
            else
                success = false;

            Hashtable ht = new Hashtable();
            if (success)
            {
                SM.YuQing.Model.Log log = new SM.YuQing.Model.Log();
                log.LogType = "操作";
                log.Message = context.User.Identity.Name + " 删除了区域 " + mall;
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
                ht.Add("errorMsg", "该区域已有关联监测数据，不允许删除");
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