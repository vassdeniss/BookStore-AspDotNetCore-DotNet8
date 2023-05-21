using BookStore.Services.DTO;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<ProductDto?> GetByGuidAsync(Guid id);

        Task CreateAsync(
            string title,
            string? description,
            string ISBN,
            string author,
            double listPrice,
            double price,
            double price50,
            double price100,
            Guid categoryId,
            string? imageUrl);

        Task EditAsync(
            Guid id,
            string title,
            string? description,
            string ISBN,
            string author,
            double listPrice,
            double price,
            double price50,
            double price100,
            Guid categoryId,
            string? imageUrl);

        Task<string?> GetImageAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<string?> DeleteImageAsync(Guid id);
    }
}
