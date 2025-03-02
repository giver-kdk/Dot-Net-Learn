using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Insfrastructure.Data
{
    // ************* Here, <Employee, Role, int> specifies identity user and role *************
    public class ApplicationDbContext : IdentityDbContext<Employee, Role, int>
    {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<TimeLog> TimeLogs { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Configure relationships and constraints
        //builder.Entity<Employee>()
        //    .HasMany(e => e.TimeLogs)
        //    .HasOne(t => t.Employee)
        //    .HasForeignKey(t => t.EmployeeId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.Entity<Employee>()
        //    .HasMany(e => e.LeaveRequests)
        //    .WithOne(l => l.Employee)
        //    .HasForeignKey(l => l.EmployeeId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
}
