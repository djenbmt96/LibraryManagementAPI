using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryManagement.Core.Helper
{
    public static class BookQueryableHelper
    {
        public static IQueryable<Book> HasKeyword(this IQueryable<Book> queryable, string keyword)
        {
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower().Trim();
                return queryable.Where(o => o.Name.ToLower().Contains(keyword)
                                        || o.Amount.ToString().Contains(keyword)
                                        || o.Image.ToLower().Contains(keyword)
                                        || o.Description.ToLower().Contains(keyword));
            }
            return queryable;
        }
    }
}
