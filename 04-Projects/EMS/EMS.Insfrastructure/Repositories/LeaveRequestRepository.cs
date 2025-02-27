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
    internal class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        Task<LeaveRequest> CreateRequest(LeaveRequest request){

        }
        Task<LeaveRequest> UpdateStatus(int requestId, LeaveStatus status, string rejectionReason = null){

        }
        Task<IEnumerable<LeaveRequest>> GetEmployeeRequests(int employeeId){

        }
        Task<IEnumerable<LeaveRequest>> GetPendingRequests(){

        }
    }
}
