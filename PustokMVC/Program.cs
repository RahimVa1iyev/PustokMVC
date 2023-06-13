using Microsoft.EntityFrameworkCore;
using PustokMVC.DAL;
using PustokMVC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PustokDbContext>(opt =>
    opt.UseSqlServer("Server=localhost\\MSSQLSERVER02;Database=PustokDB;Trusted_Connection=True;"));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<LayoutService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
