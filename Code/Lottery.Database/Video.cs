using System;
using System.Linq;
using MongoDB;
using Lottery.Entity;
using System.Collections.Generic;
namespace Lottery.Database
{
    public partial class Database : Lottery.IDatabase
    {
        /// <summary>
        /// 添加视频
        /// </summary>
        /// <param name="videoInfo">视频实体</param>
        public void InsertVideo(VideoInfo videoInfo)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                mongoDB.GetCollection<VideoInfo>().Insert(videoInfo);
            }
        }

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="ID">视频编号</param>
        public void DeleteVideo(string ID)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                var collection = mongoDB.GetCollection<VideoInfo>();
                collection.Remove(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 修改视频
        /// </summary>
        /// <param name="videoInfo">视频编号</param>
        public void UpdateVideo(VideoInfo videoInfo)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                var collection = mongoDB.GetCollection<VideoInfo>();
                collection.Update(videoInfo, u => u.ID == videoInfo.ID);
            }
        }

        /// <summary>
        /// 获取视频
        /// </summary>
        /// <param name="ID">视频编号</param>
        /// <returns></returns>
        public VideoInfo GetVideoByID(string ID)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                return mongoDB.GetCollection<VideoInfo>().FindOne(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 获取视频
        /// </summary>
        /// <param name="name">视频名称</param>
        /// <returns></returns>
        public VideoInfo GetVideoByName(string name)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                return mongoDB.GetCollection<VideoInfo>().FindOne(u => u.Name == name);
            }
        }

        /// <summary>
        /// 获取视频
        /// </summary>
        /// <param name="videoInfo">视频实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        public IList<VideoInfo> GetVideo(VideoInfo videoInfo, PageInfo pageInfo)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                var collection = mongoDB.GetCollection<VideoInfo>();
                var query = from video in collection.Linq() select video;
                if (videoInfo != null && !string.IsNullOrEmpty(videoInfo.ID)) query = query.Where(u => u.ID.Contains(videoInfo.ID));
                if (videoInfo != null && !string.IsNullOrEmpty(videoInfo.Name)) query = query.Where(u => u.Name.Contains(videoInfo.Name));
                if (videoInfo != null && !string.IsNullOrEmpty(videoInfo.File)) query = query.Where(u => u.File.Contains(videoInfo.File));
                return query.OrderByDescending(u => u.Date).GetPagingList<VideoInfo>(pageInfo);
            }
        }
    }
}
