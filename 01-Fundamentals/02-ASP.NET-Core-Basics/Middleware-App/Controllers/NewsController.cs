using Microsoft.AspNetCore.Mvc;

namespace Middleware_App.Controllers
{
    public class NewsController : Controller
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
