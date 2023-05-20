using AutoMapper;

using BookStore.Services.Contracts;
using BookStore.Services.DTO;
using BookStore.Web.ViewModels;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
    {
        private readonly IMapper mapper;
        private readonly ICategoryService service;

        public CategoryController(IMapper mapper, ICategoryService service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<CategoryDto> serviceCategories = await this.service.GetAllAsync();
            IEnumerable<CategoryViewModel> categories = this.mapper.Map<IEnumerable<CategoryViewModel>>(serviceCategories);
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

            await this.service.CreateAsync(category.Name, category.DisplayOrder);

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

            CategoryDto? dto = await this.service.GetByGuidAsync((Guid)id);
            if (dto is null)
            {
                return this.NotFound();
            }

            CategoryViewModel category = this.mapper.Map<CategoryViewModel>(dto);
            return this.View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.service.EditAsync(category.Id, category.Name, category.DisplayOrder);

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

            CategoryDto? dto = await this.service.GetByGuidAsync((Guid)id);
            if (dto is null)
            {
                return this.NotFound();
            }

            CategoryViewModel category = this.mapper.Map<CategoryViewModel>(dto);
            return this.View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePostAsync(Guid id)
        {
            await this.service.DeleteAsync(id);
            this.TempData["SuccessMessage"] = "Category deleted successfully!";
            return this.RedirectToAction("Index");
        }
    }
}
