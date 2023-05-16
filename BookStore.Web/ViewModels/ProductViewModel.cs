using BookStore.Infrastructure.Models;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [StringLength(1000, MinimumLength = 1, ErrorMessage = "{0} must have a length of {2}-{1}.")]
        public string? Description { get; set; }

        [Required]
        [StringLength(26, MinimumLength = 13, ErrorMessage = "{0} must have a length of {2}-{1}.")]
        public string ISBN { get; set; } = null!;

        [Required]
        [StringLength(60, ErrorMessage = "{0} cannot be longer than {1}.")]
        public string Author { get; set; } = null!;

        [Required]
        [Display(Name = "List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name ="Category")]
        public Guid CategoryId { get; set; }

        // TODO: VM
        [ValidateNever]
        public Category Category { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<CategoryViewModel> Categories { get; set; } = null!;
    }
}
