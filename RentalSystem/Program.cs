using Blazored.Toast;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Models;

var builder = WebApplication.CreateBuilder(args);

//DATABASE 
builder.Services.AddDbContext<RentalSystemDBConText>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddControllersWithViews();
// thư viện thông báo 
builder.Services.AddBlazoredToast();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Account/Login";
	options.ExpireTimeSpan = TimeSpan.FromHours(24);
    options.AccessDeniedPath = "/Home/Index";
});
builder.Services.AddHttpContextAccessor();   // cho phép lấy yêu cầu Http cụ thể là thông tin user 



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
app.UseAuthentication();
app.UseAuthorization();

//Thêm rounter cho admin
app.MapAreaControllerRoute(
  name: "Admin",
  areaName: "Admin",
  pattern: "Admin/{controller=AdminHome}/{action=Index}/{id?}"
);
//Thêm rounter cho user
app.MapAreaControllerRoute(
  name: "User",
  areaName: "User",
  pattern: "User/{controller=UserHome}/{action=Index}/{id?}"
);
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
