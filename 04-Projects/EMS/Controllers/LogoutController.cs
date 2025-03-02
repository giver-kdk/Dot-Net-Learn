using EMS.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    public class LogoutController : Controller
    {
        private readonly SignInManager<Employee> _signInManager;

        public LogoutController(SignInManager<Employee> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync(); // Sign out the user
            return RedirectToAction("Index", "Home");
        }
    }
}