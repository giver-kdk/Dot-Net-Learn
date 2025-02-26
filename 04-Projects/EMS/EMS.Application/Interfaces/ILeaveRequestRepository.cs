using EMS.Domain.Enums;
using EMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Interfaces
{
    public interface ILeaveRequestRepository
    {
        Task<LeaveRequest> CreateRequest(LeaveRequest request);
        Task<LeaveRequest> UpdateStatus(int requestId, LeaveStatus status, string rejectionReason = null);
        Task<IEnumerable<LeaveRequest>> GetEmployeeRequests(int employeeId);
        Task<IEnumerable<LeaveRequest>> GetPendingRequests();
    }
}
