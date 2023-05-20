using System;

namespace BookStore.Services.DTO
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}
