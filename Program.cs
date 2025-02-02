using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SelfServiceStudentSystem.Data;
using System.IO;
using System;

var builder = WebApplication.CreateBuilder(args);

// Konfigurimi i sesionit për menaxhimin e sesioneve
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Konfigurimi i lidhjes me SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konfigurimi për Controller dhe View
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Përdorimi i pipeline për zhvillim dhe prodhim
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Aktivizimi i skedarëve statikë
app.UseStaticFiles();

var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "Properties", "wwwroot");

if (Directory.Exists(wwwrootPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(wwwrootPath),
        RequestPath = ""
    });
}
else
{
    Console.WriteLine("Dosja 'wwwroot' nuk u gjet: " + wwwrootPath);
}

app.UseRouting();

app.UseSession();
app.UseAuthorization();

// Konfigurimi i rrugës për controller dhe veprim
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
