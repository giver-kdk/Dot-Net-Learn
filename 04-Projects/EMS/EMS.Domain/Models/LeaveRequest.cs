using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EMS.Domain.Enums;

namespace EMS.Domain.Models
{
    public class LeaveRequest
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("EmployeeId")]
        public int EmployeeId { get; set; }
        [JsonPropertyName("StartDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("LeaveType")]
        public LeaveType LeaveType { get; set; }
        [JsonPropertyName("Reason")]
        public string Reason { get; set; }
        [JsonPropertyName("Status")]
        public LeaveStatus Status { get; set; }
        [JsonPropertyName("RejectionReason")]
        public string? RejectionReason { get; set; }
        [JsonPropertyName("RequestDate")]
        public DateTime RequestDate { get; set; }
        [JsonPropertyName("Employee")]
        public Employee Employee { get; set; }

    }
}
