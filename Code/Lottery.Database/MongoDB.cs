using System;
using MongoDB;

namespace Lottery.Database
{
    public class MongoDB : IDisposable
    {
        private Mongo _mongo;
        private IMongoDatabase _db;

        /// <summary>
        /// 默认构造函数。
        /// 为了本程序方便使用，直接使用二个固定的参数。
        /// 采用MongoDb的默认连接字符串，连接Lottery数据库。
        /// </summary>
        public MongoDB():this("Server=127.0.0.1", "caizhongle")
        {
        }

        /// <summary>
        /// 构造函数。根据指定连接字符串和数据库名
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="dbName">数据库名，可为空，但必须在任何操作数据库之前要调用UseDb()方法</param>
        public MongoDB(string connectionString, string dbName)
        {
            if( string.IsNullOrEmpty(connectionString) )
                throw new ArgumentNullException("数据库连接不能为空.");
            _mongo = new Mongo(connectionString);
            // 立即连接 MongoDB
            _mongo.Connect();
            if( string.IsNullOrEmpty(dbName) == false )
                _db = _mongo.GetDatabase(dbName);
        }

        /// <summary>
        /// 切换到指定的数据库
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public IMongoDatabase SwitchDatabase(string dbName)
        {
            if (string.IsNullOrEmpty(dbName))
                throw new ArgumentNullException("数据库名称不能为空.");
            _db = _mongo.GetDatabase(dbName);
            return _db;
        }

        /// <summary>
        /// 获取当前连接的数据库
        /// </summary>
        public IMongoDatabase CurrentDb
        {
            get
            {
                if (_db == null)
                    throw new Exception("当前连接没有指定任何数据库。请在构造函数中指定数据库名或者调用SwitchDatabase()方法切换数据库。");
                return _db;
            }
        }

        /// <summary>
        /// 获取当前连接数据库的指定集合【依据类型】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>() where T : class
        {
            return this.CurrentDb.GetCollection<T>();
        }

        /// <summary>
        /// 获取当前连接数据库的指定集合【根据指定名称】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">集合名称</param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>(string name) where T : class
        {
            return this.CurrentDb.GetCollection<T>(name);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (_mongo != null)
            {
                _mongo.Dispose();
                _mongo = null;
            }
            
        } 
    }
}
