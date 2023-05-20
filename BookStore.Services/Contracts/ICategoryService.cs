using BookStore.Services.DTO;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<CategoryDto?> GetByGuidAsync(Guid id);

        Task CreateAsync(string name, int displayOrder);

        Task EditAsync(Guid id, string name, int displayOrder);

        Task DeleteAsync(Guid id);
    }
}
