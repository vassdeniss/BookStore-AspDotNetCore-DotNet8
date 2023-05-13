using BookStore.Web.Models;

using Microsoft.EntityFrameworkCore;

namespace BookStore.Web.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
                
        }

        public DbSet<Category> Categories { get; set; }
    }
}
