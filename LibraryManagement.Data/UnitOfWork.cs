using LibraryManagement.Data.Interface;

namespace LibraryManagement.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibraryManagementContext _context { get; set; }
        public UnitOfWork(LibraryManagementContext context)
        {
            _context = context;
        }
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_context);
        }
    }
}
