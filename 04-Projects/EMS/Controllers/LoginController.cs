using EMS.Application.DTOs;
using EMS.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EMS.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;

        public LoginController(SignInManager<Employee> signInManager, UserManager<Employee> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection model)
        {
            // *********** Find the user by email on the database ***************
            var user = await _userManager.FindByEmailAsync(Convert.ToString(model["Email"]));

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

            // ***************** Attempt to sign in the user *******************
            var result = await _signInManager.PasswordSignInAsync(user, Convert.ToString(model["Password"]), isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //return Ok(new { Message = "Login successful" });
                return RedirectToAction("Index", "Home");
            }

            return Unauthorized(new { Message = "Invalid email or password" });
        }
    }
}