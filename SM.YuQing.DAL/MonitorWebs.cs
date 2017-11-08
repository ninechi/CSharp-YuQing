using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace SM.YuQing.DAL
{
    /// <summary>
    /// 数据访问类:MonitorWebs
    /// </summary>
    public partial class MonitorWebs
    {
        public MonitorWebs()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MonitorWebs");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool ExistWeb(int RegionID, string Url, string Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MonitorWebs");
            strSql.Append(" where RegionID=@RegionID and Url=@Url and Status=@Status");
            SqlParameter[] parameters = {
					new SqlParameter("@RegionID", SqlDbType.Int,4),
                    new SqlParameter("@Url", SqlDbType.NVarChar, 500),
                    new SqlParameter("@Status", SqlDbType.NVarChar, 50)      };
            parameters[0].Value = RegionID;
            parameters[1].Value = Url;
            parameters[2].Value = Status;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool ExistWeb(int RegionID, string Url, string Name, string Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MonitorWebs");
            strSql.Append(" where RegionID=@RegionID and Url=@Url and Name=@Name and Status=@Status");
            SqlParameter[] parameters = {
					new SqlParameter("@RegionID", SqlDbType.Int,4),
                    new SqlParameter("@Url", SqlDbType.NVarChar, 500),
                    new SqlParameter("@Name", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Status", SqlDbType.NVarChar, 50)      };
            parameters[0].Value = RegionID;
            parameters[1].Value = Url;
            parameters[2].Value = Name;
            parameters[3].Value = Status;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool ExistRegion(int RegionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MonitorWebs");
            strSql.Append(" where RegionID=@RegionID");
            SqlParameter[] parameters = {
					new SqlParameter("@RegionID", SqlDbType.Int,4)     };
            parameters[0].Value = RegionID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SM.YuQing.Model.MonitorWebs model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MonitorWebs(");
            strSql.Append("RegionID,Name,Url,Status,CreateTime,CreatePerson,UpdateTime,UpdatePerson)");
            strSql.Append(" values (");
            strSql.Append("@RegionID,@Name,@Url,@Status,@CreateTime,@CreatePerson,@UpdateTime,@UpdatePerson)");
            SqlParameter[] parameters = {
					new SqlParameter("@RegionID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Url", SqlDbType.NVarChar,500),
					new SqlParameter("@Status", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.RegionID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Url;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.CreatePerson;
            parameters[6].Value = model.UpdateTime;
            parameters[7].Value = model.UpdatePerson;

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
        public bool Update(SM.YuQing.Model.MonitorWebs model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MonitorWebs set ");
            strSql.Append("RegionID=@RegionID,");
            strSql.Append("Name=@Name,");
            strSql.Append("Url=@Url,");
            strSql.Append("Status=@Status,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdatePerson=@UpdatePerson");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@RegionID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Url", SqlDbType.NVarChar,500),
					new SqlParameter("@Status", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.RegionID;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Url;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.CreatePerson;
            parameters[7].Value = model.UpdateTime;
            parameters[8].Value = model.UpdatePerson;

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
            strSql.Append("delete from MonitorWebs ");
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
            strSql.Append("delete from MonitorWebs ");
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
        public SM.YuQing.Model.MonitorWebs GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,RegionID,Name,Url,Status,CreateTime,CreatePerson,UpdateTime,UpdatePerson from MonitorWebs ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            SM.YuQing.Model.MonitorWebs model = new SM.YuQing.Model.MonitorWebs();
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
        public SM.YuQing.Model.MonitorWebs DataRowToModel(DataRow row)
        {
            SM.YuQing.Model.MonitorWebs model = new SM.YuQing.Model.MonitorWebs();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["RegionID"] != null && row["RegionID"].ToString() != "")
                {
                    model.RegionID = int.Parse(row["RegionID"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Url"] != null)
                {
                    model.Url = row["Url"].ToString();
                }
                if (row["Status"] != null)
                {
                    model.Status = row["Status"].ToString();
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
            strSql.Append("select ID,RegionID,Name,Url,Status,CreateTime,CreatePerson,UpdateTime,UpdatePerson");
            strSql.Append(" FROM MonitorWebs ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by RegionID,ID ");
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
            strSql.Append(" ID,RegionID,Name,Url,Status,CreateTime,CreatePerson,UpdateTime,UpdatePerson ");
            strSql.Append(" FROM MonitorWebs ");
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
            strSql.Append("select count(1) FROM MonitorWebs ");
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
            strSql.Append(")AS Row, T.*  from MonitorWebs T ");
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
            parameters[0].Value = "MonitorWebs";
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