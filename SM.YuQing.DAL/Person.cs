using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SM.YuQing.DAL
{
    /// <summary>
    /// 数据访问类:Person
    /// </summary>
    public partial class Person
    {
        public Person()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Person");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SM.YuQing.Model.Person model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Person(");
            strSql.Append("Name,Code,Pwd,IsLock,LastLoginTime,LoginTimes,CreateTime,CreatePerson,UpdateTime,UpdatePerson)");
            strSql.Append(" values (");
            strSql.Append("@Name,@Code,@Pwd,@IsLock,@LastLoginTime,@LoginTimes,@CreateTime,@CreatePerson,@UpdateTime,@UpdatePerson)");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@Pwd", SqlDbType.NVarChar,50),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@LoginTimes", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.Pwd;
            parameters[3].Value = model.IsLock;
            parameters[4].Value = model.LastLoginTime;
            parameters[5].Value = model.LoginTimes;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.CreatePerson;
            parameters[8].Value = model.UpdateTime;
            parameters[9].Value = model.UpdatePerson;

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
        public bool Update(SM.YuQing.Model.Person model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Person set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Code=@Code,");
            strSql.Append("Pwd=@Pwd,");
            strSql.Append("IsLock=@IsLock,");
            strSql.Append("LastLoginTime=@LastLoginTime,");
            strSql.Append("LoginTimes=@LoginTimes,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdatePerson=@UpdatePerson");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Code", SqlDbType.NVarChar,50),
					new SqlParameter("@Pwd", SqlDbType.NVarChar,50),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@LoginTimes", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID",SqlDbType.Int)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.Pwd;
            parameters[3].Value = model.IsLock;
            parameters[4].Value = model.LastLoginTime;
            parameters[5].Value = model.LoginTimes;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.CreatePerson;
            parameters[8].Value = model.UpdateTime;
            parameters[9].Value = model.UpdatePerson;
            parameters[10].Value = model.ID;

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
            strSql.Append("delete from Person ");
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
            strSql.Append("delete from Person ");
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
        public SM.YuQing.Model.Person GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Name,Code,Pwd,IsLock,LastLoginTime,LoginTimes,CreateTime,CreatePerson,UpdateTime,UpdatePerson from Person ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            SM.YuQing.Model.Person model = new SM.YuQing.Model.Person();
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
        public SM.YuQing.Model.Person DataRowToModel(DataRow row)
        {
            SM.YuQing.Model.Person model = new SM.YuQing.Model.Person();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Code"] != null)
                {
                    model.Code = row["Code"].ToString();
                }
                if (row["Pwd"] != null)
                {
                    model.Pwd = row["Pwd"].ToString();
                }
                if (row["IsLock"] != null && row["IsLock"].ToString() != "")
                {
                    model.IsLock = int.Parse(row["IsLock"].ToString());
                    switch (model.IsLock)
                    {
                        case 0:
                            model.Status = "正常";
                            break;
                        case 1:
                            model.Status = "锁定";
                            break;
                        default:
                            break;
                    }
                }
                if (row["LastLoginTime"] != null && row["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(row["LastLoginTime"].ToString());
                }
                if (row["LoginTimes"] != null && row["LoginTimes"].ToString() != "")
                {
                    model.LoginTimes = int.Parse(row["LoginTimes"].ToString());
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
                //加载角色
                string sql = "SELECT [PersonId],r.* FROM [dbo].[PersonRole] pr inner join [Role] r on pr.RoleId=r.ID where [PersonId]='" + model.ID + "'";
                DataTable dt = DbHelperSQL.Query(sql).Tables[0];
                foreach (DataRow item in dt.Rows)
                {
                    model.Role += item["Name"].ToString() + ",";
                    model.RoleID += item["ID"].ToString() + ",";
                }
                if (!string.IsNullOrEmpty(model.Role))
                {
                    model.Role = model.Role.Substring(0, model.Role.Length - 1);
                    model.RoleID = model.RoleID.Substring(0, model.RoleID.Length - 1);
                }
                //加载区域
                List<Model.Regions> regions = GetRegions(model.ID);
                foreach (Model.Regions item in regions)
                {
                    model.Regions += item.Region + ",";
                }
                if (!string.IsNullOrEmpty(model.Regions))
                {
                    model.Regions = model.Regions.Substring(0, model.Regions.Length - 1);
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
            strSql.Append("select ID,Name,Code,Pwd,IsLock,LastLoginTime,LoginTimes,CreateTime,CreatePerson,UpdateTime,UpdatePerson ");
            strSql.Append(" FROM Person ");
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
            strSql.Append(" ID,Name,Code,Pwd,IsLock,LastLoginTime,LoginTimes,CreateTime,CreatePerson,UpdateTime,UpdatePerson ");
            strSql.Append(" FROM Person ");
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
            strSql.Append("select count(1) FROM Person ");
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
            strSql.Append(")AS Row, T.*  from Person T ");
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
            parameters[0].Value = "Person";
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

        public int TestPassword(int fitemId, string encPassword)
        {
            string sql = "SELECT [ID] FROM [Person] WHERE [ID] = @UserID AND [Pwd] = @EncryptedPassword";

            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@UserID", SqlDbType.Int), 
                new SqlParameter("@EncryptedPassword", SqlDbType.NVarChar) };
            parameters[0].Value = fitemId;
            parameters[1].Value = encPassword;

            object obj = DbHelperSQL.GetSingle(sql, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SM.YuQing.Model.Person GetModelFromCode(string code)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID,Name,Code,Pwd,IsLock,LastLoginTime,LoginTimes,CreateTime,CreatePerson,UpdateTime,UpdatePerson from Person ");
            strSql.Append(" where Code=@Code ");
            SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar)			};
            parameters[0].Value = code;

            SM.YuQing.Model.Person model = new SM.YuQing.Model.Person();
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

        public int ValidateLogin(string userName, string encPassword)
        {
            string sql = "SELECT [ID] FROM [Person] WHERE [Code] = @UserName AND [Pwd] = @EncryptedPassword";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.NVarChar), 
                new SqlParameter("@EncryptedPassword", SqlDbType.NVarChar) };

            parameters[0].Value = userName;
            parameters[1].Value = encPassword;

            object obj = DbHelperSQL.GetSingle(sql, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public bool UpdateLoginInfo(int fitemId, DateTime flastLoginTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [Person] set ");
            strSql.Append("[LastLoginTime]=@lastLoginTime,");
            strSql.Append("[LoginTimes]=LoginTimes+1");
            strSql.Append(" where [ID]=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@lastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = flastLoginTime;
            parameters[1].Value = fitemId;
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

        public bool UpdateisLock(int fitemId, int fisLock)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [Person] set ");
            strSql.Append("[IsLock]=@IsLock ");
            strSql.Append(" where [ID]=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@IsLock", SqlDbType.Int),
					new SqlParameter("@ID", SqlDbType.Int)};
            parameters[0].Value = fisLock;
            parameters[1].Value = fitemId;
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

        public bool CanViewMenu(int PersonID,string menuid)
        {
            string sql = "SELECT * FROM [dbo].[PersonRole] pr "
                + "inner join RoleMenuOperation rmo on pr.RoleId=rmo.RoleId "
                + "where PersonId='" + PersonID + "' and MenuID='" + menuid + "'";

            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Model.Regions> GetRegions(int PersonID)
        {
            List<Model.Regions> regions = new List<Model.Regions>();
            DAL.Regions dalregion = new Regions();
            string sql = "SELECT [PersonID],[RegionID] FROM [PersonRegion] WHERE [PersonID]=" + PersonID;
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                Model.Regions region = dalregion.GetModel(Convert.ToInt32(item["RegionID"]));
                regions.Add(region);
            }
            return regions;
        }

        public bool AddRegions(int PersonID, int RegionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO [PersonRegion] ([PersonID],[RegionID]) VALUES (@PersonID,@RegionID)");
            SqlParameter[] parameters = {
					new SqlParameter("@PersonID", SqlDbType.Int,4),
					new SqlParameter("@RegionID", SqlDbType.Int,4)
					};
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

        public bool ClearAllRegions(int PersonID)
        {
            string sql = "DELETE FROM [PersonRegion] WHERE [PersonID]=" + PersonID;
            int rows = DbHelperSQL.ExecuteSql(sql);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Model.Role> GetRoles(int PersonID)
        {
            List<Model.Role> roles = new List<Model.Role>();
            DAL.Role dalrole = new Role();
            string sql = "SELECT [PersonId],[RoleId] FROM [PersonRole] WHERE [PersonID]=" + PersonID;
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                Model.Role role = dalrole.GetModel(Convert.ToInt32(item["RoleId"]));
                roles.Add(role);
            }
            return roles;
        }

        public bool ClearAllRoles(int PersonID)
        {
            string sql = "DELETE FROM [PersonRole] WHERE [PersonID]=" + PersonID;
            int rows = DbHelperSQL.ExecuteSql(sql);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddRoles(int PersonID, int RoleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO [PersonRole] ([PersonId],[RoleId]) VALUES (@PersonID,@RoleId)");
            SqlParameter[] parameters = {
					new SqlParameter("@PersonID", SqlDbType.Int,4),
					new SqlParameter("@RoleId", SqlDbType.Int,4)
					};
            parameters[0].Value = PersonID;
            parameters[1].Value = RoleId;

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

        public bool HavePermission(string account, string menuName, string operationName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from [RoleMenuOperation] rmo");
            strSql.Append(" inner join [PersonRole] pr on rmo.[RoleID]= pr.[RoleId]");
            strSql.Append(" inner join [Person] p on pr.[PersonID] = p.[ID]");
            strSql.Append(" inner join [Menu] m on rmo.[MenuID] = m.[ID]");
            strSql.Append(" inner join [Operation] o on rmo.[OperationID] = o.[ID]");
            strSql.Append(" where p.code=@account and m.[Name]=@menuName and o.[Name]=@operationName");
            SqlParameter[] parameters = {
					new SqlParameter("@account", SqlDbType.NVarChar),
					new SqlParameter("@menuName", SqlDbType.NVarChar),
					new SqlParameter("@operationName", SqlDbType.NVarChar)
                                        };
            parameters[0].Value = account;
            parameters[1].Value = menuName;
            parameters[2].Value = operationName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        #endregion  ExtensionMethod
    }
}