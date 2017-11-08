using System;
namespace SM.YuQing.Model
{
    /// <summary>
    /// Regions:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Regions : IEquatable<Regions>
    {
        public Regions()
        { }

        #region Model
        private int _id;
        private string _region;
        private string _mall;
        private string _keyword;
        private string _description;
        private DateTime _createtime;
        private string _createperson;
        private DateTime _updatetime;
        private string _updateperson;
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
        public string Region
        {
            set { _region = value; }
            get { return _region; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Mall
        {
            set { _mall = value; }
            get { return _mall; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Keyword
        {
            set { _keyword = value; }
            get { return _keyword; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
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
        #endregion Model

        public bool Equals(Regions other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return ID.Equals(other.ID) && Region.Equals(other.Region);
        }
    }
}