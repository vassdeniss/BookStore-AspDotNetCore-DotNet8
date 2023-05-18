using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Administrator")]
    public class BaseAdminController : Controller
    {
    }
}
