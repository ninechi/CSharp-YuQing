using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuQing.Admin.Monitor
{
    public class GetRegionIdHelper
    {
        public string GetRegionID()
        {
            SM.YuQing.Accounts.SiteIdentity identity = (SM.YuQing.Accounts.SiteIdentity)(new SM.YuQing.Accounts.AccountsPrincipal(HttpContext.Current.User.Identity.Name)).Identity;
            string personid = identity.FID.ToString();
            SM.YuQing.BLL.Regions regbll = new SM.YuQing.BLL.Regions();
            return regbll.GetRegionIdByPersonID(personid);
        }
    }
}