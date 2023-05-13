using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}
