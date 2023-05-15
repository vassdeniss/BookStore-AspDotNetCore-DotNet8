using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BookStore.Infrastructure.Models;
using BookStore.Infrastructure.Repository.Contracts;
using BookStore.Web.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Category> categories = await this.unitOfWork.CategoryRepository.GetAllAsync();

            return this.View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryViewModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            Category dbCategory = new Category()
            {
                Name = category.Name,
                DisplayOrder = category.DisplayOrder,
            };

            await this.unitOfWork.CategoryRepository.AddAsync(dbCategory);
            await this.unitOfWork.SaveAsync();

            this.TempData["SuccessMessage"] = "Category created successfully!";
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id is null)
            {
                return this.NotFound();
            }

            Category? category = await this.unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return this.NotFound();
            }

            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder,
            };

            return this.View(categoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            Category dbCategory = (await this.unitOfWork.CategoryRepository.GetByIdAsync(category.Id))!;

            dbCategory.Name = category.Name;
            dbCategory.DisplayOrder = category.DisplayOrder;

            this.unitOfWork.CategoryRepository.Update(dbCategory);

            await this.unitOfWork.SaveAsync();

            this.TempData["SuccessMessage"] = "Category edited successfully!";
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(Guid? id)
        {
            if (id is null)
            {
                return this.NotFound();
            }

            Category? category = await this.unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return this.NotFound();
            }

            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder,
            };

            return this.View(categoryViewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePostAsync(Guid? id)
        {
            Category? category = await this.unitOfWork.CategoryRepository.GetByIdAsync(id!);
            if (category is null)
            {
                return this.NotFound();
            }

            this.unitOfWork.CategoryRepository.Remove(category);
            await this.unitOfWork.SaveAsync();

            this.TempData["SuccessMessage"] = "Category deleted successfully!";
            return this.RedirectToAction("Index");
        }
    }
}
