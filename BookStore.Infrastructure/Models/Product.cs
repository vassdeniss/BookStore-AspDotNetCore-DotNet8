using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Infrastructure.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(26)]
        public string ISBN { get; set; } = null!;

        [Required]
        [MaxLength(60)]
        public string Author { get; set; } = null!;

        [Required]
        public double ListPrice { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double Price50 { get; set; }

        [Required]
        public double Price100 { get; set; }
    }
}
