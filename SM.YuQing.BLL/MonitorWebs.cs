using System;
using System.Data;
using System.Collections.Generic;
using SM.YuQing.Model;
namespace SM.YuQing.BLL
{
    /// <summary>
    /// MonitorWebs
    /// </summary>
    public partial class MonitorWebs
    {
        private readonly SM.YuQing.DAL.MonitorWebs dal = new SM.YuQing.DAL.MonitorWebs();
        public MonitorWebs()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        public bool ExistWeb(int RegionID, string Url, string Status)
        {
            return dal.ExistWeb(RegionID, Url, Status);
        }

        public bool ExistWeb(int RegionID, string Url, string Name, string Status)
        {
            return dal.ExistWeb(RegionID, Url, Name, Status);
        }

        public bool ExistRegion(int RegionID)
        {
            return dal.ExistRegion(RegionID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SM.YuQing.Model.MonitorWebs model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SM.YuQing.Model.MonitorWebs model)
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
        public SM.YuQing.Model.MonitorWebs GetModel(int ID)
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
        public List<SM.YuQing.Model.MonitorWebs> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SM.YuQing.Model.MonitorWebs> DataTableToList(DataTable dt)
        {
            List<SM.YuQing.Model.MonitorWebs> modelList = new List<SM.YuQing.Model.MonitorWebs>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SM.YuQing.Model.MonitorWebs model;
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

        #endregion  ExtensionMethod
    }
}