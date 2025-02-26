using EMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Interfaces
{
    public interface ITimeLogRepository
    {
        Task<TimeLog> ClockIn(int employeeId);
        Task<TimeLog> ClockOut(int employeeId);
        Task<IEnumerable<TimeLog>> GetEmployeeTimeLogs(int employeeId, DateTime startDate, DateTime endDate);
        Task<TimeSpan> GetTotalWorkingHours(int employeeId, DateTime date);
    }
}
