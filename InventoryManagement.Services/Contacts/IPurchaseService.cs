using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.DTOs;

namespace InventoryManagement.Services.Contacts;

public interface IPurchaseService
{
    Task<IEnumerable<Purchase>> GetAllAsync();
    Task<Purchase> GetByIdAsync(int id);
    Task AddAsync(PurchaseDTO purchaseDto);
    Task UpdateAsync(int id, PurchaseDTO purchaseDto);
    Task DeleteAsync(int id);
}
