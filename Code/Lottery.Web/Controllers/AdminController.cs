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
        public ActionResult UserList(UserInfo userInfo, PageInfo pageInfo)
        {
            if (pageInfo == null || pageInfo.PageIndex==null || pageInfo.PageSize==null) pageInfo = new PageInfo { PageIndex = 1, PageSize = 10 };
            IList<UserInfo> userInfos = Lottery.DatabaseProvider.Instance().GetUser(userInfo, null).ToPagedList(pageInfo.PageIndex.Value, pageInfo.PageSize.Value);
            return View(userInfos);
        }

        //用户
        public ActionResult User(string ID)
        {
            UserInfo uesrInfo = Lottery.DatabaseProvider.Instance().GetUser(ID);
            return View(uesrInfo);
        }

    }
}
