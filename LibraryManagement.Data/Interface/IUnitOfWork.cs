using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Interface
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;
    }
}
