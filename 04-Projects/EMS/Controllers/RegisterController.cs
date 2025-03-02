using AspNetCoreGeneratedDocument;
using EMS.Application.DTOs;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    //[ApiController]
    [Route("/[controller]")]

    // ****** Controller for registering a new user ******
    public class RegisterController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RegisterController(UserManager<Employee> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        //[HttpPost("register")]
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection model)
        //public ActionResult Register(IFormCollection model)
        {
            Console.WriteLine("********* Registering user ********* ");
            Employee user = new Employee
            {
                UserName = Convert.ToString(model["FullName"]).Replace(" ", ""),            // Required by Identity in pure form
                Email = Convert.ToString(model["Email"]),
                FullName = Convert.ToString(model["FullName"]),
                PhoneNumber = Convert.ToString(model["PhoneNumber"]),
                Position = Convert.ToString(model["Position"]),
                Salary = Convert.ToDouble(model["Salary"]),
                JoiningDate = Convert.ToDateTime(model["JoiningDate"]),
            };


            var result = await _userManager.CreateAsync(user, Convert.ToString(model["Password"]));
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error Code: {error.Code}, Description: {error.Description}");
            }
            if (result.Succeeded)
            {
                Console.WriteLine("********  Registration Successful ******** ");
                await _userManager.AddToRoleAsync(user, Convert.ToString(model["Role"]));
                //return Ok(new { Message = "User registered successfully" });
                return RedirectToAction("Index", "Login");
            }

            Console.WriteLine("******** Registration Failed ******** ");
            return RedirectToAction("Index", "Home");
        }
    }
}
