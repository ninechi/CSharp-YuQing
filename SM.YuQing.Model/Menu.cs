using System;
using System.Collections.Generic;
namespace SM.YuQing.Model
{
    /// <summary>
    /// Menu:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Menu
    {
        public Menu()
        { }
        #region Model
        private int _id;
        private string _name;
        private int _parentid;
        private string _url;
        private string _icons;
        private int _sort;
        private DateTime _createtime;
        private string _createperson;
        private DateTime _updatetime;
        private string _updateperson;
        private List<Menu> _children;
        private string _operation;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Icons
        {
            set { _icons = value; }
            get { return _icons; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatePerson
        {
            set { _createperson = value; }
            get { return _createperson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdatePerson
        {
            set { _updateperson = value; }
            get { return _updateperson; }
        }

        public List<Menu> children
        {
            set { _children = value; }
            get { return _children; }
        }

        public string Operation
        {
            set { _operation = value; }
            get { return _operation; }
        }
        #endregion Model

    }
}