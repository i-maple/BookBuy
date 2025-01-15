using CRUD_App.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_App.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CategoryModel> Categories { get; set; }
    }
}
