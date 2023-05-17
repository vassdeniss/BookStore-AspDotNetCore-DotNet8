using BookStore.Infrastructure.Models;
using BookStore.Infrastructure.Repository.Contracts;
using BookStore.Web.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Areas.Customer.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseCustomerController
    {
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Product> dbProducts = await this.unitOfWork.ProductRepository.GetAllAsync("Category");

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
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                Category = product.Category,
            });

            return this.View(products);
        }

        [HttpGet]
        public async Task<IActionResult> DetailsAsync(Guid? id)
        {
            if (id is null)
            {
                return this.NotFound();
            }

            Product? dbProduct = await this.unitOfWork.ProductRepository
                .GetAsync((p) => p.Id == id, "Category");
            if (dbProduct is null)
            {
                return this.NotFound();
            }

            ProductViewModel product = new ProductViewModel
            {
                Id = dbProduct.Id,
                Title = dbProduct.Title,
                Description = dbProduct.Description,
                ISBN = dbProduct.ISBN,
                Author = dbProduct.Author,
                ListPrice = dbProduct.ListPrice,
                Price = dbProduct.Price,
                Price50 = dbProduct.Price50,
                Price100 = dbProduct.Price100,
                CategoryId = dbProduct.CategoryId,
                ImageUrl = dbProduct.ImageUrl,
                Category = dbProduct.Category,
            };

            return this.View(product);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
