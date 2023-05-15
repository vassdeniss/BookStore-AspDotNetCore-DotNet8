using BookStore.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;

using System;

namespace BookStore.Infrastructure.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
                
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasData(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Fantasy",
                    DisplayOrder = 1,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Sci-fi",
                    DisplayOrder = 2,
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Action",
                    DisplayOrder = 3,
                });
        }
    }
}
