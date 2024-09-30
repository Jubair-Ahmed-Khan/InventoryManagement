using InventoryManagement.DataAccess.Contacts;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;
using InventoryManagement.Services.Mappers;
using InventoryManagement.Services.DTOs;

namespace InventoryManagement.Services.Services;

public class ProductService:IProductService
{
    private readonly IProductRepository _repository;
    private readonly IProductMapper _mapper;

    public ProductService(IProductRepository repository,IProductMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(ProductDTO productDto)
    {
        var newProduct = _mapper.MapToProduct(productDto);
        await _repository.AddAsync(newProduct);
    }

    public async Task UpdateAsync(int id,ProductDTO productDto)
    {
        var newProduct = _mapper.MapToProduct(productDto);
        await _repository.UpdateAsync(id, newProduct);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
