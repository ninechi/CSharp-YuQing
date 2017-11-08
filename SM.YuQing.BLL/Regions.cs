using System;
using System.Data;
using System.Collections.Generic;
using SM.YuQing.Model;
namespace SM.YuQing.BLL
{
    /// <summary>
    /// Regions
    /// </summary>
    public partial class Regions
    {
        private readonly SM.YuQing.DAL.Regions dal = new SM.YuQing.DAL.Regions();
        public Regions()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        public bool ExistRegion(int RegionID, int PersonID)
        {
            return dal.ExistRegion(RegionID, PersonID);
        }

        public bool ExistRegion(string RegionName, string Mall, string Keyword)
        {
            return dal.ExistRegion(RegionName, Mall, Keyword);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SM.YuQing.Model.Regions model)
        {
            return dal.Add(model);
        }

        public bool AddPersonRegion(int PersonID, int RegionID)
        {
            return dal.AddPersonRegion(PersonID, RegionID);
        }

        public bool DeletePersonRegion(int PersonID, int RegionID)
        {
            return dal.DeletePersonRegion(PersonID, RegionID);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SM.YuQing.Model.Regions model)
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
        public SM.YuQing.Model.Regions GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        public string GetRegionIdByPersonID(string PersonID)
        {

            return dal.GetRegionIdByPersonID(PersonID);
        }

        public int GetMaxRegionID(string CreatePerson)
        {
            return dal.GetMaxRegionID(CreatePerson);
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
        public List<SM.YuQing.Model.Regions> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SM.YuQing.Model.Regions> DataTableToList(DataTable dt)
        {
            List<SM.YuQing.Model.Regions> modelList = new List<SM.YuQing.Model.Regions>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SM.YuQing.Model.Regions model;
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