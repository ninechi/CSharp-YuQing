using System.Text;
using System.Security.Principal;
using System.Collections;
using System.Security.Cryptography;

namespace SM.YuQing.Accounts
{
    public class AccountsPrincipal : IPrincipal
    {
        protected IIdentity identity;
        protected ArrayList roleList;
        protected ArrayList permissionList;
        protected ArrayList permissionListid;
        // Methods
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userID">用户编号</param>
        public AccountsPrincipal(int userID)
        {
            this.identity = new SiteIdentity(userID);//返回用户的详细信息
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        public AccountsPrincipal(string code)
        {
            this.identity = new SiteIdentity(code);//返回用户的详细信息
        }
        /// <summary>
        /// 判断是否有该权限
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            return this.roleList.Contains(role);
        }
        /// <summary>
        /// 根据权限ID判断是否有权限
        /// </summary>
        /// <param name="permissionid"></param>
        /// <returns></returns>
        public bool HasPermissionID(int permissionid)
        {
            return this.permissionListid.Contains(permissionid);
        }
        /// <summary>
        /// 对密码进行加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPassword(string password)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(password);
            SHA1 sha = new SHA1CryptoServiceProvider();
            //return sha.ComputeHash(bytes);
            byte[] res = sha.ComputeHash(bytes);
            //char[] temp = new char[res.Length];
            //System.Array.Copy(res, temp, res.Length);
            //return new string(temp);
            string str = "";
            for (int i = 0; i < res.Length - 1; i++)
            {
                str += res[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
        /// <summary>
        /// 通过用户名和密码从新取得用户详细信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static AccountsPrincipal ValidateLogin(string userName, string password)
        {
            SM.YuQing.BLL.Person bll = new BLL.Person();
            string encPassword = EncryptPassword(password);
            int userID = bll.ValidateLogin(userName, encPassword);//返回用户内码
            if (userID > 0)
            {
                return new AccountsPrincipal(userID);//取得用户信息
            }
            return null;
        }

        // Properties
        public IIdentity Identity
        {
            get
            {
                return this.identity;
            }
            set
            {
                this.identity = value;
            }
        }
        public ArrayList Permissions
        {
            get
            {
                return this.permissionList;
            }
        }
        public ArrayList PermissionsID
        {
            get
            {
                return this.permissionListid;
            }
        }
        public ArrayList Roles
        {
            get
            {
                return this.roleList;
            }
        }
    }
}
