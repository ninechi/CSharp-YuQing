using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace YuQing.Admin.Monitor.RealTime
{
    /// <summary>
    /// DoSearch 的摘要说明
    /// </summary>
    public class DoSearch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            RealTimeMonitor(context, 1);
        }

        public DataTable RealTimeMonitor(HttpContext context, int level)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            string regionid = context.Request.QueryString["regionid"];
            string keyword = context.Request.QueryString["keyword"];
            string website = context.Request.QueryString["website"];
            string searchLevel = context.Request.QueryString["sl"];
            string func = context.Request.QueryString["func"];

            if (regionid == "" || regionid.Contains("-1"))
            {
                GetRegionIdHelper grih = new GetRegionIdHelper();
                regionid = grih.GetRegionID();
            }

            //keyword = System.Web.HttpUtility.UrlDecode(keyword);
            DataTable dt = new DataTable();
            dt.Columns.Add("Title");
            dt.Columns.Add("PublishDate");
            dt.Columns.Add("ViewsCounts");
            dt.Columns.Add("Region");
            dt.Columns.Add("RegionID");
            dt.Columns.Add("MonitorUrl");
            dt.Columns.Add("Keyword");
            dt.Columns.Add("Url");

            SM.YuQing.BLL.RealTimeMonitorHelper rtmh = new SM.YuQing.BLL.RealTimeMonitorHelper();

            try
            {
                if (website != "")
                {
                    //dt = RemoveCommonRecord(rtmh.GetDataRows(dt, website, keyword, null, null));

                    //searchLevel 搜索级别
                    if (searchLevel == "1")
                        dt = rtmh.GetDataRows(dt, website, keyword, null, null);
                    else if (searchLevel == "2")
                        dt = rtmh.GetData(dt, website, keyword, null, null);
                }
                else
                {
                    string[] regs = regionid.Split(',');
                    foreach (string reg in regs)
                    {
                        SM.YuQing.BLL.Regions regionBll = new SM.YuQing.BLL.Regions();
                        SM.YuQing.Model.Regions region = regionBll.GetModel(Convert.ToInt32(reg.Trim()));

                        SM.YuQing.BLL.MonitorWebs webBll = new SM.YuQing.BLL.MonitorWebs();
                        List<SM.YuQing.Model.MonitorWebs> webList = webBll.GetModelList("RegionID=" + reg.Trim());
                        foreach (SM.YuQing.Model.MonitorWebs web in webList)
                        {
                            if (web.Status == "启用")
                            {
                                if (searchLevel=="1")
                                    dt = rtmh.GetDataRows(dt, web.Url, keyword, region, web);
                                else if (searchLevel == "2")
                                    dt = rtmh.GetData(dt, web.Url, keyword, region, web);
                            }
                        }
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["Title"] = "未匹配到关键字“" + keyword + "”";
                    dr["PublishDate"] = "";
                    dr["ViewsCounts"] = "";
                    dr["Region"] = "";
                    dr["RegionID"] = "";
                    dr["MonitorUrl"] = "";
                    dr["Keyword"] = "";
                    dr["Url"] = "";
                    dt.Rows.Add(dr);
                }
                else
                {
                    //DataTable去重
                    int rowsCount = dt.Rows.Count;
                    for (int i = 0; i < rowsCount - 1; i++)
                        for (int j = i + 1; j < rowsCount; )
                        {
                            if (dt.Rows[i]["Url"].ToString() == dt.Rows[j]["Url"].ToString())
                            {
                                dt.Rows.RemoveAt(j);
                                rowsCount--;
                                dt.AcceptChanges();
                            }
                            else
                                j++;
                        }

                    if (func != "1")
                    {
                        //写入数据库
                        rtmh.RecordToDatabase(dt, context.User.Identity.Name);

                        //写入日志Log
                        SM.YuQing.Model.Log log = new SM.YuQing.Model.Log();
                        log.LogType = "实时监测";
                        log.Message = "本次共新增 " + dt.Rows.Count.ToString() + " 条数据";
                        log.IP = context.Request.UserHostAddress;
                        log.MenuID = 0;
                        log.PersonID = 0;
                        log.CreateTime = DateTime.Now;
                        SM.YuQing.BLL.Log logBll = new SM.YuQing.BLL.Log();
                        logBll.Add(log);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //DataRow dr = dt.NewRow();
                //dr["Title"] = "Error in ProcessRequest():" + ex.Message;
                //dr["PublishDate"] = "";
                //dr["ViewsCounts"] = "";
                //dr["Region"] = "";
                //dr["RegionID"] = "";
                //dr["MonitorUrl"] = "";
                //dr["Keyword"] = "";
                //dr["Url"] = "";
                //dt.Rows.Add(dr);
            }

            if (func == "1")  //导出excel
            {
                ExportToExcelHelper eteh = new ExportToExcelHelper();
                eteh.ExportToExcel(dt, "RealTimeResult.xls", "[监测管理] -> [实时监测]");
            }
            else
            {
                context.Response.Write(JsonConvert.SerializeObject(dt));
            }
            return dt;
        }

        //dt去重
        public DataTable RemoveCommonRecord(DataTable dt)
        {
            int rowsCount = dt.Rows.Count;
            for (int i = 0; i < rowsCount - 1; i++)
                for (int j = i + 1; j < rowsCount; )
                {
                    if (dt.Rows[i]["Url"].ToString() == dt.Rows[j]["Url"].ToString())
                    {
                        dt.Rows.RemoveAt(j);
                        rowsCount--;
                        dt.AcceptChanges();
                    }
                    else
                        j++;
                }
            return dt;
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