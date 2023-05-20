using AutoMapper;

using BookStore.Infrastructure.Models;
using BookStore.Infrastructure.Repository.Contracts;
using BookStore.Services.Contracts;
using BookStore.Services.DTO;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            IEnumerable<Product> products = await this.unitOfWork.ProductRepository.GetAllAsync("Category");

            return this.mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByGuidAsync(Guid id)
        {
            Product? product = await this.unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product is null)
            {
                return null;
            }

            return this.mapper.Map<ProductDto>(product);
        }
    }
}
