using System;
using System.Data;
using System.Collections.Generic;
using SM.YuQing.Model;
using SM.YuQing.DAL;

namespace SM.YuQing.BLL
{
    /// <summary>
    /// Person
    /// </summary>
    public partial class Person
    {
        private readonly SM.YuQing.DAL.Person dal = new SM.YuQing.DAL.Person();
        public Person()
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
        public bool Add(SM.YuQing.Model.Person model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SM.YuQing.Model.Person model)
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
        public SM.YuQing.Model.Person GetModel(int ID)
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
        public List<SM.YuQing.Model.Person> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SM.YuQing.Model.Person> DataTableToList(DataTable dt)
        {
            List<SM.YuQing.Model.Person> modelList = new List<SM.YuQing.Model.Person>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SM.YuQing.Model.Person model;
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

        public int TestPassword(int fitemId, string encPassword)
        {
            return dal.TestPassword(fitemId, encPassword);
        }

        public SM.YuQing.Model.Person GetModelFromCode(string code)
        {
            return dal.GetModelFromCode(code);
        }

        /// <summary>
        /// 验证用户名和密码
        /// </summary>
        public int ValidateLogin(string userName, string encPassword)
        {
            return dal.ValidateLogin(userName, encPassword);
        }

        public bool UpdateLoginInfo(int fitemId, DateTime flastLoginTime)
        {
            return dal.UpdateLoginInfo(fitemId, flastLoginTime);
        }

        public bool UpdateisLock(int fitemId, int fisLock)
        {
            return dal.UpdateisLock(fitemId, fisLock);
        }

        public bool ResetPwd(int id, string pwd)
        {
            Model.Person model = GetModel(id);
            model.Pwd = pwd;
            return Update(model);
        }

        public bool CanViewMenu(int PersonID, string menuid)
        {
            return dal.CanViewMenu(PersonID, menuid);
        }

        public List<Model.Regions> GetRegions(int PersonID)
        {
            return dal.GetRegions(PersonID);
        }

        public bool AddRegions(int PersonID, int RegionID)
        {
            return dal.AddRegions(PersonID, RegionID);
        }

        public bool ClearAllRegions(int PersonID)
        {
            return dal.ClearAllRegions(PersonID);
        }

        public List<Model.Role> GetRoles(int PersonID)
        {
            return dal.GetRoles(PersonID);
        }

        public bool ClearAllRoles(int PersonID)
        {
            return dal.ClearAllRoles(PersonID);
        }

        public bool AddRoles(int PersonID, int RoleId)
        {
            return dal.AddRoles(PersonID, RoleId);
        }

        /// <summary>
        /// 判断用户是否有某个菜单的某种权限
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="operationName">权限名称：详细/创建/修改/删除</param>
        /// <returns></returns>
        public bool HavePermission(string account, string menuName, string operationName)
        {
            return dal.HavePermission(account, menuName, operationName);
        }

        #endregion  ExtensionMethod
    }
}