using System;
using System.Data;
using System.Collections.Generic;
using SM.YuQing.Model;
using SM.YuQing.DAL;
namespace SM.YuQing.BLL
{
    /// <summary>
    /// Role
    /// </summary>
    public partial class Role
    {
        private readonly SM.YuQing.DAL.Role dal = new SM.YuQing.DAL.Role();
        public Role()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SM.YuQing.Model.Role model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SM.YuQing.Model.Role model)
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
        public SM.YuQing.Model.Role GetModel(int ID)
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
        public List<SM.YuQing.Model.Role> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SM.YuQing.Model.Role> DataTableToList(DataTable dt)
        {
            List<SM.YuQing.Model.Role> modelList = new List<SM.YuQing.Model.Role>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SM.YuQing.Model.Role model;
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

        public List<string> GetMenuOperation(int id)
        {
            string sql = "SELECT [MenuID],[OperationID] FROM [RoleMenuOperation] where [RoleID]='" + id + "'";
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            List<string> lst = new List<string>();
            foreach (DataRow item in dt.Rows)
            {
                if (item["OperationID"] == DBNull.Value)
                {
                    lst.Add(item["MenuID"].ToString());
                }
                else
                {
                    lst.Add(item["MenuID"] + "^" + item["OperationID"]);
                }
            }
            return lst;
        }

        public bool SaveRoleMenuOperation(string roleid, string[] ids)
        {
            //先删除所有关联
            string sql = "DELETE FROM [RoleMenuOperation] WHERE [RoleID]='" + roleid + "'";
            DbHelperSQL.ExecuteSql(sql);

            foreach (string item in ids)
            {
                if (item != "")
                {
                    string[] array = item.Split('^');
                    if (array.Length == 1)
                    {
                        sql = "INSERT INTO [RoleMenuOperation] ([RoleID],[MenuID]) "
                            + "VALUES ('" + roleid + "','" + array[0] + "')";
                    }
                    else
                    {
                        sql = "INSERT INTO [RoleMenuOperation] ([RoleID],[MenuID],[OperationID]) "
                            + "VALUES ('" + roleid + "','" + array[0] + "','" + array[1] + "')";
                    }
                    DbHelperSQL.ExecuteSql(sql);
                }
            }
            return true;
        }

        public bool DeleteRolePerson(string roleid)
        {
            string sql = "delete from [PersonRole] where [RoleId]='" + roleid + "'";
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

        public bool DeleteRoleMenuOperation(string roleid)
        {
            string sql = "delete from [RoleMenuOperation] where [RoleId]='" + roleid + "'";
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

        #endregion  ExtensionMethod
    }
}