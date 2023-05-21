using AutoMapper;

using BookStore.Infrastructure.Repository.Contracts;
using BookStore.Services.Contracts;
using BookStore.Services.DTO;
using BookStore.Web.ViewModels;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseAdminController
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public ProductController(
            IWebHostEnvironment webHostEnvironment,
            IProductService productService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.productService = productService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> UpsertAsync(Guid? id)
        {
            IEnumerable<CategoryDto> serviceCategories = 
                await this.categoryService.GetAllAsync();
            IEnumerable<CategoryViewModel> categories =
                this.mapper.Map<IEnumerable<CategoryViewModel>>(serviceCategories);

            ProductViewModel? productViewModel = id is null
                ? new ProductViewModel { Categories = categories }
                : await this.GetProductViewModelByIdAsync((Guid)id, categories);

            return productViewModel is null 
                ? this.NotFound() 
                : this.View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ProductViewModel product, IFormFile? file)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(product);
            }

            string? imageUrl = null;
            if (file is not null)
            {
                string wwwRootPath = this.webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                string? productImageUrl = await this.productService.GetImageAsync(product.Id);
                if (!string.IsNullOrEmpty(productImageUrl))
                {
                    string oldImagePath = Path.Combine(wwwRootPath, productImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using FileStream fs = new FileStream(Path.Combine(productPath, fileName), FileMode.Create);
                file.CopyTo(fs);

                imageUrl = $@"\images\product\{fileName}";
            }

            if (product.Id == Guid.Empty)
            {
                await this.productService.CreateAsync(
                    product.Title, product.Description, product.ISBN, 
                    product.Author, product.ListPrice, product.Price, 
                    product.Price50, product.Price100, product.CategoryId, 
                    imageUrl);
            }
            else
            {
                await this.productService.EditAsync(
                    product.Id, product.Title, product.Description,
                    product.ISBN, product.Author, product.ListPrice,
                    product.Price, product.Price50, product.Price100,
                    product.CategoryId, imageUrl);
            }

            this.TempData["SuccessMessage"] = $"Product {(product.Id == Guid.Empty ? "created" : "updated")} successfully!";
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<ProductDto> serviceProducts = await this.productService.GetAllAsync();
            IEnumerable<ProductViewModel> products = this.mapper.Map<IEnumerable<ProductViewModel>>(serviceProducts);
            return this.Json(new { data = products });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid? id)
        {
            if (id is null)
            {
                return this.NotFound();
            }

            string? imageUrl = await this.productService.GetImageAsync((Guid)id);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                string wwwRootPath = this.webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, imageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            try
            {
                await this.productService.DeleteAsync((Guid)id);
            }
            catch (KeyNotFoundException)
            {
                return this.NotFound();
            }

            return this.Json(new { success = true, message = "Delete successful." });
        }

        [HttpPatch]
        public async Task<IActionResult> DeleteImageAsync(Guid? id)
        {
            if (id is null)
            {
                return this.NotFound();
            }

            string? imageUrl = null;
            try
            {
                imageUrl = await this.productService.DeleteImageAsync((Guid)id);
            }
            catch (KeyNotFoundException)
            {
                return this.Json(new { success = false, message = "Error while deleting." });
            }

            if (!string.IsNullOrEmpty(imageUrl))
            {
                string wwwRootPath = this.webHostEnvironment.WebRootPath;
                string oldImagePath = Path.Combine(wwwRootPath, imageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            return this.Json(new { success = true, message = "Delete successful." });
        }

        private async Task<ProductViewModel?> GetProductViewModelByIdAsync(Guid id, IEnumerable<CategoryViewModel> categories)
        {
            ProductDto? product = await this.productService.GetByGuidAsync(id);
            if (product is null)
            {
                return null;
            }

            ProductViewModel productViewModel = this.mapper.Map<ProductViewModel>(product);
            productViewModel.Categories = categories;

            return productViewModel;
        }
    }
}
