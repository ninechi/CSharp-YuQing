using SM.YuQing.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace YuQing
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Time_Task.Instance().ExecuteTask += new System.Timers.ElapsedEventHandler(Global_ExecuteTask);
            Time_Task.Instance().Interval = 1000 * 60 * 60;//表示间隔 1 小时
            //Time_Task.Instance().Interval = 1000 * 60 * 10;//test: 10mins//ok
            Time_Task.Instance().Start();
        }

        void Global_ExecuteTask(object sender, System.Timers.ElapsedEventArgs e)
        {
            SM.YuQing.BLL.Regions bllRegions = new SM.YuQing.BLL.Regions();
            List<SM.YuQing.Model.Regions> regions = bllRegions.GetModelList("");
            SM.YuQing.BLL.MonitorInfos bllInfos = new SM.YuQing.BLL.MonitorInfos();

            SM.YuQing.BLL.Config bllConfig = new SM.YuQing.BLL.Config();
            int searchLevel = bllConfig.GetSearchLevels();

            foreach (SM.YuQing.Model.Regions region in regions)
            {
                //每天下午16点监测一次
                if (DateTime.Now.Hour == 16)
                    bllInfos.GetMonitorInfos(region, region.Keyword, searchLevel);

                //每小时监测一次
                bllInfos.GetMonitorInfos(region, "SM", searchLevel);
            }

            //foreach (SM.YuQing.Model.MonitorInfos item in infos)
            //{
            //    bllInfos.Add(item);
            //}

            //SM.YuQing.BLL.MonitorWebs bllwebs = new SM.YuQing.BLL.MonitorWebs();
            ////SM.YuQing.Model.MonitorWebs modelwebs = bllwebs.GetModel(7);
            //List<SM.YuQing.Model.MonitorWebs> webs = bllwebs.GetModelList("");
            //SM.YuQing.BLL.MonitorInfos bllinfos = new SM.YuQing.BLL.MonitorInfos();
            //List<SM.YuQing.Model.MonitorInfos> infos = bllinfos.GetMonitorInfos(webs, "SM");
            //foreach (SM.YuQing.Model.MonitorInfos item in infos)
            //{
            //    bllinfos.Add(item);
            //}
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}