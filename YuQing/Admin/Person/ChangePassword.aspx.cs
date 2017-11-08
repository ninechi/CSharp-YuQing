using SM.YuQing.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YuQing.Admin.Person
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountsPrincipal user = new AccountsPrincipal(Context.User.Identity.Name);
            this.Name.Value = ((SiteIdentity)user.Identity).FName;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string oldPwd = this.OldPwd.Value.Trim();
            string newPwd = this.NewPwd.Value.Trim();
            string checkPwd = this.CheckPwd.Value.Trim();
            
            AccountsPrincipal user;
            SM.YuQing.Model.Person currentUser;
            SM.YuQing.BLL.Person userBLL = new SM.YuQing.BLL.Person();
            user = new AccountsPrincipal(Context.User.Identity.Name);
            if (Session["UserInfo"] == null)
            {
                return;
            }
            currentUser = (SM.YuQing.Model.Person)Session["UserInfo"];
            SM.YuQing.Accounts.AccountsPrincipal newUser = SM.YuQing.Accounts.AccountsPrincipal.ValidateLogin(currentUser.Code, oldPwd);
            if (newUser == null)
            {
                lblMsg.Text = "旧密码输入有误！";
                return;
            }
            if (userBLL.ResetPwd(currentUser.ID, AccountsPrincipal.EncryptPassword(newPwd)))
            {
                lblMsg.Text = "密码修改成功！";
                string clientip = Request.UserHostAddress;
                SM.YuQing.BLL.Log.Add("操作", currentUser.Code + " 修改密码", 0, 0, clientip);
            }
        }
    }
}