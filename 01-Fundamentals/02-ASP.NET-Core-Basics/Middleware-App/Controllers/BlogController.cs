using Microsoft.AspNetCore.Mvc;

namespace Middleware_App.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Article()
        {
            return View();
        }
    }
}
