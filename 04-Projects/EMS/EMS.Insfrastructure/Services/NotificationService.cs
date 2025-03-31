using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Insfrastructure.Data;
using EMS.Insfrastructure.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace EMS.Insfrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<LeaveNotificationHub> _hubContext;

        public NotificationService(ApplicationDbContext context, IHubContext<LeaveNotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task NotifyEmployeeAsync(string employeeId, string message)
        {
            // Save the notification to the database
            var notification = new Notification
            {
                EmployeeId = employeeId,
                Message = message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Send real-time notification using SignalR
            await _hubContext.Clients.User(employeeId).SendAsync("ReceiveLeaveStatus", message);
            Console.WriteLine($"*** Notification Sent Successfully ********");
        }

    }
}
