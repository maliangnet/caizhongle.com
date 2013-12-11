using System;
using System.Web.Mvc;
using Lottery.Entity;

namespace Lottery.Web.Controllers
{
    public class POSTController : Controller
    {
        //登录
        [HttpPost]
        public ActionResult Login(UserInfo userInfo)
        {
            MessageInfo messageInfo = new MessageInfo { Success = false, Message = "信息错误" };
            if (userInfo == null)messageInfo.Message="用户实体信息错误.";
            return Json(messageInfo);
        }

    }
}
