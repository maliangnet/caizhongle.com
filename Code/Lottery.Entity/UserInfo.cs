using System;
using MongoDB.Attributes;
namespace Lottery.Entity
{
    public class UserInfo
    {
        [MongoId]
        [MongoAlias("id")]
        public string ID { set; get; }
        [MongoAlias("name")]
        public string Name { set; get; }
        [MongoAlias("email")]
        public string Email { set; get; }
    }
}
