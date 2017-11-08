using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.IO;
using ExcelLibrary.SpreadSheet;

namespace YuQing.Admin.Monitor
{
    public class ExportToExcelHelper
    {
        public void ExportToExcel(DataTable dt, string fileName, string menuName)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
                HttpContext.Current.Response.ContentType = "application/ms-excel";

                MemoryStream ms = new MemoryStream();
                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("Sheet1");

                if (dt.Rows.Count < 30)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        dt.Rows.Add(dt.NewRow());
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
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                HttpContext.Current.Response.Flush();

                SM.YuQing.Accounts.SiteIdentity identity = (SM.YuQing.Accounts.SiteIdentity)(new SM.YuQing.Accounts.AccountsPrincipal(HttpContext.Current.User.Identity.Name)).Identity;
                int personid = identity.FID;

                SM.YuQing.Model.Log log = new SM.YuQing.Model.Log();
                log.LogType = "操作";
                log.Message = HttpContext.Current.User.Identity.Name + " 在 " + menuName + " 导出了Excel" ;
                log.IP = HttpContext.Current.Request.UserHostAddress;
                log.MenuID = 0;
                log.PersonID = personid;
                log.CreateTime = DateTime.Now;
                SM.YuQing.BLL.Log logBll = new SM.YuQing.BLL.Log();
                logBll.Add(log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}