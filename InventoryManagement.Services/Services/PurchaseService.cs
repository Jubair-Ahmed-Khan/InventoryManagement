using InventoryManagement.DataAccess.Contacts;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;
using InventoryManagement.Services.Mappers;
using InventoryManagement.Services.DTOs;

namespace InventoryManagement.Services.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _repository;
    private readonly IPurchaseMapper _mapper;

    public PurchaseService(IPurchaseRepository repository,IPurchaseMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Purchase>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Purchase> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(PurchaseDTO purchaseDto)
    {
        var newProduct = _mapper.MapToPurchase(purchaseDto);
        await _repository.AddAsync(newProduct);
    }

    public async Task UpdateAsync(int id, PurchaseDTO purchaseDto)
    {
        var newProduct = _mapper.MapToPurchase(purchaseDto);
        await _repository.UpdateAsync(id, newProduct);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
