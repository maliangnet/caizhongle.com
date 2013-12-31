using System;
using MongoDB.Attributes;
namespace Lottery.Entity
{
    public class VideoCategoryInfo
    {
        [MongoId]
        public string ID { set; get; }

        public string Name { set; get; }

        public string PID { set; get; }

        public int Level { set; get; }

        public string Memo { set; get; }
        
        public DateTime? Date { set; get; }

        public int? Status { set; get; }

        /// <summary>
        /// 父级是否变化
        /// </summary>
        [MongoIgnore]
        public bool Move { get;set; }
    }
}
