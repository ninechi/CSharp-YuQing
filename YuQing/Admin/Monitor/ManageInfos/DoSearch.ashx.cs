using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Data;
using System.Collections;

namespace YuQing.Admin.Monitor.ManageInfos
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
            string property = context.Request.QueryString["property"];
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
            dt.Columns.Add("Region");
            dt.Columns.Add("Property"); 
            dt.Columns.Add("Url");

            try
            {
                string strWhere = "";
                ArrayList strWhereList = new ArrayList();

                strWhereList.Add(" RegionID in (" + regionid + ")");
                strWhereList.Add(" Keyword like \'SM\'");

                if (beginDate != "" && endDate != "")
                {
                    strWhereList.Add(" PublishDate>=\'" + beginDate + "\' and PublishDate<=\'" + endDate + " 23:59:59.999\'");
                }
                if (property != "" && !property.Contains("全部"))
                {
                    //strWhereList.Add(" Property=N\'" + (property == "空" ? "" : property) + "\'");
                    //" (Property=N\'" + (p == "空" ? "" : p) + "\' or Property=N\'" + p + "\'")
                    string[] props = property.Split(',');
                    string tmpSql = "";
                    foreach (string p in props)
                    {
                        tmpSql += " or Property=N\'" + (p == "空" ? "" : p) + "\'";
                    }
                    tmpSql = tmpSql.Substring(tmpSql.IndexOf("or")+2, tmpSql.Length-3);
                    strWhereList.Add(" (" + tmpSql + ") ");
                }

                switch(strWhereList.Count)
                { 
                    case 2:
                        strWhere = strWhereList[0].ToString() + " and " + strWhereList[1].ToString();
                        break;
                    case 3:
                        strWhere = strWhereList[0].ToString() + " and " + strWhereList[1].ToString() + " and " + strWhereList[2].ToString();
                        break;
                    case 4:
                        strWhere = strWhereList[0].ToString() + " and " + strWhereList[1].ToString() + " and " + strWhereList[2].ToString() + " and " + strWhereList[3].ToString();
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
                    if (monitorInfo.RegionID > 0)
                    {
                        SM.YuQing.BLL.Regions regionBll = new SM.YuQing.BLL.Regions();
                        SM.YuQing.Model.Regions region = regionBll.GetModel(Convert.ToInt32(monitorInfo.RegionID));
                        reg = region.Mall;
                    }
                    else
                        reg = "";
                    dr["Region"] = reg;
                    dr["Property"] = monitorInfo.Property;
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
                    dr["Region"] = "";
                    dr["Property"] = "";
                    dr["Url"] = "";
                    dt.Rows.Add(dr);
                }

                //context.Response.Write("<script>lert(\'ManageInfos.DoSearch\');</script>");
            }
            catch (Exception ex)
            {
                throw ex;
                //DataRow dr = dt.NewRow();
                //dr["ID"] = "";
                //dr["Title"] = "Error in ProcessRequest():" + ex.Message;
                //dr["PublishDate"] = "";
                //dr["ViewsCounts"] = "";
                //dr["Region"] = "";
                //dr["Property"] = "";
                //dr["Url"] = "";
                //dt.Rows.Add(dr);
            }

            if (func == "1")  //导出excel
            {
                ExportToExcelHelper eteh = new ExportToExcelHelper();
                eteh.ExportToExcel(dt, "ManageInfoResult.xls", "[监测管理] -> [监测数据管理]");
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