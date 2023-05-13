using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
