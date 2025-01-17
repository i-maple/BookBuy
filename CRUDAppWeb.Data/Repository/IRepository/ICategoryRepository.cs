using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDApp.Models;
using CRUDAppWeb.Data.Repository;

namespace CRUDAppWeb.Data.Repository.IRepository
{
    public interface ICategoryRepository: IRepository<CategoryModel>
    {
        public void Update(CategoryModel category);
    }
}
