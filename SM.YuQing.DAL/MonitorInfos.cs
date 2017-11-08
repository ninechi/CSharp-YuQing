using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace SM.YuQing.DAL
{
    /// <summary>
    /// 数据访问类:MonitorInfos
    /// </summary>
    public partial class MonitorInfos
    {
        public MonitorInfos()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MonitorInfos");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool Exists(string Url, int RegionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MonitorInfos");
            strSql.Append(" where Url=@Url and RegionID=@RegionID");
            SqlParameter[] parameters = {
                    new SqlParameter("@Url", SqlDbType.NVarChar, 500),
					new SqlParameter("@RegionID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = Url;
            parameters[1].Value = RegionID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool ExistRegion(int RegionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MonitorInfos");
            strSql.Append(" where RegionID=@RegionID");
            SqlParameter[] parameters = {
					new SqlParameter("@RegionID", SqlDbType.Int,4)     };
            parameters[0].Value = RegionID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SM.YuQing.Model.MonitorInfos model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MonitorInfos(");
            strSql.Append("Title,Url,PublishDate,ViewsCounts,RegionID,MonitorUrl,Keyword,Property,CreateTime,CreatePerson,UpdateTime,UpdatePerson)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Url,@PublishDate,@ViewsCounts,@RegionID,@MonitorUrl,@Keyword,@Property,@CreateTime,@CreatePerson,@UpdateTime,@UpdatePerson)");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Url", SqlDbType.NVarChar,500),
					new SqlParameter("@PublishDate", SqlDbType.DateTime),
					new SqlParameter("@ViewsCounts", SqlDbType.Int,4),
					new SqlParameter("@RegionID", SqlDbType.Int,4),
					new SqlParameter("@MonitorUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@Keyword", SqlDbType.NVarChar,500),
					new SqlParameter("@Property", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Url;
            parameters[2].Value = model.PublishDate;
            parameters[3].Value = model.ViewsCounts;
            parameters[4].Value = model.RegionID;
            parameters[5].Value = model.MonitorUrl;
            parameters[6].Value = model.Keyword;
            parameters[7].Value = model.Property;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.CreatePerson;
            parameters[10].Value = model.UpdateTime;
            parameters[11].Value = model.UpdatePerson;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SM.YuQing.Model.MonitorInfos model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MonitorInfos set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Url=@Url,");
            strSql.Append("PublishDate=@PublishDate,");
            strSql.Append("ViewsCounts=@ViewsCounts,");
            strSql.Append("RegionID=@RegionID,");
            strSql.Append("MonitorUrl=@MonitorUrl,");
            strSql.Append("Keyword=@Keyword,");
            strSql.Append("Property=@Property,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdatePerson=@UpdatePerson");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Url", SqlDbType.NVarChar,500),
					new SqlParameter("@PublishDate", SqlDbType.DateTime),
					new SqlParameter("@ViewsCounts", SqlDbType.Int,4),
					new SqlParameter("@RegionID", SqlDbType.Int,4),
					new SqlParameter("@MonitorUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@Keyword", SqlDbType.NVarChar,500),
					new SqlParameter("@Property", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Url;
            parameters[3].Value = model.PublishDate;
            parameters[4].Value = model.ViewsCounts;
            parameters[5].Value = model.RegionID;
            parameters[6].Value = model.MonitorUrl;
            parameters[7].Value = model.Keyword;
            parameters[8].Value = model.Property;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.CreatePerson;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.UpdatePerson;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MonitorInfos ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MonitorInfos ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SM.YuQing.Model.MonitorInfos GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Title,Url,PublishDate,ViewsCounts,RegionID,MonitorUrl,Keyword,Property,CreateTime,CreatePerson,UpdateTime,UpdatePerson from MonitorInfos ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            SM.YuQing.Model.MonitorInfos model = new SM.YuQing.Model.MonitorInfos();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SM.YuQing.Model.MonitorInfos DataRowToModel(DataRow row)
        {
            SM.YuQing.Model.MonitorInfos model = new SM.YuQing.Model.MonitorInfos();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["Url"] != null)
                {
                    model.Url = row["Url"].ToString();
                }
                if (row["PublishDate"] != null && row["PublishDate"].ToString() != "")
                {
                    model.PublishDate = DateTime.Parse(row["PublishDate"].ToString());
                }
                if (row["ViewsCounts"] != null && row["ViewsCounts"].ToString() != "")
                {
                    model.ViewsCounts = int.Parse(row["ViewsCounts"].ToString());
                }
                if (row["RegionID"] != null && row["RegionID"].ToString() != "")
                {
                    model.RegionID = int.Parse(row["RegionID"].ToString());
                }
                if (row["MonitorUrl"] != null)
                {
                    model.MonitorUrl = row["MonitorUrl"].ToString();
                }
                if (row["Keyword"] != null)
                {
                    model.Keyword = row["Keyword"].ToString();
                }
                if (row["Property"] != null)
                {
                    model.Property = row["Property"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["CreatePerson"] != null)
                {
                    model.CreatePerson = row["CreatePerson"].ToString();
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
                if (row["UpdatePerson"] != null)
                {
                    model.UpdatePerson = row["UpdatePerson"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,Url,PublishDate,ViewsCounts,RegionID,MonitorUrl,Keyword,Property,CreateTime,CreatePerson,UpdateTime,UpdatePerson ");
            strSql.Append(" FROM MonitorInfos ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by ID DESC");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,Title,Url,PublishDate,ViewsCounts,RegionID,MonitorUrl,Keyword,Property,CreateTime,CreatePerson,UpdateTime,UpdatePerson ");
            strSql.Append(" FROM MonitorInfos ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM MonitorInfos ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from MonitorInfos T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "MonitorInfos";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}