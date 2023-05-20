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
    }
}
