using System;
using System.Text;
using System.Security.Principal;
using System.Collections;
using System.Security.Cryptography;

namespace SM.YuQing.Accounts
{
    public class SiteIdentity : IIdentity
    {
        private int _fid;
        private string _fname;
        private string _fcode;
        //private string _fpermission;
        //private string _ftenantcode;
        private string _fpwd;
        //private string _fstoreno;

        public SiteIdentity(int currentUserID)
        {
            SM.YuQing.BLL.Person bll = new BLL.Person();
            SM.YuQing.Model.Person model = bll.GetModel(currentUserID);
            this._fid = model.ID;
            this._fcode = model.Code;
            this._fname = model.Name;
            //this._fpermission = model.FPermission;
            //this._ftenantcode = model.FTenantCode;
            this._fpwd = model.Pwd;
            //this._fstoreno = model.FStoreNo;
        }
        public SiteIdentity(string currentCode)
        {
            SM.YuQing.BLL.Person bll = new BLL.Person();
            SM.YuQing.Model.Person model = bll.GetModelFromCode(currentCode);
            this._fid = model.ID;
            this._fcode = model.Code;
            this._fname = model.Name;
            //this._fpermission = model.FPermission;
            //this._ftenantcode = model.FTenantCode;
            this._fpwd = model.Pwd;
            //this._fstoreno = model.FStoreNo;
        }
        public int TestPassword(string password)
        {
            string encPassword = AccountsPrincipal.EncryptPassword(password);
            SM.YuQing.BLL.Person bll = new BLL.Person();
            return bll.TestPassword(this.FID, encPassword);

        }

        public bool CanViewMenu(string menuid)
        {
            SM.YuQing.BLL.Person bll = new BLL.Person();
            return bll.CanViewMenu(this._fid, menuid);
        }

        public string AuthenticationType
        {
            get
            {
                return "Custom Authentication";
            }
            set
            {
            }
        }
        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }
        public string Name
        {
            get
            {
                return this.FCode;
            }
        }
        /// <summary>
        /// 用户内码
        /// </summary>
        public int FID
        {
            set { _fid = value; }
            get { return _fid; }
        }
        /// <summary>
        /// 用户名字
        /// </summary>
        public string FName
        {
            set { _fname = value; }
            get { return _fname; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string FCode
        {
            set { _fcode = value; }
            get { return _fcode; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string FPwd
        {
            set { _fpwd = value; }
            get { return _fpwd; }
        }
    }
}
