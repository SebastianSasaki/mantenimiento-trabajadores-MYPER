using MantenimientoTrabajadores.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1) Add services BEFORE build
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<TrabajadorRepository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 2) Configure middleware AFTER build
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default",
    pattern: "{controller=Trabajadores}/{action=Index}/{id?}");
app.Run();