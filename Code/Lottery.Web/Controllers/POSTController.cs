using System;
using System.Web.Mvc;
using Lottery.Entity;

namespace Lottery.Web.Controllers
{
    public class POSTController : Controller
    {
        //登录
        [HttpPost]
        public JsonResult Login(UserInfo userInfo)
        {
            if (userInfo == null) return Json( new MessageInfo { Success = false, Message = "信息错误" });
            if (userInfo.Email == null) return Json( new MessageInfo { Success = false, Message = "请输入邮箱." });
            if (userInfo.Password == null) return Json(new MessageInfo { Success = false, Message = "请输入密码." });
            userInfo = Lottery.DatabaseProvider.Instance().GetUser(userInfo.Email, MaLiang.Common.Security.Security.MD5(userInfo.Password));
            if (userInfo == null) return Json(new MessageInfo { Success = false, Message = "邮箱或密码错误." });
            return Json(new MessageInfo { Success = true, Message = "登录成功" });
        }

        //编辑用户
        [HttpPost]
        public JsonResult UserEdit(UserInfo userInfo)
        {
            if (userInfo == null) return Json(new MessageInfo { Success = false, Message = "信息错误" });
            if (string.IsNullOrEmpty(userInfo.ID)) return Json(new MessageInfo { Success = false, Message = "用户编号错误" });
            UserInfo userInfoEdit = Lottery.DatabaseProvider.Instance().GetUser(userInfo.ID);
            if (userInfoEdit == null) return Json(new MessageInfo { Success = false, Message = "用户不存在" });
            if (string.IsNullOrEmpty(userInfo.Name)) return Json(new MessageInfo { Success = false, Message = "请输入用户名" });
            if (string.IsNullOrEmpty(userInfo.Email)) return Json(new MessageInfo { Success = false, Message = "请输入邮箱" });
            userInfoEdit.Name = userInfo.Name;
            userInfoEdit.Email = userInfo.Email;
            Lottery.DatabaseProvider.Instance().UpdateUser(userInfoEdit);
            return Json(new MessageInfo { Success = true, Message = "修改成功" });
        }

    }
}
