using InventoryManagement.DataAccess.Contacts;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.DataAccess.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly ApplicationDbContext _context;

    public PurchaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Purchase>> GetAllAsync()
    {
        return await _context.Purchases.OrderByDescending(p => p.Id).ToListAsync();
    }

    public async Task<Purchase> GetByIdAsync(int id)
    {
        return await _context.Purchases.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Purchase product)
    {
        await _context.Purchases.AddAsync(product);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Purchase purchase)
    {
        Purchase pr = await _context.Purchases.Where(p => p.Id == id).FirstOrDefaultAsync();

        pr.PurchaseProduct = purchase.PurchaseProduct;
        pr.PurchaseQuantity = purchase.PurchaseQuantity;
        pr.PurchaseDate = purchase.PurchaseDate;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var purchase = await _context.Purchases.FindAsync(id);

        if (purchase != null)
        {
            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
        }
    }
}
