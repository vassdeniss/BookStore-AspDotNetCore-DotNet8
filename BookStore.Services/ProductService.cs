using AutoMapper;

using BookStore.Infrastructure.Models;
using BookStore.Infrastructure.Repository.Contracts;
using BookStore.Services.Contracts;
using BookStore.Services.DTO;

using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task CreateAsync(
            string title,
            string? description,
            string ISBN,
            string author,
            double listPrice,
            double price,
            double price50,
            double price100,
            Guid categoryId,
            string? imageUrl)
        {
            Product product = new Product
            {
                Title = title,
                Description = description,
                ISBN = ISBN,
                Author = author,
                ListPrice = listPrice,
                Price = price,
                Price50 = price50,
                Price100 = price100,
                CategoryId = categoryId,
            };

            if (imageUrl is not null)
            {
                product.ImageUrl = imageUrl;
            }

            await this.unitOfWork.ProductRepository.AddAsync(product);
            await this.unitOfWork.SaveAsync();
        }

        public async Task EditAsync(
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
            string? imageUrl)
        {
            Product product = (await this.unitOfWork.ProductRepository.GetByIdAsync(id))!;

            product.Title = title;
            product.Description = description;
            product.ISBN = ISBN;
            product.Author = author;
            product.ListPrice = listPrice;
            product.Price = price;
            product.Price50 = price50;
            product.Price100 = price100;
            product.CategoryId = categoryId;

            if (imageUrl is not null)
            {
                product.ImageUrl = imageUrl;
            }

            this.unitOfWork.ProductRepository.Update(product);
            await this.unitOfWork.SaveAsync();
        }

        public async Task<string?> GetImageAsync(Guid id)
        {
            Product? product = await this.unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product is null)
            {
                return null;
            }
            
            return product.ImageUrl;
        }

        public async Task DeleteAsync(Guid id)
        {
            Product? product = await this.unitOfWork.ProductRepository.GetByIdAsync(id!) 
                ?? throw new KeyNotFoundException();
            this.unitOfWork.ProductRepository.Remove(product);
            await this.unitOfWork.SaveAsync();
        }

        public async Task<string?> DeleteImageAsync(Guid id)
        {
            Product? product = await this.unitOfWork.ProductRepository.GetByIdAsync(id!);
            if (product is null)
            {
                throw new KeyNotFoundException();
            }

            string? imageUrl = product.ImageUrl;
            product.ImageUrl = null;
            this.unitOfWork.ProductRepository.Update(product);
            await this.unitOfWork.SaveAsync();

            return imageUrl;
        }
    }
}
