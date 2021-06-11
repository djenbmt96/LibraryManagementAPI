using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryManagement.Core.Helper
{
    public static class GenreQueryableHelper
    {
        public static IQueryable<Genre> HasKeyword(this IQueryable<Genre> query, string keyword)
        {
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim().ToLower();
                return query.Where(o => o.Name.ToLower().Contains(keyword));
            }
            return query;
        }
    }
}
