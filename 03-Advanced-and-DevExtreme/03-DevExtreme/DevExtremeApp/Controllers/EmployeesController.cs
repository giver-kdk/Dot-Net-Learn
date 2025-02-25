using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevExtremeApp.Models;

namespace DevExtremeApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DxEmployeeContext _context;

        public EmployeesController(DxEmployeeContext context)
        {
            _context = context;
        }

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

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
