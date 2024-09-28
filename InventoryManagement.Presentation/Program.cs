using InventoryManagement.DataAccess.Repositories;
using InventoryManagement.DataAccess.Contacts;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;
using InventoryManagement.Services.Services;
using InventoryManagement.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>
        (
            options => {
                var connectionString = builder.Configuration.GetConnectionString("default");
                options.UseSqlServer(connectionString);
            }
        );

        builder.Services.AddIdentity<User, IdentityRole>
        (
            options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireLowercase = false;
           }
        )
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
		builder.Services.AddScoped<ISalesRepository, SalesRepository>();


		builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IPurchaseService, PurchaseService>();
		builder.Services.AddScoped<ISalesService, SalesService>();


		builder.Services.AddControllersWithViews();

        builder.Services.AddResponseCaching();

        
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseResponseCaching();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute
        (
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        app.Run();
    }
}
