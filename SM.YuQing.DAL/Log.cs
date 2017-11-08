using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace SM.YuQing.DAL
{
    /// <summary>
    /// 数据访问类:Log
    /// </summary>
    public partial class Log
    {
        public Log()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Log");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SM.YuQing.Model.Log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Log(");
            strSql.Append("LogType,Message,MenuID,PersonID,IP,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@LogType,@Message,@MenuID,@PersonID,@IP,@CreateTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@LogType", SqlDbType.NVarChar,50),
					new SqlParameter("@Message", SqlDbType.NVarChar,500),
					new SqlParameter("@MenuID", SqlDbType.Int,4),
					new SqlParameter("@PersonID", SqlDbType.Int,4),
					new SqlParameter("@IP", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.LogType;
            parameters[1].Value = model.Message;
            parameters[2].Value = model.MenuID;
            parameters[3].Value = model.PersonID;
            parameters[4].Value = model.IP;
            parameters[5].Value = model.CreateTime;

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
        public bool Update(SM.YuQing.Model.Log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Log set ");
            strSql.Append("LogType=@LogType,");
            strSql.Append("Message=@Message,");
            strSql.Append("MenuID=@MenuID,");
            strSql.Append("PersonID=@PersonID,");
            strSql.Append("IP=@IP,");
            strSql.Append("CreateTime=@CreateTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@LogType", SqlDbType.NVarChar,50),
					new SqlParameter("@Message", SqlDbType.NVarChar,500),
					new SqlParameter("@MenuID", SqlDbType.Int,4),
					new SqlParameter("@PersonID", SqlDbType.Int,4),
					new SqlParameter("@IP", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.LogType;
            parameters[2].Value = model.Message;
            parameters[3].Value = model.MenuID;
            parameters[4].Value = model.PersonID;
            parameters[5].Value = model.IP;
            parameters[6].Value = model.CreateTime;

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
            strSql.Append("delete from Log ");
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
            strSql.Append("delete from Log ");
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
        public SM.YuQing.Model.Log GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,LogType,Message,MenuID,PersonID,IP,CreateTime from Log ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            SM.YuQing.Model.Log model = new SM.YuQing.Model.Log();
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
        public SM.YuQing.Model.Log DataRowToModel(DataRow row)
        {
            SM.YuQing.Model.Log model = new SM.YuQing.Model.Log();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["LogType"] != null)
                {
                    model.LogType = row["LogType"].ToString();
                }
                if (row["Message"] != null)
                {
                    model.Message = row["Message"].ToString();
                }
                if (row["MenuID"] != null && row["MenuID"].ToString() != "")
                {
                    model.MenuID = int.Parse(row["MenuID"].ToString());
                }
                if (row["PersonID"] != null && row["PersonID"].ToString() != "")
                {
                    model.PersonID = int.Parse(row["PersonID"].ToString());
                }
                if (row["IP"] != null)
                {
                    model.IP = row["IP"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
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
            strSql.Append("select ID,LogType,Message,MenuID,PersonID,IP,CreateTime ");
            strSql.Append(" FROM Log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
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
            strSql.Append(" ID,LogType,Message,MenuID,PersonID,IP,CreateTime ");
            strSql.Append(" FROM Log ");
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
            strSql.Append("select count(1) FROM Log ");
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
            strSql.Append(")AS Row, T.*  from Log T ");
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
            parameters[0].Value = "Log";
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