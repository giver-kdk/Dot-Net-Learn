using EMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace EMS.Domain.Models
{
    public class Employee : IdentityUser<int>
    {
        //public int Id { get; set; }
        public string FullName { get; set; } = "";
        //public string Email { get; set; } = "";           // Already provided by Identity
        public string PhoneNumber { get; set; }
        public string Position { get; set; } = "";
        public double Salary { get; set; } = 0;
        
        public DateTime JoiningDate { get; set; }           // Set when registered
        public int PresentDaysCount { get; set; } = 0;           // Increment when 'Clock In'
        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
        public TimeSpan TotalWorkingHours { get; set; } = TimeSpan.Zero;

    }

}
