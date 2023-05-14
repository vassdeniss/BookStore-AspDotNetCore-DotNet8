using BookStore.Web.Data;
using BookStore.Web.Models;

using Microsoft.AspNetCore.Mvc;

using System;
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

            this.TempData["SuccessMessage"] = "Category created successfully!";
            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            if (id is null)
            {
                return this.NotFound();
            }

            Category? category = this.context.Categories.Find(id);
            if (category is null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.context.Categories.Update(category);
            this.context.SaveChanges();

            this.TempData["SuccessMessage"] = "Category edited successfully!";
            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(Guid? id)
        {
            if (id is null)
            {
                return this.NotFound();
            }

            Category? category = this.context.Categories.Find(id);
            if (category is null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public IActionResult DeletePost(Guid? id)
        {
            Category? category = this.context.Categories.Find(id);
            if (category is null)
            {
                return this.NotFound();
            }

            this.context.Categories.Remove(category);
            this.context.SaveChanges();

            this.TempData["SuccessMessage"] = "Category deleted successfully!";
            return this.RedirectToAction(nameof(Index));
        }
    }
}
