using EMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;


namespace EMS.Domain.Models
{
    public class Employee : IdentityUser<int>
    {
        //public int Id { get; set; }
        [JsonPropertyName("FullName")]
        public string FullName { get; set; } = "";
        [JsonPropertyName("PhoneNumber")]
        //public string Email { get; set; } = "";           // Already provided by Identity
        public string PhoneNumber { get; set; }
        [JsonPropertyName("Position")]
        public string Position { get; set; } = "";
        [JsonPropertyName("Salary")]
        public double Salary { get; set; } = 0;
        [JsonPropertyName("JoiningDate")]
        
        public DateTime JoiningDate { get; set; }           // Set when registered
        [JsonPropertyName("PresentDaysCount")]
        public int PresentDaysCount { get; set; } = 0;           // Increment when 'Clock In'
        [JsonPropertyName("Status")]
        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
        [JsonPropertyName("TotalWorkingHours")]
        public TimeSpan TotalWorkingHours { get; set; } = TimeSpan.Zero;

        [JsonPropertyName("ProfilePicture")]
        public string? ProfilePicture { get; set; } = null;

    }

}
