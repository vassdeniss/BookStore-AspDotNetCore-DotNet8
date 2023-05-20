using AutoMapper;

using BookStore.Infrastructure.Models;
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
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseAdminController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public ProductController(
            IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment,
            IProductService productService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
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

            Product dbProduct = product.Id == Guid.Empty 
                ? new Product() 
                : (await this.unitOfWork.ProductRepository.GetByIdAsync(product.Id))!;

            dbProduct.Title = product.Title;
            dbProduct.Description = product.Description;
            dbProduct.ISBN = product.ISBN;
            dbProduct.Author = product.Author;
            dbProduct.ListPrice = product.ListPrice;
            dbProduct.Price = product.Price;
            dbProduct.Price50 = product.Price50;
            dbProduct.Price100 = product.Price100;
            dbProduct.CategoryId = product.CategoryId;

            if (file is not null)
            {
                string wwwRootPath = this.webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                if (!string.IsNullOrEmpty(dbProduct.ImageUrl))
                {
                    string oldImagePath = Path.Combine(wwwRootPath, dbProduct.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using FileStream fs = new FileStream(Path.Combine(productPath, fileName), FileMode.Create);
                file.CopyTo(fs);

                dbProduct.ImageUrl = $@"\images\product\{fileName}";
            }

            if (product.Id == Guid.Empty)
            {
                await this.unitOfWork.ProductRepository.AddAsync(dbProduct);
            }
            else
            {
                this.unitOfWork.ProductRepository.Update(dbProduct);
            }

            await this.unitOfWork.SaveAsync();
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
            Product? product = await this.unitOfWork.ProductRepository.GetByIdAsync(id!);
            if (product is null)
            {
                return this.Json(new { success = false, message = "Error while deleting." });
            }

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                string wwwRootPath = this.webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            this.unitOfWork.ProductRepository.Remove(product);
            await this.unitOfWork.SaveAsync();

            return this.Json(new { success = true, message = "Delete successful." });
        }

        [HttpPatch]
        public async Task<IActionResult> DeleteImageAsync(Guid? id)
        {
            Product? product = await this.unitOfWork.ProductRepository.GetByIdAsync(id!);
            if (product is null)
            {
                return this.Json(new { success = false, message = "Error while deleting." });
            }

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                string wwwRootPath = this.webHostEnvironment.WebRootPath;
                string oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            product.ImageUrl = null;
            this.unitOfWork.ProductRepository.Update(product);
            await this.unitOfWork.SaveAsync();
            
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
