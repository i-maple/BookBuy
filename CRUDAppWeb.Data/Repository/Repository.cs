using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CRUDApp.Data;
using CRUDAppWeb.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CRUDAppWeb.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        void IRepository<T>.Add(T entity)
        {
            dbSet.Add(entity);
        }

        void IRepository<T>.Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        void IRepository<T>.DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        IEnumerable<T> IRepository<T>.GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query.Where<T>(filter);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query;
        }

        T IRepository<T>.GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties)
        {
            IQueryable<T> query = dbSet;
            query = query.Where<T>(filter);
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }
        Task<T> IRepository<T>.GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties)
        {
            IQueryable<T> query = dbSet;
            query = query.Where<T>(filter);
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefaultAsync();
        }
    }
}
