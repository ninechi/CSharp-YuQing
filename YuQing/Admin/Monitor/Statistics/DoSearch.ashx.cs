using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Data;
using System.Collections;
using System.IO;
using ExcelLibrary.SpreadSheet;

namespace YuQing.Admin.Monitor.Statistics
{
    /// <summary>
    /// DoSearch 的摘要说明
    /// </summary>
    public class DoSearch : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();

            string regionid = context.Request.QueryString["RegionId"];
            string beginDate = context.Request.QueryString["beginDate"];
            string endDate = context.Request.QueryString["endDate"];
            string func = context.Request.QueryString["func"];

            if (regionid == "" || regionid.Contains("-1"))
            {
                GetRegionIdHelper grih = new GetRegionIdHelper();
                regionid = grih.GetRegionID();
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Region");
            dt.Columns.Add("Positive");
            dt.Columns.Add("Negative");
            dt.Columns.Add("Neutral");
            dt.Columns.Add("Empty");
            dt.Columns.Add("Total");

            try
            {
                string strWhere = "";
                ArrayList strWhereList = new ArrayList();

                //strWhereList.Add(" RegionID in (" + regionid + ")");
                //strWhereList.Add(" Keyword like \'SM\'");

                if (beginDate != "" && endDate != "")
                {
                    if (regionid != "")
                    {
                        string[] regs = regionid.Split(',');
                        foreach (string regid in regs)
                        {
                            strWhere = " PublishDate>=\'" + beginDate + "\' and PublishDate<=\'" + endDate + " 23:59:59.999\'" + " and RegionID=" + regid;
                            SM.YuQing.BLL.Regions regionBll = new SM.YuQing.BLL.Regions();
                            SM.YuQing.Model.Regions region = regionBll.GetModel(Convert.ToInt32(regid));
                            dt = GetDataRow(dt, null, region.Mall, strWhere);
                        }
                    }
                    else
                    {
                        SM.YuQing.BLL.Regions regionBll = new SM.YuQing.BLL.Regions();
                        List<SM.YuQing.Model.Regions> regionList = regionBll.GetModelList("");
                        foreach (SM.YuQing.Model.Regions region in regionList)
                        {
                            strWhere = " PublishDate>=\'" + beginDate + "\' and PublishDate<=\'" + endDate + " 23:59:59.999\'" + " and RegionID=" + region.ID;
                            dt = GetDataRow(dt, region, "", strWhere);
                        }
                    }
                }
                else
                {
                    if (regionid != "")
                    {
                        string[] regs = regionid.Split(',');
                        foreach (string regid in regs)
                        {
                            SM.YuQing.BLL.Regions regionBll = new SM.YuQing.BLL.Regions();
                            SM.YuQing.Model.Regions region = regionBll.GetModel(Convert.ToInt32(regid));
                            dt = GetDataRow(dt, null, region.Mall, " RegionID=" + regid);
                        }
                    }
                    else
                    {
                        SM.YuQing.BLL.Regions regionBll = new SM.YuQing.BLL.Regions();
                        List<SM.YuQing.Model.Regions> regionList = regionBll.GetModelList("");
                        foreach (SM.YuQing.Model.Regions region in regionList)
                        {
                            dt = GetDataRow(dt, region, "", " RegionID=" + region.ID.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //DataRow dr = dt.NewRow();
                //dr["Region"] = "Error in ProcessRequest():" + ex.Message; ;
                //dr["Positive"] = "";
                //dr["Negative"] = "";
                //dr["Neutral"] = "";
                //dr["Empty"] = "";
                //dr["Total"] = "";
                //dt.Rows.Add(dr);
            }

            if (func == "1")  //导出excel
            {
                ExportToExcelHelper eteh = new ExportToExcelHelper();
                eteh.ExportToExcel(dt, "StatisticsResult.xls", "[监测管理] -> [监测性质统计表]");
            }
            else
                context.Response.Write(JsonConvert.SerializeObject(dt));
        }

        private DataTable GetDataRow(DataTable dt, SM.YuQing.Model.Regions region, string reg, string strWhere)
        {
            int positive = 0, negative = 0, neutral = 0, empty = 0;
            try
            {
                DataRow dr = dt.NewRow();
                dr["Region"] = region==null?reg:region.Mall;

                SM.YuQing.BLL.MonitorInfos monitorInfoBll = new SM.YuQing.BLL.MonitorInfos();
                List<SM.YuQing.Model.MonitorInfos> monitorInfoList = monitorInfoBll.GetModelList(strWhere + " and Keyword like \'SM\'");
                foreach (SM.YuQing.Model.MonitorInfos monitorInfo in monitorInfoList)
                {
                    switch (monitorInfo.Property)
                    {
                        case "正面":
                            positive++;
                            break;
                        case "负面":
                            negative++;
                            break;
                        case "中性":
                            neutral++;
                            break;
                        case "":
                            empty++;
                            break;
                    }
                }
                dr["Positive"] = positive.ToString();
                dr["Negative"] = negative.ToString();
                dr["Neutral"] = neutral.ToString();
                dr["Empty"] = empty.ToString();
                dr["Total"] = (positive + negative + neutral + empty).ToString();
                dt.Rows.Add(dr);
            }
            catch (Exception ex)
            {
                DataRow dr = dt.NewRow();
                dr["Region"] = "Error in GetDataRow():" + ex.Message; ;
                dr["Positive"] = "";
                dr["Negative"] = "";
                dr["Neutral"] = "";
                dr["Empty"] = "";
                dr["Total"] = "";
                dt.Rows.Add(dr);
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