using System.Diagnostics;
using EMS.Domain.Enums;
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

        public async Task<IActionResult> Index(String filter = "Pending")
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

            var today = DateTime.Today;
            // Calculate hours worked
            var timeLog = await _context.TimeLogs
                .Where(t => t.EmployeeId == employeeId && t.Log == today && t.LogType=="ClockIn")
                .OrderByDescending(t => t.Log)
                .FirstOrDefaultAsync();

            if (timeLog != null)
            {
                if (timeLog.LogType =="ClockOut")
                {
                    // If clocked out, use WorkingHoursPerDay
                    ViewData["HoursWorked"] = timeLog.WorkingHoursPerDay?.TotalHours.ToString("0.00") ?? "0.00";
                }
                else
                {
                    // If still clocked in, calculate the difference between current time and ClockIn
                    var hoursWorked = (DateTime.Now - timeLog.Log).TotalHours;
                    ViewData["HoursWorked"] = hoursWorked.ToString("0.00");
                }
            }
            else
            {
                ViewData["HoursWorked"] = "0.00";
            }


            // Review Leave Logic
            var leaveRequests = _context.LeaveRequests
                .Include(lr => lr.Employee) // Ensures Employee data is loaded
                .AsQueryable();

            switch (filter)
            {
                case "Pending":
                    leaveRequests = leaveRequests.Where(lr => lr.Status == LeaveStatus.Pending);
                    break;
                case "Closed":
                    leaveRequests = leaveRequests
                    .Where(lr => lr.Status == LeaveStatus.Approved || lr.Status == LeaveStatus.Rejected)
                    .OrderByDescending(lr => lr.RequestDate);
                    break;
            }

            // Store filter value in ViewBag for frontend use
            ViewBag.Filter = filter;

            var leaveRequestList = await leaveRequests.ToListAsync();

            Console.WriteLine($"Total Leave Requests Found: {leaveRequestList.Count}");

            return View(leaveRequestList);
        }

        [HttpGet]
        public async Task<IActionResult> GetHoursWorked()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { hoursWorked = "0.00" });
            }

            if (!int.TryParse(userId, out int employeeId))
            {
                return Json(new { hoursWorked = "0.00" });
            }

            var timeLog = await _context.TimeLogs
                .Where(t => t.EmployeeId == employeeId)
                .OrderByDescending(t => t.Log)
                .FirstOrDefaultAsync();

            if (timeLog != null)
            {
                if (timeLog.LogType == "ClockOut")
                {
                    // If clocked out, use WorkingHoursPerDay
                    return Json(new { hoursWorked = timeLog.WorkingHoursPerDay?.TotalHours.ToString("0.00") ?? "0.00" });
                }
                else
                {
                    // If still clocked in, calculate the difference between current time and ClockIn
                    var hoursWorked = (DateTime.Now - timeLog.Log).TotalHours;
                    return Json(new { hoursWorked = hoursWorked.ToString("0.00") });
                }
            }
            else
            {
                return Json(new { hoursWorked = "0.00" });
            }
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
