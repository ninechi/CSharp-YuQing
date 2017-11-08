using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace SM.YuQing.BLL
{
    public class RealTimeMonitorHelper
    {
        public DataTable GetData(DataTable dt, string website, string keyword, SM.YuQing.Model.Regions region, SM.YuQing.Model.MonitorWebs web)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("Title");
            //dt.Columns.Add("PublishDate");
            //dt.Columns.Add("ViewsCounts");
            //dt.Columns.Add("Region");
            //dt.Columns.Add("RegionID");
            //dt.Columns.Add("MonitorUrl");
            //dt.Columns.Add("Keyword");
            //dt.Columns.Add("Url");

            DataTable dtUrl = new DataTable();
            dtUrl.Columns.Add("Title");
            dtUrl.Columns.Add("PublishDate");
            dtUrl.Columns.Add("ViewsCounts");
            dtUrl.Columns.Add("Region");
            dtUrl.Columns.Add("RegionID");
            dtUrl.Columns.Add("MonitorUrl");
            dtUrl.Columns.Add("Keyword");
            dtUrl.Columns.Add("Url");

            //int searchLevel = 1;

            try
            {
                //get all urls
                dtUrl = RemoveCommonUrl(GetUrls(website));

                //一级页面
                dt = GetDataRows(dt, website, keyword, null, null);

                //二级页面
                int count = dtUrl.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    string site = dtUrl.Rows[i]["Url"].ToString();
                    dt = GetDataRows(dt, site, keyword, null, null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public DataTable GetUrls(string website)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Title");
            dt.Columns.Add("PublishDate");
            dt.Columns.Add("ViewsCounts");
            dt.Columns.Add("Region");
            dt.Columns.Add("RegionID");
            dt.Columns.Add("MonitorUrl");
            dt.Columns.Add("Keyword");
            dt.Columns.Add("Url");

            DataRow dr = dt.NewRow();
            dr["Title"] = website;
            dr["PublishDate"] = "";
            dr["ViewsCounts"] = "";
            dr["Region"] = "";
            dr["RegionID"] = "";
            dr["MonitorUrl"] = "";
            dr["Keyword"] = "";
            dr["Url"] = website;
            dt.Rows.Add(dr);

            try
            {
                string html = GetHtmlString(website, "utf-8");
                if (html.IndexOf("charset") < 0)
                {
                    html = GetHtmlString(website, "gb2312");
                    if (html.IndexOf("charset") < 0)
                    {
                        html = GetHtmlString(website, "gbk");
                    }
                }

                //string html = GetHtmlString(website, "UTF-8");
                //string str = html.Substring(0, html.IndexOf("charset=") + 64);  //报错点？？？

                //if (str.ToLower().Contains("gb2312"))
                //{
                //    html = GetHtmlString(website, "GB2312");
                //}
                //else if (str.ToLower().Contains("gbk"))
                //{
                //    html = GetHtmlString(website, "GBK");
                //}

                string regex = @"href=(\W)?((http|https)://)(([\w\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[\w\&%_\./-~-]*)?";
                Regex re = new Regex(regex, RegexOptions.IgnoreCase);
                MatchCollection matches = re.Matches(html);
                if (matches != null)
                {
                    IEnumerator enu = matches.GetEnumerator();
                    while (enu.MoveNext() && enu.Current != null)
                    {
                        Match match = (Match)(enu.Current);
                        string s = match.Value.Replace(">", "").Replace("<", "").Trim();//去掉头尾的“>”和“<”
                        if (s.Contains("\'") || s.Contains("\""))
                            //ret = str.Substring(6, str.Length - 6);  //去掉href="
                            s = s.Remove(0, 6);
                        else
                            s = s.Remove(0, 5);

                        dr = dt.NewRow();
                        dr["Title"] = s;
                        dr["PublishDate"] = "";
                        dr["ViewsCounts"] = "";
                        dr["Region"] = "";
                        dr["RegionID"] = "";
                        dr["MonitorUrl"] = "";
                        dr["Keyword"] = "";
                        dr["Url"] = s;
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public string GetHtmlTitle(string html)
        {
            string ret = "";
            ret = html.Substring(html.ToLower().IndexOf("<title>")+7, html.ToLower().IndexOf("</title>"));
            return ret;
        }

        public DataTable RemoveCommonUrl(DataTable dt)
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

        public DataTable GetDataRows(DataTable dt, string website, string keyword, SM.YuQing.Model.Regions region, SM.YuQing.Model.MonitorWebs web)
        {
            try
            {
                string html = GetHtmlString(website, "utf-8");
                if (html.IndexOf("charset") < 0)
                {
                    html = GetHtmlString(website, "gb2312");
                    if (html.IndexOf("charset") < 0)
                    {
                        html = GetHtmlString(website, "gbk");
                    }
                }

                //string html = GetHtmlString(website, "UTF-8");
                //string str = html.Substring(0, html.IndexOf("charset=") + 64);  //报错点？？？

                //if (str.ToLower().Contains("gb2312"))
                //{
                //    html = GetHtmlString(website, "GB2312");
                //}
                //else if (str.ToLower().Contains("gbk"))
                //{
                //    html = GetHtmlString(website, "GBK");
                //}

                ////将获取的内容写入文本
                //using (StreamWriter sw = new StreamWriter("c:\\html.html"))
                //{
                //    sw.Write(html);
                //}

                //string regex = @">.*" + keyword + @".*<";
                //string regex = @">((.*|\s*))?" + keyword + @"((.*|\s*)<)?";


                if (html.IndexOf("charset=") >= 0)
                {
                    string regex = @">[^<>]*" + keyword + @"[^<>]*<";
                    if(keyword.ToLower()=="sm")
                        regex = @">[^<>]*" + keyword + @"[^a-zA-Z][^<>]*<";
                    Regex re = new Regex(regex, RegexOptions.IgnoreCase);
                    MatchCollection matches = re.Matches(html);
                    if (matches != null)
                    {
                        IEnumerator enu = matches.GetEnumerator();
                        while (enu.MoveNext() && enu.Current != null)
                        {
                            Match match = (Match)(enu.Current);
                            if (html.LastIndexOf(match.Value) > html.Length)
                                continue;

                            string url = GetUrlFromHtml(html.Substring(0, html.LastIndexOf(match.Value)));
                            SM.YuQing.BLL.MonitorInfos bll = new MonitorInfos();
                            //if (bll.Exists(url, region == null ? 0 : region.ID))  //存在此Url+RegionID
                            //    continue;

                            string tmpHtml = GetHtmlString(url, "UTF-8");
                            if (tmpHtml.IndexOf("charset=") < 0)
                            {
                                tmpHtml = GetHtmlString(url, "GB2312");
                                if (tmpHtml.IndexOf("charset=") < 0)
                                {
                                    tmpHtml = GetHtmlString(url, "GBK");
                                }
                            }
                            string title = match.Value.Replace(">", "").Replace("<", "").Trim();  //去掉头尾的“>”和“<”

                            if (Regex.IsMatch(match.Value, @"[\u4e00-\u9fa5]") && !Regex.IsMatch(match.Value, @";|function") && url != "" && !url.ToLower().Contains("error"))  //包含中文，去除js代码、空url && tmpHtml.Contains(title) && Regex.IsMatch(title, @"\p{ASCII}")
                            {
                                DataRow dr = dt.NewRow();
                                //dr["Title"] = match.Value.Substring(1, match.Value.Length - 2).Trim();  //去掉头尾的“>”和“<”
                                //if (isLastLevel)
                                //    dr["Title"] = GetHtmlTitle(html);
                                //else

                                dr["Title"] = title.Length > 100 ? (title.Substring(0, 97) + "...") : title;
                                dr["PublishDate"] = "";
                                dr["ViewsCounts"] = "";
                                dr["Region"] = region == null ? "" : region.Mall;
                                dr["RegionID"] = region == null ? "" : region.ID.ToString();
                                dr["MonitorUrl"] = web == null ? website : web.Url;
                                dr["Keyword"] = keyword;
                                dr["Url"] = url;
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //DataRow dr = dt.NewRow();
                //dr["Title"] = "Error in GetDataRows():" + ex.Message;
                //dr["PublishDate"] = "";
                //dr["ViewsCounts"] = "";
                //dr["Region"] = "";
                //dr["RegionID"] = "";
                //dr["MonitorUrl"] = "";
                //dr["Keyword"] = "";
                //dr["Url"] = "";
                //dt.Rows.Add(dr);
            }
            return dt;
        }

        private string GetHtmlString(string website, string encoding)
        {
            string ret = "";
            try
            {
                WebRequest wr = WebRequest.Create(website);
                WebResponse res = wr.GetResponse();
                Stream st = res.GetResponseStream();
                StreamReader sr = new StreamReader(st, Encoding.GetEncoding(encoding));
                ret = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                ret = "Error in GetHtmlString():" + ex.Message;
            }
            return ret;
        }

        private string GetUrlFromHtml(string html)
        {
            string ret = "";
            try
            {
                ArrayList urls = new ArrayList();
                //string regex = @"href=\W((http|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?";  //  ok
                string regex = @"href=(\W)?((http|https)://)(([\w\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[\w\&%_\./-~-]*)?";
                Regex re = new Regex(regex);
                MatchCollection matches = re.Matches(html);
                IEnumerator enu = matches.GetEnumerator();
                int count = matches.Count;

                if (matches.Count > 0)
                {
                    string str = matches[count - 1].Value;
                    if (str.Contains("\'") || str.Contains("\""))
                        //ret = str.Substring(6, str.Length - 6);  //去掉href="
                        ret = str.Remove(0, 6);
                    else
                        ret = str.Remove(0, 5);
                    //ret = str.Substring(5, str.Length - 5);
                }
                else
                    ret = "";
            }
            catch (Exception ex)
            {
                ret = "Error in GetUrlFromHtml():" + ex.Message;
            }
            return ret;
        }

        public int RecordToDatabase(DataTable dt, string userName)
        {
            int count = 0;
            //写入数据库
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SM.YuQing.BLL.MonitorInfos bll = new SM.YuQing.BLL.MonitorInfos();
                SM.YuQing.Model.MonitorInfos monitorInfo = new SM.YuQing.Model.MonitorInfos();
                monitorInfo.Title = dt.Rows[i]["Title"].ToString();
                monitorInfo.PublishDate = dt.Rows[i]["PublishDate"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(dt.Rows[i]["PublishDate"].ToString());
                monitorInfo.ViewsCounts = dt.Rows[i]["ViewsCounts"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[i]["ViewsCounts"].ToString());
                int regionid = dt.Rows[i]["RegionID"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[i]["RegionID"].ToString());
                monitorInfo.RegionID = regionid;
                monitorInfo.MonitorUrl = dt.Rows[i]["MonitorUrl"].ToString();
                monitorInfo.Keyword = dt.Rows[i]["Keyword"].ToString();
                monitorInfo.Url = dt.Rows[i]["Url"].ToString();
                monitorInfo.Property = "";
                monitorInfo.CreatePerson = userName;
                monitorInfo.CreateTime = DateTime.Now;
                monitorInfo.UpdatePerson = userName;
                monitorInfo.UpdateTime = DateTime.Now;

                if (!bll.Exists(dt.Rows[i]["Url"].ToString(), regionid))  //存在此Url+RegionID
                { 
                    bll.Add(monitorInfo);
                    count++;
                }
            }
            return count;
        }
    }
}
