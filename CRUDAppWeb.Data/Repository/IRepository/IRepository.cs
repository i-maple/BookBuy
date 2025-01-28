using System.Linq.Expressions;

namespace CRUDAppWeb.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {  
        public IEnumerable<T> GetAll(string? includeProperties = null);
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
        public void Add(T entity);
        public void Delete(T entity);
        public void DeleteRange(IEnumerable<T> entities);
    }
}
