using Microsoft.EntityFrameworkCore;
using CRUDApp.Data.Repository.IRepository;
using CRUDApp.Data.Repository;
using Microsoft.AspNetCore.Identity;
using CRUDApp.Data;
using CRUDApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CRUDApp.Data")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddRazorPages();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();





app.MapRazorPages();
app.Run();

// OOPS, Linq, Life Cycle, Fizzbuzz, !DSA, CSS, MVC, RAzor pages, Differences between minimal api and web api, Asynchronous Programming,
// Entity Framework, EFCore, Difference berween .NET and .NET core, Swapping using 2 variables, ######{Basic Concepts), String, JIT -> , AOT, 