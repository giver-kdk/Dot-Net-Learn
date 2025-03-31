using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using EMS.Insfrastructure.Data;
using EMS.Insfrastructure.Services;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager; // UserManager for Identity
        private readonly IImageService _imageService;

        public ProfileController(ApplicationDbContext context, UserManager<Employee> userManager, IImageService imageService)
        {
            _context = context;
            _userManager = userManager;
            _imageService = imageService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Login");
            }
            if (!int.TryParse(userId, out int employeeId))
            {
                return BadRequest("Invalid user ID.");
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> EditView()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Login");
            }
            if (!int.TryParse(userId, out int employeeId))
            {
                return BadRequest("Invalid user ID.");
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

            //Console.WriteLine(collection);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromForm] IFormCollection collection)
        {
            // Map the form data
            string fullName = Convert.ToString(collection["FullName"]);
            string email = Convert.ToString(collection["Email"]);
            string phoneNumber = Convert.ToString(collection["PhoneNumber"]);
            //EmployeeStatus status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), Convert.ToString(collection["Status"]));
            string status = Convert.ToString(collection["Status"]);

            // Get user ID and convert to integer
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Edit", "Profile");
            }
            if (!int.TryParse(userId, out int employeeId))
            {
                return BadRequest("Invalid user ID.");
            }

            var profileImage = collection.Files["ProfilePicture"];
            Console.WriteLine("********* Profile Data: " + profileImage?.FileName);
            if (ModelState.IsValid)
            {
                try
                {
                    var existingEmployee = await _context.Employees.FindAsync(employeeId);
                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }
                    // Update fields of employee
                    try
                    {
                        // By default, we have to modify all properties to update Employee.
                        // Non-modified properties are set to 0, null or "" as per definition in Model. So, Update only modified properties one by one
                        existingEmployee.FullName = fullName ?? existingEmployee.FullName;
                        existingEmployee.Email = email ?? existingEmployee.Email;
                        existingEmployee.PhoneNumber = phoneNumber ?? existingEmployee.PhoneNumber;
                        if (status != null)
                        {
                            existingEmployee.Status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), status);
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        Console.WriteLine("*** Edit Error ***");
                    }


                    // Handle image upload to Cloudinary
                    if (profileImage!= null && !IsValidImage(profileImage))
                    {
                        Console.WriteLine("********* Image is not valid ***********");
                        return RedirectToAction("EditView");
                    }

                    // Delete old image from Cloudinary if exists
                    if (!string.IsNullOrEmpty(existingEmployee.ProfilePicture))
                    {
                        BackgroundJob.Enqueue(() => _imageService.DeleteImageAsync(existingEmployee.ProfilePicture));
                    }

                    // Upload new image
                    var uploadResult = await _imageService.UploadProfilePictureAsync(profileImage, employeeId);
                    if (uploadResult == null)
                    {
                        Console.WriteLine("********* No Image URL Returned by Cloudinary ***********");
                        return RedirectToAction("EditView");
                    }

                    existingEmployee.ProfilePicture = uploadResult;

                    // Mark the entity as modified and save changes
                    _context.Entry(existingEmployee).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToAction("EditView");
                }
                catch (DbUpdateConcurrencyException)
                {
                    Console.WriteLine("*** DB Concurrency Error ***");
                }
            }
            return RedirectToAction("EditView");
        }

        private bool IsValidImage(IFormFile file)
        {
            if (file.Length > 5 * 1024 * 1024) // 5MB
                return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return !string.IsNullOrEmpty(extension) && allowedExtensions.Contains(extension);
        }


    }
}
