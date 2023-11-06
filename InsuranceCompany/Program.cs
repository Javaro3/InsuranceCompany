using InsuranceCompany.Data;
using InsuranceCompany.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<InsuranceCompanyContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.UseDbInitializerMiddleware();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
