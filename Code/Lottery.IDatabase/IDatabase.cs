using System;
using Lottery.Entity;
using System.Collections.Generic;

namespace Lottery
{
    public partial interface IDatabase
    {
        #region 用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userInfo">用户实体</param>
        void InsertUser(UserInfo userInfo);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ID">用户编号</param>
        void DeleteUser(string ID);
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userInfo">用户编号</param>
        void UpdateUser(UserInfo userInfo);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="ID">用户编号</param>
        /// <returns></returns>
        UserInfo GetUserByID(string ID);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        UserInfo GetUserByEmail(string email);
         /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UserInfo GetUser(string email, string password);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userInfo">用户实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        IList<UserInfo> GetUser(UserInfo userInfo, PageInfo pageInfo);
        #endregion

        #region 视频
        /// <summary>
        /// 添加视频
        /// </summary>
        /// <param name="videoInfo">视频实体</param>
        void InsertVideo(VideoInfo videoInfo);
         /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="ID">视频编号</param>
        void DeleteVideo(string ID);
        /// <summary>
        /// 修改视频
        /// </summary>
        /// <param name="videoInfo">视频编号</param>
        void UpdateVideo(VideoInfo videoInfo);
        /// <summary>
        /// 获取视频
        /// </summary>
        /// <param name="ID">视频编号</param>
        /// <returns></returns>
        VideoInfo GetVideoByID(string ID);
         /// <summary>
        /// 获取视频
        /// </summary>
        /// <param name="name">视频名称</param>
        /// <returns></returns>
        VideoInfo GetVideoByName(string name);
        /// <summary>
        /// 获取视频
        /// </summary>
        /// <param name="videoInfo">视频实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        IList<VideoInfo> GetVideo(VideoInfo videoInfo, PageInfo pageInfo);
        #endregion

        #region 视频分类
        /// <summary>
        /// 添加视频分类
        /// </summary>
        /// <param name="videoCategoryInfo">视频分类实体</param>
        void InsertVideoCategory(VideoCategoryInfo videoCategoryInfo);
        /// <summary>
        /// 删除视频分类
        /// </summary>
        /// <param name="ID">视频分类编号</param>
        void DeleteVideoCategory(string ID);
        /// <summary>
        /// 修改视频分类
        /// </summary>
        /// <param name="videoCategoryInfo">视频分类编号</param>
        void UpdateVideoCategory(VideoCategoryInfo videoCategoryInfo);
        /// <summary>
        /// 获取视频分类
        /// </summary>
        /// <param name="ID">视频分类编号</param>
        /// <returns></returns>
        VideoCategoryInfo GetVideoCategoryByID(string ID);
        /// <summary>
        /// 获取视频分类
        /// </summary>
        /// <param name="name">视频分类名称</param>
        /// <returns></returns>
        VideoCategoryInfo GetVideoCategoryByName(string name);
        /// <summary>
        /// 获取视频分类
        /// </summary>
        /// <param name="videoCategoryInfo">视频分类实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        IList<VideoCategoryInfo> GetVideoCategory(VideoCategoryInfo videoCategoryInfo, PageInfo pageInfo);
        #endregion
    }
}
