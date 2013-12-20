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
        /// <param name="ID">用户编号</param>
        /// <returns></returns>
        public UserInfo GetUserByID(string ID)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                return mongoDB.GetCollection<UserInfo>().FindOne(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        public UserInfo GetUserByEmail(string email)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                return mongoDB.GetCollection<UserInfo>().FindOne(u => u.Email == email);
            }
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public UserInfo GetUser(string email,string password)
        {
            using (MongoDB mongoDB = new MongoDB())
            {
                return mongoDB.GetCollection<UserInfo>().FindOne(u => u.Email == email && u.Password==password);
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
                if (userInfo != null && !string.IsNullOrEmpty(userInfo.ID)) query = query.Where(u => u.ID.Contains(userInfo.ID));
                if (userInfo != null && !string.IsNullOrEmpty(userInfo.Name)) query = query.Where(u => u.Name.Contains(userInfo.Name));
                if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email)) query = query.Where(u => u.Email.Contains(userInfo.Email));
                return query.OrderByDescending(u => u.Date).GetPagingList<UserInfo>(pageInfo);
            }
        }
    }
}
