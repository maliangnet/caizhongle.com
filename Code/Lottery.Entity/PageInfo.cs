using System;

namespace Lottery.Entity
{
    public class PageInfo
    {
        private int? pageIndex;
        public int? PageIndex { set { pageIndex = value; } get { return pageIndex==null?1:pageIndex; } }
        private int? pageSize;
        public int? PageSize { set { pageSize = value; } get { return pageSize == null ? 12 : pageSize; } }
        public int? TotalRecord { set; get; }

    }
}
