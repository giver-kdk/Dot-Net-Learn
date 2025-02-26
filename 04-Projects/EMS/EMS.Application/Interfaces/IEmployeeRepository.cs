using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain.Models;                

namespace EMS.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        // Employee CRUD Operation
        Task<Employee> GetById(int id);
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> AddEmp(Employee employee);
        Task UpdateEmp(Employee employee);
        Task DeleteEmp(int id);
    }
}
