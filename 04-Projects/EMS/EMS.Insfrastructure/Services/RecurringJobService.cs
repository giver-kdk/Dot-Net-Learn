using EMS.Application.Interfaces;
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
            var timeLogs= await _context.TimeLogs
                .Where(t => t.ClockIn.Date == today && t.ClockOut == null)
                .ToListAsync();

            if (!timeLogs.Any())
            {
                Console.WriteLine("No Clock-In Today. Skipping auto-clock-out.");
                return; 
            }

            // Auto-clock-out for each employee
            foreach (var timeLog in timeLogs)
            {
                timeLog.ClockOut = today.Date.AddHours(19).AddMinutes(30); // 7:30 PM today
                timeLog.WorkingHoursPerDay = timeLog.ClockOut - timeLog.ClockIn;
            }

            await _context.SaveChangesAsync();
            Console.WriteLine("Auto-clock-out completed for all employees.");
        }
    }
}
