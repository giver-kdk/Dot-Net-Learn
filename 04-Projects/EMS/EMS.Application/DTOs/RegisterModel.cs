using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.DTOs
{
    // ******** Abstracted data model for new user registration ********
    public class RegisterModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Position { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Manager" or "Employee"
        public double Salary { get; set; } = 0;
        public DateTime JoiningDate { get; set; }
    }
}
