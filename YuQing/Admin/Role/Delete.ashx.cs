using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Role
{
    /// <summary>
    /// Delete 的摘要说明
    /// </summary>
    public class Delete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.Form["id"];

            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            
            SM.YuQing.BLL.Role bll = new SM.YuQing.BLL.Role();
            SM.YuQing.Model.Role role = bll.GetModel(Convert.ToInt32(id));

            //删除与人员相关
            bll.DeleteRolePerson(id);
            //删除与菜单相关
            bll.DeleteRoleMenuOperation(id);
            //删除角色
            bool success = bll.Delete(Convert.ToInt32(id));
            Hashtable ht = new Hashtable();
            if (success)
            {
                ht.Add("success", true);
                string clientip = context.Request.UserHostAddress;
                SM.YuQing.BLL.Log.Add("操作", context.User.Identity.Name + " 删除角色[" + role.Name + "]", 0, 0, clientip);
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