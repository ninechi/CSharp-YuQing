using System;
using System.Data;
using System.Collections.Generic;
using SM.YuQing.Model;
using SM.YuQing.DAL;
namespace SM.YuQing.BLL
{
    /// <summary>
    /// Menu
    /// </summary>
    public partial class Menu
    {
        private readonly SM.YuQing.DAL.Menu dal = new SM.YuQing.DAL.Menu();
        public Menu()
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
        public bool Add(SM.YuQing.Model.Menu model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SM.YuQing.Model.Menu model)
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
        public SM.YuQing.Model.Menu GetModel(int ID)
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
        public List<SM.YuQing.Model.Menu> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SM.YuQing.Model.Menu> DataTableToList(DataTable dt)
        {
            List<SM.YuQing.Model.Menu> modelList = new List<SM.YuQing.Model.Menu>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SM.YuQing.Model.Menu model;
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

        public List<SM.YuQing.Model.Menu> GetMenuTree()
        {
            List<SM.YuQing.Model.Menu> list = new List<SM.YuQing.Model.Menu>();
            DataTable dt = dal.GetList(0, "ParentID is null", "Sort").Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                list.Add(GetModelAndChildren(Convert.ToInt32(row["ID"])));
            }
            return list;
        }

        public SM.YuQing.Model.Menu GetModelAndChildren(int ID)
        {
            SM.YuQing.Model.Menu menu = dal.GetModel(ID);
            //加载子菜单
            List<SM.YuQing.Model.Menu> list = new List<SM.YuQing.Model.Menu>();
            DataTable dt = dal.GetList(0, "ParentID='" + menu.ID + "'", "Sort").Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                list.Add(GetModelAndChildren(Convert.ToInt32(row["ID"])));
            }
            menu.children = list;
            //加载操作
            string Operation = "";
            string sql = "select OperationID,Name from MenuOperation mo "
                + "inner join Operation o on mo.OperationID = o.ID "
                + "where mo.MenuID='" + menu.ID + "'";

            DataTable dtOpe = DbHelperSQL.Query(sql).Tables[0];
            foreach (DataRow row in dtOpe.Rows)
            {
                Operation += row["OperationID"] + "^" + row["Name"] + ",";
            }
            if (Operation.Length > 0)
            {
                Operation = Operation.Substring(0, Operation.Length - 1);
            }
            menu.Operation = Operation;
            return menu;
        }

        #endregion  ExtensionMethod
    }
}