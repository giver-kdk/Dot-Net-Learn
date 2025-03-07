﻿using EMS.Application.DTOs;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using EMS.Insfrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context; 
        private readonly UserManager<Employee> _userManager; // UserManager for Identity

        public EmployeeController(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LeaveRequest(string filter = "Pending")
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


            //Console.WriteLine(employeeId);
            // Fetch leave requests based on the filter
            var leaveRequests = _context.LeaveRequests
                .Where(lr => lr.EmployeeId == employeeId);

            var reqs = await leaveRequests.ToListAsync();

            //Console.WriteLine("Total Reqs: " + reqs.Count);

            if (filter == "Pending")
            {
                leaveRequests = leaveRequests.Where(lr => lr.Status == LeaveStatus.Pending);
            }
            else if (filter == "Closed")
            {
                leaveRequests = leaveRequests.Where(lr => (lr.Status == LeaveStatus.Approved || lr.Status == LeaveStatus.Rejected));
            }

            // Sort Leave Reuquest list by RequestDate in descending order
            var sortedLeaveRequests = await leaveRequests
                .OrderByDescending(lr => lr.RequestDate)
                .ToListAsync();

            ViewBag.Filter = filter;
            Console.WriteLine(sortedLeaveRequests.Count);
            return View(sortedLeaveRequests);
        }

        public IActionResult AskLeave()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeaveRequest(IFormCollection model)
        {
            if (ModelState.IsValid)
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


                // ************* Parse the enum value from the Enum's string version *************
                LeaveType leaveTypeEnum = (LeaveType)Enum.Parse(typeof(LeaveType), Convert.ToString(model["LeaveType"]));

                var leaveRequest = new LeaveRequest
                {
                    EmployeeId = employeeId,
                    StartDate = Convert.ToDateTime(model["StartDate"]),
                    EndDate = Convert.ToDateTime(model["EndDate"]),
                    LeaveType = leaveTypeEnum,
                    Reason = Convert.ToString(model["Reason"]),
                    Status = LeaveStatus.Pending,
                    RequestDate = DateTime.UtcNow
                };

                _context.LeaveRequests.Add(leaveRequest);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        //[HttpGet]
        public async Task<IActionResult> MyAttendance()
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
                return NotFound();
            }
            // Map Employee to EmployeeDto
            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Email = employee.Email,
                Position = employee.Position,
                PresentDaysCount = employee.PresentDaysCount,
                AbscentDaysCount = CalculateAbsentDays(employee),
                AttendancePercentage = CalculateAttendancePercentage(employee), // Implement this method
                TotalWorkingHours = employee.TotalWorkingHours,
                Status = employee.Status
            };
            return View(employeeDto);
        }
        // Helper method to calculate absent days
        private int CalculateAbsentDays(Employee employee)
        {
            // Assuming total working days are fixed or calculated based on joining date
            int totalWorkingDays = (DateTime.Today - employee.JoiningDate).Days;
            return totalWorkingDays - employee.PresentDaysCount;
        }

        // Helper method to calculate attendance percentage
        private double CalculateAttendancePercentage(Employee employee)
        {
            int totalWorkingDays = (DateTime.Today - employee.JoiningDate).Days;
            if (totalWorkingDays == 0)
            {
                return 0; // Avoid division by zero
            }
            return (employee.PresentDaysCount / (double)totalWorkingDays) * 100;
        }

        public async Task<IActionResult> ClockIn()
        {
            // Get user ID and convert to integer
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Login");
            }
            if (!int.TryParse(userId, out int employeeId))
            {
                return BadRequest("Invalid user ID.");
            }

            // Increment user's present days in attendace
            var employee = await _context.Employees
               .FirstOrDefaultAsync(e => e.Id == employeeId);
            employee.PresentDaysCount++;


            // Save the clock in time log on database
            var timeLog = new TimeLog
            {
                EmployeeId = employeeId,
                ClockIn = DateTime.UtcNow,
                ClockOut = null,
                WorkingHoursPerDay = null
            };

            _context.TimeLogs.Add(timeLog);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ClockOut()
        {
            // Get user ID and convert to integer
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Login");
            }
            if (!int.TryParse(userId, out int employeeId))
            {
                return BadRequest("Invalid user ID.");
            }

            var today = DateTime.UtcNow.Date;

            // Find the most recent TimeLog entry for the current employee and today's date
            var timeLog = await _context.TimeLogs
                .Where(t => t.EmployeeId == employeeId &&
                             t.ClockIn.Date == today &&
                             t.ClockOut == null)                // Ensure ClockOut is null
                .OrderByDescending(t => t.ClockIn)
                .FirstOrDefaultAsync();

            if (timeLog == null)
            {
                return NotFound("No ClockIn record found for today.");
            }

            timeLog.ClockOut = DateTime.UtcNow;

            timeLog.WorkingHoursPerDay = timeLog.ClockOut - timeLog.ClockIn;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> SetStatus(string status)
        {
        
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Login");
            }

            // Convert the user ID to integer
            if (!int.TryParse(userId, out int employeeId))
            {
                return BadRequest("Invalid user ID."); 
            }

            // Fetch the employee from the database
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
                _ => employee.Status // Default to current status if invalid
            };

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
