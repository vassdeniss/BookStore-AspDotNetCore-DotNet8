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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            IEnumerable<Category> dbCategories = await this.unitOfWork.CategoryRepository.GetAllAsync();

            return this.mapper.Map<IEnumerable<CategoryDto>>(dbCategories);
        }

        public async Task<CategoryDto?> GetByGuidAsync(Guid id)
        {
            Category? category = await this.unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return null;
            }

            return this.mapper.Map<CategoryDto>(category);
        }

        public async Task CreateAsync(string name, int displayOrder)
        {
            Category category = new Category()
            {
                Name = name,
                DisplayOrder = displayOrder,
            };

            await this.unitOfWork.CategoryRepository.AddAsync(category);
            await this.unitOfWork.SaveAsync();
        }

        public async Task EditAsync(Guid id, string name, int displayOrder)
        {
            Category category = (await this.unitOfWork.CategoryRepository.GetByIdAsync(id))!;

            category.Name = name;
            category.DisplayOrder = displayOrder;

            this.unitOfWork.CategoryRepository.Update(category);
            await this.unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Category category = (await this.unitOfWork.CategoryRepository.GetByIdAsync(id))!;
            this.unitOfWork.CategoryRepository.Remove(category);
            await this.unitOfWork.SaveAsync();
        }
    }
}
