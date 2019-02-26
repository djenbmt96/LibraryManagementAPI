using LibraryManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<ResultModel<IEnumerable<T>>> GetAll();
        Task<ResultModel<T>> GetById(object id);
        Task<ResultModel<T>> Insert(T entity);
        Task<ResultModel<T>> Update(T entity);
        Task<ResultModel<T>> Delete(T entity);
        Task<ResultModel<IEnumerable<T>>> GetByPageAsync(int page, int pageSize, string searchKeyword = null);
    }
}
