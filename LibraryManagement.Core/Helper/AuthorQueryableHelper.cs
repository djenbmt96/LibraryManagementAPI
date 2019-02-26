using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryManagement.Core.Helper
{
    public static class AuthorQueryableHelper
    {
        public static IQueryable<Author> HasKeyword(this IQueryable<Author> queryable, string keyword)
        {
            if(!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower().Trim();
                return queryable.Where(o => o.Name.ToLower().Contains(keyword)
                                            || o.YearOfBirth.ToLower().Contains(keyword));
            }
            return queryable;
        }
    }
}
