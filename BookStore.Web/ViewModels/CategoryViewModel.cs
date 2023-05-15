using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "{0} must be between {1}-{2}.")]
        [DisplayName("Name")]
        public string Name { get; set; } = null!;

        [Range(1, 100, ErrorMessage = "{0} must be between {1}-{2}.")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
