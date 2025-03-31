using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Interfaces
{
    public interface INotificationService
    {
        Task NotifyEmployeeAsync(string employeeId, string message);
    }
}
