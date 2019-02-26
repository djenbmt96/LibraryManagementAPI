using LibraryManagement.Core.Helper;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Implements
{
    public class BookService : BaseService<Book>, IBaseService<Book>
    {
        public BookService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public override async Task<ResultModel<IEnumerable<Book>>> GetByPageAsync(int page, int pageSize, string searchKeyword = null)
        {
            var query = _repository.GetMany();
            var entities = await query.HasKeyword(searchKeyword).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new ResultModel<IEnumerable<Book>>
            {
                Data = entities,
                Success = true,
                Message = ""
            };
        }
    }
}
