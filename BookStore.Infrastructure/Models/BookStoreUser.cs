using Microsoft.AspNetCore.Identity;

using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Infrastructure.Models
{
    public class BookStoreUser : IdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }
    }
}
