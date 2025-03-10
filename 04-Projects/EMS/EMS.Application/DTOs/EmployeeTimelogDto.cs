using EMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.DTOs
{
    public class EmployeeTimelogDto
    {
        public EmployeeDto Employee { get; set; }
        public List<TimeLog> TimeLogs { get; set; }
    }
}
