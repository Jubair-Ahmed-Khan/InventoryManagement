using InventoryManagement.DataAccess.Contacts;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;

namespace InventoryManagement.Services.Services;

public class ProductService:IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Product product)
    {
        await _repository.AddAsync(product);
    }

    public async Task UpdateAsync(int id,Product product)
    {
       await _repository.UpdateAsync(id,product);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
