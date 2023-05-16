using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using BookStore.Infrastructure.Models;
using BookStore.Infrastructure.Repository.Contracts;
using BookStore.Web.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseAdminController
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductViewModel product)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            Product dbProduct = new Product()
            {
                Title = product.Title,
                Description = product.Description,
                ISBN = product.ISBN,
                Author = product.Author,
                ListPrice = product.ListPrice,
                Price = product.Price,
                Price50 = product.Price50,
                Price100 = product.Price100,
            };

            await this.unitOfWork.ProductRepository.AddAsync(dbProduct);
            await this.unitOfWork.SaveAsync();

            this.TempData["SuccessMessage"] = "Product created successfully!";
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id is null)
            {
                return this.NotFound();
            }

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
            };

            return this.View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel product)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            Product dbProduct = (await this.unitOfWork.ProductRepository.GetByIdAsync(product.Id))!;

            dbProduct.Title = product.Title;
            dbProduct.Description = product.Description;
            dbProduct.ISBN = product.ISBN;
            dbProduct.Author = product.Author;
            dbProduct.ListPrice = product.ListPrice;
            dbProduct.Price = product.Price;
            dbProduct.Price50 = product.Price50;
            dbProduct.Price100 = product.Price100;

            this.unitOfWork.ProductRepository.Update(dbProduct);

            await this.unitOfWork.SaveAsync();

            this.TempData["SuccessMessage"] = "Product edited successfully!";
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(Guid? id)
        {
            if (id is null)
            {
                return this.NotFound();
            }

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

            this.unitOfWork.ProductRepository.Remove(product);
            await this.unitOfWork.SaveAsync();

            this.TempData["SuccessMessage"] = "Product deleted successfully!";
            return this.RedirectToAction("Index");
        }
    }
}
