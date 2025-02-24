using System;
using System.Collections.Generic;

namespace EntityFramework_App.Models;

public partial class Employee
{
    public decimal EmpId { get; set; }

    public string? EmpName { get; set; }

    public decimal? Salary { get; set; }

    public decimal? DeptId { get; set; }
}
