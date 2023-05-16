using BookStore.Infrastructure.Models;
using BookStore.Infrastructure.Repository.Contracts;
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

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Product> dbProducts = await this.unitOfWork.ProductRepository.GetAllAsync();

            IEnumerable<ProductViewModel> products = dbProducts.Select((product) => new ProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ISBN = product.ISBN,
                Author = product.Author,
                ListPrice = product.ListPrice,
                Price = product.Price,
                Price50 = product.Price50,
                Price100 = product.Price100,
            });

            return this.View(products);
        }

        [HttpGet]
        public async Task<IActionResult> UpsertAsync(Guid? id)
        {
            IEnumerable<Category> dbCategories = await this.unitOfWork.CategoryRepository.GetAllAsync();
            IEnumerable<CategoryViewModel> categoryList = dbCategories.Select((category) => new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder,
            });

            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Categories = categoryList;

            if (id is null)
            {
                return this.View(productViewModel);
            }

            Product? product = await this.unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product is null)
            {
                return this.NotFound();
            }

            productViewModel.Id = product.Id;
            productViewModel.Title = product.Title;
            productViewModel.Description = product.Description;
            productViewModel.ISBN = product.ISBN;
            productViewModel.Author = product.Author;
            productViewModel.ListPrice = product.ListPrice;
            productViewModel.Price = product.Price;
            productViewModel.Price50 = product.Price50;
            productViewModel.Price100 = product.Price100;
            productViewModel.CategoryId = product.CategoryId;
            productViewModel.ImageUrl = product.ImageUrl;

            return this.View(productViewModel);
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
        public async Task<IActionResult> DeleteAsync(Guid? id)
        {
            if (id is null)
            {
                return this.NotFound();
            }

            IEnumerable<Category> dbCategories = await this.unitOfWork.CategoryRepository.GetAllAsync();
            IEnumerable<CategoryViewModel> categoryList = dbCategories.Select((category) => new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder,
            });

            Product? product = await this.unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product is null)
            {
                return this.NotFound();
            }

            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ISBN = product.ISBN,
                Author = product.Author,
                ListPrice = product.ListPrice,
                Price = product.Price,
                Price50 = product.Price50,
                Price100 = product.Price100,
                CategoryId = product.CategoryId,
                Categories = categoryList,
                ImageUrl = product.ImageUrl,
            };

            return this.View(productViewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePostAsync(Guid? id)
        {
            Product? product = await this.unitOfWork.ProductRepository.GetByIdAsync(id!);
            if (product is null)
            {
                return this.NotFound();
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

            this.TempData["SuccessMessage"] = "Product deleted successfully!";
            return this.RedirectToAction("Index");
        }
    }
}
