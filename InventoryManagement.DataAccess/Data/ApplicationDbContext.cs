using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using InventoryManagement.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.DataAccess.Data;

public class ApplicationDbContext:IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Sale> Sales { get; set; }
}
