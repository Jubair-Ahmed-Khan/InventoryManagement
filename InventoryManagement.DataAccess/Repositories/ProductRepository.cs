using Microsoft.Extensions.DependencyInjection;
using InventoryManagement.DataAccess.Contacts;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context, IServiceScopeFactory serviceScopeFactory)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.OrderByDescending(p=>p.Id).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Product product)
    {
        Product pr = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();

        pr.ProductName = product.ProductName;
        pr.ProductQuantity = product.ProductQuantity;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}

