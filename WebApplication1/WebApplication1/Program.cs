using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("NLU");

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add database connection
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString)));

// Add authentication cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = "/User/Login"; // chuyển hướng khi chưa đăng nhập
    options.AccessDeniedPath = "/AccessDenied"; // khi bị chặn
    /*
     * Framework tự động: /User/Login?ReturnUrl=/Home/Index
     */
    options.ReturnUrlParameter = "ReturnUrl"; // chuyển hướng khi đã đăng nhập
});

// Thêm session (tạm thời để test)
builder.Services.AddDistributedMemoryCache(); // lưu session trong RAM
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // thời gian sống của session
    options.Cookie.HttpOnly = true; // chỉ có HTTP truy cập
    options.Cookie.IsEssential = true; // cookie cần thiết
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

/* Tạm thời để test */
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");






app.Run();
