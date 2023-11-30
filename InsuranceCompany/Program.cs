using InsuranceCompany.Middleware;
using InsuranceCompany.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository;
using Service;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<InsuranceCompanyContext>(option => option.UseSqlServer(connectionString));
builder.Services.AddDbContext<InsuranceCompanyIdentityContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+=\\/?! àáâãäå¸æçèéêëìíîïğñòóôõö÷øùúûüışÿÀÁÂÃÄÅ¨ÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÚÛÜİŞß"
    )
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<InsuranceCompanyIdentityContext>();

builder.Services.AddTransient<InsuranceCompanyCookieManager>();
builder.Services.AddTransient<InsuranceCompanyCache>();
builder.Services.AddTransient<InsuranceCompanyFilter>();
builder.Services.AddMemoryCache();

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
