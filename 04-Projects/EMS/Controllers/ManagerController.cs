using EMS.Application.DTOs;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using EMS.Insfrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace EMS.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager; // UserManager for Identity

        public ManagerController(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SetStatus(string status)
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

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                return NotFound("Employee not found."); 
            }

            employee.Status = status switch
            {
                "Active" => EmployeeStatus.Active,
                "Busy" => EmployeeStatus.Busy,
                "OnLeave" => EmployeeStatus.OnLeave,
                _ => employee.Status // Default to current status if invalid
            };

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return Ok(); 
        }

        public async Task<IActionResult> ReviewLeave(string filter="Pending"){

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Login");
            }
            if (!int.TryParse(userId, out int employeeId))
            {
                return BadRequest("Invalid user ID.");
            }


            //Console.WriteLine(employeeId);
            // Fetch leave requests based on the filter

            //var reqs = await leaveRequests.ToListAsync();

            //Console.WriteLine("Total Reqs: " + reqs.Count);
            var leaveRequests = _context.LeaveRequests
                .Where(lr => 1==1);

            if (filter == "Pending")
            {
                leaveRequests = _context.LeaveRequests
                .Where(lr => lr.Status == LeaveStatus.Pending);
                //leaveRequests = leaveRequests.Where(lr => lr.Status == LeaveStatus.Pending);
            }
            else if (filter == "Closed")
            {
                leaveRequests = _context.LeaveRequests
                    .Where(lr => lr.Status == LeaveStatus.Approved || lr.Status == LeaveStatus.Rejected);
                //leaveRequests = leaveRequests.Where(lr => (lr.Status == LeaveStatus.Approved || lr.Status == LeaveStatus.Rejected));
            }

            // Sort Leave Reuquest list by RequestDate in descending order
            //var sortedLeaveRequests = await leaveRequests
                //.OrderByDescending(lr => lr.RequestDate)
                //.ToListAsync();

            ViewBag.Filter = filter;
            //Console.WriteLine(leaveRequests.Count);
            return View(await leaveRequests.ToListAsync());
        }
        public async Task<IActionResult> EmployeeAttendance()
        {

            return View();
        }
    }
}
