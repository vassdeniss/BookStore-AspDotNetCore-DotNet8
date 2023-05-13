using BookStore.Web.Data;
using BookStore.Web.Models;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;

namespace BookStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BookStoreDbContext context;

        public CategoryController(BookStoreDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Category> categories = this.context.Categories.ToList();

            return this.View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.context.Categories.Add(category);
            this.context.SaveChanges();

            return this.RedirectToAction(nameof(Index));
        }
    }
}
