using System;
using System.Data;
using System.Collections.Generic;
using SM.YuQing.Model;
using System.Net;
using System.IO;
using System.Text;

namespace SM.YuQing.BLL
{
    /// <summary>
    /// MonitorInfos
    /// </summary>
    public partial class MonitorInfos
    {
        private readonly SM.YuQing.DAL.MonitorInfos dal = new SM.YuQing.DAL.MonitorInfos();
        public MonitorInfos()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        public bool Exists(string Url, int RegionID)
        {
            return dal.Exists(Url, RegionID);
        }

        public bool ExistRegion(int RegionID)
        {
            return dal.ExistRegion(RegionID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SM.YuQing.Model.MonitorInfos model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SM.YuQing.Model.MonitorInfos model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SM.YuQing.Model.MonitorInfos GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SM.YuQing.Model.MonitorInfos> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SM.YuQing.Model.MonitorInfos> DataTableToList(DataTable dt)
        {
            List<SM.YuQing.Model.MonitorInfos> modelList = new List<SM.YuQing.Model.MonitorInfos>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SM.YuQing.Model.MonitorInfos model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod

        #region  ExtensionMethod

        public void GetMonitorInfos(Model.Regions region, string keywords, int searchLevel)
        {
            List<Model.MonitorInfos> infos = new List<Model.MonitorInfos>();
            
            DataTable dt = new DataTable();
            dt.Columns.Add("Title");
            dt.Columns.Add("PublishDate");
            dt.Columns.Add("ViewsCounts");
            dt.Columns.Add("Region");
            dt.Columns.Add("RegionID");
            dt.Columns.Add("MonitorUrl");
            dt.Columns.Add("Keyword");
            dt.Columns.Add("Url");

            RealTimeMonitorHelper rtmh = new RealTimeMonitorHelper();
            string[] keywordArray = keywords.Split('、');

            //foreach(Model.Regions region in regions)
            {
                SM.YuQing.BLL.MonitorWebs bllWebs = new SM.YuQing.BLL.MonitorWebs();
                List<SM.YuQing.Model.MonitorWebs> webs = bllWebs.GetModelList(" RegionID=" + region.ID.ToString());

                foreach (Model.MonitorWebs web in webs)
                {
                    foreach (string keyword in keywordArray)
                    {
                        if (searchLevel == 1)
                            dt = rtmh.GetDataRows(dt, web.Url, keyword.Trim(), region, web);
                        else if (searchLevel == 2)
                            dt = rtmh.GetData(dt, web.Url, keyword, region, web);
                    }

                    //string keys = null;
                    //if (keywords != null)
                    //{
                    //    keys = keywords;
                    //}
                    //else
                    //{
                    //    Model.Regions region = bllRegions.GetModel(web.RegionID);
                    //    keys = region.Keyword;
                    //}
                    //string[] arrayKeys = keys.Split(new char[] {'、'});

                    //string content = getRequest(web.Url);
                    //content = System.Text.RegularExpressions.Regex.Replace(content, @"<!--(.|[\r\n])*?-->", "");
                    //while (content.Contains("<a"))
                    //{
                    //    try
                    //    {
                    //        content = content.Remove(0, content.IndexOf("<a"));
                    //        string link = content.Substring(0, content.IndexOf("</a>") + 4);
                    //        content = content.Remove(0, content.IndexOf("</a>") + 4);

                    //        string url = GetUrlFromLink(link);

                    //        string text = ReplaceHtmlTag(link);
                    //        string matchkey = GetMatchKey(text, arrayKeys);

                    //        if (matchkey != "" && url != "")
                    //        {
                    //            Model.MonitorInfos info = new Model.MonitorInfos();
                    //            info.Title = text;
                    //            info.Url = url.Contains("http://") ? url : web.Url + url;
                    //            info.PublishDate = DateTime.Now;
                    //            info.ViewsCounts = 0;
                    //            info.RegionID = web.RegionID;
                    //            info.MonitorUrl = web.Url;
                    //            info.Keyword = matchkey;
                    //            info.Property = "";
                    //            info.CreatePerson = "";
                    //            info.CreateTime = DateTime.Now;
                    //            info.UpdatePerson = "";
                    //            info.UpdateTime = DateTime.Now;
                    //            if (!Exists(info))
                    //            {
                    //                infos.Add(info);
                    //            }
                    //        }
                    //    }
                    //    catch { }
                    //}
                }
            }

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

            //写入数据库表MonitorInfo
            int count = rtmh.RecordToDatabase(dt, "System");

            //写入数据库表Log
            if (count != 0)
            {
                SM.YuQing.Model.Log log = new SM.YuQing.Model.Log();
                log.LogType = "后台";
                log.Message = "本次共新增 " + count.ToString() + " 条数据（" + region.Mall + "）";
                log.IP = "";
                log.MenuID = 0;
                log.PersonID = 0;
                log.CreateTime = DateTime.Now;
                SM.YuQing.BLL.Log logBll = new SM.YuQing.BLL.Log();
                logBll.Add(log);
            }
            //return infos;
        }

        private string getRequest(string uri)
        {
            string content = "";
            try
            {
                HttpWebRequest requestScore = (HttpWebRequest)WebRequest.Create(uri);
                requestScore.Method = WebRequestMethods.Http.Get;
                requestScore.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                requestScore.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";

                HttpWebResponse response = (HttpWebResponse)requestScore.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                content = reader.ReadToEnd().ToLower();

                if (content.Contains("charset=gb2312"))
                {
                    content = getRequest(uri, Encoding.GetEncoding("gb2312"));
                }
            }
            catch { }

            return content;
        }

        private string getRequest(string uri,Encoding encoding)
        {
            HttpWebRequest requestScore = (HttpWebRequest)WebRequest.Create(uri);
            requestScore.Method = WebRequestMethods.Http.Get;
            requestScore.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            requestScore.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";

            HttpWebResponse response = (HttpWebResponse)requestScore.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), encoding);
            string content = reader.ReadToEnd().ToLower();

            return content;
        }

        public string ReplaceHtmlTag(string html)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

            return strText.Trim();
        }

        private string GetUrlFromLink(string link)
        {
            string url = "";
            if(link.Contains("href=\""))
            {
                link = link.Remove(0, link.IndexOf("href=\"")+6);
                url = link.Substring(0, link.IndexOf("\""));
            }
            else if (link.Contains("href='"))
            {
                link = link.Remove(0, link.IndexOf("href='") + 6);
                url = link.Substring(0, link.IndexOf("'"));
            }
            return url;
        }

        private string GetMatchKey(string text, string[] keys)
        {
            string match = "";
            foreach (string item in keys)
            {
                if (text.Contains(item))
                {
                    match = item;
                    break;
                }
            }
            return match;
        }

        public bool Exists(Model.MonitorInfos info)
        {
            DataTable dt = dal.GetList("[Url]='" + info.Url + "' and [RegionID]='" + info.RegionID + "'").Tables[0];
            return dt.Rows.Count > 0;
        }

        #endregion  ExtensionMethod
    }
}