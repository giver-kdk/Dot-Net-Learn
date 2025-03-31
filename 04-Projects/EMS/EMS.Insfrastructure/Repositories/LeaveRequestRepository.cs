using EMS.Application.Interfaces;
using EMS.Domain.Enums;
using EMS.Domain.Models;
using EMS.Insfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Insfrastructure.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<LeaveRequest> CreateRequest(LeaveRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LeaveRequest>> GetEmployeeRequests(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LeaveRequest>> GetPendingRequests()
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequest> UpdateStatus(int requestId, LeaveStatus status, string rejectionReason = null)
        {
            throw new NotImplementedException();
        }
    }
}
