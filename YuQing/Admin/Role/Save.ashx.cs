using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Role
{
    /// <summary>
    /// Save 的摘要说明
    /// </summary>
    public class Save : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.QueryString["id"];
            string Name = context.Request.Form["Name"];
            string Description = context.Request.Form["Description"];
            bool success;

            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            string clientip = context.Request.UserHostAddress;

            if (id == null)
            {
                SM.YuQing.BLL.Role bll = new SM.YuQing.BLL.Role();
                SM.YuQing.Model.Role model = new SM.YuQing.Model.Role();
                model.Name = Name;
                model.Description = Description;
                model.CreateTime = DateTime.Now;
                model.CreatePerson = "";
                model.UpdateTime = DateTime.Now;
                model.UpdatePerson = "";

                success = bll.Add(model);
                SM.YuQing.BLL.Log.Add("操作", context.User.Identity.Name + " 创建角色[" + Name + "]", 0, 0, clientip);

            }
            else
            {
                SM.YuQing.BLL.Role bll = new SM.YuQing.BLL.Role();
                SM.YuQing.Model.Role model = bll.GetModel(Convert.ToInt32(id));
                model.Name = Name;
                model.Description = Description;
                model.UpdatePerson = "";
                model.UpdateTime = DateTime.Now;

                success = bll.Update(model);
                SM.YuQing.BLL.Log.Add("操作", context.User.Identity.Name + " 修改角色[" + Name + "]", 0, 0, clientip);
            }
            Hashtable ht = new Hashtable();
            if (success)
            {
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