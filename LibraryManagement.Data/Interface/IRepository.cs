using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Interface
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(List<string> includes = null);
        Task<T> GetByIdAsync(object id);
        Task<T> GetAsync(Expression<Func<T, bool>> where = null, List<string> includes = null);
        IQueryable<T> GetMany(Expression<Func<T, bool>> where = null, List<string> includes = null);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveAsync();
    }
}
