using BVTamDuc.Data;
using BVTamDuc.Pages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

using System;
using System.Web;
using Microsoft.EntityFrameworkCore.SqlServer;


var builder = WebApplication.CreateBuilder(args);

// Lấy chuỗi kết nối từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Đăng ký dịch vụ
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "XSRF-TOKEN";
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/DangNhap";
        options.LogoutPath = "/DangXuat";
    });

var app = builder.Build();

// Khởi tạo và kiểm tra kết nối CSDL
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    try
    {
        dbContext.Database.CanConnect();
        Console.WriteLine("Kết nối cơ sở dữ liệu thành công.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
    }
}

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/TrangChu");
    return Task.CompletedTask;
});
app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();
