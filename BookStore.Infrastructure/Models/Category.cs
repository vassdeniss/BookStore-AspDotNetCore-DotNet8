using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Infrastructure.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}
