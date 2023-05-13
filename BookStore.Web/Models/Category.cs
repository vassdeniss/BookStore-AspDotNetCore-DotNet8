using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Name")]
        public string Name { get; set; } = null!;

        [Range(1, 100, ErrorMessage = "{0} must be between {1}-{2}.")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
