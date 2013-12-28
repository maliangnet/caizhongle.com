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
        /// 添加视频分类
        /// </summary>
        /// <param name="videoCategoryInfo">视频分类实体</param>
        public void InsertVideoCategory(VideoCategoryInfo videoCategoryInfo)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                mongoDB.GetCollection<VideoCategoryInfo>().Insert(videoCategoryInfo);
            }
        }

        /// <summary>
        /// 删除视频分类
        /// </summary>
        /// <param name="ID">视频分类编号</param>
        public void DeleteVideoCategory(string ID)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                var collection = mongoDB.GetCollection<VideoCategoryInfo>();
                collection.Remove(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 修改视频分类
        /// </summary>
        /// <param name="videoCategoryInfo">视频分类编号</param>
        public void UpdateVideoCategory(VideoCategoryInfo videoCategoryInfo)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                var collection = mongoDB.GetCollection<VideoCategoryInfo>();
                collection.Update(videoCategoryInfo, u => u.ID == videoCategoryInfo.ID);
            }
        }

        /// <summary>
        /// 获取视频分类
        /// </summary>
        /// <param name="ID">视频分类编号</param>
        /// <returns></returns>
        public VideoCategoryInfo GetVideoCategoryByID(string ID)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                return mongoDB.GetCollection<VideoCategoryInfo>().FindOne(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 获取视频分类
        /// </summary>
        /// <param name="name">视频分类名称</param>
        /// <returns></returns>
        public VideoCategoryInfo GetVideoCategoryByName(string name)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                return mongoDB.GetCollection<VideoCategoryInfo>().FindOne(u => u.Name == name);
            }
        }

        /// <summary>
        /// 获取视频分类
        /// </summary>
        /// <param name="name">视频分类名称</param>
        /// <param name="name">视频分类父级编号</param>
        /// <returns></returns>
        public VideoCategoryInfo GetVideoCategoryByNameAndPID(string name,string PID)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                return mongoDB.GetCollection<VideoCategoryInfo>().FindOne(u => u.Name == name && u.PID == PID);
            }
        }

        /// <summary>
        /// 获取视频分类
        /// </summary>
        /// <param name="name">视频分类父级编号</param>
        /// <returns></returns>
        public VideoCategoryInfo GetVideoCategoryByPID(string PID)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                return mongoDB.GetCollection<VideoCategoryInfo>().FindOne(u => u.PID == PID);
            }
        }

        /// <summary>
        /// 获取视频分类
        /// </summary>
        /// <param name="videoCategoryInfo">视频分类实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        public IList<VideoCategoryInfo> GetVideoCategory(VideoCategoryInfo videoCategoryInfo)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                var collection = mongoDB.GetCollection<VideoCategoryInfo>();
                var query = from user in collection.Linq() select user;
                if (videoCategoryInfo != null && !string.IsNullOrEmpty(videoCategoryInfo.ID)) query = query.Where(u => u.ID.Contains(videoCategoryInfo.ID));
                if (videoCategoryInfo != null && !string.IsNullOrEmpty(videoCategoryInfo.Name)) query = query.Where(u => u.Name.Contains(videoCategoryInfo.Name));
                return query.OrderByDescending(u => u.Date).ToList<VideoCategoryInfo>();
            }
        }
    }
}
