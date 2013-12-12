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
        /// <param name="userID">用户编号</param>
        void DeleteUser(string userID);
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
        UserInfo GetUser(string ID);
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
    }
}
