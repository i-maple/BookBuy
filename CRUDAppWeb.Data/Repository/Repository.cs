using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CRUD_App.Data;
using CRUDAppWeb.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CRUDAppWeb.Data.Repository
{
    public class Repository<T> : IRepository<T> where T: class
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

        IEnumerable<T> IRepository<T>.GetAll()
        {
            IQueryable<T> query = dbSet;
            return dbSet;
        }

        T IRepository<T>.GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where<T>(filter);
            return query.FirstOrDefault();
        }
    }
}
