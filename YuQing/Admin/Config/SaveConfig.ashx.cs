using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Config
{
    /// <summary>
    /// SaveConfig 的摘要说明
    /// </summary>
    public class SaveConfig : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string count = context.Request.Form["Count"];

            bool success;
            string errorMsg = "";

            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            string clientip = context.Request.UserHostAddress;

            SM.YuQing.BLL.Config bll = new SM.YuQing.BLL.Config();
            SM.YuQing.Model.Config config = bll.GetModelByName("SearchLevels");
            config.Count = Convert.ToInt32(count);

            config.UpdatePerson = context.User.Identity.Name;
            config.UpdateTime = DateTime.Now;

            success = bll.Update(config);

            Hashtable ht = new Hashtable();
            if (success)
            {
                ht.Add("success", true);
                SM.YuQing.BLL.Log.Add("操作", context.User.Identity.Name + " 修改配置", 0, 0, clientip);
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}