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

        public string Password { set; get; }

        public string Phone { set; get; }

        public string RegisterIP { set; get; }

        public int? GroupID { set; get; }

        public string Photo { set; get; }

        public int? Sex { set; get; }

        public int? Age { set; get; }

        public DateTime? Date { set; get; }

        public int? Status { set; get; }

        //附加属性
        [MongoIgnore]
        public string EmailEqual { set; get; }
        [MongoIgnore]
        public string NameEqual { set; get; }
    }
}
