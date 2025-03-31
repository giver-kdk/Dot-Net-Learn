using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class TimeLog
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("EmployeeId")]
        public int EmployeeId { get; set; }
        [JsonPropertyName("Log")]
        public DateTime Log { get; set; }
        [JsonPropertyName("LogType")]
        public String LogType { get; set; }
        [JsonPropertyName("WorkingHoursPerDay")]
        public TimeSpan? WorkingHoursPerDay { get; set; }
        [JsonPropertyName("Employee")]
        public Employee Employee { get; set; }
    }
}
