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

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Category fantasy = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Fantasy",
                DisplayOrder = 1,
            };
            Category scifi = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Sci-fi",
                DisplayOrder = 2,
            };
            Category action = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Action",
                DisplayOrder = 3,
            };

            modelBuilder.Entity<Category>()
                .HasData(fantasy, scifi, action);
            
            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   Id = Guid.NewGuid(),
                   Title = "Fortune of Time",
                   Author = "Billy Spark",
                   Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                   ISBN = "SWD9999001",
                   ListPrice = 99,
                   Price = 90,
                   Price50 = 85,
                   Price100 = 80,
                   ImageUrl = string.Empty,
                   CategoryId = fantasy.Id,
               },
               new Product
               {
                   Id = Guid.NewGuid(),
                   Title = "Dark Skies",
                   Author = "Nancy Hoover",
                   Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                   ISBN = "CAW777777701",
                   ListPrice = 40,
                   Price = 30,
                   Price50 = 25,
                   Price100 = 20,
                   ImageUrl = string.Empty,
                   CategoryId = fantasy.Id,
               },
               new Product
               {
                   Id = Guid.NewGuid(),
                   Title = "Vanish in the Sunset",
                   Author = "Julian Button",
                   Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                   ISBN = "RITO5555501",
                   ListPrice = 55,
                   Price = 50,
                   Price50 = 40,
                   Price100 = 35,
                   ImageUrl = string.Empty,
                   CategoryId = scifi.Id,
               },
               new Product
               {
                   Id = Guid.NewGuid(),
                   Title = "Cotton Candy",
                   Author = "Abby Muscles",
                   Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                   ISBN = "WS3333333301",
                   ListPrice = 70,
                   Price = 65,
                   Price50 = 60,
                   Price100 = 55,
                   ImageUrl = string.Empty,
                   CategoryId = scifi.Id,
               },
               new Product
               {
                   Id = Guid.NewGuid(),
                   Title = "Rock in the Ocean",
                   Author = "Ron Parker",
                   Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                   ISBN = "SOTJ1111111101",
                   ListPrice = 30,
                   Price = 27,
                   Price50 = 25,
                   Price100 = 20,
                   ImageUrl = string.Empty,
                   CategoryId = action.Id,
               },
               new Product
               {
                   Id = Guid.NewGuid(),
                   Title = "Leaves and Wonders",
                   Author = "Laura Phantom",
                   Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                   ISBN = "FOT000000001",
                   ListPrice = 25,
                   Price = 23,
                   Price50 = 22,
                   Price100 = 20,
                   ImageUrl = string.Empty,
                   CategoryId = action.Id,
               });
        }
    }
}
