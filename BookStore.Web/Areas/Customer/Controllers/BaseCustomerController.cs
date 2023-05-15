using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class BaseCustomerController : Controller
    {
    }
}
