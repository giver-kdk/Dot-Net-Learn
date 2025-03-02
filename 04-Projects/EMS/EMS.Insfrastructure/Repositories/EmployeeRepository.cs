using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Insfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace EMS.Insfrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetById(int id)
        {
            return await _context.Employees
                //.Include(e => e.TimeLogs)
                //.Include(e => e.LeaveRequests)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        Task<IEnumerable<Employee>> IEmployeeRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<Employee> IEmployeeRepository.AddEmp(Employee employee)
        {
            throw new NotImplementedException();
        }

        Task IEmployeeRepository.UpdateEmp(Employee employee)
        {
            throw new NotImplementedException();
        }

        Task IEmployeeRepository.DeleteEmp(int id)
        {
            throw new NotImplementedException();
        }

    
    }
}
