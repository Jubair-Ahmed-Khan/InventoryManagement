using InventoryManagement.DataAccess.Models;

namespace InventoryManagement.Services.Contacts;

public interface IPurchaseService
{
    Task<IEnumerable<Purchase>> GetAllAsync();
    Task<Purchase> GetByIdAsync(int id);
    Task AddAsync(Purchase purchase);
    Task UpdateAsync(int id, Purchase purchase);
    Task DeleteAsync(int id);
}
