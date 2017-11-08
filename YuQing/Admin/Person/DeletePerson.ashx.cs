using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Person
{
    /// <summary>
    /// DeletePerson 的摘要说明
    /// </summary>
    public class DeletePerson : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.Form["id"];

            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            SM.YuQing.BLL.Person bll = new SM.YuQing.BLL.Person();
            SM.YuQing.Model.Person person = bll.GetModel(Convert.ToInt32(id));

            //删除区域关联
            bll.ClearAllRegions(Convert.ToInt32(id));
            //删除角色关联
            bll.ClearAllRoles(Convert.ToInt32(id));
            //删除用户
            bool success = bll.Delete(Convert.ToInt32(id));
            Hashtable ht = new Hashtable();
            if (success)
            {
                ht.Add("success", true);
                string clientip = context.Request.UserHostAddress;
                SM.YuQing.BLL.Log.Add("操作", context.User.Identity.Name + " 删除用户[" + person.Code + "]", 0, 0, clientip);
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