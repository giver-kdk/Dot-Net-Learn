using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Insfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Insfrastructure.Services
{
    public class RecurringJobService : IRecurringJobService
    {
        private readonly ApplicationDbContext _context;

        public RecurringJobService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AutoClockOut()
        {
            var today = DateTime.Now.Date;

            // Find all employees who haven't clocked out today
            var timeLogs = await _context.TimeLogs
            .Where(t => t.Log.Date == today.Date && t.LogType == "ClockIn") 
            .GroupBy(t => t.EmployeeId) 
            .Where(g => !g.Any(t => t.LogType == "ClockOut")) // Filter groups that don't have a ClockOut
            .SelectMany(g => g) // Flatten the groups back to individual records
            .ToListAsync();

            if (!timeLogs.Any())
            {
                Console.WriteLine("No Clock-In Today. Skipping auto-clock-out.");
                return; 
            }

            // Auto-clock-out for each employee
            foreach (var timeLog in timeLogs)
            {
                var clockOutLog = new TimeLog
                {
                    EmployeeId = timeLog.EmployeeId,
                    Log = timeLog.Log.Date.AddHours(19).AddMinutes(30), // 7:30 PM today
                    LogType = "ClockOut"
                };
                await _context.TimeLogs.AddAsync(clockOutLog);
            }

            await _context.SaveChangesAsync();
            Console.WriteLine("Auto-clock-out completed for all employees.");
        }
    }
}
