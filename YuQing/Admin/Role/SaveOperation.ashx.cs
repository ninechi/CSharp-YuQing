using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Role
{
    /// <summary>
    /// SaveOperation 的摘要说明
    /// </summary>
    public class SaveOperation : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            string returnValue = "";
            string[] ids = context.Request.Form["ids"].Split(',');
            string id = context.Request.Form["id"];
            SM.YuQing.BLL.Role bll = new SM.YuQing.BLL.Role();
            if (bll.SaveRoleMenuOperation(id, ids))
            {
                returnValue = "OK";
                string clientip = context.Request.UserHostAddress;
                SM.YuQing.Model.Role role = bll.GetModel(Convert.ToInt32(id));
                SM.YuQing.BLL.Log.Add("操作", context.User.Identity.Name + " 给角色[" + role.Name + "]分配权限", 0, 0, clientip);

            }
            context.Response.Write(returnValue);
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