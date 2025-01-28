using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CRUDApp.Data.Repository.IRepository;
using CRUDApp.Models;
using CRUDAppWeb.Data.Repository;

namespace CRUDApp.Data.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company company)
        {
            _db.Companies.Update(company);
        }
    }
}
