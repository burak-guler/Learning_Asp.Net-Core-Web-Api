using Microsoft.EntityFrameworkCore;
using WebApi.Models.Repositories.Config;

namespace WebApi.Models.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) 
        {
            
        }
        public DbSet<Book> Books { get; set; }


        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //model oluşturulurken BookConfig de yaptığımız configuration ifadeleri dikkate alınacak
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}
