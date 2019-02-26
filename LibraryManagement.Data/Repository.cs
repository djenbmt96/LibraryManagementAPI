using LibraryManagement.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManagement.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public DbContext DbContext { get; set; }
        internal DbSet<T> DbSet;
        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync(List<string> includes = null)
        {
            var query = DbSet.AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }
        public async Task<T> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> where = null, List<string> includes = null)
        {
            try
            {
                var query = DbSet.AsQueryable();
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                return await (where != null ? query.Where(where) : query).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Get repository type of " + typeof(T), ex);
            }
        }
        public IQueryable<T> GetMany(Expression<Func<T, bool>> where = null, List<string> includes = null)
        {
            try
            {
                var query = DbSet.AsQueryable();
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                return where != null ? query.Where(where) : query;
            }
            catch (Exception ex)
            {
                throw new Exception("GetMany repository type of " + typeof(T), ex);
            }
        }
        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }
        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }
        public async Task<bool> SaveAsync()
        {
            int save = await DbContext.SaveChangesAsync();
            return save >= 0;
        }
    }
}
