using System;
using System.Web.Mvc;
using Lottery.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

namespace Lottery.Web.Controllers
{
    public partial class POSTController : Controller
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
            if (string.IsNullOrEmpty(userInfo.Name)) return Json(new MessageInfo { Success = false, Message = "请输入名称." });
            if (string.IsNullOrEmpty(userInfo.Email)) return Json(new MessageInfo { Success = false, Message = "请输入邮箱." });
            if (string.IsNullOrEmpty(userInfo.Password)) return Json(new MessageInfo { Success = false, Message = "请输入密码." });
            UserInfo userInfoJudge = Lottery.DatabaseProvider.Instance().GetUserByEmail(userInfo.Email);
            if (userInfoJudge != null) return Json(new MessageInfo { Success = false, Message = "邮箱已被专用,请换个试试." });
            userInfo.ID = Guid.NewGuid().ToString();
            userInfo.Password = MaLiang.Common.Security.Security.MD5(userInfo.Password);
            userInfo.Date = DateTime.Now;
            userInfo.RegisterIP = MaLiang.Web.Utils.GetIP();
            userInfo.Status = Convert.ToInt16(EnumInfo.Status.Start);
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
            IList<UserInfo> userInfos = Lottery.DatabaseProvider.Instance().GetUser(new UserInfo { NameEqual = userInfo.Name }, null);
            if (userInfos != null && userInfos.Count > 1) return Json(new MessageInfo { Success = false, Message = "此用户名称已被使用,请换个试试." });
            userInfos = Lottery.DatabaseProvider.Instance().GetUser(new UserInfo { EmailEqual = userInfo.Email }, null);
            if (userInfos != null && userInfos.Count > 1) return Json(new MessageInfo { Success = false, Message = "此邮箱已被使用,请换个试试." });
            userInfoEdit.Name = userInfo.Name;
            userInfoEdit.Email = userInfo.Email;
            Lottery.DatabaseProvider.Instance().UpdateUser(userInfoEdit);
            return Json(new MessageInfo { Success = true, Message = "修改成功.",Model=userInfoEdit });
        }

        //新建视频
        [HttpPost]
        public JsonResult VideoNew(VideoInfo videoInfo)
        {
            if (videoInfo == null) return Json(new MessageInfo { Success = false, Message = "信息错误." });
            if (string.IsNullOrEmpty(videoInfo.Name)) return Json(new MessageInfo { Success = false, Message = "请输入名称." });
            if (string.IsNullOrEmpty(videoInfo.VideoCategoryID)) return Json(new MessageInfo { Success = false, Message = "请选择视频分类." });
            VideoCategoryInfo videoCategoryInfo = Lottery.DatabaseProvider.Instance().GetVideoCategoryByID(videoInfo.VideoCategoryID);
            if (videoCategoryInfo == null) return Json(new MessageInfo { Success = false, Message = "视频分类不存在." });
            VideoCategoryInfo videoCategoryInfoChild = Lottery.DatabaseProvider.Instance().GetVideoCategoryByPID(videoInfo.VideoCategoryID);
            if (videoCategoryInfoChild != null) return Json(new MessageInfo { Success = false, Message = "视频分类下有子分类,请选择子分类." });
            if (string.IsNullOrEmpty(videoInfo.File)) return Json(new MessageInfo { Success = false, Message = "请输入文件名." });
            VideoInfo videoInfoJudge = Lottery.DatabaseProvider.Instance().GetVideoByNameAndCategoryID(videoInfo.Name,videoInfo.VideoCategoryID);
            if (videoInfoJudge != null) return Json(new MessageInfo { Success = false, Message = "名称已被专用,请换个试试." });
            videoInfo.ID = Guid.NewGuid().ToString();
            videoInfo.Date = DateTime.Now;
            videoInfo.Status = Convert.ToInt16(EnumInfo.Status.Start);
            videoInfo.VideoCategoryName = videoCategoryInfo.Name;
            Lottery.DatabaseProvider.Instance().InsertVideo(videoInfo);
            return Json(new MessageInfo { Success = true, Message = "添加成功.", Model = videoInfo });
        }

        //编辑视频
        [HttpPost]
        public JsonResult VideoEdit(VideoInfo videoInfo)
        {
            if (videoInfo == null) return Json(new MessageInfo { Success = false, Message = "信息错误." });
            if (string.IsNullOrEmpty(videoInfo.ID)) return Json(new MessageInfo { Success = false, Message = "视频编号错误." });
            VideoInfo videoInfoEdit = Lottery.DatabaseProvider.Instance().GetVideoByID(videoInfo.ID);
            if (videoInfoEdit == null) return Json(new MessageInfo { Success = false, Message = "视频不存在" });
            if (string.IsNullOrEmpty(videoInfo.Name)) return Json(new MessageInfo { Success = false, Message = "请输入名称." });
            if (string.IsNullOrEmpty(videoInfo.VideoCategoryID)) return Json(new MessageInfo { Success = false, Message = "请选择视频分类." });
            VideoCategoryInfo videoCategoryInfo = Lottery.DatabaseProvider.Instance().GetVideoCategoryByID(videoInfo.VideoCategoryID);
            if (videoCategoryInfo == null) return Json(new MessageInfo { Success = false, Message = "视频分类不存在." });
            VideoCategoryInfo videoCategoryInfoChild = Lottery.DatabaseProvider.Instance().GetVideoCategoryByPID(videoInfo.VideoCategoryID);
            if (videoCategoryInfoChild != null) return Json(new MessageInfo { Success = false, Message = "视频分类下有子分类,请选择子分类." });
            IList<VideoInfo> videoInfos = Lottery.DatabaseProvider.Instance().GetVideo(new VideoInfo { NameEqual = videoInfo.Name, VideoCategoryID = videoInfo.VideoCategoryID }, null);
            if (videoInfos != null && videoInfos.Count > 1) return Json(new MessageInfo { Success = false, Message = "此名称已被使用,请换个试试." });
            if (string.IsNullOrEmpty(videoInfo.File)) return Json(new MessageInfo { Success = false, Message = "请输入文件名." });
            videoInfoEdit.Name = videoInfo.Name;
            videoInfoEdit.File = videoInfo.File;
            videoInfoEdit.VideoCategoryName = videoCategoryInfo.Name;
            Lottery.DatabaseProvider.Instance().UpdateVideo(videoInfoEdit);
            return Json(new MessageInfo { Success = true, Message = "修改成功.", Model = videoInfoEdit });
        }

        //新建视频分类
        [HttpPost]
        public JsonResult VideoCategoryNew(VideoCategoryInfo videoCategoryInfo)
        {
            if (videoCategoryInfo == null) return Json(new MessageInfo { Success = false, Message = "信息错误." });
            if (string.IsNullOrEmpty(videoCategoryInfo.Name)) return Json(new MessageInfo { Success = false, Message = "请输入名称." });
            if (string.IsNullOrEmpty(videoCategoryInfo.PID)) return Json(new MessageInfo { Success = false, Message = "请选择上级." });
            VideoCategoryInfo videoCategoryInfoJudge = Lottery.DatabaseProvider.Instance().GetVideoCategoryByNameAndPID(videoCategoryInfo.Name, videoCategoryInfo.PID);
            if (videoCategoryInfoJudge != null) return Json(new MessageInfo { Success = false, Message = "名称已被专用,请换个试试." });
            videoCategoryInfo.ID = Guid.NewGuid().ToString();
            videoCategoryInfo.Level = 1;
            if (videoCategoryInfo.PID != "0")
            {
                VideoCategoryInfo videoCategoryInfoParent = Lottery.DatabaseProvider.Instance().GetVideoCategoryByID(videoCategoryInfo.PID);
                videoCategoryInfo.Level = videoCategoryInfoParent == null ? videoCategoryInfo.Level : videoCategoryInfoParent.Level + 1;
            }
            videoCategoryInfo.Date = DateTime.Now;
            videoCategoryInfo.Status = Convert.ToInt16(EnumInfo.Status.Start);
            Lottery.DatabaseProvider.Instance().InsertVideoCategory(videoCategoryInfo);
            return Json(new MessageInfo { Success = true, Message = "添加成功.", Model = videoCategoryInfo });
        }

        //编辑视频分类
        [HttpPost]
        public JsonResult VideoCategoryEdit(VideoCategoryInfo videoCategoryInfo)
        {
            if (videoCategoryInfo == null) return Json(new MessageInfo { Success = false, Message = "信息错误." });
            if (string.IsNullOrEmpty(videoCategoryInfo.ID)) return Json(new MessageInfo { Success = false, Message = "视频分类编号错误." });
            VideoCategoryInfo videoCategoryInfoEdit = Lottery.DatabaseProvider.Instance().GetVideoCategoryByID(videoCategoryInfo.ID);
            if (videoCategoryInfoEdit == null) return Json(new MessageInfo { Success = false, Message = "视频分类不存在" });
            if (string.IsNullOrEmpty(videoCategoryInfo.Name)) return Json(new MessageInfo { Success = false, Message = "请输入名称." });
            if (string.IsNullOrEmpty(videoCategoryInfo.PID)) return Json(new MessageInfo { Success = false, Message = "请选择上级编号." });
            if (videoCategoryInfoEdit.PID != videoCategoryInfo.PID)
            {
                if (Lottery.DatabaseProvider.Instance().GetVideoCategoryByPID(videoCategoryInfo.ID) != null)
                    return Json(new MessageInfo { Success = false, Message = "此分类下有子级,不能修改." });
                else
                {
                    VideoCategoryInfo videoCategoryInfoParent = Lottery.DatabaseProvider.Instance().GetVideoCategoryByID(videoCategoryInfo.PID);
                    videoCategoryInfoEdit.Level = videoCategoryInfoParent == null ? 1 : videoCategoryInfoParent.Level + 1;
                }
            }            
            videoCategoryInfoEdit.Name = videoCategoryInfo.Name;
            videoCategoryInfoEdit.PID = videoCategoryInfo.PID;
            videoCategoryInfoEdit.Memo = videoCategoryInfo.Memo;
            Lottery.DatabaseProvider.Instance().UpdateVideoCategory(videoCategoryInfoEdit);
            return Json(new MessageInfo { Success = true, Message = "修改成功.", Model = videoCategoryInfoEdit });
        }

    }

    // POST工具类
    public partial class POSTController : Controller
    {
        [HttpPost]
        public JsonResult UploadImage(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0) return Json(new MessageInfo { Success = false, Message = "请添加文件." });
            string extension = Path.GetExtension(file.FileName);
            if (!Regex.IsMatch(extension, "\\.gif|jpg|jpeg|bmp|png$",RegexOptions.IgnoreCase)) return Json(new MessageInfo { Success = false, Message = "图片格式必须是:gif、jpg、jpeg、bmp或png." });
            string name = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string directory = "/Upload/Video";
            string directoryPhysical =Server.MapPath(directory);
            string path = Path.Combine(directoryPhysical, name + extension);
            file.SaveAs(path);
            string thumNail1 = name + "_10" + extension;
            string thumNail2 = name + "_20" + extension;
            MaLiang.Common.Image.ThumNail.MakeThumNail(path, directoryPhysical +"/" +thumNail1, 70, 39);
            MaLiang.Common.Image.ThumNail.MakeThumNail(path, directoryPhysical +"/" +thumNail2, 200, 110);
            Lottery.Entity.FileInfo fileInfo = new Entity.FileInfo { Name = name + extension, Directory = directory, Path = "/Upload/Video/" + thumNail1, ThumNail1 = thumNail1, ThumNail2 = thumNail2 };
            return Json(new MessageInfo { Success = true, Message = "添加成功", Model = fileInfo });
        }

        [HttpPost]
        public JsonResult DeleteImage(string name)
        {
            if (string.IsNullOrEmpty(name)) return Json(new MessageInfo { Success = false, Message = "文件名错误." });
            string directory = "/Upload/Video/"; string directoryPhysical = Server.MapPath(directory);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(directoryPhysical + name);
            string extension = Path.GetExtension(name);
            if (fileInfo.Exists == false) return Json(new MessageInfo { Success = false, Message = "文件不存在." });
            fileInfo.Delete();
            fileInfo = new System.IO.FileInfo(directoryPhysical + "/" + name.Replace(extension,"") + "_10" + extension);
            if (fileInfo.Exists) fileInfo.Delete();
            fileInfo = new System.IO.FileInfo(directoryPhysical + "/" + name.Replace(extension, "") + "_20" + extension);
            if (fileInfo.Exists) fileInfo.Delete();
            return Json(new MessageInfo { Success = true, Message = "操作成功"});
        }
    }
}
