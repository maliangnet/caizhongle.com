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
            if (userInfo == null) return Json( new MessageInfo { Success = false, Message = "信息错误" });
            if (userInfo.Email == null) return Json( new MessageInfo { Success = false, Message = "请输入邮箱." });
            if (userInfo.Password == null) return Json(new MessageInfo { Success = false, Message = "请输入密码." });
            userInfo = Lottery.DatabaseProvider.Instance().GetUser(userInfo.Email, MaLiang.Common.Security.Security.MD5(userInfo.Password));
            if (userInfo == null) return Json(new MessageInfo { Success = false, Message = "邮箱或密码错误." });
            return Json(new MessageInfo { Success = true, Message = "登录成功" });
        }

    }
}
