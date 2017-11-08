using Newtonsoft.Json;
using SM.YuQing.Accounts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Person
{
    /// <summary>
    /// SavePerson 的摘要说明
    /// </summary>
    public class SavePerson : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.QueryString["id"];
            string Name = context.Request.Form["Name"];
            string Code = context.Request.Form["Code"];
            string Pwd = context.Request.Form["Pwd"];
            string RoleID = context.Request.Form["RoleID"];
            
            bool success;
            string errorMsg = "";

            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            string clientip = context.Request.UserHostAddress;

            if (id == null)
            {
                SM.YuQing.BLL.Person bll = new SM.YuQing.BLL.Person();
                SM.YuQing.Model.Person p = bll.GetModelFromCode(Code);
                if (p == null)
                {
                    SM.YuQing.Model.Person person = new SM.YuQing.Model.Person();
                    person.Name = Name;
                    person.Code = Code;
                    person.Pwd = AccountsPrincipal.EncryptPassword(Pwd);
                    person.CreatePerson = context.User.Identity.Name;
                    person.CreateTime = DateTime.Now;
                    person.UpdatePerson = context.User.Identity.Name;
                    person.UpdateTime = DateTime.Now;
                    person.IsLock = 0;
                    person.LastLoginTime = DateTime.Now;
                    person.LoginTimes = 0;

                    success = bll.Add(person);

                    SM.YuQing.BLL.Log.Add("操作", context.User.Identity.Name + " 创建用户[" + person.Code + "]", 0, 0, clientip);
                }
                else
                {
                    success = false;
                    errorMsg = "此用户名已存在！";
                }
            }
            else
            {
                string[] ids = context.Request.Form["ids"].Split(',');

                SM.YuQing.BLL.Person bll = new SM.YuQing.BLL.Person();
                SM.YuQing.Model.Person person = bll.GetModel(Convert.ToInt32(id));
                person.Name = Name;
                if (Pwd != "")
                {
                    person.Pwd = AccountsPrincipal.EncryptPassword(Pwd);
                }
                person.UpdatePerson = context.User.Identity.Name;
                person.UpdateTime = DateTime.Now;

                success = bll.Update(person);
                bll.ClearAllRegions(person.ID);
                AddRegions(person, ids);
                bll.ClearAllRoles(person.ID);
                if (!string.IsNullOrEmpty(RoleID))
                {
                    bll.AddRoles(person.ID, Convert.ToInt32(RoleID));
                }
                SM.YuQing.BLL.Log.Add("操作", context.User.Identity.Name + " 修改用户[" + person.Code + "]", 0, 0, clientip);
            }
            Hashtable ht = new Hashtable();
            if (success)
            {
                ht.Add("success", true);
            }
            else
            {
                if (errorMsg == "")
                {
                    ht.Add("errorMsg", "Some errors occured.");
                }
                else
                {
                    ht.Add("errorMsg", errorMsg);
                }
            }
            context.Response.Write(JsonConvert.SerializeObject(ht));
        }

        public void AddRegions(SM.YuQing.Model.Person person, string[] ids)
        {
            SM.YuQing.BLL.Person bll = new SM.YuQing.BLL.Person();
            foreach (string item in ids)
            {
                if (item != "" && item != "undefined")
                {
                    bll.AddRegions(person.ID, Convert.ToInt32(item));
                }
            }
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