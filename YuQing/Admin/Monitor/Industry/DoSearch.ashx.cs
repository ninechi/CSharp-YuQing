using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Data;
using System.Collections;

namespace YuQing.Admin.Monitor.Industry
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

            string regionid = context.Request.QueryString["regionid"], reg = "";
            string beginDate = context.Request.QueryString["beginDate"];
            string endDate = context.Request.QueryString["endDate"];
            string keyword = context.Request.QueryString["keyword"];
            string func = context.Request.QueryString["func"];

            if (regionid == "" || regionid.Contains("-1"))
            {
                GetRegionIdHelper grih = new GetRegionIdHelper();
                regionid = grih.GetRegionID();
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Title");
            dt.Columns.Add("PublishDate");
            dt.Columns.Add("ViewsCounts");
            dt.Columns.Add("Keyword");
            dt.Columns.Add("Region");
            dt.Columns.Add("Url");

            try
            {
                string strWhere = "";
                ArrayList strWhereList = new ArrayList();

                if (beginDate != "" && endDate != "")
                {
                    strWhereList.Add(" PublishDate>=\'" + beginDate + "\' and PublishDate<=\'" + endDate + " 23:59:59.999\'");
                }
                if (regionid != "")
                {
                    strWhereList.Add(" RegionID in (" + regionid + ")");
                }
                if (keyword != "")
                {
                    strWhereList.Add(" Keyword like N\'%" + keyword + "%\'");
                }

                switch (strWhereList.Count)
                {
                    case 1:
                        strWhere = strWhereList[0].ToString();
                        break;
                    case 2:
                        strWhere = strWhereList[0].ToString() + " and " + strWhereList[1].ToString();
                        break;
                    case 3:
                        strWhere = strWhereList[0].ToString() + " and " + strWhereList[1].ToString() + " and " + strWhereList[2].ToString();
                        break;
                    default:
                        break;
                }

                SM.YuQing.BLL.MonitorInfos monitorInfoBll = new SM.YuQing.BLL.MonitorInfos();
                List<SM.YuQing.Model.MonitorInfos> monitorInfoList = monitorInfoBll.GetModelList(strWhere);
                foreach (SM.YuQing.Model.MonitorInfos monitorInfo in monitorInfoList)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = monitorInfo.ID;
                    dr["Title"] = monitorInfo.Title;
                    dr["PublishDate"] = monitorInfo.PublishDate.ToString("yyyy-MM-dd");
                    dr["ViewsCounts"] = monitorInfo.ViewsCounts;
                    dr["Keyword"] = monitorInfo.Keyword;
                    if (monitorInfo.RegionID > 0)
                    {
                        SM.YuQing.BLL.Regions regionBll = new SM.YuQing.BLL.Regions();
                        SM.YuQing.Model.Regions region = regionBll.GetModel(Convert.ToInt32(monitorInfo.RegionID));
                        reg = region.Mall;
                    }
                    else
                        reg = "";
                    dr["Region"] = reg;
                    dr["Url"] = monitorInfo.Url;
                    dt.Rows.Add(dr);
                }

                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = "";
                    dr["Title"] = "未查询到数据";
                    dr["PublishDate"] = "";
                    dr["ViewsCounts"] = "";
                    dr["Keyword"] = "";
                    dr["Region"] = "";
                    dr["Url"] = "";
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //DataRow dr = dt.NewRow();
                //dr["Title"] = "Error in ProcessRequest():" + ex.Message;
                //dr["PublishDate"] = "";
                //dr["ViewsCounts"] = "";
                //dr["Keyword"] = "";
                //dr["Region"] = "";
                //dr["Url"] = "";
                //dt.Rows.Add(dr);
            }

            if (func == "1")  //导出excel
            {
                ExportToExcelHelper eteh = new ExportToExcelHelper();
                eteh.ExportToExcel(dt, "IndustryResult.xls", "[监测管理] -> [行业监测]");
            }
            else
                context.Response.Write(JsonConvert.SerializeObject(dt));
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