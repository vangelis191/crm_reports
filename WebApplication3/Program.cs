using FastReport.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication3.Repository;
using WebApplication3.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
 FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));

var connectionString = builder.Configuration.GetConnectionString("SqlConnection");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Error: Connection string not found in appsettings.json.");
    return;
}

builder.Services.AddSingleton(connectionString); 
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Role";
       
    });

builder.Services.AddAuthorization(options =>
{
   
    options.AddPolicy("EmployeePolicy", policy =>
    {
        policy.RequireRole("EMPLOYEE");
    });

    options.AddPolicy("ManagerPolicy", policy =>
    {
        policy.RequireRole("MANAGER");
    });
});


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
   
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name: "manageCustomers",
    pattern: "/customers/{action=Index}/{id?}",
    defaults: new { controller = "ManageCustomers" }
    );

app.Run();