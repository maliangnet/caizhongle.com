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
            if (userInfo == null) return Json( new MessageInfo { Success = false, Message = "信息错误." });
            if (string.IsNullOrEmpty(userInfo.Email)) return Json( new MessageInfo { Success = false, Message = "请输入邮箱." });
            if (string.IsNullOrEmpty(userInfo.Password)) return Json(new MessageInfo { Success = false, Message = "请输入密码." });
            userInfo = Lottery.DatabaseProvider.Instance().GetUser(userInfo.Email, MaLiang.Common.Security.Security.MD5(userInfo.Password));
            if (userInfo == null) return Json(new MessageInfo { Success = false, Message = "邮箱或密码错误." });
            return Json(new MessageInfo { Success = true, Message = "登录成功." });
        }

        //新建用户
        [HttpPost]
        public JsonResult UserNew(UserInfo userInfo)
        {
            if (userInfo == null) return Json(new MessageInfo { Success = false, Message = "信息错误." });
            if (string.IsNullOrEmpty(userInfo.Name)) return Json(new MessageInfo { Success = false, Message = "请输入邮箱." });
            if (string.IsNullOrEmpty(userInfo.Email)) return Json(new MessageInfo { Success = false, Message = "请输入邮箱." });
            if (string.IsNullOrEmpty(userInfo.Password)) return Json(new MessageInfo { Success = false, Message = "请输入密码." });
            UserInfo userInfoJudge = Lottery.DatabaseProvider.Instance().GetUserByEmail(userInfo.Email);
            if (userInfoJudge != null) return Json(new MessageInfo { Success = false, Message = "邮箱已被专用,请换个试试." });
            userInfo.ID = Guid.NewGuid().ToString();
            userInfo.Password = MaLiang.Common.Security.Security.MD5(userInfo.Password);
            Lottery.DatabaseProvider.Instance().InsertUser(userInfo);
            userInfo.Password = "";
            return Json(new MessageInfo { Success = true, Message = "添加成功.",Model=userInfo });
        }

        //编辑用户
        [HttpPost]
        public JsonResult UserEdit(UserInfo userInfo)
        {
            if (userInfo == null) return Json(new MessageInfo { Success = false, Message = "信息错误." });
            if (string.IsNullOrEmpty(userInfo.ID)) return Json(new MessageInfo { Success = false, Message = "用户编号错误." });
            UserInfo userInfoEdit = Lottery.DatabaseProvider.Instance().GetUserByID(userInfo.ID);
            if (userInfoEdit == null) return Json(new MessageInfo { Success = false, Message = "用户不存在" });
            if (string.IsNullOrEmpty(userInfo.Name)) return Json(new MessageInfo { Success = false, Message = "请输入用户名." });
            if (string.IsNullOrEmpty(userInfo.Email)) return Json(new MessageInfo { Success = false, Message = "请输入邮箱." });
            userInfoEdit.Name = userInfo.Name;
            userInfoEdit.Email = userInfo.Email;
            Lottery.DatabaseProvider.Instance().UpdateUser(userInfoEdit);
            return Json(new MessageInfo { Success = true, Message = "修改成功.",Model=userInfoEdit });
        }

    }
}
