using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lottery.Web.Controllers
{
    public class WebController : Controller
    {
        //首页
        public ActionResult Index()
        {
            for (int i = 0; i < 100; i++)
            {
                string id = Guid.NewGuid().ToString();
                ViewBag.id = id;
                char name = (char)new Random().Next(65, 112);
                string email = new Random().Next(i, 1000000).ToString() + "@caizhongle.com";
                Lottery.DatabaseProvider.Instance().InsertUser(new Lottery.Entity.UserInfo { ID = id, Name = name.ToString(), Email = email, Password = MaLiang.Common.Security.Security.MD5(id) });
            }
            return View();
        }

    }
}
