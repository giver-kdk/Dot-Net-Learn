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
    public class TimeLogRepository : ITimeLogRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<TimeLog> ClockIn(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<TimeLog> ClockOut(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TimeLog>> GetEmployeeTimeLogs(int employeeId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<TimeSpan> GetTotalWorkingHours(int employeeId, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
