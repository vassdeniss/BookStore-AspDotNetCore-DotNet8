using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BookStore.Infrastructure.Models;
using BookStore.Infrastructure.Repository.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Category> categories = await this.categoryRepo.GetAllAsync();

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

            await this.categoryRepo.AddAsync(category);
            await this.categoryRepo.SaveAsync();

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

            Category? category = await this.categoryRepo.GetByIdAsync(id);
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

            this.categoryRepo.Update(category);
            await this.categoryRepo.SaveAsync();

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

            Category? category = await this.categoryRepo.GetByIdAsync(id);
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
            Category? category = await this.categoryRepo.GetByIdAsync(id!);
            if (category is null)
            {
                return this.NotFound();
            }

            this.categoryRepo.Remove(category);
            await this.categoryRepo.SaveAsync();

            this.TempData["SuccessMessage"] = "Category deleted successfully!";
            return this.RedirectToAction(nameof(IndexAsync));
        }
    }
}
