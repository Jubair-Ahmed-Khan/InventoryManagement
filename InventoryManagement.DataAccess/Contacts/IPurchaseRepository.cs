using InventoryManagement.DataAccess.Models;

namespace InventoryManagement.DataAccess.Contacts;

public interface IPurchaseRepository
{
    Task<IEnumerable<Purchase>> GetAllAsync();
    Task<Purchase> GetByIdAsync(int id);
    Task AddAsync(Purchase product);
    Task UpdateAsync(int id, Purchase purchase);
    Task DeleteAsync(int id);
}
