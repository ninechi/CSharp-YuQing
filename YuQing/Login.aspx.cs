using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YuQing
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = SM.YuQing.Library.PageValidate.InputText(Request.Form["Code"].Trim(), 30);
            string password = SM.YuQing.Library.PageValidate.InputText(Request.Form["Pwd"].Trim(), 30);

            SM.YuQing.BLL.Log.Add("登录", userName + " 尝试登录", 0, 0, Request.UserHostAddress);

            //验证登录信息，如果验证通过则返回当前用户对象的安全上下文信息
            SM.YuQing.Accounts.AccountsPrincipal newUser = SM.YuQing.Accounts.AccountsPrincipal.ValidateLogin(userName, password);
            if (newUser == null)//记录登录次数
            {
                if ((Session["PassErrorCountAdmin"] != null) && (Session["PassErrorCountAdmin"].ToString() != ""))
                {
                    int PassErroeCount = Convert.ToInt32(Session["PassErrorCountAdmin"]);
                    Session["PassErrorCountAdmin"] = PassErroeCount + 1;
                }
                else
                {
                    Session["PassErrorCountAdmin"] = 1;
                }
                lblMsg.Text = "用户名或密码错误！";
                return;
            }
            else
            {
                SM.YuQing.BLL.Person userBLL = new SM.YuQing.BLL.Person();

                SM.YuQing.Model.Person currentUser = userBLL.GetModel(((SM.YuQing.Accounts.SiteIdentity)newUser.Identity).FID);
                Context.User = newUser;

                if (currentUser.IsLock == 1)
                {
                    lblMsg.Text = "您的用户名已被管理锁定！";
                    return;
                }
                FormsAuthentication.SetAuthCookie(userName, false);
                //登录成功日志
                string clientip = Request.UserHostAddress;
                SM.YuQing.BLL.Log.Add("登录", currentUser.Code + " 登录成功", 0, 0, clientip);
                userBLL.UpdateLoginInfo(currentUser.ID, DateTime.Now);
                Session["UserInfo"] = currentUser;
                if (Session["returnPage"] != null)
                {
                    string returnpage = Session["returnPage"].ToString();
                    Session["returnPage"] = null;
                    Response.Redirect(returnpage);
                }
                else
                {
                    if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    {
                        Response.Redirect("Admin/Index.aspx");
                    }
                    else
                    {
                        Response.Redirect(Request.QueryString["ReturnUrl"].ToString());
                    }
                }
            }
        }
    }
}