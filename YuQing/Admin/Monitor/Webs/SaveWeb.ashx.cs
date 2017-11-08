using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Collections;

namespace YuQing.Admin.Monitor.Webs
{
    /// <summary>
    /// SaveWeb 的摘要说明
    /// </summary>
    public class SaveWeb : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.QueryString["id"];
            string Url = context.Request.Form["Url"];
            string Name = context.Request.Form["Name"];
            string RegionID = context.Request.Form["RegionID"];
            string Status = context.Request.Form["Status"];
            string errMsg = "";
            bool success;

            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            SM.YuQing.Accounts.SiteIdentity identity = (SM.YuQing.Accounts.SiteIdentity)(new SM.YuQing.Accounts.AccountsPrincipal(context.User.Identity.Name)).Identity;
            int personid = identity.FID;

            if (id == null) //新增
            {
                SM.YuQing.BLL.MonitorWebs bll = new SM.YuQing.BLL.MonitorWebs();
                if (!bll.ExistWeb(Convert.ToInt32(RegionID), Url, Status))
                {
                    SM.YuQing.Model.MonitorWebs web = new SM.YuQing.Model.MonitorWebs();
                    web.Url = Url;
                    web.Name = Name;
                    web.RegionID = Convert.ToInt32(RegionID);
                    web.Status = Status;
                    web.CreatePerson = context.User.Identity.Name;
                    web.CreateTime = DateTime.Now;
                    web.UpdatePerson = context.User.Identity.Name;
                    web.UpdateTime = DateTime.Now;

                    success = bll.Add(web);
                    if (success)
                    {
                        SM.YuQing.Model.Log log = new SM.YuQing.Model.Log();
                        log.LogType = "操作";
                        log.Message = context.User.Identity.Name + " 新增了网站 " + Url + "(" + Name + ")";
                        log.IP = context.Request.UserHostAddress;
                        log.MenuID = 0;
                        log.PersonID = personid;
                        log.CreateTime = DateTime.Now;
                        SM.YuQing.BLL.Log logBll = new SM.YuQing.BLL.Log();
                        logBll.Add(log);
                    }
                    else
                    {
                        success = false;
                        errMsg = "some error occured.";
                    }
                }
                else
                {
                    success = false;
                    errMsg = "该网址已存在于此区域中！";
                }
            }
            else  //修改
            {
                SM.YuQing.BLL.MonitorWebs bll = new SM.YuQing.BLL.MonitorWebs();
                SM.YuQing.Model.MonitorWebs web = bll.GetModel(Convert.ToInt32(id));
                string oldUrl = web.Url, oldName = web.Name, oldRegionID = web.RegionID.ToString(), oldStatus = web.Status;

                web.Url = Url;
                web.Name = Name;
                web.RegionID = Convert.ToInt32(RegionID);
                web.Status = Status;

                web.UpdatePerson = context.User.Identity.Name;
                web.UpdateTime = DateTime.Now;

                if (oldUrl == Url && oldName == Name && oldRegionID == RegionID && oldStatus == Status)
                {
                    success = false;
                    errMsg = "您未修改任何信息";
                }
                else if (oldUrl == Url && oldName != Name && oldRegionID == RegionID && oldStatus == Status)
                {
                    success = bll.Update(web);
                }
                else if (!bll.ExistWeb(Convert.ToInt32(RegionID), Url, Status))
                {
                    success = bll.Update(web);
                }
                else
                {
                    success = false;
                    errMsg = "该网址已存在于此区域中！";
                }

                if (success)
                {
                    SM.YuQing.Model.Log log = new SM.YuQing.Model.Log();
                    log.LogType = "操作";
                    log.Message = context.User.Identity.Name + " 修改了ID=" + id + "的网站信息" + " | " + (oldUrl == Url ? "" : (oldUrl + " -> " + Url + " | ")) + (oldName == Name ? "" : (oldName + " -> " + Name + " | ")) + (oldRegionID == RegionID ? "" : ("RegionID: " + oldRegionID + " -> " + RegionID + " | ")) + (oldStatus == Status ? "" : (oldStatus + " -> " + Status + " | "));
                    log.IP = context.Request.UserHostAddress;
                    log.MenuID = 0;
                    log.PersonID = personid;
                    log.CreateTime = DateTime.Now;
                    SM.YuQing.BLL.Log logBll = new SM.YuQing.BLL.Log();
                    logBll.Add(log);
                }
                else if(!success && errMsg=="")
                {
                    errMsg = "some error occured.";
                }
            }

            Hashtable ht = new Hashtable();
            if (success)
            {
                ht.Add("success", true);
            }
            else
            {
                ht.Add("errorMsg", errMsg);
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