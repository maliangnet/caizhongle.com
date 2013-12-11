using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lottery.Web.Controllers
{
    public class WebController : Controller
    {
        //
        // GET: /Web/

        public ActionResult Index()
        {
            string id = Guid.NewGuid().ToString();
            ViewBag.id = id;
            Lottery.DatabaseProvider.Instance().InsertUser(new Lottery.Entity.UserInfo { ID = id, Name = "Name", Email = "Email" });
            return View();
        }

    }
}
