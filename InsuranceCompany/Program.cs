using InsuranceCompany.Data;
using InsuranceCompany.Middleware;
using InsuranceCompany.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<InsuranceCompanyContext>(option => option.UseSqlServer(connectionString));
builder.Services.AddDbContext<InsuranceCompanyIdentityContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services
    .AddDefaultIdentity<ApplicationUser>()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<InsuranceCompanyIdentityContext>();


var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseSession();
app.UseDbInitializerMiddleware();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();