using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.DTOs;

namespace InventoryManagement.Services.Contacts;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task AddAsync(ProductDTO productDto);
    Task UpdateAsync(int id,ProductDTO productDto);
    Task DeleteAsync(int id);
}
