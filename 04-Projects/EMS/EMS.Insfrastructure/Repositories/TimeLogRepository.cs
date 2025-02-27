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

        Task<TimeLog> ClockIn(int employeeId){

        }
        Task<TimeLog> ClockOut(int employeeId){

        }
        Task<IEnumerable<TimeLog>> GetEmployeeTimeLogs(int employeeId, DateTime startDate, DateTime endDate){

        }
        Task<TimeSpan> GetTotalWorkingHours(int employeeId, DateTime date){

        }
    }
}
