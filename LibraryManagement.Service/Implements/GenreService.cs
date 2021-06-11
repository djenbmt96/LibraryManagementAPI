using LibraryManagement.Core.Helper;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using LibraryManagement.Services.Implements;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Implements
{
    public class GenreService : BaseService<Genre>
    {
        public GenreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<ResultModel<IEnumerable<Genre>>> GetByPageAsync(int page, int pageSize, string searchKeyword = null)
        {
            var query = _repository.GetMany();
            var entities = await query.HasKeyword(searchKeyword).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new ResultModel<IEnumerable<Genre>>
            {
                Success = true,
                Data = entities,
                Message = ""
            };
        }
    }
}
