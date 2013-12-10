using System;
using MongoDB.Attributes;
namespace Lottery.Entity
{
    public class UserInfo
    {
        [MongoId]
        public string ID { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
    }
}
