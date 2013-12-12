using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lottery.Entity;
using Webdiyer.WebControls.Mvc;

namespace Lottery.Web.Controllers
{
    public class AdminController : Controller
    {
        //登录页面
        public ActionResult Index()
        {
            return View();
        }

        //后台首页
        public ActionResult Home()
        {
            return View();
        }

        //用户列表
        public ActionResult UserList(UserInfo userInfo,int id=1)
        {
            IList<UserInfo> userInfos = Lottery.DatabaseProvider.Instance().GetUser(userInfo, null).ToPagedList(id, 10);
            return View(userInfos);
        }

    }
}
