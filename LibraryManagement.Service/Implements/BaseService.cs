using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Implements
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        internal IRepository<T> _repository;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<T>();
        }
        public async Task<ResultModel<IEnumerable<T>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            var result = new ResultModel<IEnumerable<T>>
            {
                Data = entities,
                Success = true,
                Message = ""
            };
            return result;
        }
        public async Task<ResultModel<T>> GetById(object id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var result = new ResultModel<T>
            {
                Data = entity,
                Success = true,
                Message = ""
            };
            return result;
        }
        public async Task<ResultModel<T>> Insert(T entity)
        {
            _repository.Insert(entity);
            var success = await _repository.SaveAsync();
            if (success)
            {
                return new ResultModel<T>
                {
                    Data = entity,
                    Success = true,
                    Message = ""
                };
            }
            return new ResultModel<T>
            {
                Data = entity,
                Success = false,
                Message = "Can not insert " + typeof(T).Name
            };
        }
        public async Task<ResultModel<T>> Update(T entity)
        {
            _repository.Update(entity);
            var success = await _repository.SaveAsync();
            if (success)
            {
                return new ResultModel<T>
                {
                    Data = entity,
                    Success = true,
                    Message = ""
                };
            }
            return new ResultModel<T>
            {
                Data = entity,
                Success = false,
                Message = "Can not update " + typeof(T).Name
            };
        }
        public async Task<ResultModel<T>> Delete(T entity)
        {
            _repository.Delete(entity);
            var success = await _repository.SaveAsync();
            if (success)
            {
                return new ResultModel<T>
                {
                    Data = entity,
                    Success = true,
                    Message = ""
                };
            }
            return new ResultModel<T>
            {
                Data = entity,
                Success = false,
                Message = "Can not delete " + typeof(T).Name
            };
        }

        public virtual Task<ResultModel<IEnumerable<T>>> GetByPageAsync(int page, int pageSize, string searchKeyword = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
