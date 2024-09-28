using InventoryManagement.DataAccess.Contacts;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;

namespace InventoryManagement.Services.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _repository;

    public PurchaseService(IPurchaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Purchase>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Purchase> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Purchase purchase)
    {
        await _repository.AddAsync(purchase);
    }

    public async Task UpdateAsync(int id, Purchase purchase)
    {
        await _repository.UpdateAsync(id, purchase);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
