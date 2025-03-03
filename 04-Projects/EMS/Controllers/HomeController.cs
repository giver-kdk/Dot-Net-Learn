using System.Diagnostics;
using EMS.Domain.Models;
using EMS.Insfrastructure.Data;
using EMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager; // UserManager for Identity

        public HomeController(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Login");
            }
            // Go to login page if the user doesn't exists or if user is logged out
            if (!int.TryParse(userId, out int employeeId))
            {
                return BadRequest("Invalid user ID.");
            }

            var employee = await _context.Employees
                .Where(e => e.Id == employeeId)
                .Select(e => e.Status) // Assuming Status is an enum in the database
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            ViewData["CurrentStatus"] = employee.ToString(); // Convert enum to string

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
