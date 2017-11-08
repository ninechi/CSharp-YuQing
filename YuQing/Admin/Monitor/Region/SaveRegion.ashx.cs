using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Collections;

namespace YuQing.Admin.Monitor.Region
{
    /// <summary>
    /// SaveRegion 的摘要说明
    /// </summary>
    public class SaveRegion : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.QueryString["id"];
            string Region = context.Request.Form["Region"];
            string Mall = context.Request.Form["Mall"];
            string Keyword = context.Request.Form["Keyword"];
            string errMsg = "";
            bool success;
            Hashtable ht = new Hashtable();

            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            SM.YuQing.Accounts.SiteIdentity identity = (SM.YuQing.Accounts.SiteIdentity)(new SM.YuQing.Accounts.AccountsPrincipal(context.User.Identity.Name)).Identity;
            int personid = identity.FID;

            SM.YuQing.BLL.Regions bll = new SM.YuQing.BLL.Regions();
            if (id == null)
            {
                if (!bll.ExistRegion(Region.Trim(), Mall.Trim(), Keyword.Trim()))
                {
                    SM.YuQing.Model.Regions region = new SM.YuQing.Model.Regions();
                    region.Region = Region;
                    region.Mall = Mall;
                    region.Keyword = Keyword;
                    region.CreatePerson = context.User.Identity.Name;
                    region.CreateTime = DateTime.Now;
                    region.UpdatePerson = context.User.Identity.Name;
                    region.UpdateTime = DateTime.Now;

                    success = bll.Add(region);

                    if (success)
                    {
                        int maxRegionID = bll.GetMaxRegionID(context.User.Identity.Name);
                        bll.AddPersonRegion(personid, maxRegionID);

                        SM.YuQing.Model.Log log = new SM.YuQing.Model.Log();
                        log.LogType = "操作";
                        log.Message = context.User.Identity.Name + " 新增了区域 " + Mall;
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
                    errMsg = "此区域已存在";
                }
                
            }
            else
            {
                SM.YuQing.Model.Regions region = bll.GetModel(Convert.ToInt32(id));
                string oldRegion = region.Region, oldMall = region.Mall, oldKeyword = region.Keyword;

                if (oldRegion == Region && oldMall == Mall && oldKeyword == Keyword)
                {
                    success = false;
                    errMsg = "您未修改任何信息";
                }
                else if (!bll.ExistRegion(Region.Trim(), Mall.Trim(), Keyword.Trim()))
                {
                    region.Region = Region;
                    region.Mall = Mall;
                    region.Keyword = Keyword;

                    region.UpdatePerson = context.User.Identity.Name;
                    region.UpdateTime = DateTime.Now;

                    success = bll.Update(region);

                    if (success)
                    {
                        SM.YuQing.Model.Log log = new SM.YuQing.Model.Log();
                        log.LogType = "操作";
                        log.Message = context.User.Identity.Name + " 修改了ID=" + id + "的区域信息" + " | " + (oldRegion == Region ? "" : (oldRegion + " -> " + Region + " | ")) + (oldMall == Mall ? "" : (oldMall + " -> " + Mall + " | ")) + (oldKeyword == Keyword ? "" : ("关键字 | "));
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
                    errMsg = "此区域已存在";
                }
            }
            //if (!bll.ExistRegion(Region.Trim(), Mall.Trim(), Keyword.Trim()))
            //{
                
            //}
            //else
            //{
            //    success = false;
            //    errMsg = "此区域已存在";
            //}

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