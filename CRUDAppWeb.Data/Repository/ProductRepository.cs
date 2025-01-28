using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDApp.Data.Repository.IRepository;
using CRUDApp.Models;
using CRUDAppWeb.Data.Repository;

namespace CRUDApp.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        void IProductRepository.Update(Product product)
        {
            _db.Product.Update(product);
        }
    }
}
