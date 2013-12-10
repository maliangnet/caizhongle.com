using System;
using System.Linq;
using System.Collections.Generic;
using Lottery.Entity;

namespace Lottery.Database
{
    public static class PagingExtension
    {
        public static List<T> GetPagingList<T>(this IQueryable<T> query, PageInfo pageInfo)
        {
            if (query == null) throw new ArgumentNullException("查询条件错误.");
            if (pageInfo == null)  return query.ToList();
            pageInfo.TotalRecord = query.Count();
            List<T> list = query.Skip(pageInfo.PageIndex * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
            if (list == null || list.Count == 0)
            {
                if (pageInfo.PageIndex > 0 && pageInfo.TotalRecord > 0)
                {
                    pageInfo.PageIndex = 0;
                    list = query.Skip(pageInfo.PageIndex * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
                }
            }
            return list;
        }
    }
}
