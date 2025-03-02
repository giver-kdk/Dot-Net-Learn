using EMS.Insfrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EMS.Application.Interfaces;
using EMS.Insfrastructure.Repositories;
using EMS.Domain.Models;
using EMS.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ************* Add DB Context and Identity *************
String connectionString = builder.Configuration.GetConnectionString("myConStr");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<Employee, Role>(options =>
{
    //options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>();

// Register repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ITimeLogRepository, TimeLogRepository>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// ******** Enable authentication + authorization and also enables 'Identity Area' ********
app.UseAuthentication();                
app.UseAuthorization(); 

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


// ************ Initialize the Role from EMS.Infrastructure.Data.RoleSeedData ************ 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleSeedData.Initialize(services);
}


app.Run();
