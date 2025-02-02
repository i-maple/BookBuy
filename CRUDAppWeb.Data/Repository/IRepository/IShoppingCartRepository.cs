using CRUDApp.Models;
using CRUDAppWeb.Data.Repository.IRepository;

namespace CRUDApp.Data.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        public int IncrementCount(ShoppingCart shoppingCart, int count);

        public int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
