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
        /// 添加用户
        /// </summary>
        /// <param name="userInfo">用户实体</param>
        public void InsertUser(UserInfo userInfo)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                mongoDB.GetCollection<UserInfo>().Insert(userInfo);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userID">用户编号</param>
        public void DeleteUser(string userID)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                var collection = mongoDB.GetCollection<UserInfo>();
                collection.Remove(u => u.ID == userID);
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userInfo">用户编号</param>
        public void UpdateUser(UserInfo userInfo)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                var collection = mongoDB.GetCollection<UserInfo>();
                collection.Update(userInfo,u=>u.ID == userInfo.ID);
            }
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns></returns>
        public UserInfo GetUser(string userID)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                return mongoDB.GetCollection<UserInfo>().FindOne(u => u.ID == userID);
            }
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userInfo">用户实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        public IList<UserInfo> GetUser(UserInfo userInfo, PageInfo pageInfo)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                var collection = mongoDB.GetCollection<UserInfo>();
                var query = from user in collection.Linq() select user;
                if (userInfo != null && !string.IsNullOrEmpty(userInfo.ID))query = query.Where(u => u.ID.Contains(userInfo.ID));
                return query.OrderBy(u => u.ID).GetPagingList<UserInfo>(pageInfo);
            }
        }
    }
}
