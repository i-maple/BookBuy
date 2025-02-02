using CRUDAppWeb.Data.Repository.IRepository;

namespace CRUDApp.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get;  }
        public ICoverTypeRepository CoverType { get; }
        public IProductRepository Product { get; }
        public ICompanyRepository Company { get; }
        public IShoppingCartRepository ShoppingCart { get; }
        public IOrderDetailRepository OrderDetail { get; }
        public IOrderHeaderRepository OrderHeader { get; }
        public IApplicationUserRepository ApplicationUser { get; }
        public void Save();
    }
}
