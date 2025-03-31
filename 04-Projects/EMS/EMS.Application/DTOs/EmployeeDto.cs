using EMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int PresentDaysCount { get; set; }
        public int AbscentDaysCount { get; set; }
        public double AttendancePercentage { get; set; }
        public TimeSpan TotalWorkingHours { get; set; } = TimeSpan.Zero;
        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
        public string? ProfilePicture { get; set; } = null;

    }
}
