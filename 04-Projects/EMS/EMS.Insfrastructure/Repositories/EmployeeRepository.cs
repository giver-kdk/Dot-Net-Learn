using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Insfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Insfrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.TimeLogs)
                .Include(e => e.LeaveRequests)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        Task<IEnumerable<Employee>> GetAll(){
            return await _context.Employees.ToListAsync();
        }
        Task<Employee> AddEmp(Employee employee){

        }
        Task UpdateEmp(Employee employee){

        }
        Task DeleteEmp(int id){

        }
    }
}
