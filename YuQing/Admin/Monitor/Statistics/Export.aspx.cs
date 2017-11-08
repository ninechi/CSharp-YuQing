using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using ExcelLibrary.SpreadSheet;

namespace YuQing.Admin.Monitor.Statistics
{
    public partial class Export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                ExportToExcel();
        }

        public void ExportToExcel()
        {
            string beginDate = Request.QueryString["beginDate"];
            string endDate = Request.QueryString["endDate"];
            string regionid = Request.QueryString["RegionId"];

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

                if (beginDate != "" && endDate != "")
                {
                    strWhere = " PublishDate>=\'" + beginDate + "\' and PublishDate<=\'" + endDate + " 23:59:59.999\'";
                    if (regionid != "")
                    {
                        string[] regs = regionid.Split(',');
                        foreach (string regid in regs)
                        {
                            strWhere += " and RegionID=" + regid;
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
                            strWhere += " and RegionID=" + region.ID;
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

                Response.Clear();
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", "StatisticsResult.xls"));
                Response.ContentType = "application/ms-excel";

                MemoryStream ms = new MemoryStream();
                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("Sheet1");

                if (dt.Rows.Count < 30)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Region"] = "";
                        dr["Positive"] = "";
                        dr["Negative"] = "";
                        dr["Neutral"] = "";
                        dr["Empty"] = "";
                        dr["Total"] = "";
                        dt.Rows.Add(dr);
                    }
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[0, i] = new Cell(dt.Columns[i].ColumnName);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 1, j] = new Cell(dt.Rows[i][j].ToString());
                    }
                }

                workbook.Worksheets.Add(worksheet);
                workbook.SaveToStream(ms);
                Response.BinaryWrite(ms.ToArray());
                Response.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable GetDataRow(DataTable dt, SM.YuQing.Model.Regions region, string reg, string strWhere)
        {
            int positive = 0, negative = 0, neutral = 0, empty = 0;
            try
            {
                DataRow dr = dt.NewRow();
                dr["Region"] = region == null ? reg : region.Mall;

                SM.YuQing.BLL.MonitorInfos monitorInfoBll = new SM.YuQing.BLL.MonitorInfos();
                List<SM.YuQing.Model.MonitorInfos> monitorInfoList = monitorInfoBll.GetModelList(strWhere);
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
                        case "空":
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
    }
}