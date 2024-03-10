using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Models.Repositories.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //migration aldığında eğer bu tabloda veri yoksa aşağıdaki işlemler yapılır.
            //yani veri eklenir.
            builder.HasData(
                new Book {Id=1, Title = "Karagöz ve Hacivat", Price = 75 },
                new Book { Id = 2, Title = "Mesnevi", Price = 175 },
                new Book { Id = 3, Title = "Devlet", Price = 375 }
                );
        }
    }
}
