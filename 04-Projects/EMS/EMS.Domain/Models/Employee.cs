using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public int PhoneNumber { get; set; } = 0;
        public string Department { get; set; } = "";
        public string Position { get; set; } = "";
        public decimal Salary { get; set; } = 0;
        // 'virtual' keyword allows EF Core to lazy load (i.e; load only when accessed)
        public virtual ICollection<TimeLog>? TimeLogs { get; set; }
        public virtual ICollection<LeaveRequest>? LeaveRequests { get; set; }
    }

}
