using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string EmployeeId { get; set; } 
        [Required]
        public string Message { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public bool IsRead { get; set; } = false; 
    }
}
