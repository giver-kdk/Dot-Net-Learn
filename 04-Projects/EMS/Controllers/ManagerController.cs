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

        public async Task<IActionResult> ReviewLeave(string filter = "Pending")
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

            Console.WriteLine($"Employee ID: {employeeId}");

            // Fetch leave requests with filtering
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



        public async Task<IActionResult> AcceptLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = await _context.LeaveRequests
                .FirstOrDefaultAsync(lr => lr.Id == leaveRequestId);

            if (leaveRequest == null)
            {
                return NotFound("Leave request not found.");
            }

            // Update the status to Approved
            leaveRequest.Status = LeaveStatus.Approved;
            leaveRequest.RejectionReason = null; // Clear rejection reason if any

            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<IActionResult> RejectLeaveRequest(int leaveRequestId, string rejectionReason)
        {
            var leaveRequest = await _context.LeaveRequests
                .FirstOrDefaultAsync(lr => lr.Id == leaveRequestId);

            if (leaveRequest == null)
            {
                return NotFound("Leave request not found.");
            }

            // Update the status to Rejected and set the rejection reason
            leaveRequest.Status = LeaveStatus.Rejected;
            leaveRequest.RejectionReason = rejectionReason;

            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<IActionResult> EmployeeAttendance(string filter = "Present", string search = null)
        {
            var employees = await _context.Employees
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Position = e.Position,
                    PresentDaysCount = e.PresentDaysCount,
                    AbscentDaysCount = CalculateAbsentDays(e),
                    AttendancePercentage = CalculateAttendancePercentage(e),
                    TotalWorkingHours = e.TotalWorkingHours,
                    Status = e.Status
                })
                .ToListAsync();

            // Fetch all TimeLogs for today's date
            var today = DateTime.UtcNow.Date;
            var timeLogs = await _context.TimeLogs
                .Where(t => t.ClockIn.Date == today)
                .ToListAsync();

            Console.WriteLine($"Total TimeLogs Found: {timeLogs.Count}");

            // Apply search filter
            if (!string.IsNullOrEmpty(search))
            {
                employees = employees
                    .Where(e => e.FullName.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Apply attendance filter
            if (!string.IsNullOrEmpty(filter))
            {
                switch (filter)
                {
                    case "Present":
                        var presentEmployeeIds = timeLogs.Select(t => t.EmployeeId).Distinct();
                        employees = employees
                            .Where(e => presentEmployeeIds.Contains(e.Id))
                            .ToList();
                        break;
                    case "Absent":
                        // Filter employees who do not have a TimeLog entry for today
                        var absentEmployeeIds = timeLogs.Select(t => t.EmployeeId).Distinct();
                        employees = employees
                            .Where(e => !absentEmployeeIds.Contains(e.Id))
                            .ToList();
                        break;
                }
            }

            // Sort by attendance percentage (highest to lowest)
            employees = employees
                .OrderByDescending(e => e.AttendancePercentage)
                .ToList();

            ViewBag.Filter = filter;
            ViewBag.Search = search;


            Console.WriteLine($"Total Employees Found: {employees.Count}");

            return View(employees);
        }

        private static int CalculateAbsentDays(Employee employee)
        {
            var totalWorkingDays = (DateTime.UtcNow - employee.JoiningDate).Days;
            return totalWorkingDays - employee.PresentDaysCount;
        }

        private static double CalculateAttendancePercentage(Employee employee)
        {
            var totalWorkingDays = (DateTime.UtcNow - employee.JoiningDate).Days;
            if (totalWorkingDays == 0)
            {
                return 0;                               // Avoid division by zero
            }
            return (employee.PresentDaysCount / (double)totalWorkingDays) * 100;
        }


        public IActionResult EmployeeTimelogs(int id)
        {
            Console.WriteLine($"Employee ID: {id}");
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            var timeLogs = _context.TimeLogs
                .Where(t => t.EmployeeId == id)
                .OrderByDescending(t => t.ClockIn) // Order by latest clock-in
                .ToList();

            ViewBag.Employee = employee; // Pass the employee object to the view

            return View(timeLogs);
        }

        // ************* Manager Employees *************

        // GET: Employees
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        [HttpGet]
        [Route("[controller]/id")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Create
        [HttpPost]
        // Remove 'ValidateAntiForgeryToken' for allowing DELETE requests from client
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Position,Salary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                // *** 'OK' status with new data will signal Data Grid UI to update ***     
                return Ok(employee);
            }
            return BadRequest();
        }


        // POST: Employees/Edit/5
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Position,Salary")] Employee employee)
        {
            Console.WriteLine("*** Edit Action ***");

            if (id != employee.Id)
            {
                Console.WriteLine("*** Not Found - No Edit ***");
                return NotFound();
            }

            try
            {
                // Check existing Employee from the database
                var existingEmployee = await _context.Employees.FindAsync(id);
                if (existingEmployee != null)
                {
                    // By default, we have to modify all properties to update Employee.
                    // Non-modified properties are set to null. So, Update only modified properties one by one
                    if (employee.Name != null)
                    {
                        existingEmployee.Name = employee.Name;
                    }
                    if (employee.Position != null)
                    {
                        existingEmployee.Position = employee.Position;
                    }
                    if (employee.Salary != 0)
                    {
                        existingEmployee.Salary = employee.Salary;
                    }
                    // Mark the entity as modified and save changes
                    _context.Entry(existingEmployee).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
                // *** 'OK' status with new data will signal Data Grid UI to update ***     
                return Ok(existingEmployee);
            }
            catch (DbUpdateConcurrencyException)
            {
                Console.WriteLine("*** Edit Error ***");

                if (!EmployeeExists(employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: Employees/Delete/5
        [HttpDelete]
        // Remove 'ValidateAntiForgeryToken' for allowing DELETE requests from client
        //[ValidateAntiForgeryToken]                            
        public async Task<IActionResult> Delete(int? id)
        {
            Console.WriteLine("*** Delete Method ***");

            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            // *** 'OK' status will signal Data Grid UI to update ***     
            return Ok();
        }
    }
}
