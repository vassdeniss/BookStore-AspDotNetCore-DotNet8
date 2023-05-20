using BookStore.Infrastructure.Models;

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Services.DTO
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string ISBN { get; set; } = null!;

        public string Author { get; set; } = null!;

        public double ListPrice { get; set; }

        public double Price { get; set; }

        public double Price50 { get; set; }

        public double Price100 { get; set; }

        public string? ImageUrl { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}
