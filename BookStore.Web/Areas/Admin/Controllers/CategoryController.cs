using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BookStore.Infrastructure.Models;
using BookStore.Infrastructure.Repository.Contracts;

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
        public async Task<IActionResult> CreateAsync(Category category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.unitOfWork.CategoryRepository.AddAsync(category);
            await this.unitOfWork.SaveAsync();

            this.TempData["SuccessMessage"] = "Category created successfully!";
            return this.RedirectToAction(nameof(IndexAsync));
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

            return this.View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.unitOfWork.CategoryRepository.Update(category);
            await this.unitOfWork.SaveAsync();

            this.TempData["SuccessMessage"] = "Category edited successfully!";
            return this.RedirectToAction(nameof(IndexAsync));
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

            return this.View(category);
        }

        [HttpPost]
        [ActionName(nameof(DeleteAsync))]
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
            return this.RedirectToAction(nameof(IndexAsync));
        }
    }
}
