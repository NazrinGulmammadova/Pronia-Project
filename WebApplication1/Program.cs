using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration["ConnectionStrings:Default"];
builder.Services.AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString);
    }
);
builder.Services.AddIdentity<UserSide, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireDigit = true;
    opt.Password.RequireNonAlphanumeric = true;

    opt.User.RequireUniqueEmail = true;

    opt.Lockout.MaxFailedAccessAttempts = 3;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(15);
    opt.Lockout.AllowedForNewUsers = false;
}).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout=TimeSpan.FromSeconds(10);
});
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Authentication/Login";
});

builder.Services.AddScoped<IShippingItemRepository,ShippingItemRepository>();

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{Id?}"
    );
app.Run();
