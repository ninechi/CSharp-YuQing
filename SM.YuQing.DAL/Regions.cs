using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace SM.YuQing.DAL
{
    /// <summary>
    /// 数据访问类:Regions
    /// </summary>
    public partial class Regions
    {
        public Regions()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Regions");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool ExistRegion(int RegionID, int PersonID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PersonRegion");
            strSql.Append(" where RegionID=@RegionID and PersonID <> @PersonID");
            SqlParameter[] parameters = {
					new SqlParameter("@RegionID", SqlDbType.Int,4),
                    new SqlParameter("@PersonID", SqlDbType.Int,4)       };
            parameters[0].Value = RegionID;
            parameters[0].Value = PersonID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public bool ExistRegion(string RegionName, string Mall, string Keyword)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Regions");
            strSql.Append(" where Region=@RegionName and Mall=@Mall and Keyword=@Keyword");
            SqlParameter[] parameters = {
                    new SqlParameter("@RegionName", SqlDbType.NChar,50),
                    new SqlParameter("@Mall", SqlDbType.NChar,50),
					new SqlParameter("@Keyword", SqlDbType.NChar,500)    };
            parameters[0].Value = RegionName;
            parameters[1].Value = Mall;
            parameters[2].Value = Keyword;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SM.YuQing.Model.Regions model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Regions(");
            strSql.Append("Region,Mall,Keyword,Description,CreateTime,CreatePerson,UpdateTime,UpdatePerson)");
            strSql.Append(" values (");
            strSql.Append("@Region,@Mall,@Keyword,@Description,@CreateTime,@CreatePerson,@UpdateTime,@UpdatePerson)");
            SqlParameter[] parameters = {
					new SqlParameter("@Region", SqlDbType.NVarChar,50),
					new SqlParameter("@Mall", SqlDbType.NVarChar,50),
					new SqlParameter("@Keyword", SqlDbType.NVarChar,500),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Region;
            parameters[1].Value = model.Mall;
            parameters[2].Value = model.Keyword;
            parameters[3].Value = model.Description;
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
        //更新PersonRegion表
        public bool AddPersonRegion(int PersonID, int RegionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PersonRegion(");
            strSql.Append("PersonID, RegionID)");
            strSql.Append(" values (");
            strSql.Append("@PersonID, @RegionID)");
            SqlParameter[] parameters = {
					new SqlParameter("@PersonID", SqlDbType.Int, 4),
					new SqlParameter("@RegionID", SqlDbType.Int, 4)};
            parameters[0].Value = PersonID;
            parameters[1].Value = RegionID;
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

        public bool DeletePersonRegion(int PersonID, int RegionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PersonRegion ");
            strSql.Append(" where PersonID=@PersonID and RegionID=@RegionID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PersonID", SqlDbType.Int, 4),
					new SqlParameter("@RegionID", SqlDbType.Int, 4)};
            parameters[0].Value = PersonID;
            parameters[1].Value = RegionID;

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
        public bool Update(SM.YuQing.Model.Regions model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Regions set ");
            strSql.Append("Region=@Region,");
            strSql.Append("Mall=@Mall,");
            strSql.Append("Keyword=@Keyword,");
            strSql.Append("Description=@Description,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdatePerson=@UpdatePerson");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Region", SqlDbType.NVarChar,50),
					new SqlParameter("@Mall", SqlDbType.NVarChar,50),
					new SqlParameter("@Keyword", SqlDbType.NVarChar,500),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Region;
            parameters[2].Value = model.Mall;
            parameters[3].Value = model.Keyword;
            parameters[4].Value = model.Description;
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
            strSql.Append("delete from Regions ");
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
            strSql.Append("delete from Regions ");
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
        public SM.YuQing.Model.Regions GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Region,Mall,Keyword,Description,CreateTime,CreatePerson,UpdateTime,UpdatePerson from Regions ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            SM.YuQing.Model.Regions model = new SM.YuQing.Model.Regions();
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

        public string GetRegionIdByPersonID(string PersonID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [RegionID]=stuff((select ','+convert(varchar(64),RegionID) from dbo.PersonRegion t where PersonID=dbo.PersonRegion.PersonID for xml path('')), 1, 1, '') from dbo.PersonRegion ");
            strSql.Append(" where PersonID=@PersonID ");
            strSql.Append(" group by PersonID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PersonID", SqlDbType.Int,4)			};
            parameters[0].Value = PersonID;

            SM.YuQing.Model.Regions model = new SM.YuQing.Model.Regions();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
        }

        public int GetMaxRegionID(string CreatePerson)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Max(ID) from dbo.Regions ");
            strSql.Append(" where CreatePerson=@CreatePerson ");
            SqlParameter[] parameters = {
					new SqlParameter("@CreatePerson", SqlDbType.NChar, 50)			};
            parameters[0].Value = CreatePerson;

            SM.YuQing.Model.Regions model = new SM.YuQing.Model.Regions();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SM.YuQing.Model.Regions DataRowToModel(DataRow row)
        {
            SM.YuQing.Model.Regions model = new SM.YuQing.Model.Regions();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Region"] != null)
                {
                    model.Region = row["Region"].ToString();
                }
                if (row["Mall"] != null)
                {
                    model.Mall = row["Mall"].ToString();
                }
                if (row["Keyword"] != null)
                {
                    model.Keyword = row["Keyword"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
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
            strSql.Append("select ID,Region,Mall,Keyword,Description,CreateTime,CreatePerson,UpdateTime,UpdatePerson ");
            strSql.Append(" FROM Regions ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by ID ");
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
            strSql.Append(" ID,Region,Mall,Keyword,Description,CreateTime,CreatePerson,UpdateTime,UpdatePerson ");
            strSql.Append(" FROM Regions ");
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
            strSql.Append("select count(1) FROM Regions ");
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
            strSql.Append(")AS Row, T.*  from Regions T ");
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
            parameters[0].Value = "Regions";
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