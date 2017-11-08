using System;
namespace SM.YuQing.Model
{
    /// <summary>
    /// Person:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Person
    {
        public Person()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _code;
        private string _pwd;
        private int _islock;
        private DateTime _lastlogintime;
        private int _logintimes;
        private DateTime _createtime;
        private string _createperson;
        private DateTime _updatetime;
        private string _updateperson;
        private string _role;
        private string _status;
        private string _regions;
        private string _roleid;
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
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Pwd
        {
            set { _pwd = value; }
            get { return _pwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsLock
        {
            set { _islock = value; }
            get { return _islock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastLoginTime
        {
            set { _lastlogintime = value; }
            get { return _lastlogintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LoginTimes
        {
            set { _logintimes = value; }
            get { return _logintimes; }
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

        public string Role
        {
            set { _role = value; }
            get { return _role; }
        }

        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }

        public string Regions
        {
            set { _regions = value; }
            get { return _regions; }
        }

        public string RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        #endregion Model

    }
}